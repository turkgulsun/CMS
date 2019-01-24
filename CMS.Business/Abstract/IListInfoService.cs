using CMS.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CMS.Business.Abstract
{
    public interface IListInfoService
    {
        List<ListInfo> GetAll();
        ListInfo Get(Expression<Func<ListInfo, bool>> filter=null);
    }
}
