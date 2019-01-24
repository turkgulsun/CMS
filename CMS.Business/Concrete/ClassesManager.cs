using System;
using System.Collections.Generic;
using System.Text;
using CMS.Business.Abstract;
using CMS.DataAccess.Abstract;
using CMS.Entities.Concrete;

namespace CMS.Business.Concrete
{
    public class ClassesManager : IClassesService
    {
        public IClassesDal _classesDal;

        public ClassesManager(IClassesDal classesDal)
        {
            _classesDal = classesDal;
        }

        public void Add(Classes classes)
        {
            _classesDal.Add(classes);
        }

        public void Delete(int classId)
        {
            _classesDal.Delete(new Classes { ClassID = classId });
        }

    public List<Classes> GetAll()
    {
        return _classesDal.GetList();
    }

    public Classes GetById(int classId)
    {
        return _classesDal.Get(c => c.ClassID == classId);
    }

    public void Update(Classes classes)
    {
         _classesDal.Update(classes);
    }
}
}
