using System.Globalization;

namespace MongoDB.Microservice.Blog.Services
{

    public static class PersainDateTime
    {
        public static string MonthNamePersian(this DateTime value)
        {
            PersianCalendar pc = new PersianCalendar();
            return PersianDateTimeEnums.MountOfYearPersian.FirstOrDefault(m => m.Key == pc.GetMonth(value)).Value;

        }
        public static string DayOfWeekNamePersian(this DateTime value)
        {
            PersianCalendar pc = new PersianCalendar();
            return PersianDateTimeEnums.WeekDays.FirstOrDefault(w => w.Key == (Int16)pc.GetDayOfWeek(value)).Value;
        }
        public static string DayOfMonthPersian(this DateTime value)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetDayOfMonth(value).ToString(); ;
        }
        public static string DateTimePersian(this DateTime value)
        {
            PersianCalendar pc = new PersianCalendar();
            DateTime thisDate = value;
            return pc.GetYear(thisDate).ToString() + "/" + pc.GetMonth(thisDate).ToString() + "/" + pc.GetDayOfMonth(thisDate).ToString() + "  " + pc.GetHour(thisDate).ToString() + ":" + pc.GetMinute(thisDate).ToString() + ":" + pc.GetSecond(thisDate).ToString();
        }

        public static string DateTimePersian(this DateTime value, string format)
        {
            PersianCalendar pc = new PersianCalendar();
            DateTime thisDate = value;
            switch (format)
            {
                case "HH:mm Y/M/D": return pc.GetHour(thisDate).ToString() + ":" + pc.GetMinute(thisDate).ToString() + "  " + pc.GetYear(thisDate).ToString() + "/" + pc.GetMonth(thisDate).ToString() + "/" + pc.GetDayOfMonth(thisDate).ToString();
            }

            return pc.GetYear(thisDate).ToString() + "/" + pc.GetMonth(thisDate).ToString() + "/" + pc.GetDayOfMonth(thisDate).ToString() + "  " + pc.GetHour(thisDate).ToString() + ":" + pc.GetMinute(thisDate).ToString() + ":" + pc.GetSecond(thisDate).ToString();
        }


        public static string DatePersian(this DateTime value)
        {
            PersianCalendar pc = new PersianCalendar();
            DateTime thisDate = value;
            return pc.GetYear(thisDate).ToString() + "/" + pc.GetMonth(thisDate).ToString("00") + "/" + pc.GetDayOfMonth(thisDate).ToString("00");// + "  " + pc.GetHour(thisDate).ToString() + ":" + pc.GetMinute(thisDate).ToString() + ":" + pc.GetSecond(thisDate).ToString();
        }
        public static string YearPersian(this DateTime value)
        {
            PersianCalendar pc = new PersianCalendar();
            DateTime thisDate = value;
            return pc.GetYear(thisDate).ToString();
        }
        public static string MounthPersian(this DateTime value)
        {
            PersianCalendar pc = new PersianCalendar();
            DateTime thisDate = value;
            return pc.GetMonth(thisDate).ToString();
        }

        public static string DateInDeatilPersian(this DateTime value)
        {
            return value.DayOfWeekNamePersian() + " " + value.DayOfMonthPersian() + " " + value.MonthNamePersian() + " " + value.YearPersian();
        }

        public static string DateInDeatilWithTimePersian(this DateTime value)
        {
            return value.DayOfWeekNamePersian() + " " + value.DayOfMonthPersian() + " " + value.MonthNamePersian() + " " + value.YearPersian() + " ساعت " + value.Hour + ":" + value.Minute + ":" + value.Second;
        }


        public static DateTime DatePersianToDateTimeMiladi(this string value)
        {
            string[] seprationDateAndTime = value.Split(' ');
            string[] d = seprationDateAndTime[0].Split('/');
            string[] t = new string[3] { "0", "0", "0" };
            if (seprationDateAndTime.Count() == 2)
            {
                var ti = seprationDateAndTime[1].Split(':');
                t[0] = ti[0];
                t[1] = ti[1];
                if (ti.Count() == 3)
                {
                    t[2] = ti[2];
                }
                return new DateTime(int.Parse(d[0]), int.Parse(d[1]), int.Parse(d[2]), int.Parse(t[0]), int.Parse(t[1]), int.Parse(t[2]), new System.Globalization.PersianCalendar());
            }
            else
            {
                return new DateTime(int.Parse(d[0]), int.Parse(d[1]), int.Parse(d[2]), new System.Globalization.PersianCalendar());
            }
        }

        public static DateTime DatePersianToDateTimeMiladi(int year, int mounth, int day)
        {
            return new DateTime(year, mounth, day, new System.Globalization.PersianCalendar());
        }

        public static DateTime DatePersianToDateTimeMiladi(int year, int mounth, int day, int hour, int minute, int secound)
        {
            return new DateTime(year, mounth, day, hour, minute, secound, new System.Globalization.PersianCalendar());
        }


        public static byte Age(this DateTime value)
        {
            return (byte)((DateTime.Now - value).TotalDays / 365.242199);
        }


    }

    public class PersianDateTimeEnums
    {
        public static Dictionary<int, string> WeekDays { get; } = new Dictionary<int, string>
        {
            {0, "یکشنبه" },
            {1, "دوشنبه" },
            {2, "سه شنبه" },
            {3, "چهار شنبه" },
            {4, "پنج شنبه" },
            {5, "جمعه" },
            {6, "شنبه" }
        };

        public static Dictionary<int, string> MountOfYearPersian { get; } = new Dictionary<int, string>
        {
            {1, "فروردین" },
            {2, "اردیبهشت" },
            {3, "خرداد" },
            {4, "تیر" },
            {5, "مرداد" },
            {6, "شهریور" },
            {7, "مهر" },
            {8, "آبان" },
            {9, "آذر" },
            {10, "دی" },
            {11, "بهمن" },
            {12, "اسفند" }
        };
    }
}
