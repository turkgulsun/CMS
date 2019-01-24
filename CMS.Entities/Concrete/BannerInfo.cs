using CMS.Core.Entities;
using CMS.Entities.ValidationRules.FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Web;

namespace CMS.Entities.Concrete
{

    [Table("BannerInfo")]
    [FluentValidation.Attributes.Validator(typeof(BannerInfoValidator))]
    public class BannerInfo : IEntity
    {
        public int BannerInfoID { get; set; }
        public int BannerID { get; set; }
        public int LanguageID { get; set; }

        [Display(Name = "Ad")]
        public string Name { get; set; }

        [Display(Name = "Detay")]
        public string Description { get; set; }
        public string Banner { get; set; }
        public string Extension { get; set; }
        public string Url { get; set; }

    }
}
