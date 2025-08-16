using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class ChurchBulletinContext : DbContext
{
    private readonly IDatabaseConfiguration _config;

    public DataContext(IDatabaseConfiguration config)
    {
        _config = config;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseSqlServer(_config.GetConnectionString());

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //new ChurchBulletinMap().Map(modelBuilder);
    }

    public override string ToString()
    {
        return base.ToString() + "-" + GetHashCode();
    }
}