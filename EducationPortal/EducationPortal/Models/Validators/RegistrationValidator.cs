using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortal.Models.Validators
{
    class RegistrationValidator : AbstractValidator<RegistrationModel>
    {
        public RegistrationValidator()
        {
            RuleFor(i => i.Email)
                .MinimumLength(10).WithMessage("Email too short")
                .MaximumLength(30).WithMessage("Email too long")
                .EmailAddress().WithMessage("It is not email");
            RuleFor(i => i.Password)
                .MinimumLength(5).WithMessage("Password is too short")
                .Equal(i => i.ConfirmPassword).WithMessage("Dont match");
            RuleFor(i => i.Name)
                .MinimumLength(5).WithMessage("Name is too short")
                .MaximumLength(15).WithMessage("Too long");
        }
    }
}
