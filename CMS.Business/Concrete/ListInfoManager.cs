using CMS.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using CMS.Entities.Concrete;
using System.Linq.Expressions;
using CMS.DataAccess.Abstract;

namespace CMS.Business.Concrete
{
    public class ListInfoManager : IListInfoService
    {
        public IListInfoDal _listInfoDal;
        public ListInfoManager(IListInfoDal listInfoDal)
        {
            _listInfoDal = listInfoDal;
        }

        public ListInfo Get(Expression<Func<ListInfo, bool>> filter = null)
        {
            return _listInfoDal.Get(filter);
        }

        public List<ListInfo> GetAll()
        {
            return _listInfoDal.GetList();
        }
    }
}
