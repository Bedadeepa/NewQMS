using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace QMS
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
           
            routes.MapRoute(
           name: "Login",
           url: "Login",
           defaults: new { controller = "Registration", action = "Login", id = UrlParameter.Optional }
           );
            routes.MapRoute(
           name: "CounterReport",
           url: "CounterReport",
           defaults: new { controller = "Admin", action = "CounterReport", id = UrlParameter.Optional }
           );
            routes.MapRoute(
           name: "Counter2Report",
           url: "Counter2Report",
           defaults: new { controller = "Admin", action = "Counter2Report", id = UrlParameter.Optional }
           );
            routes.MapRoute(
           name: "",
           url: "Logout",
           defaults: new { controller = "Registration", action = "Logout", id = UrlParameter.Optional }
           );

            routes.MapRoute(
          name: "Dashboard",
          url: "Dashboard",
          defaults: new { controller = "Admin", action = "UserPanel", id = UrlParameter.Optional }
          );
            routes.MapRoute(
         name: "CreateUser",
         url: "CreateUser",
         defaults: new { controller = "Registration", action = "CreateUser", id = UrlParameter.Optional }
         );
            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Registration", action = "Login", id = UrlParameter.Optional }
           );
        }
    }
}
