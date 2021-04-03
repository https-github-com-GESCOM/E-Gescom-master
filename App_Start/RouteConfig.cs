using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Login
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
           // static string ConSt = @"data source=DESKTOP-8TCMO59\SQLEXPRESS;user id=sa;password=guylain;database=boss";
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                 //string ConSt = @"SERVER = (DESCRIPTION =(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=XE)));uid=oss;pwd=_soft";

        defaults: new { controller = "Account", action = "LOGIN", id = UrlParameter.Optional }
            );
        }
    }
}
