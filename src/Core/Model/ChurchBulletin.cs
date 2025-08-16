namespace ChurchBulletin.Core.Model;

public class Bulletin
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Place { get; set; }
    public DateTime Date { get; set; }
}