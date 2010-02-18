
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

            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            if (delta < 1 * MINUTE)
            {
                return ts.Seconds == 1 ? "One Second ago" : ts.Seconds + " Seconds ago";
            }
            if (delta < 2 * MINUTE)
            {
                return "a Minute ago";
            }
            if (delta < 45 * MINUTE)
            {
                return ts.Minutes + " Minutes ago";
            }
            if (delta < 90 * MINUTE)
            {
                return "an Hour ago";
            }
            if (delta < 24 * HOUR)
            {
                return ts.Hours + " Hours ago";
            }
            if (delta < 48 * HOUR)
            {
                return "Yesterday";
            }
            if (delta < 30 * DAY)
            {
                return ts.Days + " Days ago";
            }
            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "One Month ago" : months + " Months ago";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "One year ago" : years + " Years ago";
            }

        }
    }
}