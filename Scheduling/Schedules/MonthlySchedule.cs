using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Scheduling
{
    /// <summary>
    /// Represents a schedule that occurs every X months on a specific date during the month (e.g. the 10th of each month).
    /// </summary>
    public class MonthlySchedule : Schedule
    {
        public override void Accept(IScheduleVisitor entity)
        {
            entity.Visit(this);
        }

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
