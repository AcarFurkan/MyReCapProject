using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class ColorValidator :AbstractValidator<CarColor>
    {
        public ColorValidator()
        {
            RuleFor(c => c.ColorName).MinimumLength(2);
        }
    }
}
