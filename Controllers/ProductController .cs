using Login.Models;
using Login.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

// exo : creer un controller account avec quatre action qui mene a quatre vues differentt
//1 er : login 
// 2eme : register
// 3eme : forgot password
// 4 : terms and condition
// les contres vues sont interconnectes par des liens sur les pages
namespace Login.Controllers
{
    public class ProductController : Controller
    {


        public ActionResult Index()
        {
            return View();
        }

    }
}
     