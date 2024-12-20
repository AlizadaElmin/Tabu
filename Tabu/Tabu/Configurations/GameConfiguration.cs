using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tabu.Entities;

namespace Tabu.Configurations;

public class GameConfiguration:IEntityTypeConfiguration<Game>
{
   
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder
            .HasKey(x=>x.Id);
        builder
            .Property(x => x.LanguageCode)
            .HasDefaultValue("az");
        builder
            .HasOne(x => x.Language)
            .WithMany(x => x.Games)
            .HasForeignKey(x => x.LanguageCode);
    }
}