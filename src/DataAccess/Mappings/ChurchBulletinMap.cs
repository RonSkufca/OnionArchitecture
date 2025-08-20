using ChurchBulletin.Core.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChurchBulletin.DataAccess.Mappings;

public class ChurchBulletinMap : EntityMapBase<BulletinItem>
{
    protected override void MapMembers(EntityTypeBuilder<BulletinItem> entity)
    {
        entity.Property(e => e.Name);
        entity.Property(e => e.Place);
        entity.Property(e => e.Date);
    }
}