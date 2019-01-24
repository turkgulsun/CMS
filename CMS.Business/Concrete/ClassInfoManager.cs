using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using CMS.Business.Abstract;
using CMS.DataAccess.Abstract;
using CMS.Entities.Concrete;

namespace CMS.Business.Concrete
{
    public class ClassInfoManager : IClassInfoService
    {
        private IClassInfoDal _classInfoDal;

        public ClassInfoManager(IClassInfoDal classInfoDal)
        {
            _classInfoDal = classInfoDal;
        }

        public void Add(ClassInfo classInfo)
        {
            _classInfoDal.Add(classInfo);
        }

        public void Delete(int classInfoID)
        {
            _classInfoDal.Delete(new ClassInfo { ClassInfoID = classInfoID });
        }

        public void Update(ClassInfo classInfo)
        {
            _classInfoDal.Update(classInfo);
        }


        public List<ClassInfo> GetAll()
        {
            return _classInfoDal.GetList();
        }

        public List<ClassInfo> GetAll(int classInfoId)
        {
            return _classInfoDal.GetList(cI => cI.ClassInfoID == classInfoId);
        }

        public ClassInfo Get(Expression<Func<ClassInfo, bool>> filter = null)
        {
            return _classInfoDal.Get(filter);
            
        }

    }
}
