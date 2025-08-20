using ChurchBulletin.Core.Queries;
using ChurchBulletin.DataAccess.Handlers;

namespace ChurchBulletin.IntegrationTests;

public class ChurchBulletinItemByDateAndTimeQueryTests
{
    [Test]
    public void ShouldGetWithinDate()
    {
        var dateTime = new DateTime(2022, 1, 1);
        var query = new ChurchBulletinItemByDateAndTimeQuery(dateTime);

        var handler = new ChurchBulletinItemByDateTimeHandler();
        handler.Handle(query);
    }
}