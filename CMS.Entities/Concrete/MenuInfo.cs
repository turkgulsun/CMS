using CMS.Core.Entities;
using CMS.Entities.ValidationRules.FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CMS.Entities.Concrete
{
    [Table("MenuInfo")]    
    [FluentValidation.Attributes.Validator(typeof(MenuInfoValidator))]
    public class MenuInfo:IEntity
    {
        [Key]
        public int MenuInfoID { get; set; }
        public int MenuID { get; set; }
        public int LanguageID { get; set; }
        [Display(Name = "Menü")]
        public string Menu { get; set; }
        public string Url { get; set; }

    }
}
