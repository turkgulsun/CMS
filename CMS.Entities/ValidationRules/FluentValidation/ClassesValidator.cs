using CMS.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Entities.ValidationRules.FluentValidation
{
    public class ClassesValidator : AbstractValidator<Classes>
    {
        
        public ClassesValidator()
        {
            RuleFor(t => t.Sort).NotEmpty();
        }
    }
}
