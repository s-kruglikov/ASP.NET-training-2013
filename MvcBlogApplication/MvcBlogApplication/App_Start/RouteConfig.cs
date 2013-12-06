using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;

namespace MvcBlog.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // route used to navigation default
            routes.MapRoute(
                name: null,
                url: "",
                defaults: new { controller = "Posts", action = "List", category = (string)null, page = 1 }
            );

            routes.MapRoute(
                null,
                "Page{page}",
                new { controller = "Posts", action = "List", category = (string)null },
                new { page = @"\d+" }
            );

            routes.MapRoute(
                null,
                "{category}",
                new { controller = "Posts", action = "List", page = 1 }
            );

            routes.MapRoute(
                null,
                "{category}/Page{page}",
                new { controller = "Posts", action = "List" },
                new { page = @"\d+" }
            );

            routes.MapRoute(
                null,
                "{category}/Post{postId}",
                new { controller = "Posts", action = "SinglePost"}
            );

            //routes.MapRoute(
            //    null,
            //    "{category}/Post{postId}",
            //    new { controller = "Posts", action = "AddComment" }
            //);

            routes.MapRoute(null, "{controller}/{action}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Posts", action = "List", id = UrlParameter.Optional }
            );
        }
    }
}