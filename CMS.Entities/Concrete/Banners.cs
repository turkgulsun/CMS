using CMS.Core.Entities;
using CMS.Entities.ValidationRules.FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CMS.Entities.Concrete
{
    [Table("Banners")]
    [FluentValidation.Attributes.Validator(typeof(BannersValidator))]
    public class Banners : IEntity
    {
        [Key]
        public int BannerID { get; set; }
        public int BannerListID { get; set; }
        public string Target { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }

        [Display(Name = "Sıra")]
        public int Sort { get; set; }

        [Display(Name = "Yayınla")]
        public bool Active { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
