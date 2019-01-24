using CMS.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Entities.ValidationRules.FluentValidation
{
    public class MenusValidator: AbstractValidator<Menus>
    {
        public MenusValidator()
        {            
            RuleFor(t => t.MenuListID).NotEmpty().WithMessage("Lütfen menü yerini seçiniz.");
        }
    }
}
