using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class RentalValidator :AbstractValidator<Rental>
    {
        public RentalValidator()
        {
            RuleFor(r => r.RentDate).GreaterThan(new DateTime(2019,1,1));//bunu zaten ben atiyorum oylesine denemek icin koydum ayrica ileride programa gelistirme yaparsak ileri yonelikli kiralama o zaman lazim olacak.
            RuleFor(r => r.ReturnDate).Null(); 

        }
    }
}
