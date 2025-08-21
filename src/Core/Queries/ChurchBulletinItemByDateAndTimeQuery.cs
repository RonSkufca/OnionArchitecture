namespace ChurchBulletin.Core.Queries;

public class ChurchBulletinItemByDateAndTimeQuery(DateTime targetDate)
{
    public DateTime TargetDate { get; } = targetDate;
}