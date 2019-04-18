using CMS.Business.Abstract;
using CMS.Business.Enums;
using CMS.Entities.Concrete;
using CMS.UI.Areas.Admin.Models.UsersVM;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.UI.Areas.Admin.Controllers
{

    [RouteArea("admin")]
    [RoutePrefix("account")]
    public class AccountController : Controller
    {
        //private IBannerService _bannerService;
        private IUsersService _usersService;
        private IUserTypesService _userTypesService;

        public AccountController(IUsersService usersService, IUserTypesService userTypesService)
        {
            _usersService = usersService;
            _userTypesService = userTypesService;
        }

        [Route("index")]
        [HttpGet]
        // GET: Admin/Account
        public ActionResult Index(int page = 1)
        {
            var users = _usersService.GetAll();

            var model = (from u in users
                         select new UserListVM
                         {
                             UserID = u.UserID,
                             UserName = u.UserName,
                             EMail = u.EMail,
                             CreationDate = u.CreationDate
                         }).ToPagedList(page, (int)PagingEnums.Paging.PageSize);

            return View(model);
        }

        [Route("create")]
        public ActionResult Create()
        {
            var userTypesList = _userTypesService.GetAll();

            var model = new UsersVM
            {
                User = new Users(),
                UserTypes = userTypesList
            };
            return View(model);
        }


        [HttpPost]
        [ValidateInput(false)]
        [Route("create")]
        public ActionResult Create(Users user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var userTypesList = _userTypesService.GetAll();
                    var model = new UsersVM
                    {
                        User = new Users(),
                        UserTypes = userTypesList
                    };
                }

                user.CreationDate = DateTime.Now;
                _usersService.Add(user);

                TempData.Add("message", "Kullanıcı başarıyla eklendi.");

                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                TempData.Add("message", "Kullanıcı oluştururken hata ile karşılaştı. Hata: " + ex.Message);
                return View();
            }
        }

        [HttpPost]
        [Route("delete")]
        public ActionResult Delete(List<int> ids)
        {
            try
            {
                foreach (var id in ids)
                {
                    _usersService.Delete(id);
                }

                TempData.Add("message", "Kullanıcı başarıyla silindi.");
                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                TempData.Add("message", "Kullanıcı silme  işleminde hata ile karşılaştı. Hata: " + ex.Message);
                return RedirectToAction("index");
            }
        }

        [Route("edit/{uId}")]
        public ActionResult Edit(int uId)
        {
            var userTypesList = _userTypesService.GetAll();
            var model = new UsersVM
            {
                User = _usersService.GetById(uId),
                UserTypes = userTypesList
            };
            return View(model);
        }

        [HttpPost]
        [Route("edit/{uId}")]
        [ValidateInput(false)]
        public ActionResult Edit(int uId, Users user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var userTypesList = _userTypesService.GetAll();
                    var model = new UsersVM
                    {
                        User = new Users(),
                        UserTypes = userTypesList
                    };
                }

                _usersService.Update(user);

                TempData.Add("message", "Kullanıcı bilgileri başarıyla güncellendi.");

                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                TempData.Add("message", "Kullanıcı bilgileri güncelleme işleminde hata ile karşılaştı. Hata: " + ex.Message);
                return View();
            }
        }
    }
}