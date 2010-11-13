using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Scheduling
{
    public class MonthlySchedule : Schedule 
    {
        public enum Type
        {
            DayOfMonth = 0,
            DayOfWeek = 1
        }

        private Type _type;
        public virtual Type ReoccurrenceType
        {
            get { return _type; }
            set
            {
                if (_type == value)
                    return;

                _type = value;
                OnPropertyChanged("ReoccurrenceType");
            }
        }


        public override string Description
        {
            get
            {
                if (ReoccurrenceType == Type.DayOfMonth)
                {
                    // Monthly on day 21
                    // Every 6 months on day 21
                    return String.Concat(
                        String.Format(Strings.Plural(Frequency, "Monthly on day {1}", "Every {0} months on day {1}"), Frequency, StartDate.Day),
                        (EndDate.HasValue ? String.Format(", ending {0}", EndDate.Value.ToString("d MMMM yyyy")) : ""));
                }
                else if (ReoccurrenceType == Type.DayOfWeek)
                {
                    // Monthly on the first Tuesday
                    // Every 6 months on the last Saturday
                    return String.Concat(
                        String.Format(Strings.Plural(Frequency, "Monthly on the {1} {2}", "Every {0} months on the {1} {2}"), Frequency, GetDescriptiveWeek(GetWeek(StartDate)).ToLower(), StartDate.DayOfWeek.ToString()),
                        (EndDate.HasValue ? String.Format(", ending {0}", EndDate.Value.ToString("d MMMM yyyy")) : ""));
                }

                return string.Empty;
            }
        }

        public MonthlySchedule()
            : base()
        {
            Period = Period.Month;
            ReoccurrenceType = Type.DayOfMonth;
        }

        private int GetWeek(DateTime date)
        {
            // add dates to the list until we reach the requested end
            int week = ((int)Math.Floor((double)(date.Day - 1) / 7)) + 1;
            if (date >= (new DateTime(date.Year, date.Month, 1).AddMonths(1).AddDays(-7)))
                week = 5;

            return week;
        }

        private string GetDescriptiveWeek(int weekNumber)
        {
            switch (weekNumber)
            {
                case 1: return "First"; break;
                case 2: return "Second"; break;
                case 3: return "Third"; break;
                case 4: return "Fourth"; break;
                case 5: return "Last"; break;
            }

            return string.Empty;
        }

        public override IList<DateTime> GetOccurences(DateTime start, DateTime end)
        {
            List<DateTime> list = new List<DateTime>();
            
            // check bounds to end immediately if necessary
            if (end < StartDate || (EndDate.HasValue && start > EndDate))
                return list;
            else if (start < StartDate)
            {
                // modify start if the requested start is before the scheduled start
                start = StartDate;
            }

            // limit the end to either the parameter value or when this schedule ends
            if (EndDate.HasValue && EndDate < end)
                end = EndDate.Value;

            // add dates to the list until we reach the requested end
            int week = GetWeek(StartDate);
            DateTime cur = StartDate;
            while (cur <= end)
            {
                if (_type == Type.DayOfMonth)
                {
                    // TODO: this is a fairly brute-force approach and will get more inefficient the further the requested dates are from the start date.
                    if (cur >= start && cur <= end)
                        list.Add(cur);
                }
                else if (_type == Type.DayOfWeek)
                {
                    DateTime dow = CalculateDate(week, StartDate.DayOfWeek, cur);
                    if (dow >= start && dow <= end)
                        list.Add(dow);
                }

                cur = cur.AddMonths(Frequency);
            }

            return list;
        }

        /// <summary>
        /// Calculate a date during the month using DayOfWeek logic, i.e. 2nd Thursday in April 2010.
        /// </summary>
        /// <param name="weekNumber">One-based week number during the month.  Values equal to or greater than five will be considered the last week in the month.</param>
        /// <param name="dow">The day of the week.</param>
        /// <param name="target">The target month/year.  Day number is ignored.</param>
        /// <returns>The DateTime during the target month representing the day of week.</returns>
        protected virtual DateTime CalculateDate(int weekNumber, DayOfWeek dow, DateTime target)
        {
            DateTime first = new DateTime(target.Year, target.Month, 1);

            if (weekNumber >= 5)
                return CalculateDate(1, dow, first.AddMonths(1)).AddDays(-7);
            else if (dow < first.DayOfWeek)
                return first.AddDays(((weekNumber - 1) * 7) + (7 - Math.Abs(dow - first.DayOfWeek)));
            else
                return first.AddDays(((weekNumber - 1) * 7) + Math.Abs(first.DayOfWeek - dow));
        }
    }
}
