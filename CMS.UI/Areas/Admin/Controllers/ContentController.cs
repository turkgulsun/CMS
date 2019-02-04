using CMS.Business.Abstract;
using CMS.Business.Enums;
using CMS.Entities.Concrete;
using CMS.UI.Areas.Admin.Models.ContentsVM;
using CMS.UI.Functions;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        private GeneralFunctions _generalFunctions;

        public ContentController(IContentsService contentsService, IContentInfoService contentInfoService, IContentClassesService contentClassesService,
            IContentImagesService contentImagesService, GeneralFunctions generalFunctions)
        {
            _contentsService = contentsService;
            _contentInfoService = contentInfoService;
            _contentClassesService = contentClassesService;
            _contentImagesService = contentImagesService;
            _generalFunctions = generalFunctions;
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
                    Contents = new Contents(),
                    ContentInfo = new ContentInfo()
                };

                model.Contents.ClassID = cId;
                model.Contents.Active = true;
                model.Contents.Sort = 1;

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
        public ActionResult Create(Contents content, ContentInfo contentInfo, HttpPostedFileBase uploadfile)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var model = new ContentsVM
                    {
                        Contents = new Contents(),
                        ContentInfo = new ContentInfo()
                    };

                    model.Contents.Active = true;
                    model.Contents.Sort = 1;
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

                    //Varsa içerik resmini de silelim
                    if (image != null)
                    {
                        string filePath = HttpContext.Server.MapPath("/Uploads/Contents/" + id + "/" + image);
                        if (System.IO.File.Exists(filePath))
                            System.IO.File.Delete(filePath);
                    }

                    var contentInfo = _contentInfoService.Get(x => x.ContentID == id);
                    _contentInfoService.Delete(contentInfo.ContentInfoID);
                }
                TempData.Add("message", "İçerik başarıyla silindi.");
                string url = String.Format("index/{0}", classId);
                return RedirectToAction(url);
            }
            catch (Exception ex)
            {
                TempData.Add("message", "İçerik silme işlemi yapılırken hata ile karşılaşıldı. Hata: " + ex.Message);
                return RedirectToAction("index");
            }
        }
    }
}