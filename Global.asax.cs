using Login.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;


// exo : creer un controller account avec quatre action qui mene a quatre vues differentt
//1 er : login 
// 2eme : register
// 3eme : forgot password
// 4 : terms and condition
// les contres vues sont interconnectes par des liens sur les pages

namespace Login
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // Initialize the product database.
            Database.SetInitializer(new ProductDatabaseInitializer());
        }
        protected void Session_End()
        {
            FormsAuthentication.SignOut();
            Response.Redirect(FormsAuthentication.LoginUrl);

        }
    }
}
