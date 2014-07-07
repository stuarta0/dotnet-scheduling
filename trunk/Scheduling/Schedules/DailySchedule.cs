using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduling
{
    public class DailySchedule : Schedule
    {
        public override IEnumerable<DateTime> GetOccurrences(DateTime start, DateTime from)
        {
            // limit from to start
            if (from <= start)
                from = start;
            else
            {
                // if the parameter from time is after the schedule from time, perform calculations starting tomorrow (without time component)
                if (from.TimeOfDay > start.TimeOfDay)
                    from = from.AddDays(1);

                // now wipe out time component as we will use the schedule's start time of day and we don't want
                // the time of day during our following calculations
                from = from.Date;

                int rollover = ((int)Math.Floor((from - start.Date).TotalDays)) % Frequency;
                if (rollover > 0)
                    from = from.AddDays(Frequency - rollover);

                // now restore time to what start says it should be
                from = from.Date.Add(start.TimeOfDay);
            }
            
            // add dates to the list until we reach the requested end
            DateTime cur = from;
            while (true)
            {
                yield return cur;
                cur = cur.AddDays(Frequency);
            }
        }
    }
}
