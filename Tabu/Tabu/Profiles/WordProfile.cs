using AutoMapper;
using Tabu.DTOs.Words;
using Tabu.Entities;

namespace Tabu.Profiles;

public class WordProfile:Profile
{
    public WordProfile()
    {
        CreateMap<WordCreateDto, Word>();

        CreateMap<WordGetDto, Word>().ReverseMap();

        CreateMap<WordUpdateDto, Word>();
    }

}