using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduling
{
    public class YearlySchedule : Schedule
    {
        public override string Description
        {
            get
            {
                // Annually on April 12
                // Every 2 years on April 12
                return String.Concat(
                    String.Format(Strings.Plural(Frequency, "Annually on {1}", "Every {0} years on {1}"), Frequency, StartDate.ToString("MMMM d")),
                    (EndDate.HasValue ? String.Format(", ending {0}", EndDate.Value.ToString("d MMMM yyyy")) : ""));
            }
        }

        public YearlySchedule()
            : base()
        {
            Period = Period.Year;
        }

        public override IList<DateTime> GetOccurences(DateTime start, DateTime end)
        {
            List<DateTime> list = new List<DateTime>();

            // check bounds to end immediately if necessary
            if (end < StartDate || (EndDate.HasValue && start > EndDate))
                return list;
            else if (start <= StartDate)
                // modify start if the requested start is equal to or before the scheduled start
                start = StartDate;
            else
            {
                // the requested start is within the schedule, so calculate the corresponding start based on 
                // how far through two of the scheduled dates we are
                int rollover = (start.Year - StartDate.Year) % Frequency;
                if (rollover > 0)
                    start = new DateTime(start.Year + (Frequency - rollover), StartDate.Month, StartDate.Day);
                else if (start.Year == StartDate.Year)
                {
                    // if same year, determine if before or after StartDate, if after StartDate, then move the requested
                    // start forward to the next occurrence
                    if (start <= StartDate)
                        start = StartDate;
                    else
                        start = StartDate.AddYears(Frequency);
                }
                else
                {
                    start = new DateTime(start.Year, StartDate.Month, StartDate.Day);
                }
            }

            // limit the end to either the parameter value or when this schedule ends
            if (EndDate.HasValue && EndDate < end)
                end = EndDate.Value;

            // add dates to the list until we reach the requested end
            DateTime cur = start;
            while (cur <= end)
            {
                list.Add(cur);
                cur = cur.AddYears(Frequency);
            }

            return list;
        }
    }
}
