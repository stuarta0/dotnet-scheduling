using Scheduling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Examples.Extending
{
    /// <summary>
    /// Returns February 29th for each leap year starting on the given DateTime start parameter.
    /// </summary>
    class LeapYearSchedule : IExtendedSchedule
    {
        public int Frequency { get; set; }

        public DateTime? NextOccurrence(DateTime start, DateTime from)
        {
            foreach (var d in GetOccurrences(start, from))
                return d;
            return null;
        }

        public IEnumerable<DateTime> GetOccurrences(DateTime start)
        {
            return GetOccurrences(start, start);
        }

        public IEnumerable<DateTime> GetOccurrences(DateTime start, DateTime from)
        {
            if (!DateTime.IsLeapYear(start.Year))
                throw new ArgumentException("Start DateTime must be within a leap year.");

            var cur = new DateTime(start.Year, 2, 29);
            while (true)
            {
                if (cur >= from)
                    yield return cur;
                cur = cur.AddYears(4 * Frequency);
            }
        }

        public void Accept(IScheduleVisitor entity)
        {
            // the standard IScheduleVisitor doesn't know about our custom type
            // unfortunately to wire it up correctly, we need to type check
            if (entity is IExtendedScheduleVisitor)
                Accept((IExtendedScheduleVisitor)entity);
        }

        public void Accept(IExtendedScheduleVisitor entity)
        {
            entity.Visit(this);
        }
    }
}
