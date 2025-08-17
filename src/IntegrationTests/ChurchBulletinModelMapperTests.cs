using ChurchBulletin.Core.Model;
using DataAccess;
using DataAccess.Mappings;

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

        var context = new ChurchBulletinContext();
        context.Add(bulletin);
        context.SaveChanges();
        
        Assert.Pass();
    }
}