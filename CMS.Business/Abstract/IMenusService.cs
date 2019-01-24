using CMS.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CMS.Business.Abstract
{
    public interface IMenusService
    {
        List<Menus> GetAll();
        Menus Get(Expression<Func<Menus, bool>> filter); // LINQ desteği sunabilmek içinde expression'ları kullanıyoruz.
        Menus GetById(int id);
        void Add(Menus menu);
        void Update(Menus menu);
        void Delete(int menuID);
    }
}
