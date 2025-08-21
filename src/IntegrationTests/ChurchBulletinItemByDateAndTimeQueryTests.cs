using ChurchBulletin.Core.Model;
using ChurchBulletin.Core.Queries;
using ChurchBulletin.DataAccess.Handlers;
using ChurchBulletin.DataAccess.Mappings;
using Shouldly;

namespace ChurchBulletin.IntegrationTests;

public class ChurchBulletinItemByDateAndTimeQueryTests
{
    [Test]
    public void ShouldGetWithinDate()
    {
        // Create test data
        var item1 = new BulletinItem() { Date = new DateTime(2000, 1, 1, 12, 0, 0) };
        var item2 = new BulletinItem() { Date = new DateTime(2001, 1, 1, 12, 0, 0) };
        var item3 = new BulletinItem() { Date = new DateTime(2002, 1, 1, 12, 0, 0) };
        var item4 = new BulletinItem() { Date = new DateTime(2000, 1, 1, 12, 0, 0) };

        // Add them to the database
        using (var context = new ChurchBulletinContext())
        {
            context.AddRange(item1, item2, item3, item4);
            context.SaveChanges();
        }
        
        // Arrange
        var dateTime = new DateTime(2022, 1, 1);
        var query = new ChurchBulletinItemByDateAndTimeQuery(dateTime);
        var handler = new ChurchBulletinItemByDateTimeHandler();
        
        // Act
        IEnumerable<BulletinItem> items = handler.Handle(query);
        
        // Assert
        items.Count().ShouldBe(2);
    }
}