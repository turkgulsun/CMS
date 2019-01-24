using System.Collections.Generic;
using CMS.Entities.Concrete;

namespace CMS.Business.Abstract
{
    public interface IClassTypeInfoService
    {
        List<ClassTypeInfo> GetAll();
        List<ClassTypeInfo> GetAll(int classTypeInfoID);

        
    }
}