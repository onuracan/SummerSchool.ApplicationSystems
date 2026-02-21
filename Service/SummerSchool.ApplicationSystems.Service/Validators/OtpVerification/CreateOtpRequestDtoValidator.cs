using FluentValidation;
using SummerSchool.ApplicationSystems.Core.DTOs.OtpVerification.Request;

namespace SummerSchool.ApplicationSystems.Service.Validators.OtpVerification;

public class CreateOtpRequestDtoValidator : AbstractValidator<CreateOtpRequestDto>
{
    public CreateOtpRequestDtoValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("Telefon numarası boş olamaz.")
            .NotNull()
            .WithMessage("Telefon numarası zorunludur.")
            .Matches(@"^(5\d{9}|0\d{10}|0\d{3}\s?\d{3}\s?\d{2}\s?\d{2})$")
            .WithMessage("Geçerli bir Türkiye telefon numarası giriniz. (Örn: 5551234567, 05551234567)")
            .MinimumLength(10)
            .WithMessage("Telefon numarası en az 10 karakter olmalıdır.")
            .MaximumLength(11)
            .WithMessage("Telefon numarası en fazla 11 karakter olmalıdır.");
    }
}
