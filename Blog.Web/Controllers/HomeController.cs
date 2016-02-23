using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

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
    }
}
