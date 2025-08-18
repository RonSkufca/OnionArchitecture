using ChurchBulletin.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Mappings;

public class ChurchBulletinContext : DbContext
{
    public DbSet<Bulletin> Bulletins => Set<Bulletin>();
    
   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseInMemoryDatabase("ChurchBulletin");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new ChurchBulletinMap().Map(modelBuilder);
    }

    public override string ToString()
    {
        return base.ToString() + "-" + GetHashCode();
    }
}