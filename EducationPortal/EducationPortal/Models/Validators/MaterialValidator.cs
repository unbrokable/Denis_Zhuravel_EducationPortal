using EducationPortal.Models.MaterialsViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortal.Models.Validators
{

    class MaterialValidator : AbstractValidator<MaterialViewModel>
    {
        public MaterialValidator()
        {
            RuleFor(i => i.Location).NotEmpty().WithMessage("Invalid location");
            RuleFor(i => i.Name).MinimumLength(5).WithMessage( "Name is too short");
        }
    }

    class ArticleMaterialValidator : AbstractValidator<ArticleMaterialViewModel>
    {
        public ArticleMaterialValidator()
        {
            Include(new MaterialValidator());
            RuleFor(i => i.DateOfPublished).NotNull().WithMessage("Invalid date");
        }
    }

    class VideoMaterialValidator : AbstractValidator<VideoMaterialViewModel>
    {
        public VideoMaterialValidator()
        {
            Include(new MaterialValidator());
            RuleFor(i => i.Length).NotNull().GreaterThan(new TimeSpan(0)).WithMessage("Invalid Length");
            RuleFor(i => i.Resolution).NotNull().WithMessage("Invalid Length");
        }
    }

    class BookMaterialValidator : AbstractValidator<BookMaterialViewModel>
    {
        public BookMaterialValidator()
        {
            Include(new MaterialValidator());
            RuleFor(i => i.DateOfPublished).NotNull().WithMessage("Invalid date");
            RuleFor(i => i.Author).MinimumLength(5).WithMessage("Invalid Author");
            RuleFor(i => i.Format).MinimumLength(3).WithMessage("Invalid Format");
        }
    }
}
