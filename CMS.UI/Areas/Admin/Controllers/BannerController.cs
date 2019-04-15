using CMS.Business.Abstract;
using CMS.Entities.Concrete;
using CMS.UI.Areas.Admin.Models.BannersVM;
using CMS.UI.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using CMS.Business.Enums;

namespace CMS.UI.Areas.Admin.Controllers
{
    [RouteArea("admin")]
    [RoutePrefix("banners")]
    public class BannerController : Controller
    {

        private IBannerService _bannerService;
        private IBannerInfoService _bannerInfoService;
        private GeneralFunctions _generalFunctions;
        private IListsService _listsService;
        private IListInfoService _listInfoService;

        public BannerController(IBannerService bannerService, IBannerInfoService bannerInfoService, GeneralFunctions generalFunctions, IListInfoService listInfoService, IListsService listsService)
        {
            _bannerService = bannerService;
            _bannerInfoService = bannerInfoService;
            _generalFunctions = generalFunctions;
            _listsService = listsService;
            _listInfoService = listInfoService;
        }

        // GET: Admin/Banner
        [Route("index")]
        [HttpGet]
        public ActionResult Index(int page = 1)
        {

            //comment
            var banners = _bannerService.GetAll();
            var bannerInfo = _bannerInfoService.GetAll();
            var lists = _listsService.GetAll();
            var listInfo = _listInfoService.GetAll();

            var model = (from b in banners
                         join bI in bannerInfo on b.BannerID equals bI.BannerID
                         join l in lists on b.BannerListID equals l.ListID
                         join lI in listInfo on l.ListID equals lI.ListID
                         select new BannersListVM
                         {
                             BannerID = b.BannerID,
                             BannerInfoID = bI.BannerInfoID,
                             Name = bI.Name,
                             Banner = bI.Banner,
                             CreationDate = b.CreationDate,
                             Active = b.Active,
                             ListType = lI.Value,
                             Description = bI.Description
                         }).ToPagedList(page, (int)PagingEnums.Paging.PageSize);

            return View(model);
        }


        [Route("create")]
        public ActionResult Create()
        {

            var listTypes = _generalFunctions.GetListType("Banner Yeri");

            var model = new BannersVM
            {
                Banners = new Banners(),
                BannerInfo = new BannerInfo(),
                ListType = listTypes
            };

            model.Banners.Active = true;
            model.Banners.Sort = 1;

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [Route("create")]
        public ActionResult Create(FormCollection collection, Banners banners, BannerInfo bannerInfo, HttpPostedFileBase uploadfile)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var listTypes = _generalFunctions.GetListType("Banner Yeri");
                    var model = new BannersVM
                    {
                        Banners = new Banners(),
                        BannerInfo = new BannerInfo(),
                        ListType = listTypes
                    };
                }

                banners.CreationDate = DateTime.Now;
                if (uploadfile != null)
                    bannerInfo.Banner = uploadfile.FileName;
                else
                    bannerInfo.Banner = null;
                _bannerService.Add(banners);
                int bannerID = banners.BannerID;

                //Bannerı kayıt edelim.
                if (bannerInfo.Banner != null && uploadfile != null)
                {
                    _generalFunctions.CreateDirectory(HttpContext.Server.MapPath("/Uploads/Banners/"), bannerID.ToString());
                    uploadfile.SaveAs(HttpContext.Server.MapPath("/Uploads/Banners/" + bannerID + "/" + uploadfile.FileName));
                }

                //Banner bilgilerini kayıt edelim.
                bannerInfo.BannerID = bannerID;
                bannerInfo.LanguageID = 1;
                _bannerInfoService.Add(bannerInfo);

                TempData.Add("message", "Banner başarıyla eklendi.");

                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                TempData.Add("message", "Banner yüklenirken hata ile karşılaştı. Hata: " + ex.Message);
                return View();

            }
        }

        [HttpPost]
        public ActionResult Delete(List<int> ids, FormCollection collection, BannersListVM _bannersListVM)
        {
            try
            {
                foreach (var id in ids)
                {
                    _bannerService.Delete(id);
                    var bannerInfo = _bannerInfoService.Get(x => x.BannerID == id);
                    //Bannerı silelim.
                    string filePath = HttpContext.Server.MapPath("/Uploads/Banners/" + id + "/" + bannerInfo.Banner);
                    if (System.IO.File.Exists(Server.MapPath(filePath)))
                        System.IO.File.Delete(Server.MapPath(filePath));
                    _bannerInfoService.Delete(bannerInfo.BannerInfoID);
                }
                TempData.Add("message", "Banner başarıyla silindi.");
                return RedirectToAction("index");
            }
            catch
            {
                return View();
            }
        }

        [Route("Edit/{id}")]
        public ActionResult Edit(int id)
        {
            try
            {
                var listTypes = _generalFunctions.GetListType("Banner Yeri");
                var model = new BannersVM
                {
                    Banners = _bannerService.GetById(id),
                    BannerInfo = _bannerInfoService.Get(b => b.BannerID == id),
                    ListType = listTypes
                };
                return View(model);
            }
            catch (Exception ex)
            {
                TempData.Add("message", "Banner güncelleme ekranı yüklenirken hata ile karşılaştı. Hata: " + ex.Message);
                return View();
            }
        }

        // POST: Admin/Classes/Edit/5
        [HttpPost]
        [ValidateInput(false)]
        [Route("Edit/{id}")]
        public ActionResult Edit(int id, FormCollection collection, HttpPostedFileBase uploadfile, Banners banners, BannerInfo bannerInfo)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var model = new BannersVM
                    {
                        Banners = _bannerService.GetById(id),
                        BannerInfo = _bannerInfoService.Get(b => b.BannerID == id)
                    };
                    return View(model);
                }

                //Bannerı update edelim.
                _bannerService.Update(banners);

                //Yeni bir banner yüklediyse
                if (bannerInfo.Banner != null && uploadfile != null)
                {
                    //Önceki resmi dosyadan silelim ki boşuna yer kaplamasın.
                    string filePath = "/Uploads/Banners/" + id + "/" + bannerInfo.Banner;
                    if (System.IO.File.Exists(Server.MapPath(filePath)))
                        System.IO.File.Delete(Server.MapPath(filePath));

                    if (uploadfile != null)
                        bannerInfo.Banner = uploadfile.FileName;

                    _generalFunctions.CreateDirectory(HttpContext.Server.MapPath("/Uploads/Banners/"), id.ToString());
                    uploadfile.SaveAs(HttpContext.Server.MapPath("/Uploads/Banners/" + id + "/" + uploadfile.FileName));
                }

                //bannerInfo.BannerID = id;
                //bannerInfo.LanguageID = 1;
                _bannerInfoService.Update(bannerInfo);

                TempData.Add("message", "Banner başarıyla güncellendi.");

                return RedirectToAction("index");

            }
            catch (Exception ex)
            {
                TempData.Add("message", "Banner güncellenirken hata ile karşılaştı. Hata: " + ex.Message);
                return View();
            }
        }
    }
}