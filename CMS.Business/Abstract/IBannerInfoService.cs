using CMS.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CMS.Business.Abstract
{
    public interface IBannerInfoService
    {
        List<BannerInfo> GetAll();
        BannerInfo Get(Expression<Func<BannerInfo, bool>> filter = null);
        void Add(BannerInfo bannerInfo);
        void Update(BannerInfo bannerInfo);
        void Delete(int bannerInfoID);
    }
}
