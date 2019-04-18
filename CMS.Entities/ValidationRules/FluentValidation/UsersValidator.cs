using CMS.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Entities.ValidationRules.FluentValidation
{
    public class UsersValidator: AbstractValidator<Users>
    {
        public UsersValidator()
        {
            RuleFor(t => t.EMail).NotEmpty().WithMessage("Lütfen email adresinizi giriniz. ").EmailAddress().WithMessage("Geçerli bir email adresi giriniz.");
            RuleFor(t => t.Password).NotEmpty().WithMessage("Lütfen şifrenizi giriniz.");
            RuleFor(t => t.UserTypeID).NotEmpty().WithMessage("Lütfen kullanıcı rolü seçiniz.");
        }
    }
}
