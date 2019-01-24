using System.Collections.Generic;
using CMS.Entities.Concrete;

namespace CMS.Business.Abstract
{
    public interface IClassTypesService
    {
        List<ClassTypes> GetAll();
        List<ClassTypes> GetAll(int classTypeID);
    }
}