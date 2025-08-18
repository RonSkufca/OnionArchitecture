using ChurchBulletin.Core.Model;
using DataAccess;
using DataAccess.Mappings;
using Shouldly;

namespace IntegrationTests;

public class ChurchBulletinModelMapperTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void ShouldMapChurchBulletin()
    {
        var bulletin = new Bulletin
        {
            Date = DateTime.Parse("2022-01-01"),
            Name = "Bible Study",
            Place = "RM 102"
        };

        using (var context = new ChurchBulletinContext())
        {
            context.Add(bulletin);
            context.SaveChanges();    
        }

        List<Bulletin> bulletins;
        
        using (var context1 = new ChurchBulletinContext())
        {
            bulletins = context1.Bulletins.ToList();
        }
        
        bulletins.ShouldNotBeNull();
        bulletins.Count.ShouldBe(1);
        bulletins.ShouldBeAssignableTo<List<Bulletin>>();
    }
}