using Microsoft.EntityFrameworkCore;

namespace ChurchBulletin.DataAccess.Mappings;

public interface IEntityFrameworkMapping
{
    void Map(ModelBuilder modelBuilder);
}