using FluentValidation;
using Tabu.DTOs.Words;

namespace Tabu.Validators.Word;

public class WordUpdateDtoValidator:AbstractValidator<WordUpdateDto>
{
    public WordUpdateDtoValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty()
            .NotNull()
            .WithMessage("Söz boş ola bilməz.")
            .MaximumLength(32)
            .WithMessage("Maksimum uzunluq 32dən çox ola bilməz.");
        
        RuleFor(x => x.BannedWords)
            .NotNull();
            
        RuleForEach(x => x.BannedWords)
            .NotNull()
            .WithMessage("Banned words boş ola bilməz.")
            .MaximumLength(32)
            .WithMessage("Maksimum uzunluq 32dən çox ola bilməz.");
        
        // RuleFor(x => x.LanguageCode)
        //     .NotEmpty()
        //     .NotNull()
        //     .WithMessage("Language code boş ola bilməz.")
        //     .MaximumLength(2)
        //     .WithMessage("Maksimum uzunluq 32dən çox ola bilməz.");
    }
}