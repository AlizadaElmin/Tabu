using Microsoft.EntityFrameworkCore;
using Tabu.Entities;

namespace Tabu.DAL;

public class TabuDBContext:DbContext
{
    public TabuDBContext(DbContextOptions opt) : base(opt) { }
    
    public DbSet<Language> Languages { get; set; }
    public DbSet<Word> Words { get; set; }
    public DbSet<BannedWord> BannedWords { get; set; }
    public DbSet<Game> Games { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TabuDBContext).Assembly);   
        base.OnModelCreating(modelBuilder);
    }
}