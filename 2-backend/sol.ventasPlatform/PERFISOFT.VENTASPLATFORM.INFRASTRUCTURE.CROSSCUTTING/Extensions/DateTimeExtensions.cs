namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.CROSSCUTTING.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime ConvertDateTimeClient(this DateTime date, string destinationTimeZoneId)
        {
            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(date, destinationTimeZoneId);
        }
    }
}
