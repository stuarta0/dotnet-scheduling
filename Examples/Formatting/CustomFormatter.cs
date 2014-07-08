using Scheduling;
using Scheduling.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Examples.Formatting
{
    /// <summary>
    /// This formatter allows a custom implementation of generating descriptions for certain schedule types.
    /// </summary>
    public class CustomFormatter : ScheduleFormatter
    {
        public CustomFormatter(DateTime? start)
            : base(start)
        { }

        /// <summary>
        /// Allow 2 weeks to show up as "Fortnightly", otherwise return the base implementation of a week description.
        /// </summary>
        public override string Format(WeeklySchedule s)
        {
            if (s.Frequency == 2)
                return "Fortnightly"; // to be complete this should output which days that occur fortnightly
            else
                return base.Format(s);
        }

        /// <summary>
        /// Allow 3 months to show up as "Quarterly", otherwise return the base implementation of a month description.
        /// </summary>
        public override string Format(MonthlySchedule s)
        {
            if (s.Frequency == 3)
                return String.Format(String.Concat("Quarterly", (Start.HasValue ? " on day {0}" : "")), Start.Value.Day);
            else
                return base.Format(s);
        }
    }
}
