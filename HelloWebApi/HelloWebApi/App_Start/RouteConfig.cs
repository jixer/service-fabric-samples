using System.Web.Http;

namespace HelloWebApi.App_Start
{
    public static class RouteConfig
    {
        /// <summary>
        /// Routing registration.
        /// </summary>
        /// <param name="routes"></param>
        public static void RegisterRoutes(HttpRouteCollection routes)
        {
            routes.MapHttpRoute(
                name: "Default",
                routeTemplate: "{action}",
                defaults: new { controller = "Default", action = "GetHello" },
                constraints: new { }
                );
        }
    }
}
