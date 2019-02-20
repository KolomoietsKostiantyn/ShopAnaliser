using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ShopParser
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Конфигурация и службы веб-API

            // Маршруты веб-API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
               name: "DefaultSort",
               routeTemplate: "api/{controller}/{action}/{shop}/{category}/{start}/{end}" // string shop, string category, int start, int end
           );



            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
