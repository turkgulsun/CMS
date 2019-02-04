using CMS.Core.Entities;
using CMS.Entities.ValidationRules.FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CMS.Entities.Concrete
{

    [Table(name:"ContentInfo")]
    [FluentValidation.Attributes.Validator(typeof(ContentInfoValidator))] 
    public class ContentInfo : IEntity
    {
        [Key]
        public int ContentInfoID { get; set; }
        public int ContentID { get; set; }
        public int LanguageID { get; set; }
        [Display(Name ="Başlık")]
        public string Title { get; set; }
        [Display(Name = "Özet")]
        public string Summary { get; set; }
        [Display(Name = "Detay")]
        public string Description { get; set; }
        [Display(Name = "Meta Title")]
        public string MetaTitle { get; set; }
        [Display(Name = "Meta Keywords")]
        public string MetaKeywords { get; set; }
        [Display(Name = "Meta Description")]
        public string MetaDescription { get; set; }
    }
}
