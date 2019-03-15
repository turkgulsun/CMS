using CMS.Business.Abstract;
using CMS.Entities.Concrete;
using CMS.UI.Areas.Admin.Models.MenusVM;
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
    [RoutePrefix("menus")]
    public class MenuController : Controller
    {
        // GET: Admin/Menu

        private IMenusService _menusService;
        private IMenuInfoService _menuInfoService;
        private GeneralFunctions _generalFunctions;
        private IListsService _listsService;
        private IListInfoService _listInfoService;
        public MenuController(IMenusService menusService, IMenuInfoService menuInfoService, GeneralFunctions generalFunctions, IListInfoService listInfoService, IListsService listsService)
        {
            _menusService = menusService;
            _menuInfoService = menuInfoService;
            _generalFunctions = generalFunctions;
            _listsService = listsService;
            _listInfoService = listInfoService;
        }

        [Route("index/{menuId?}")]
        public ActionResult Index(int? menuId = 0, int page = 1)
        {
            try
            {
                var menus = _menusService.GetAll();
                var menuInfo = _menuInfoService.GetAll();

                var lists = _listsService.GetAll();
                var listInfo = _listInfoService.GetAll();

                if (menuId > 0)
                    ViewBag.MenuId = menuId;
                else
                    menuId = 0;

                var model = (from m in menus
                             join mI in menuInfo on m.MenuID equals mI.MenuID
                             join l in lists on m.MenuListID equals l.ListID
                             join lI in listInfo on l.ListID equals lI.ListID
                             where m.ParentID == menuId
                             select new MenuListVM
                             {
                                 MenuID = m.MenuID,
                                 MenuInfoID = mI.MenuInfoID,
                                 Menu = mI.Menu,
                                 MenuListID = m.MenuListID,
                                 MenuYeri = lI.Value,
                                 Active = m.Active,
                                 CreationDate = m.CreationDate
                             }
                           ).ToPagedList(page, (int)PagingEnums.Paging.PageSize);

                return View(model);

            }
            catch (Exception ex)
            {
                TempData.Add("message", "Menü listeleme işleminde hata ile karşılaştı. Hata: " + ex.Message);
                return View();
            }
        }


        [Route("create/{menuId?}")]
        public ActionResult Create(int? menuId)
        {
            try
            {
                var listTypes = _generalFunctions.GetListType("Menü Yeri");
                var model = new MenusVM
                {
                    Menus = new Menus(),
                    MenuInfo = new MenuInfo(),
                    ListType = listTypes
                };

                if (menuId != null)
                    model.Menus.ParentID = Convert.ToInt32(menuId);
                model.Menus.Active = true;
                model.Menus.Sort = 1;

                return View(model);
            }
            catch (Exception ex)
            {
                TempData.Add("message", "Menü ekleme sayfası yüklenirken hata ile karşılaştı. Hata: " + ex.Message);
                return View();
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        [Route("create/{menuId?}")]
        public ActionResult Create(Menus menus, MenuInfo menuInfo, HttpPostedFileBase uploadfile)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var listTypes = _generalFunctions.GetListType("Menü Yeri");
                    var model = new MenusVM
                    {
                        Menus = new Menus(),
                        MenuInfo = new MenuInfo(),
                        ListType = listTypes
                    };
                    model.Menus.Active = true;
                    model.Menus.Sort = 1;
                }

                menus.CreationDate = DateTime.Now;
                if (uploadfile != null)
                    menus.Image = uploadfile.FileName;
                else
                    menus.Image = null;

                _menusService.Add(menus);
                int menuID = menus.MenuID;

                //Menüyü resmini kayıt edelim.
                if (menus.Image != null && uploadfile != null)
                {
                    _generalFunctions.CreateDirectory(HttpContext.Server.MapPath("/Uploads/Menus/"), menuID.ToString());
                    uploadfile.SaveAs(HttpContext.Server.MapPath("/Uploads/Menus/" + menuID + "/" + uploadfile.FileName));
                }


                //Menü bilgilerini kayıt edelim.
                menuInfo.MenuID = menuID;
                menuInfo.LanguageID = 1;
                _menuInfoService.Add(menuInfo);

                TempData.Add("message", "Menü başarıyla eklendi.");

                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                TempData.Add("message", "Menü kayıt işleminde hata ile karşılaştı. Hata: " + ex.Message);
                return View();
            }
        }


        [Route("edit/{id}")]
        public ActionResult Edit(int id)
        {
            try
            {
                var listTypes = _generalFunctions.GetListType("Menü Yeri");
                var model = new MenusVM
                {
                    Menus = _menusService.GetById(id),
                    MenuInfo = _menuInfoService.Get(m => m.MenuID == id),
                    ListType = listTypes
                };
                return View(model);
            }
            catch (Exception ex)
            {
                TempData.Add("message", "Menü güncelleme sayfası yüklenirken hata ile karşılaşıldı. Hata: " + ex.Message);
                return View();
            }
        }


        [HttpPost]
        [ValidateInput(false)]
        [Route("edit/{id}")]
        public ActionResult Edit(Menus menus, MenuInfo menuInfo, HttpPostedFile uploadfile)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var listTypes = _generalFunctions.GetListType("Menü Yeri");
                    var model = new MenusVM
                    {
                        Menus = new Menus(),
                        MenuInfo = new MenuInfo(),
                        ListType = listTypes
                    };
                    model.Menus.Active = true;
                    model.Menus.Sort = 1;
                }

                if (uploadfile != null)
                    menus.Image = uploadfile.FileName;
                else
                    menus.Image = null;

                _menusService.Update(menus);

                //Menüyü resmini kayıt edelim.
                if (menus.Image != null && uploadfile != null)
                {
                    //Önceki resmi dosyadan silelim ki boşuna yer kaplamasın.
                    string filePath = "/Uploads/Menus/" + menus.MenuID + "/" + menus.Image;
                    if (System.IO.File.Exists(filePath))
                        System.IO.File.Delete(filePath);

                    _generalFunctions.CreateDirectory(HttpContext.Server.MapPath("/Uploads/Menus/"), menus.MenuID.ToString());
                    uploadfile.SaveAs(HttpContext.Server.MapPath("/Uploads/Menus/" + menus.MenuID + "/" + uploadfile.FileName));
                }

                //Menü bilgilerini güncelleyelim.
                _menuInfoService.Update(menuInfo);

                TempData.Add("message", "Menü başarıyla güncellendi.");

                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                TempData.Add("message", "Menü güncelleme yapılırken hata ile karşılaşıldı. Hata: " + ex.Message);
                return View();
            }
        }

        [HttpPost]
        [Route("delete")]
        public ActionResult Delete(List<int> ids, MenuListVM _menuListVM)
        {
            try
            {
                foreach (var id in ids)
                {
                    string image = _menuListVM.Image;
                    _menusService.Delete(id);

                    //Varsa menü resmini de silelim
                    string filePath = HttpContext.Server.MapPath("/Uploads/Menus/" + id + "/" + image);
                    if (System.IO.File.Exists(filePath))
                        System.IO.File.Delete(filePath);


                    var menuInfo = _menuInfoService.Get(x => x.MenuID == id);
                    _menuInfoService.Delete(menuInfo.MenuInfoID);
                }
                TempData.Add("message", "Menü başarıyla silindi.");
                return RedirectToAction("index");
            }
            catch(Exception ex)
            {
                TempData.Add("message", "Menü silme işlemi yapılırken hata ile karşılaşıldı. Hata: " + ex.Message);
                return View();
            }
        }
    }
}