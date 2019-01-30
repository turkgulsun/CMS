using CMS.Entities.Concrete;
using CMS.UI.Areas.Admin.Models.ListsVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.UI.Areas.Admin.Models.BannersVM
{
    public class BannersVM
    {
        public Banners Banners { get; set; }
        public BannerInfo BannerInfo { get; set; }
        public List<ListsList> ListType { get; set; }

    }
}