using CMS.Business.Abstract;
using CMS.Entities.Concrete;
using CMS.UI.Areas.Admin.Models;
using CMS.UI.Areas.Admin.Models.ClassesVM;
using CMS.UI.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.UI.Areas.Admin.Controllers
{

    [RouteArea("admin")]
    [RoutePrefix("classes")]
    public class ClassesController : Controller
    {

        private IClassesService _classesService;
        private IClassInfoService _classInfoService;
        private GeneralFunctions _generalFunctions;

        public ClassesController(IClassesService classesService, IClassInfoService classInfoService, GeneralFunctions generalFunctions)
        {
            _classesService = classesService;
            _classInfoService = classInfoService;
            _generalFunctions = generalFunctions;
        }

        // GET: Admin/Classes

        [Route("index/{id?}/{classTypeId?}/{classId?}")]
        public ActionResult Index(int? id, int? classId, int? classTypeId)
        {
            int _classTypeId = 0;
            int _classId = 0;
            //Sınıf tipini viewbaga atayalım.
            if (classTypeId != null)
                _classTypeId = Convert.ToInt32(classTypeId);
            else
                _classTypeId = Convert.ToInt32(id);

            if (classId != null)
                ViewBag.ClassId = classId;
            else
                classId = 0;

            ViewBag.ClassTypeID = _classTypeId;

            var classes = _classesService.GetAll();
            var classInfo = _classInfoService.GetAll();

            var model = (from c in classes
                         join cI in classInfo on c.ClassID equals cI.ClassID
                         where c.ClassTypeID == _classTypeId && c.ParentID == classId
                         //&& (classId == null || c.ParentID == classId)
                         select new ClassesListVM
                         {
                             ClassID = c.ClassID,
                             Name = cI.Name,
                             Summary = cI.Summary,
                             CreationDate = c.CreationDate,
                             Active = c.Active,
                             ClassTypeID = c.ClassTypeID

                         }).ToList();


            return View(model);
        }

        // GET: Admin/Classes/Create

        [Route("create/{id?}/{classId?}")]
        public ActionResult Create(int id, int? classId)
        {
            var model = new ClassesVM
            {
                Classes = new Classes(),
                ClassInfo = new ClassInfo()
            };

            if (classId != null)
                model.Classes.ParentID = Convert.ToInt32(classId);


            model.Classes.ClassTypeID = id;
            model.Classes.Sort = 1;
            model.Classes.Active = true;

            return View(model);
        }

        // POST: Admin/Classes/Create
        [HttpPost]
        [ValidateInput(false)]
        [Route("create/{id?}/{classId?}")]
        public ActionResult Create(FormCollection collection, Classes classes, ClassInfo classInfo, HttpPostedFileBase uploadfile)
        {
            try
            {
                //Validation kontrolü yapalım.
                if (!ModelState.IsValid)
                {
                    var model = new ClassesVM
                    {
                        Classes = classes,
                        ClassInfo = classInfo
                    };

                    return View("Create", model);
                }
                classes.CreationDate = DateTime.Now;

                if (uploadfile != null)
                    classes.Image = uploadfile.FileName;
                else
                    classes.Image = null;

                //Sınıf kayıt edelim.
                _classesService.Add(classes);

                //Dönen sınıf Id'yi alalım.
                int classID = classes.ClassID;


                //Sınıf resmini kayıt edelim.
                if (classes.Image != null && uploadfile != null)
                {
                    _generalFunctions.CreateDirectory(HttpContext.Server.MapPath("/Uploads/Classes/"), classID.ToString());
                    uploadfile.SaveAs(HttpContext.Server.MapPath("/Uploads/Classes/" + classID + "/" + uploadfile.FileName));
                }


                //Sınıf bilgilerini kayıt edelim.
                classInfo.ClassID = classID;
                classInfo.LanguageID = 1;
                _classInfoService.Add(classInfo);

                TempData.Add("message", "Sınıf başarıyla eklendi.");

                return RedirectToAction("Index", "Classes", new { @id = classes.ClassTypeID });
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: Admin/Classes/Edit/5
        [Route("Edit/{id}")]
        public ActionResult Edit(int id)
        {
            try
            {
                var model = new ClassesVM
                {
                    Classes = _classesService.GetById(id),
                    ClassInfo = _classInfoService.Get(c => c.ClassID == id)
                };
                return View(model);
            }
            catch (Exception ex)
            {

                return Content("Hata: " + ex.Message);
                throw;
            }



        }

        // POST: Admin/Classes/Edit/5
        [HttpPost]
        [ValidateInput(false)]
        [Route("Edit/{id}")]
        public ActionResult Edit(int id, FormCollection collection, HttpPostedFileBase uploadfile, Classes classes, ClassInfo classInfo)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var model = new ClassesVM
                    {
                        Classes = _classesService.GetById(id),
                        ClassInfo = _classInfoService.Get(c => c.ClassID == id)
                    };
                    return View(model);
                }

                //Yeni bir sınıf resmi yüklediyse
                if (uploadfile != null)
                    classes.Image = uploadfile.FileName;

                //Sınıfı update edelim.
                _classesService.Update(classes);

                //Yeni resim yüklediyse update edelim
                if (classes.Image != null && uploadfile != null)
                {
                    //Önceki resmi dosyadan silelim ki boşuna yer kaplamasın.
                    string filePath = "/Uploads/Classes/" + id + "/" + uploadfile.FileName;
                    if (System.IO.File.Exists(filePath))
                        System.IO.File.Delete(filePath);

                    _generalFunctions.CreateDirectory(HttpContext.Server.MapPath("/Uploads/Classes/"), id.ToString());
                    uploadfile.SaveAs(HttpContext.Server.MapPath("/Uploads/Classes/" + id + "/" + uploadfile.FileName));
                }


                //Sınıf bilgilerini kayıt edelim.
                if (classes.ParentID > 0)
                    classInfo.ClassID = classes.ClassID;
                else
                    classInfo.ClassID = id;
                classInfo.LanguageID = 1;
                _classInfoService.Update(classInfo);

                TempData.Add("message", "Sınıf başarıyla güncellendi.");

                return RedirectToAction("Index", "Classes", new { @id = classes.ClassTypeID });

            }
            catch
            {
                return View();
            }
        }
        // POST: Admin/Classes/Delete/5
        [HttpPost]
        public ActionResult Delete(List<int> ids, FormCollection collection, ClassesListVM _classesListVM)
        {
            try
            {
                foreach (var id in ids)
                {
                    _classesService.Delete(id);

                    //Varsa sınıf resmini de silelim
                    string filePath = HttpContext.Server.MapPath("/Uploads/Classes/" + id + "/" + _classesListVM.Image);
                    if (System.IO.File.Exists(filePath))
                        System.IO.File.Delete(filePath);


                    var classInfo = _classInfoService.Get(x => x.ClassID == id); ;
                    _classInfoService.Delete(classInfo.ClassInfoID);
                }
                TempData.Add("message", "Sınıf başarıyla silindi.");
                return RedirectToAction("Index", "Classes", new { @id = _classesListVM.ClassTypeID });
            }
            catch
            {
                return View();
            }
        }
    }
}
