using FluentValidation;
using SummerSchool.ApplicationSystems.Core.DTOs.CourseApplication.Request;

namespace SummerSchool.ApplicationSystems.Service.Validators.CourseApplication;

public class CreateCourseApplicationRequestDtoValidator : AbstractValidator<CreateCourseApplicationRequestDto>
{
    public CreateCourseApplicationRequestDtoValidator()
    {
        RuleFor(x => x.CourseId)
            .NotEmpty()
            .WithMessage("Ders ID boş olamaz.")
            .NotEqual(Guid.Empty)
            .WithMessage("Geçerli bir ders ID giriniz.");
       
    }
}
