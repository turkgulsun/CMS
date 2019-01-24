using CMS.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using CMS.Entities.Concrete;
using System.Linq.Expressions;
using CMS.DataAccess.Abstract;

namespace CMS.Business.Concrete
{
    public class ListsManager : IListsService
    {
        public IListsDal _listsDal;
        public ListsManager(IListsDal listsDal)
        {
            _listsDal = listsDal;
        }
        public Lists Get(Expression<Func<Lists, bool>> filter = null)
        {
            return _listsDal.Get(filter);
        }

        public List<Lists> GetAll()
        {
            return _listsDal.GetList();
        }
    }
}
