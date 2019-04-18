using CMS.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CMS.Business.Abstract
{
    public interface IUsersService
    {
        List<Users> GetAll();
        Users Get(Expression<Func<Users, bool>> filter = null);
        Users GetById(int id);
        void Add(Users users);
        void Update(Users users);
        void Delete(int userID);
    }
}
