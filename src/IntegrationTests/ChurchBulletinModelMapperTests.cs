using ChurchBulletin.Core.Model;
using ChurchBulletin.DataAccess.Mappings;
using Shouldly;

namespace ChurchBulletin.IntegrationTests;

public class ChurchBulletinModelMapperTests
{
    [Test]
    public void ShouldMapChurchBulletin()
    {
        var bulletinItem = new BulletinItem
        {
            Date = DateTime.Parse("2022-01-01"),
            Name = "Bible Study",
            Place = "RM 102"
        };

        using (var context = new ChurchBulletinContext())
        {
            context.Add(bulletinItem);
            context.SaveChanges();    
        }

        List<BulletinItem> bulletins;
        
        using (var context1 = new ChurchBulletinContext())
        {
            bulletins = context1.Bulletins.ToList();
        }
        
        bulletins.ShouldNotBeNull();
        bulletins.Count.ShouldBe(1);
        bulletins.ShouldBeAssignableTo<List<BulletinItem>>();
    }
}