using System;
using System.Globalization;

namespace Common.Utilities
{
    public class DateTimehelper
    {
        public static string GetPersianDateTime(DateTime dateTime)
        {
            PersianCalendar pc = new PersianCalendar();
            //using (var sha256 = new SHA256CryptoServiceProvider())

            return string.Format("{0}/{1}/{2}  {3}:{4}:{5}", pc.GetYear(dateTime), pc.GetMonth(dateTime), pc.GetDayOfMonth(dateTime), pc.GetHour(dateTime), pc.GetMinute(dateTime), pc.GetSecond(dateTime));

        }
    }
}
