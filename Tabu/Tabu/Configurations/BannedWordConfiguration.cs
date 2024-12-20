using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tabu.Entities;

namespace Tabu.Configurations;

public class BannedWordConfiguration:IEntityTypeConfiguration<BannedWord>
{
 
    public void Configure(EntityTypeBuilder<BannedWord> builder)
    {
        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Text)
            .HasMaxLength(32);
        builder
            .HasOne(x=>x.Word)
            .WithMany(x=>x.BannedWords)
            .HasForeignKey(x=>x.WordId);
    }
}