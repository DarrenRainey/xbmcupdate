
using System;

namespace XbmcUpdate.UpdateEngine
{
    public class XbmcVersionInfo
    {
        public int BuildNumber { get; set; }
        public string Suplier { get; set; }
        public DateTime InstallationDate { get; set; }

        public string Age()
        {
            var ts = new TimeSpan(DateTime.Now.Ticks - InstallationDate.Ticks);
            double delta = ts.TotalSeconds;

            const int second = 1;
            const int minute = 60 * second;
            const int hour = 60 * minute;
            const int day = 24 * hour;
            const int month = 30 * day;

            if (delta < 1 * minute)
            {
                return ts.Seconds == 1 ? "One Second ago" : ts.Seconds + " Seconds ago";
            }
            if (delta < 2 * minute)
            {
                return "a Minute ago";
            }
            if (delta < 45 * minute)
            {
                return ts.Minutes + " Minutes ago";
            }
            if (delta < 90 * minute)
            {
                return "an Hour ago";
            }
            if (delta < 24 * hour)
            {
                return ts.Hours + " Hours ago";
            }
            if (delta < 48 * hour)
            {
                return "Yesterday";
            }
            if (delta < 30 * day)
            {
                return ts.Days + " Days ago";
            }
            if (delta < 12 * month)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "One Month ago" : months + " Months ago";
            }
            int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
            return years <= 1 ? "One year ago" : years + " Years ago";
        }
    }
}