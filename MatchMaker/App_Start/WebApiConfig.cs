using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MatchMaker
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();
            // Routes for People API
            config.Routes.MapHttpRoute(
                name: "Create",
                routeTemplate: "api/People/{action}",
                defaults: new { controller = "People" }
            );
            config.Routes.MapHttpRoute(
                name: "Update",
                routeTemplate: "api/People/{action}/{id}",
                defaults: new { controller = "People" }
            ); 
        }
    }
}
