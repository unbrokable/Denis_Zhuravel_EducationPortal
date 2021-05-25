using Application.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortal.Models.Validators
{
    class CourseValidator : AbstractValidator<CourseDTO>
    {
        public CourseValidator()
        {
            RuleFor(i => i.Name).MinimumLength(5).WithMessage("Name is too short");
            RuleFor(i => i.Description).MinimumLength(5).WithMessage("Description is too short");
        }
    }
}
