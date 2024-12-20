using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tabu.Entities;

namespace Tabu.Configurations;

public class WordConfiguration:IEntityTypeConfiguration<Word>
{
    public void Configure(EntityTypeBuilder<Word> builder)
    {
       builder.HasKey(x=>x.Id);
       builder
           .Property(x => x.Text)
           .HasMaxLength(32);
       builder
           .HasOne(x => x.Language)
           .WithMany(x => x.Words)
           .HasForeignKey(x => x.LanguageCode);
       builder
           .HasMany(x=>x.BannedWords)
           .WithOne(x => x.Word)
           .HasForeignKey(x => x.WordId);
       
    }
}