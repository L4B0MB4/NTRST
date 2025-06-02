namespace NTRST.Models.Extensions;

public static class DateTimeExtensions
{
    public static long ToUnixTimestampMicroseconds(this DateTime dateTime)
    {
        DateTimeOffset dto = dateTime.ToUniversalTime();
        TimeSpan timeSinceEpoch = dto - DateTimeOffset.UnixEpoch;
        long milliseconds = (long)timeSinceEpoch.TotalMilliseconds;
        return milliseconds;
    }
}