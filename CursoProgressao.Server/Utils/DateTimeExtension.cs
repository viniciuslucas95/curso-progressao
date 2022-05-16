namespace CursoProgressao.Server.Utils;

public static class DateTimeExtension
{
    public static DateTime GetCurrentUtcTime(this DateTime _)
    {
        TimeSpan offset = DateTimeOffset.Now.Offset;
        return DateTime.SpecifyKind(DateTime.Now - offset, DateTimeKind.Utc);
    }
}
