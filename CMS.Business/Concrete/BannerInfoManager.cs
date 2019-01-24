using CMS.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using CMS.Entities.Concrete;
using System.Linq.Expressions;
using CMS.DataAccess.Abstract;

namespace CMS.Business.Concrete
{
    public class BannerInfoManager : IBannerInfoService
    {

        public IBannerInfoDal _bannerInfoDal;

        public BannerInfoManager(IBannerInfoDal bannerInfoDal)
        {
            _bannerInfoDal = bannerInfoDal;
        }

        public void Add(BannerInfo banneInfo)
        {
            _bannerInfoDal.Add(banneInfo);
        }

        public void Delete(int bannerInfoID)
        {
            _bannerInfoDal.Delete(new BannerInfo { BannerInfoID = bannerInfoID });
        }

        public BannerInfo Get(Expression<Func<BannerInfo, bool>> filter = null)
        {
            return _bannerInfoDal.Get(filter);
        }

        public List<BannerInfo> GetAll()
        {
            return _bannerInfoDal.GetList();
        }

        public void Update(BannerInfo banners)
        {
            _bannerInfoDal.Update(banners);
        }
    }
}
