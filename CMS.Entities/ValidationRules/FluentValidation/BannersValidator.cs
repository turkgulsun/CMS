using CMS.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Entities.ValidationRules.FluentValidation
{
    public class BannersValidator : AbstractValidator<Banners>
    {
        public BannersValidator()
        {
            RuleFor(t => t.BannerListID).NotEmpty().WithMessage("Lütfen banner yeri seçiniz.");
        }
    }
}
