using Microsoft.EntityFrameworkCore;

namespace FishApi.Data;

public class FishContext : DbContext
{
    public FishContext(DbContextOptions<FishContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Aquarium>();
        modelBuilder.Entity<Fish>();
        modelBuilder.Entity<Decoration>();
    }

    public DbSet<Aquarium> Aquariums { get; set; }
    public DbSet<Fish> Fish { get; set; }
    public DbSet<Decoration> Decorations { get; set; }
}
