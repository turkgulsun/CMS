using CMS.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Entities.ValidationRules.FluentValidation
{
    public class BannerInfoValidator: AbstractValidator<BannerInfo>
    {
        public BannerInfoValidator()
        {
            RuleFor(t => t.Name).NotEmpty().WithMessage("Lütfen banner adını giriniz.");
        }
    }
}
