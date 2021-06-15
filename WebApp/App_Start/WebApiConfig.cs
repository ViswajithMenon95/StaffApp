using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

			var cors = new EnableCorsAttribute("*", "*", "*");
			config.EnableCors(cors);
		}
    }
}
