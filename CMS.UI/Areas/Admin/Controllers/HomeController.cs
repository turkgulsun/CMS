using CMS.Business.Abstract;
using CMS.Entities.Concrete;
using CMS.UI.Areas.Admin.Models.ClassTypesVM;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using CMS.Business.Enums;
using CMS.UI.Areas.Admin.Models.ClassesVM;

namespace CMS.UI.Areas.Admin.Controllers
{
    [RouteArea("admin")]
    //[RoutePrefix("home")]
    [Route("index")]
    public class HomeController : Controller
    {

        private IClassTypeInfoService _classTypInfoservice;
        private IClassesService _classesService;
        private IClassInfoService _classInfoService;

        public HomeController(IClassTypeInfoService classTypeInfoservice, IClassesService classesService, IClassInfoService classInfoService)
        {
            _classTypInfoservice = classTypeInfoservice;
            _classesService = classesService;
            _classInfoService = classInfoService;
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

        [Route("home/ContentClassList")]
        public ActionResult ContentClassList()
        {
            var classes = _classesService.GetAll();
            var classInfo = _classInfoService.GetAll();

            var model = (from c in classes
                         join cI in classInfo on c.ClassID equals cI.ClassID
                         where c.ClassTypeID == (int)ClassTypeEnums.ClassType.Icerikler
                         select new ClassesListVM { ClassID = c.ClassID, Name = cI.Name }).ToList();

            return PartialView(model);

        }
    }
}