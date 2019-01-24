using System;
using System.Collections.Generic;
using System.Text;
using CMS.Core.DataAccess.EntityFramework;
using CMS.DataAccess.Abstract;
using CMS.Entities.Concrete;

namespace CMS.DataAccess.Concrete.EntityFramework
{
   public class EfClassesDal:EfEntityRepositoryBase<Classes,CmsContext>,IClassesDal
    {

    }
}
