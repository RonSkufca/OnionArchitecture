namespace ChurchBulletin.Core.Model;

public class BulletinItem : EntityBase<BulletinItem>
{
    public override Guid Id { get; set; }
    public string? Name { get; init; }
    public string? Place { get; init; }
    public DateTime Date { get; init; }
}