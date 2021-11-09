using System;
using System.Collections.Generic;
using System.Text;

namespace PF.Mobile.App.Models
{
    public class TimeDifferenceToTimeIntervalConverter
    {
        private DateTime referenceTime;

        public TimeDifferenceToTimeIntervalConverter(DateTime referenceTime)
        {
            this.referenceTime = referenceTime;
        }

        public string ToTimeSinceString(DateTime value)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            TimeSpan ts = new TimeSpan(referenceTime.Ticks - value.Ticks);
            double seconds = ts.TotalSeconds;

            // Less than one minute
            //if (seconds < 1 * MINUTE)
            //    return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";

            //if (seconds < 60 * MINUTE)
            //    return ts.Minutes + " minutes ago";

            //if (seconds < 120 * MINUTE)
            //    return "an hour ago";

            if (seconds < 24 * HOUR)
                return "today";

            if (seconds < 48 * HOUR)
                return "yesterday";

            if (seconds < 30 * DAY)
                return "this month";

            if (seconds < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "one month ago" : months + " months ago";
            }

            int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
            return years <= 1 ? "one year ago" : years + " years ago";
        }
    }
}
