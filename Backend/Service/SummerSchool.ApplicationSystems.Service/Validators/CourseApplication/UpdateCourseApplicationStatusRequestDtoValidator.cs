using FluentValidation;
using SummerSchool.ApplicationSystems.Core.DTOs.CourseApplication.Request;
using SummerSchool.ApplicationSystems.Core.Enums;

namespace SummerSchool.ApplicationSystems.Service.Validators.CourseApplication;

public class UpdateCourseApplicationStatusRequestDtoValidator : AbstractValidator<UpdateCourseApplicationStatusRequestDto>
{
    public UpdateCourseApplicationStatusRequestDtoValidator()
    {
        RuleFor(x => x.ApplicationStatus)
            .NotEmpty()
            .WithMessage("Geçerli bir başvuru durumu seçiniz. (1: Başvuruldu, 2: Kabul Edildi, 3: Reddedildi)");
    }
}
