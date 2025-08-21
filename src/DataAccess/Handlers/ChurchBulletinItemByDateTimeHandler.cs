using ChurchBulletin.Core.Model;
using ChurchBulletin.Core.Queries;
using ChurchBulletin.DataAccess.Mappings;

namespace ChurchBulletin.DataAccess.Handlers;

public class ChurchBulletinItemByDateTimeHandler
{
    private readonly ChurchBulletinContext _context;

    public ChurchBulletinItemByDateTimeHandler(ChurchBulletinContext context)
    {
        _context = context;
    }
    
    public IEnumerable<BulletinItem> Handle(ChurchBulletinItemByDateAndTimeQuery query)
    {
        var bulletinItems = _context.Bulletins.Where(x => x.Date == query.TargetDate);
        
        return bulletinItems;
    }
}