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

            config.Routes.MapHttpRoute(
            name: "DefaultApi",
            routeTemplate: "api/Values/{id}",
            defaults: new { controller = "Values", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "FormFields",
                routeTemplate: "api/Form/{action}",
                defaults: new { controller = "Form"}
            );
        }
    }
}
