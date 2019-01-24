using CMS.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using CMS.Entities.Concrete;
using System.Linq.Expressions;
using CMS.DataAccess.Abstract;

namespace CMS.Business.Concrete
{
    public class MenuInfoManager : IMenuInfoService
    {
        public IMenuInfoDal _menuInfoDal;

        public MenuInfoManager(IMenuInfoDal menuInfoDal)
        {
            _menuInfoDal = menuInfoDal;
        }

        public void Add(MenuInfo menuInfo)
        {
            _menuInfoDal.Add(menuInfo);
        }

        public void Delete(int menuInfoID)
        {
            _menuInfoDal.Delete(new MenuInfo { MenuInfoID = menuInfoID });
        }

        public MenuInfo Get(Expression<Func<MenuInfo, bool>> filter = null)
        {
            return _menuInfoDal.Get(filter);
        }

        public List<MenuInfo> GetAll()
        {
            return _menuInfoDal.GetList();
        }

        public MenuInfo GetById(int id)
        {
            return _menuInfoDal.Get(x => x.MenuInfoID == id);
        }

        public void Update(MenuInfo menuInfo)
        {
            _menuInfoDal.Update(menuInfo);
        }
    }
}
