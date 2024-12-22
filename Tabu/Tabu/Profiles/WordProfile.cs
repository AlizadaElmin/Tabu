using AutoMapper;
using Tabu.DTOs.Words;
using Tabu.Entities;

namespace Tabu.Profiles;

public class WordProfile:Profile
{
    public WordProfile()
    {
        CreateMap<WordUpdateDto, Word>()
            .ForMember(dest => dest.BannedWords, opt => opt.MapFrom(src => src.BannedWords.Select(bw => new BannedWord { Text = bw }).ToList()));

        CreateMap<WordCreateDto, Word>()
            .ForMember(dest => dest.BannedWords,
                opt => opt.MapFrom(src => src.BannedWords.Select(bw => new BannedWord { Text = bw }).ToList()));
        
        CreateMap<Word, WordGetDto>()
            .ForMember(dest => dest.BannedWords, 
                opt => opt.MapFrom(src => src.BannedWords.Select(bw => bw.Text).ToList())).ReverseMap(); 
    }
}