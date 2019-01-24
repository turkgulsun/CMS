using CMS.Core.DataAccess.EntityFramework;
using CMS.DataAccess.Abstract;
using CMS.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.DataAccess.Concrete.EntityFramework
{
    public class EfContentClassesDal: EfEntityRepositoryBase<ContentClasses,CmsContext>,IContentClassesDal
    {
    }
}
