using FluentValidation;
using SummerSchool.ApplicationSystems.Core.DTOs.Course.Request;

namespace SummerSchool.ApplicationSystems.Service.Validators.Course;

public class CreateCourseRequestDtoValidator : AbstractValidator<CreateCourseRequestDto>
{
    public CreateCourseRequestDtoValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .WithMessage("Ders kodu boş olamaz.")
            .MaximumLength(50)
            .WithMessage("Ders kodu en fazla 50 karakter olabilir.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Ders adı boş olamaz.")
            .MaximumLength(200)
            .WithMessage("Ders adı en fazla 200 karakter olabilir.");

        RuleFor(x => x.Department)
            .NotEmpty()
            .WithMessage("Bölüm boş olamaz.")
            .MaximumLength(200)
            .WithMessage("Bölüm en fazla 200 karakter olabilir.");

        RuleFor(x => x.Faculty)
            .NotEmpty()
            .WithMessage("Fakülte boş olamaz.")
            .MaximumLength(200)
            .WithMessage("Fakülte en fazla 200 karakter olabilir.");

        RuleFor(x => x.Quota)
            .GreaterThan(0)
            .WithMessage("Kontenjan 0'dan büyük olmalıdır.");
    }
}
