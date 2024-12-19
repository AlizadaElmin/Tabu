using Microsoft.EntityFrameworkCore;
using Tabu.Entities;

namespace Tabu.DAL;

public class TabuDBContext:DbContext
{
    public TabuDBContext(DbContextOptions opt) : base(opt) { }
    
    public DbSet<Language> Languages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Language>(model =>
        {
            model.HasKey(x => x.Code);
            model.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(2);

            model.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(64);
            
            model.Property(x=>x.Icon)
                .IsRequired()
                .HasMaxLength(128);
        });
        base.OnModelCreating(modelBuilder);
    }
}