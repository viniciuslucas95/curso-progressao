namespace CursoProgressao.Shared.Utils;

public static class DateTimeExtension
{
    public static DateTime GetCurrentUtcTime(this DateTime _)
    {
        TimeSpan offset = DateTimeOffset.Now.Offset;
        return DateTime.SpecifyKind(DateTime.Now - offset, DateTimeKind.Utc);
    }

    public static DateTime GetUtcTime(this DateTime datetime)
    {
        TimeSpan offset = new DateTimeOffset(datetime).Offset;
        return DateTime.SpecifyKind(DateTime.Now - offset, DateTimeKind.Utc);
    }
}
