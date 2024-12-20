using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tabu.Entities;

namespace Tabu.Configurations;

public class LanguageConfiguration:IEntityTypeConfiguration<Language>
{
    public void Configure(EntityTypeBuilder<Language> builder)
    {
        builder.HasKey(x => x.Code);
        builder.HasIndex(x => x.Name).IsUnique();
        builder.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(2);

        builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(64);
            
        builder.Property(x=>x.Icon)
                .IsRequired()
                .HasMaxLength(128);

        builder.HasData(new Language
            {
                Code = "az",
                Name = "Azərbaycan",
                Icon = "https://cdn-icons-png.flaticon.com/512/630/630657.png"

            },
            new Language
            {
                Code = "en",
                Name = "English",
                Icon = "https://cdn-icons-png.flaticon.com/512/5111/5111640.png"
            });
    }
}