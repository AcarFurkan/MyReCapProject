using Core.Utilities.MyValidators;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator :AbstractValidator<User>
    {
        public UserValidator()
        {
            //RuleFor(u => u.Email).EmailAddress(); // bu sadece @ varligini kontrol ediyor. bunun icin asagida bunu regex ile kontrol ediyorum.
            RuleFor(u => u.Email).Must(MyValidators.EmailControl).WithMessage("Lutfen gecerli bir email adresi giriniz.");//bunu mesajlara aktar.
            RuleFor(u => u.FirstName).MinimumLength(2);
            RuleFor(u => u.LastName).MinimumLength(2);
            RuleFor(u => u.Password).Must(MyValidators.PasswordControl).WithMessage("en az bir rakam ve en az bir harf icermelidir ve en az 8 karakter olmadir. ");//bunu mesajlara aktar.

        }
    }
}
//*****YUKARIDAKI  MyValidators.EmailControl VE  MyValidators.PasswordControl ASLINDAKI ASSAGIDA KI GIBI REGEXLER ILE CONTROL EDILIYOR AMA BUNLARI CORE KATMANINA TASIDIM  CORE-->UTILITIES-->MYVALIDATOR-->MYVALIDATOR.CS  ***/////////
/*
 
        private bool MyPasswordValidation(string arg) 
        {
            bool isPassword = Regex.IsMatch(arg, @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$"); 
            return isPassword;
        }

        private bool MyEmailValidation(string arg)
        {
            bool isEmail = Regex.IsMatch(arg, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            return isEmail;
        }
 */
