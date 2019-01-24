using CMS.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Entities.ValidationRules.FluentValidation
{
    public class MenuInfoValidator : AbstractValidator<MenuInfo>
    {
        public MenuInfoValidator()
        {
            RuleFor(t => t.Menu).NotEmpty().WithMessage("Lütfen menü adını giriniz.");
        }
    }
}
