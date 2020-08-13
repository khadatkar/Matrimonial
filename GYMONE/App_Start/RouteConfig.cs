using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GYMONE
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("RegisterMember",
                "RegisterMember/{action}/{name}",
                new { controller = "RegisterMember", action = "Edit", name = UrlParameter.Optional },
                new[] { "GYMONE.Controllers" });

            routes.MapRoute("CMSDemo",
                "CMSDemo/{action}/{id}",
                new { controller = "CMSDemo", action = "Index", id = UrlParameter.Optional },
                new[] { "GYMONE.Controllers" });



            //routes.MapRoute("CMSDemo1",
            //    "CMSDemo/{action}/{name}",
            //    new { controller = "CMSDemo", action = "MemberDetails", name = UrlParameter.Optional },
            //    new[] { "GYMONE.Controllers" });



            routes.MapRoute(
                 "Default",
                "{controller}/{action}/{id}",
                new { controller = "Demo", action = "Index", id = UrlParameter.Optional },
                 //new { controller = "CMSDemo", action = "Index", id = UrlParameter.Optional },
                new[] { "GYMONE.Controllers" }
                //defaults: new { controller = "Demo", action = "Index", id = UrlParameter.Optional }
                //defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
                //defaults: new { controller = "Dashboard", action = "AdminDashboard", id = UrlParameter.Optional }
            );
        }
    }
}