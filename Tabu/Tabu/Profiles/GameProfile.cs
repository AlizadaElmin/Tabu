using AutoMapper;
using Tabu.DTOs.Games;
using Tabu.Entities;

namespace Tabu.Profiles;

public class GameProfile:Profile
{
    public GameProfile()
    {
        CreateMap<GameCreateDto, Game>()
            .ForMember(x => x.Time, opt => opt.MapFrom(y => new TimeSpan(10000000 * y.Seconds)))
            .ForMember(x => x.BannedWordCount, y => y.MapFrom(z => (int)z.GameLevel));
    }
}
