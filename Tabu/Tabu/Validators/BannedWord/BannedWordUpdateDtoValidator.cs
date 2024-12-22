using FluentValidation;
using Tabu.DTOs.BannedWords;

namespace Tabu.Validators.BannedWord;

public class BannedWordUpdateDtoValidator:AbstractValidator<BannedWordUpdateDto>
{
    public BannedWordUpdateDtoValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty()
            .NotNull()
            .WithMessage("Söz boş ola bilməz.")
            .MaximumLength(32)
            .WithMessage("Maksimum uzunluq 32dən çox ola bilməz.");
        
    }
}