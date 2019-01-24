using CMS.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CMS.Business.Abstract
{
    public interface IBannerService
    {

        List<Banners> GetAll();
        Banners Get(Expression<Func<Banners, bool>> filter = null);
        Banners GetById(int bannerID);
        void Add(Banners banners);
        void Update(Banners banners);
        void Delete(int bannerID);

    }
}
