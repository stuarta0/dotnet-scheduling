using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduling
{
    public class YearlySchedule : Schedule
    {
        //public override IList<DateTime> GetOccurences(DateTime start, DateTime end)
        public override IEnumerable<DateTime> GetOccurrences(DateTime start, DateTime from)
        {
            if (from <= start)
                from = start;
            else
            {
                // the requested from is within the schedule, so calculate the corresponding from based on 
                // how far through two of the scheduled dates we are

                // if the parameter from time is after the schedule from time, perform calculations starting tomorrow
                if (from.TimeOfDay > start.TimeOfDay)
                    from = from.AddDays(1);

                // now wipe out time component as we will use the schedule's start time of day and we don't want
                // the time of day during our following calculations
                from = from.Date; 

                int rollover = (from.Year - start.Year) % Frequency;
                if (rollover > 0)
                    from = new DateTime(from.Year + (Frequency - rollover), start.Month, start.Day);
                else if (from.Year == start.Year)
                {
                    // if same year, determine if before or after start, if after start, then move the requested
                    // from forward to the next occurrence
                    if (from <= start)
                        from = start;
                    else
                        from = start.AddYears(Frequency);
                }
                else
                {
                    from = new DateTime(from.Year, start.Month, start.Day);
                }

                from = from.Date.Add(start.TimeOfDay);
            }

            // add dates to the list until we reach the requested end
            DateTime cur = from;
            while (true)
            {
                yield return cur;
                cur = cur.AddYears(Frequency);
            }
        }
    }
}
