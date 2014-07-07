using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Scheduling
{
    public class MonthlySchedule : Schedule
    {
        public override IEnumerable<DateTime> GetOccurrences(DateTime start, DateTime from)
        {
            DateTime cur = start;
            while (true)
            {
                if (cur >= from)
                    yield return cur;

                cur = cur.AddMonths(Frequency);
            }
        }
    }
}
