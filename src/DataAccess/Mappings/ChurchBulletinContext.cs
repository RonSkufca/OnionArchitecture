using Microsoft.EntityFrameworkCore;

namespace DataAccess.Mappings;

public class ChurchBulletinContext : DbContext
{
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