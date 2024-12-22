using FluentValidation;
using Tabu.DTOs.BannedWords;

namespace Tabu.Validators.BannedWord;

public class BannedWordCreateDtoValidator:AbstractValidator<BannedWordCreateDto>
{
    public BannedWordCreateDtoValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty()
            .NotNull()
            .WithMessage("Söz boş ola bilməz.")
            .MaximumLength(32)
            .WithMessage("Maksimum uzunluq 32dən çox ola bilməz.");

        
    }
}