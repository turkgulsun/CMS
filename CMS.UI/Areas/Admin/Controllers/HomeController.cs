using CMS.Business.Abstract;
using CMS.UI.Areas.Admin.Models.ClassTypesVM;
using System.Web.Mvc;

namespace CMS.UI.Areas.Admin.Controllers
{
    [RouteArea("admin")]
   // [RoutePrefix("home")]
    [Route("index")]
    public class HomeController : Controller
    {

        private IClassTypeInfoService _classTypInfoservice;

        public HomeController(IClassTypeInfoService classTypeInfoservice)
        {
            _classTypInfoservice = classTypeInfoservice;
        }

        // GET: Admin/Home
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }


        [Route("home/ClassTypeList")]
        public ActionResult ClassTypeList()
        {
            var model = new ClassTypeInfoListVM()
            {
                ClassTypeInfo = _classTypInfoservice.GetAll()
            };

            return PartialView(model);
            // return Content("test");
        }
    }
}