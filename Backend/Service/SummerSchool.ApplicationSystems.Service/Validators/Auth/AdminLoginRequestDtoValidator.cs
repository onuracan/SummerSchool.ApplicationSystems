using FluentValidation;
using SummerSchool.ApplicationSystems.Core.DTOs.Auth.Request;

namespace SummerSchool.ApplicationSystems.Service.Validators.Auth;

public class AdminLoginRequestDtoValidator : AbstractValidator<AdminLoginRequestDto>
{
    public AdminLoginRequestDtoValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage("Kullanıcı adı boş olamaz.")
            .NotNull()
            .WithMessage("Kullanıcı adı zorunludur.")
            .MinimumLength(5)
            .WithMessage("Kullanıcı adı en az 5 karakter olmalıdır.")
            .MaximumLength(10)
            .WithMessage("Kullanıcı adı en fazla 10 karakter olmalıdır.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Şifre boş olamaz.")
            .NotNull()
            .WithMessage("Şifre zorunludur.")
            .MinimumLength(10)
            .WithMessage("Şifre en az 10 karakter olmalıdır.")
            .MaximumLength(20)
            .WithMessage("Şifre en fazla 20 karakter olmalıdır.");
    }
}
