using FluentValidation;
using Tabu.DTOs.Languages;

namespace Tabu.Validators.Language;

public class LanguageCreateDtoValidator:AbstractValidator<LanguageCreateDto>
{
    public LanguageCreateDtoValidator()
    {
        RuleFor(x => x.Code)
            .NotNull()
            .NotEmpty()
                .WithMessage("Code boş ola bilməz.")
            .MaximumLength(2)
                .WithMessage("Code uzunluğu 2-dən artıq ola bilməz.");

        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
                .WithMessage("Name boş ola bilməz.")
            .MaximumLength(64)
                .WithMessage("Name uzunluğu 64-dən artıq ola bilməz.");


        RuleFor(x => x.IconUrl)
            .NotNull()
            .NotEmpty()
                .WithMessage("Icon boş ola bilməz.")
            .Matches("^http(s)?://([\\w-]+.)+[\\w-]+(/[\\w- ./?%&=])?$")
                .WithMessage("Icon dəyəri link olmalıdır.")
            .MaximumLength(128)
                .WithMessage("Icon uzunluğu 64-dən artıq ola bilməz.");
    }   
}