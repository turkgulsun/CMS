using CMS.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using CMS.Entities.Concrete;
using System.Linq.Expressions;
using CMS.DataAccess.Abstract;

namespace CMS.Business.Concrete
{
    public class MenuManager : IMenusService
    {

        public IMenusDal _menusDal;

        public MenuManager(IMenusDal menusDal)
        {
            _menusDal = menusDal;
        }

        public void Add(Menus menu)
        {
            _menusDal.Add(menu);
        }

        public void Delete(int menuID)
        {
            _menusDal.Delete(new Menus { MenuID = menuID });
        }

        public Menus Get(Expression<Func<Menus, bool>> filter = null)
        {
            return _menusDal.Get(filter);
        }

        public List<Menus> GetAll()
        {
            return _menusDal.GetList();
        }

        public Menus GetById(int id)
        {
            return _menusDal.Get(x => x.MenuID == id);
        }

        public void Update(Menus menu)
        {
            _menusDal.Update(menu);
        }
    }
}
