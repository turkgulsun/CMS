using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMS.UI.Areas.Admin.Models.ContentsVM
{
    public class ContentsListVM
    {
        public int ContentID { get; set; }
        public int ClassID { get; set; }
        public int ContentInfoID { get; set; }
        [Display(Name = "Başlık")]
        public string Title { get; set; }
        [Display(Name = "Özet")]
        public string Summary { get; set; }
        [Display(Name = "Oluşturma Tarihi")]
        public DateTime CreationDate { get; set; }
        [Display(Name = "Yayın")]
        public bool Active { get; set; }
        public string Image { get; set; }
    }
}