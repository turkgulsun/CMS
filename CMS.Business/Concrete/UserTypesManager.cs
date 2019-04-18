using CMS.Business.Abstract;
using CMS.DataAccess.Abstract;
using CMS.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CMS.Business.Concrete
{
    public class UserTypesManager : IUserTypesService
    {
        public IUserTypesDal _userTypesDal;

        public UserTypesManager(IUserTypesDal userTypesDal)
        {
            _userTypesDal = userTypesDal;
        }
        public void Add(UserTypes userTypes)
        {
            _userTypesDal.Add(userTypes);
        }

        public void Delete(int userTypeID)
        {
            _userTypesDal.Delete(new UserTypes { UserTypeID = userTypeID });
        }

        public UserTypes Get(Expression<Func<UserTypes, bool>> filter = null)
        {
            return _userTypesDal.Get(filter);
        }

        public List<UserTypes> GetAll()
        {
            return _userTypesDal.GetList();
        }

        public void Update(UserTypes userTypes)
        {
            _userTypesDal.Update(userTypes);
        }
    }
}
