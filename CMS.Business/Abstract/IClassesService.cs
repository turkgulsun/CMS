using System;
using System.Collections.Generic;
using System.Text;
using CMS.Entities.Concrete;

namespace CMS.Business.Abstract
{
   public interface IClassesService
   {
       List<Classes> GetAll();
       void Add(Classes classes);
       void Update(Classes classes);
       void Delete(int classId);
        Classes GetById(int classId);
    }
}
