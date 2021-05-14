using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Shared.Utilities.Extensions
{
    public static class DateTimeExtension
    {
        public static string FullDateAndTimeStringWithUnderScore(this DateTime dateTime)
        {
            return $"{dateTime.Millisecond}_{dateTime.Second}_{dateTime.Minute}_{dateTime.Hour}_{dateTime.Day}_{dateTime.Month}_{dateTime.Year}";
        }
    }
}
