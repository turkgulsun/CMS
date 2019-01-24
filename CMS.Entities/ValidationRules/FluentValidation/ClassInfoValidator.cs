using CMS.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Entities.ValidationRules.FluentValidation
{
    public class ClassInfoValidator : AbstractValidator<ClassInfo>
    {
        public ClassInfoValidator()
        {
            RuleFor(t => t.Name).NotEmpty().WithMessage("Lütfen sınıf adını giriniz.");
        }
    }
}
