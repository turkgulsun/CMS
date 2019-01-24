using CMS.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CMS.Business.Abstract
{
    public interface IListsService
    {
        List<Lists> GetAll();
        Lists Get(Expression<Func<Lists, bool>> filter = null);

    }
}
