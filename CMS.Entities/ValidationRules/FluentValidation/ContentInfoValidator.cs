using CMS.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Entities.ValidationRules.FluentValidation
{
    public class ContentInfoValidator: AbstractValidator<ContentInfo>
    {
        public ContentInfoValidator()
        {
            RuleFor(t => t.Title).NotEmpty().WithMessage("Lütfen içerik başlığını giriniz.");
        }
    }
}
