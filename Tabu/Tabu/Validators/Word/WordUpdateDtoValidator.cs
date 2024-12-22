using FluentValidation;
using Tabu.DTOs.Words;
using Tabu.Enums;

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
            .NotNull()
            .Must(x => x.Count == (int)GameLevel.Hard)
            .WithMessage((int)GameLevel.Hard+" ədəd unikal qadağan olunmuş söz yazmalısınız.");


        RuleForEach(x => x.BannedWords)
            .NotNull()
            .WithMessage("Banned words boş ola bilməz.")
            .MaximumLength(32)
            .WithMessage("Maksimum uzunluq 32dən çox ola bilməz.");
        
    }
}