using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.UI.Controllers
{
    public class TestController : Controller
    {
        // GET: Test

        //[Route("books")]
        public ActionResult Index()
        {
            return Content("Only books");
        }

        //[Route("books/{title}")]
        public ActionResult Index(string title)
        {
            return Content(title);
        }
    }
}