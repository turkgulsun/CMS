using CMS.Business.Abstract;
using CMS.Business.Enums;
using CMS.Entities.Concrete;
using CMS.UI.Areas.Admin.Models.ContentsVM;
using CMS.UI.Areas.Admin.Models.MenusVM;
using CMS.UI.Functions;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;

namespace CMS.UI.Areas.Admin.Controllers
{
    [RouteArea("admin")]
    [RoutePrefix("contents")]
    public class ContentController : Controller
    {
        private IContentsService _contentsService;
        private IContentInfoService _contentInfoService;
        private IContentClassesService _contentClassesService;
        private IContentImagesService _contentImagesService;
        private IMenuInfoService _menuInfoService;
        private IMenusService _menusService;
        private GeneralFunctions _generalFunctions;

        public ContentController(IContentsService contentsService, IContentInfoService contentInfoService, IContentClassesService contentClassesService,
            IContentImagesService contentImagesService, GeneralFunctions generalFunctions, IMenusService menusService, IMenuInfoService menuInfoService)
        {
            _contentsService = contentsService;
            _contentInfoService = contentInfoService;
            _contentClassesService = contentClassesService;
            _contentImagesService = contentImagesService;
            _generalFunctions = generalFunctions;
            _menusService = menusService;
            _menuInfoService = menuInfoService;
        }


        // GET: Admin/Content
        [Route("index/{cId?}")]
        public ActionResult Index(int page = 1, int? cId = 0)
        {
            try
            {
                var contents = _contentsService.GetAll();
                var contentInfo = _contentInfoService.GetAll();

                var model = (from c in contents
                             join cI in contentInfo on c.ContentID equals cI.ContentID
                             where c.ClassID == cId
                             select new ContentsListVM
                             {
                                 ContentID = c.ContentID,
                                 ClassID = c.ClassID,
                                 ContentInfoID = cI.ContentInfoID,
                                 CreationDate = c.CreationDate,
                                 Summary = cI.Summary,
                                 Title = cI.Title,
                                 Image = c.Image,
                                 Active = c.Active

                             }
                           ).ToPagedList(page, (int)PagingEnums.Paging.PageSize);

                return View(model);
            }
            catch (Exception ex)
            {
                TempData.Add("message", "İçerik listeleme işleminde hata ile karşılaştı. Hata: " + ex.Message);
                return View();
            }

        }

        [Route("create/{cId}")]
        public ActionResult Create(int cId)
        {
            try
            {
                var model = new ContentsVM
                {
                    Content = new Contents(),
                    ContentInfo = new ContentInfo()
                };

                model.Content.ClassID = cId;
                model.Content.Active = true;
                model.Content.Sort = 1;

                //Menüleri getirelim.
                GetContentMenuList(null);

                return View(model);
            }
            catch (Exception ex)
            {
                TempData.Add("message", "İçerik kaydetme işleminde hata ile karşılaştı. Hata: " + ex.Message);
                return View();
            }
        }

        [Route("create/{cId}")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Contents content, ContentInfo contentInfo, HttpPostedFileBase uploadfile, List<int> ids)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var model = new ContentsVM
                    {
                        Content = new Contents(),
                        ContentInfo = new ContentInfo()
                    };

                    model.Content.Active = true;
                    model.Content.Sort = 1;

                    //Menüleri getirelim.
                    GetContentMenuList(null);
                }

                content.CreationDate = DateTime.Now;
                content.LastModifiedDate = DateTime.Now;

                if (uploadfile != null)
                    content.Image = uploadfile.FileName;
                else
                    content.Image = null;
                _contentsService.Add(content);
                int contentId = content.ContentID;

                //İçerik resmini kayıt edelim.
                if (content.Image != null && uploadfile != null)
                {
                    _generalFunctions.CreateDirectory(HttpContext.Server.MapPath("/Uploads/Contents/"), contentId.ToString());
                    uploadfile.SaveAs(HttpContext.Server.MapPath("/Uploads/Contents/" + contentId + "/" + uploadfile.FileName));
                }

                contentInfo.ContentID = contentId;
                contentInfo.LanguageID = 1;
                _contentInfoService.Add(contentInfo);

