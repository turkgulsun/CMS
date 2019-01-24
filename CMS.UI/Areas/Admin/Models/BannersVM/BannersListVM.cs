using System;
using System.ComponentModel.DataAnnotations;


namespace CMS.UI.Areas.Admin.Models.BannersVM
{
    public class BannersListVM
    {
        public int BannerID { get; set; }
        public int BannerInfoID { get; set; }

        [Display(Name = "İsim")]
        public string Name { get; set; }
        public string Banner { get; set; }
        [Display(Name = "Yayın")]
        public bool Active { get; set; }

        [Display(Name = "Oluşturma Tarihi")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Banner Yeri")]
        public string ListType { get; set; }
        public string Description { get; set; }

    }
}