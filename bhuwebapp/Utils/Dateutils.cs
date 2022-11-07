using System;

namespace bhuwebapp.Utils
{
    public static class Dateutils
    {
        public static string DateAsString(DateTime date)
        {
            return date.ToString();
        }

        public static long DateAsTimestamp(DateTime date)
        {
            return (date.Ticks - 621355968000000000) / 10000000;
        }
    }
}