                //Seçilen menüleri kayıt edelim.
                ContentClasses contentClasses;
                if (ids != null)
                {
                    foreach (var id in ids)
                    {
                        contentClasses = new ContentClasses();
                        contentClasses.ContentID = content.ContentID;
                        contentClasses.MenuID = id;
                        contentClasses.ClassID = 0;
                        _contentClassesService.Add(contentClasses);
                    }
                }
                TempData.Add("message", "İçerik başarıyla eklendi.");

                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                TempData.Add("message", "İçerik kaydetme işleminde hata ile karşılaştı. Hata: " + ex.Message);
                return View();
            }
        }

        [HttpPost]
        [Route("delete")]
        public ActionResult Delete(List<int> ids, ContentsListVM contentsListVM)
        {
            try
            {
                string classId = null;
                foreach (var id in ids)
                {
                    string image = null;
                    var model = _contentsService.GetById(id);
                    image = model.Image;
                    classId = Convert.ToString(model.ClassID);

                    _contentsService.Delete(id);

                    //İçerik resmini silelim
                    if (image != null)
                    {
                        string filePath = HttpContext.Server.MapPath("/Uploads/Contents/" + id + "/" + image);
                        if (System.IO.File.Exists(filePath))
                            System.IO.File.Delete(filePath);
                    }

                    var contentInfo = _contentInfoService.Get(x => x.ContentID == id);
                    _contentInfoService.Delete(contentInfo.ContentInfoID);

                    //İçerik sınıflarını silelim.
                    var deleteContentClasses = _contentClassesService.GetAll(cc => cc.ContentID == id);
                    foreach (var item in deleteContentClasses)
                    {
                        _contentClassesService.Delete(item.ContentClassID);
                    }
                }
                TempData.Add("message", "İçerik başarıyla silindi.");
                string url = String.Format("index/{0}", classId);
                return Redirect(url);
            }
            catch (Exception ex)
            {
                TempData.Add("message", "İçerik silme işlemi yapılırken hata ile karşılaşıldı. Hata: " + ex.Message);
                return RedirectToAction("index");
            }
        }

        [NonAction]
        public void GetContentMenuList(List<int> id = null)
        {
            var menuRecursion = new MenuRecursion(_menusService, _menuInfoService);
            var menuUi = menuRecursion.GenerateMenuUi(id);
            ViewBag.Menu = menuUi;
        }


        [Route("edit/{cId}")]
        public ActionResult Edit(int cId)
        {
            try
            {
                var model = new ContentsVM
                {
                    Content = _contentsService.GetById(cId),
                    ContentInfo = _contentInfoService.Get(cI => cI.ContentID == cId),
                    ContentClasses = _contentClassesService.GetAll(cc => cc.ContentID == cId)
                };

                List<int> _contentClasses = new List<int>();
                if (model.ContentClasses != null)
                {
                    foreach (var item in model.ContentClasses)
                    {
                        _contentClasses.Add(item.MenuID);
                    }
                }

                GetContentMenuList(_contentClasses);
                return View(model);
            }
            catch (Exception ex)
            {
                return Content("Hata: " + ex.Message);
                throw;
            }
        }

        [HttpPost]
        [Route("edit/{cId}")]
        [ValidateInput(false)]
        public ActionResult Edit(int cId, Contents content, ContentInfo contentInfo, HttpPostedFileBase uploadfile, List<int> ids)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var model = new ContentsVM
                    {
                        Content = new Contents(),
                        ContentInfo = new ContentInfo()
                    };

                    model.Content.Active = true;
                    model.Content.Sort = 1;

                    //Menüleri getirelim.
                    GetContentMenuList(null);
                }

                content.LastModifiedDate = DateTime.Now;

                //Yeni bir banner yüklediyse
                if (content.Image != null && uploadfile != null)
                {
                    //Önceki resmi dosyadan silelim ki boşuna yer kaplamasın.
                    string filePath = "/Uploads/Contents/" + cId + "/" + content.Image;
                    if (System.IO.File.Exists(filePath))
                        System.IO.File.Delete(filePath);

                    if (uploadfile != null)
                        content.Image = uploadfile.FileName;

                    _generalFunctions.CreateDirectory(HttpContext.Server.MapPath("/Uploads/Contents/"), cId.ToString());
                    uploadfile.SaveAs(HttpContext.Server.MapPath("/Uploads/Contents/" + cId + "/" + uploadfile.FileName));
                }

                _contentsService.Update(content);
                int contentId = content.ContentID;
                _contentInfoService.Update(contentInfo);

                //Veri tekrarını önlemek için önce Content Class'ı silelim.
                var deleteContentClasses = _contentClassesService.GetAll(cc => cc.ContentID == cId);
                foreach (var item in deleteContentClasses)
                {
                    _contentClassesService.Delete(item.ContentClassID);
                }

                if (ids != null)
                {
                    //Seçilen menüleri güncelleyelim.
                    ContentClasses contentClasses;
                    foreach (var id in ids)
                    {
                        contentClasses = new ContentClasses();
                        contentClasses.ContentID = content.ContentID;
                        contentClasses.MenuID = id;
                        contentClasses.ClassID = 0;
                        _contentClassesService.Add(contentClasses);
                    }
                }

                TempData.Add("message", "İçerik başarıyla güncellendi.");

                string url = String.Format("index/{0}", content.ClassID);
                return Redirect(url);
            }
            catch (Exception ex)
            {
                TempData.Add("message", "İçerik güncelleme işleminde hata ile karşılaştı. Hata: " + ex.Message);
                return View();
            }
        }
    }
}