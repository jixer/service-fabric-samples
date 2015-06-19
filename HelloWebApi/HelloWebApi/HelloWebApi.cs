using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric;
using Microsoft.ServiceFabric.Services;
using System.Fabric.Description;
using Microsoft.Owin.Hosting;
using Owin;
using System.Web.Http;
using HelloWebApi.App_Start;
using System.Diagnostics;

namespace HelloWebApi
{
    public class HelloWebApi : StatelessService
    {
        protected override ICommunicationListener CreateCommunicationListener()
        {
            return new OwinCommunicationListener();
        }
    }

    public class OwinCommunicationListener : ICommunicationListener
    {
        private string listeningAddress;
        private string publishAddress;
        private IDisposable serverHandle;

        public void Abort()
        {
            ServiceEventSource.Current.Message("Abort");
            StopWebServer();
        }

        public Task CloseAsync(CancellationToken cancellationToken)
        {
            ServiceEventSource.Current.Message("Close");
            StopWebServer();
            return Task.FromResult(true);
        }

        public void Initialize(ServiceInitializationParameters serviceInitializationParameters)
        {
            ServiceEventSource.Current.Message("Initialize");

            EndpointResourceDescription serviceEndpoint =
                serviceInitializationParameters.CodePackageActivationContext.GetEndpoint("ServiceEndpoint");
            int port = serviceEndpoint.Port;

            listeningAddress = string.Format("http://+:{0}/", port);
            publishAddress = listeningAddress.Replace("+", FabricRuntime.GetNodeContext().IPAddressOrFQDN);
        }

        public Task<string> OpenAsync(CancellationToken cancellationToken)
        {
            ServiceEventSource.Current.Message("Starting web server on {0}", listeningAddress);

            serverHandle = WebApp.Start(listeningAddress, StartupConfiguration);

            return Task.FromResult(publishAddress);         
        }

        private void StartupConfiguration(IAppBuilder appBuilder)
        {
            System.Net.ServicePointManager.DefaultConnectionLimit = 256;

            HttpConfiguration config = new HttpConfiguration();

            FormatterConfig.ConfigureFormatters(config.Formatters);
            RouteConfig.RegisterRoutes(config.Routes);

            appBuilder.UseWebApi(config);
        }

        private void StopWebServer()
        {
            if (serverHandle != null)
            {
                serverHandle.Dispose();
            }
        }
    }
}
