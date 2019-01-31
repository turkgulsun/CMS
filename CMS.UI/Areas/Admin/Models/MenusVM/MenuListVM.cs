using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMS.UI.Areas.Admin.Models.MenusVM
{
    public class MenuListVM
    {
        public int MenuID { get; set; }
        public int MenuInfoID { get; set; }
        public int MenuListID { get; set; }
        [Display(Name = "Menü")]
        public string Menu { get; set; }
        public string MenuYeri { get; set; }
        [Display(Name = "Yayın")]
        public bool Active { get; set; }
        [Display(Name = "Oluşturma Tarihi")]
        public DateTime CreationDate { get; set; }
        public string Image { get; set; }
    }
}