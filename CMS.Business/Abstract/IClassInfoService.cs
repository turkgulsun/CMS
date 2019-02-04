using System.Collections.Generic;
using CMS.Entities.Concrete;
using System.Linq.Expressions;
using System;

namespace CMS.Business.Abstract
{
    public interface IClassInfoService
    {
        List<ClassInfo> GetAll();
        List<ClassInfo> GetAll(int classInfoId);

        ClassInfo Get(Expression<Func<ClassInfo, bool>> filter = null);

        void Add(ClassInfo classInfo);
        void Update(ClassInfo classInfo);
        void Delete(int classInfoId);
    }
}