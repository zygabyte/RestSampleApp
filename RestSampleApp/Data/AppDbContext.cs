using Microsoft.EntityFrameworkCore;
using RestSampleApp.Entities;

namespace RestSampleApp.Data;

public class AppDbContext : DbContext
{
    private readonly DbContextOptions<AppDbContext> dco;


    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> dco) : base(dco)
    {
        this.dco = dco;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (dco is not null)
        {
            base.OnConfiguring(optionsBuilder);
            return;
        }

        optionsBuilder.UseNpgsql("Host=localhost;Database=RestSampleDB;Username=postgres;Password=postgres");
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Item> Items { get; set; }
}