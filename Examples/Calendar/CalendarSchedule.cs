using Scheduling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Examples.Calendar
{
    /// <summary>
    /// Implements the original Scheduling.Schedule logic of having a start and end date.
    /// </summary>
    public class CalendarSchedule
    {
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ISchedule Schedule { get; set; }

        public CalendarSchedule()
        { }

        public DateTime? NextOccurrence(DateTime? from = null)
        {
            return Schedule.NextOccurrence(StartDate, from ?? StartDate);
        }

        public IList<DateTime> GetOccurrences(DateTime from, DateTime to)
        {
            if (EndDate.HasValue && EndDate < to)
                to = EndDate.Value;

            var result = new List<DateTime>();
            foreach (var d in Schedule.GetOccurrences(StartDate, from).TakeWhile<DateTime>(dt => dt <= to))
                result.Add(d);

            return result;
        }
    }
}
