using CMS.Core.DataAccess.EntityFramework;
using CMS.DataAccess.Abstract;
using CMS.Entities.Concrete;


namespace CMS.DataAccess.Concrete.EntityFramework
{
    public class EfListInfoDal: EfEntityRepositoryBase<ListInfo,CmsContext>,IListInfoDal
    {
    }
}
