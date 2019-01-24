using CMS.Core.DataAccess;
using CMS.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.DataAccess.Abstract
{
    public interface IContentsDal : IEntityRepository<Contents>
    {
    }
}
