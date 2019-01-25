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
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string MetaTitle { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
    }
}
