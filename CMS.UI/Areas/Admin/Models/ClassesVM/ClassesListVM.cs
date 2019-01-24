using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMS.UI.Areas.Admin.Models.ClassesVM
{
    public class ClassesListVM
    {
        public int ClassID { get; set; }

        public int ClassTypeID { get; set; }

        [Display(Name = "İsim")]
        public string Name { get; set; }

        [StringLength(255)]
        [Display(Name = "Özet")]
        public string Summary { get; set; }
        public string Image { get; set; }

        public DateTime CreationDate { get; set; }

        public bool Active { get; set; }
    }
}