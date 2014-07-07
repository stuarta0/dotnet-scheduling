using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Scheduling
{
    /// <summary>
    /// Uses day of week logic. i.e. Every X months on the 2nd Monday.
    /// </summary>
    public class MonthlyDaySchedule : MonthlySchedule
    {
        public override IEnumerable<DateTime> GetOccurrences(DateTime start, DateTime from)
        {
            int week = GetWeek(start);
            DateTime cur = start;
            while (true)
            {
                DateTime dow = CalculateDate(week, start.DayOfWeek, cur);
                if (dow >= from)
                    yield return dow;
                cur = cur.AddMonths(Frequency);
            }
        }

        public static int GetWeek(DateTime date)
        {
            int week = ((int)Math.Floor((double)(date.Day - 1) / 7)) + 1;
            if (date >= (new DateTime(date.Year, date.Month, 1).AddMonths(1).AddDays(-7)))
                week = 5;

            return week;
        }

        /// <summary>
        /// Calculate a date during the month using DayOfWeek logic, i.e. 2nd Thursday in April 2010.
        /// </summary>
        /// <param name="weekNumber">One-based week number during the month.  Values equal to or greater than five will be considered the last week in the month.</param>
        /// <param name="dow">The day of the week.</param>
        /// <param name="target">The target month/year.  Day number is ignored.</param>
        /// <returns>The DateTime during the target month representing the day of week.</returns>
        public static DateTime CalculateDate(int weekNumber, DayOfWeek dow, DateTime target)
        {
            DateTime first = new DateTime(target.Year, target.Month, 1);

            if (weekNumber >= 5)
                return CalculateDate(1, dow, first.AddMonths(1)).AddDays(-7);
            else if (dow < first.DayOfWeek)
                return first.AddDays(((weekNumber - 1) * 7) + (7 - Math.Abs(dow - first.DayOfWeek))).Add(target.TimeOfDay);
            else
                return first.AddDays(((weekNumber - 1) * 7) + Math.Abs(first.DayOfWeek - dow)).Add(target.TimeOfDay);
        }
    }
}