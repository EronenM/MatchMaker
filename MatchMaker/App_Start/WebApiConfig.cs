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

            config.Routes.MapHttpRoute(
            name: "DeleteApi",
            routeTemplate: "api/Form/DeleteFull/{id}",
            defaults: new { controller = "Form", action = "DeleteFull", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
            name: "DeleteOnlyPreferencesApi",
            routeTemplate: "api/Form/DeletePreferences/{id}",
            defaults: new { controller = "Form", action = "DeletePreferences", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
            name: "FormFields",
            routeTemplate: "api/Form/{action}",
            defaults: new { controller = "Form" }
            );
        }
    }
}
