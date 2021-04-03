using Login.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Login.Controllers
{
    // [Authorize (Roles ="Admin, Customer")]
    [Authorize]
     public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index(HomeModel model)
        {
            model.UserName = HttpContext.User.Identity.Name;
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult About()
        {
            
            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            return View();
        }
    }
}