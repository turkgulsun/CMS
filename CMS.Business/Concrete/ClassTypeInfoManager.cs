using CMS.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using CMS.Entities.Concrete;
using CMS.DataAccess.Abstract;

namespace CMS.Business.Concrete
{
    public class ClassTypeInfoManager : IClassTypeInfoService
    {
        private IClassTypeInfoDal _classTypeInfoDal;

        public ClassTypeInfoManager(IClassTypeInfoDal classTypeInfoDal)
        {
            _classTypeInfoDal = classTypeInfoDal;

        }
        public List<ClassTypeInfo> GetAll()
        {
            return _classTypeInfoDal.GetList();

        }

        public List<ClassTypeInfo> GetAll(int classTypeInfoID)
        {
            return _classTypeInfoDal.GetList(cti => cti.ClassTypeInfoID == classTypeInfoID);
        }
    }
}
