using CMS.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CMS.Business.Abstract
{
    public interface IUserTypesService
    {
        List<UserTypes> GetAll();
        UserTypes Get(Expression<Func<UserTypes, bool>> filter = null);
        void Add(UserTypes userTypes);
        void Update(UserTypes userTypes);
        void Delete(int userTypeID);
    }
}
