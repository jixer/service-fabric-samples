using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace HelloWebApi.Controllers
{
    public class DefaultController : ApiController
    {
        public string GetHello()
        {
            return string.Format("Hello, Your GUID is '{0}'", Guid.NewGuid());
        }
    }
}
