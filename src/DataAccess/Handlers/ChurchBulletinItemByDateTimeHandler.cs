using ChurchBulletin.Core.Model;
using ChurchBulletin.Core.Queries;
using ChurchBulletin.DataAccess.Mappings;

namespace ChurchBulletin.DataAccess.Handlers;

public class ChurchBulletinItemByDateTimeHandler(ChurchBulletinContext context)
{
    public IEnumerable<BulletinItem> Handle(ChurchBulletinItemByDateAndTimeQuery query)
    {
        var bulletinItems = context.Bulletins.Where(x => x.Date == query.TargetDate);
        
        return bulletinItems;
    }
}