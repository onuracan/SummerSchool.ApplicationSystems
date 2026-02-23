using FluentValidation;
using SummerSchool.ApplicationSystems.Core.DTOs.Student.Request;

namespace SummerSchool.ApplicationSystems.Service.Validators.Student;

public class CreateStudentRequestDtoValidator : AbstractValidator<CreateStudentRequestDto>
{
    public CreateStudentRequestDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("Ad boş olamaz.")
            .MaximumLength(100)
            .WithMessage("Ad en fazla 100 karakter olabilir.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Soyad boş olamaz.")
            .MaximumLength(100)
            .WithMessage("Soyad en fazla 100 karakter olabilir.");

        RuleFor(x => x.IdentityNumber)
            .NotEmpty()
            .WithMessage("Kimlik/Pasaport numarası boş olamaz.")
            .Length(11)
            .WithMessage("Kimlik numarası 11 karakter olmalıdır.")
            .Matches(@"^\d{11}$")
            .WithMessage("Kimlik numarası sadece rakamlardan oluşmalıdır.");

        RuleFor(x => x.SchoolNumber)
            .MaximumLength(50)
            .WithMessage("Okul numarası en fazla 50 karakter olabilir.");

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

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("Telefon numarası boş olamaz.")
            .Matches(@"^(5\d{9}|0\d{10})$")
            .WithMessage("Geçerli bir Türkiye telefon numarası giriniz. (Örn: 5551234567, 05551234567)")
            .MaximumLength(20)
            .WithMessage("Telefon numarası en fazla 20 karakter olabilir.");

        RuleFor(x => x.EMail)
            .NotEmpty()
            .WithMessage("E-Posta adresi boş olamaz.")
            .EmailAddress()
            .WithMessage("Geçerli bir e-posta adresi giriniz.")
            .MaximumLength(200)
            .WithMessage("E-Posta adresi en fazla 200 karakter olabilir.");

        RuleFor(x => x.CountryCode)
            .NotEmpty()
            .WithMessage("Ülke kodu seçilmiş olmalıdır.")
            .MaximumLength(5)
            .WithMessage("Ülke kodu en fazla 5 karakter olmalıdır.")
            .MinimumLength(2)
            .WithMessage("Ülke kodu en az 2 karakter olmalıdır.");
    }
}
