using CMS.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using CMS.Entities.Concrete;
using System.Linq.Expressions;
using CMS.DataAccess.Abstract;

namespace CMS.Business.Concrete
{
    public class BannerManager : IBannerService
    {
        public IBannersDal _bannersDal;

        public BannerManager(IBannersDal bannerDal)
        {
            _bannersDal = bannerDal;
        }

        public void Add(Banners banners)
        {
            _bannersDal.Add(banners);
        }

        public void Delete(int bannerID)
        {
            _bannersDal.Delete(new Banners { BannerID = bannerID });
        }

        public Banners Get(Expression<Func<Banners, bool>> filter = null)
        {
            return _bannersDal.Get(filter);
        }

        public List<Banners> GetAll()
        {
           return  _bannersDal.GetList();
        }

        public Banners GetById(int bannerID)
        {
            return _bannersDal.Get(b => b.BannerID == bannerID);

        }

        public void Update(Banners banners)
        {
            _bannersDal.Update(banners);
        }
    }
}
