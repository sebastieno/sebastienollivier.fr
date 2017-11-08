using System;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("toutvapeter")]
        public ActionResult Bug()
        {
            throw new ArgumentException("This is a bug");
        }

        [Route("oops")]
        public ActionResult Oops()
        {
            return View();
        }
    }
}
