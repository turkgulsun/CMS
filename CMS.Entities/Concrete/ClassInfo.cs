using CMS.Core.Entities;
using CMS.Entities.ValidationRules.FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CMS.Entities.Concrete
{
    [Table("ClassInfo")]
    [FluentValidation.Attributes.Validator(typeof(ClassInfoValidator))]
    public class ClassInfo : IEntity
    {

        [Key]
        public int ClassInfoID { get; set; }
        public int ClassID { get; set; }
        public int LanguageID { get; set; }

        [Display(Name = "Sınıf Adı")]
        public string Name { get; set; }

        [Display(Name = "Özet")]
        public string Summary { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "Detay")]
        public string Description { get; set; }
        public string MetaTitle { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
    }
}
