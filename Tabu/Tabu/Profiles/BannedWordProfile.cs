using AutoMapper;
using Tabu.DTOs.BannedWords;
using Tabu.Entities;

namespace Tabu.Profiles;

public class BannedWordProfile:Profile
{
    public BannedWordProfile()
    {
        CreateMap<BannedWordCreateDto, BannedWord>();

        CreateMap<BannedWordGetDto, BannedWord>().ReverseMap();

        CreateMap<BannedWordUpdateDto, BannedWord>();
    }
 
}