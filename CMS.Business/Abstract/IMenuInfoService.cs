using CMS.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CMS.Business.Abstract
{
   public interface IMenuInfoService
    {
        List<MenuInfo> GetAll();
        MenuInfo Get(Expression<Func<MenuInfo, bool>> filter);
        MenuInfo GetById(int id);
        void Add(MenuInfo menuInfo);
        void Update(MenuInfo menuInfo);
        void Delete(int menuInfoID);
    }
}
