using ChurchBulletin.Core.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappings;

public class ChurchBulletinMap : EntityMapBase<Bulletin>
{
    protected override void MapMembers(EntityTypeBuilder<Bulletin> entity)
    {
        entity.Property(e => e.Name);
        entity.Property(e => e.Place);
        entity.Property(e => e.Date);
    }
}