using ChurchBulletin.Core;
using ChurchBulletin.Core.Model;
using Shouldly;

namespace ChurchBulletin.UnitTests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var bulletin = new BulletinItem();

        bulletin.ShouldNotBeNull();
    }
}