using CMS.Business.Abstract;
using CMS.DataAccess.Abstract;
using CMS.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CMS.Business.Concrete
{
    public class UsersManager : IUsersService
    {

        public IUsersDal _usersDal;

        public UsersManager(IUsersDal usersDal)
        {
            _usersDal = usersDal;
        }

        public void Add(Users users)
        {
            _usersDal.Add(users);
        }

        public void Delete(int userID)
        {
            _usersDal.Delete(new Users { UserID = userID });
        }

        public Users Get(Expression<Func<Users, bool>> filter = null)
        {
            return _usersDal.Get(filter);
        }

        public List<Users> GetAll()
        {
            return _usersDal.GetList();
        }

        public Users GetById(int id)
        {
            return _usersDal.Get(x => x.UserID == id);
        }

        public void Update(Users users)
        {
            _usersDal.Update(users);
        }
    }
}
