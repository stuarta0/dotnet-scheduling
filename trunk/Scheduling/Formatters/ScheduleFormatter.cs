using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Scheduling.Formatters
{
    /// <summary>
    /// Handles creation of human-readable descriptions for all built-in schedule types.  Can be inherited and further schedule types defined.
    /// </summary>
    public class ScheduleFormatter : IScheduleVisitor, INotifyPropertyChanged
    {
        /// <summary>
        /// Optional start date to include in the description.
        /// </summary>
        public DateTime? Start { get; set; }

        /// <summary>
        /// Gets the last description that was generated.
        /// </summary>
        public string Description { get; protected set; }

        public ScheduleFormatter(DateTime? start = null)
        {
            Start = start;
            DescriptionChanged += delegate { };
            PropertyChanged += delegate { };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public delegate void ScheduleFormatEventHandler(object sender, ScheduleFormatEventArgs args);

        /// <summary>
        /// Event fired when formatter visited by schedule.  Useful for looping over schedules and getting descriptions.
        /// </summary>
        public event ScheduleFormatEventHandler DescriptionChanged;
        protected virtual void OnDescriptionChanged(ISchedule schedule, string description)
        {
            Description = description;
            DescriptionChanged(this, new ScheduleFormatEventArgs(schedule, description));
            PropertyChanged(this, new PropertyChangedEventArgs("Description"));
        }

        /// <summary>
        /// Visitor pattern for generating description for this schedule.  Will update Description and fire events.
        /// </summary>
        public virtual void Visit(DailySchedule s) { OnDescriptionChanged(s, Format(s)); }

        /// <summary>
        /// Visitor pattern for generating description for this schedule.  Will update Description and fire events.
        /// </summary>
        public virtual void Visit(WeeklySchedule s) { OnDescriptionChanged(s, Format(s)); }

        /// <summary>
        /// Visitor pattern for generating description for this schedule.  Will update Description and fire events.
        /// </summary>
        public virtual void Visit(MonthlySchedule s) { OnDescriptionChanged(s, Format(s)); }

        /// <summary>
        /// Visitor pattern for generating description for this schedule.  Will update Description and fire events.
        /// </summary>
        public virtual void Visit(MonthlyDaySchedule s) { OnDescriptionChanged(s, Format(s)); }

        /// <summary>
        /// Visitor pattern for generating description for this schedule.  Will update Description and fire events.
        /// </summary>
        public virtual void Visit(YearlySchedule s) { OnDescriptionChanged(s, Format(s)); }
        


        /// <summary>
        /// Gets the generated description for the schedule.  Will not update Description or fire events.
        /// </summary>
        public virtual string Format(DailySchedule s)
        {
            // Daily
            // Every 2 days
             return String.Format(
                    Strings.Plural(s.Frequency, "Daily", "Every {0} days"),
                    s.Frequency
                );
        }


        /// <summary>
        /// Gets the generated description for the schedule.  Will not update Description or fire events.
        /// </summary>
        public virtual string Format(WeeklySchedule s)
        {
            // Weekly
            // Weekly on Sunday, Monday and Thursday
            // Weekly on all days
            // Weekdays
            // Weekends
            // Every 3 weeks
            // Every 3 weeks on Sunday, Monday and Thursday
            // Every 3 weeks on all days
            // Every 3 weeks on weekdays
            // Every 3 weeks on weekends

            bool append = false;
            StringBuilder sb = new StringBuilder();
            if (s.Frequency == 1)
            {
                if (s.Saturday && s.Sunday && !(s.Monday || s.Tuesday || s.Wednesday || s.Thursday || s.Friday))
                    sb.Append("Weekends");
                else if (s.Monday && s.Tuesday && s.Wednesday && s.Thursday && s.Friday && !(s.Saturday || s.Sunday))
                    sb.Append("Weekdays");
                else
                {
                    sb.Append("Weekly");
                    append = true;
                }
            }
            else
            {
                sb.AppendFormat("Every {0} weeks", s.Frequency);
                append = true;
            }

            if (append && s.TotalDays > 0)
            {
                sb.Append(" on ");
                if (s.Saturday && s.Sunday && !(s.Monday || s.Tuesday || s.Wednesday || s.Thursday || s.Friday))
                    sb.Append("weekends");
                else if (s.Monday && s.Tuesday && s.Wednesday && s.Thursday && s.Friday && !(s.Saturday || s.Sunday))
                    sb.Append("weekdays");
                else if (s.TotalDays == 7)
                    sb.Append("all days");
                else
                {
                    List<string> days = new List<string>();
                    if (s.Sunday) days.Add("Sunday");
                    if (s.Monday) days.Add("Monday");
                    if (s.Tuesday) days.Add("Tuesday");
                    if (s.Wednesday) days.Add("Wednesday");
                    if (s.Thursday) days.Add("Thursday");
                    if (s.Friday) days.Add("Friday");
                    if (s.Saturday) days.Add("Saturday");
                    sb.Append(Strings.Join(", ", " and ", days));
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Gets the generated description for the schedule.  Will not update Description or fire events.
        /// </summary>
        public virtual string Format(MonthlySchedule s)
        {
            // Monthly on day 21
            // Every 6 months on day 21

            var sb = new StringBuilder();
            if (s.Frequency == 1)
                sb.Append("Monthly");
            else
                sb.AppendFormat("Every {0} months", s.Frequency);

            if (Start.HasValue)
                sb.AppendFormat(" on day {0}", Start.Value.Day);

            return sb.ToString();
            //(EndDate.HasValue ? String.Format(", ending {0}", EndDate.Value.ToString("d MMMM yyyy")) : ""));
        }

        /// <summary>
        /// Gets the generated description for the schedule.  Will not update Description or fire events.
        /// </summary>
        public virtual string Format(MonthlyDaySchedule s)
        {
            // Monthly on the first Tuesday
            // Every 6 months on the last Saturday
            var sb = new StringBuilder();
            if (s.Frequency == 1)
                sb.Append("Monthly");
            else
                sb.AppendFormat("Every {0} months", s.Frequency);

            if (Start.HasValue)
                sb.AppendFormat(" on the {0} {1}", GetDescriptiveWeek(MonthlyDaySchedule.GetWeek(Start.Value)), Start.Value.DayOfWeek);

            return sb.ToString();
            //(EndDate.HasValue ? String.Format(", ending {0}", EndDate.Value.ToString("d MMMM yyyy")) : ""));
        }
        private string GetDescriptiveWeek(int weekNumber)
        {
            switch (weekNumber)
            {
                case 1: return "first"; break;
                case 2: return "second"; break;
                case 3: return "third"; break;
                case 4: return "fourth"; break;
                case 5: return "last"; break;
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the generated description for the schedule.  Will not update Description or fire events.
        /// </summary>
        public virtual string Format(YearlySchedule s)
        {
            // Annually on April 12
            // Every 2 years on April 12
            var sb = new StringBuilder();
            if (s.Frequency == 1)
                sb.Append("Annually");
            else
                sb.AppendFormat("Every {0} years", s.Frequency);

            if (Start.HasValue)
                sb.AppendFormat(" on {0}", Start.Value.ToString("MMMM d"));

            return sb.ToString();
            //(EndDate.HasValue ? String.Format(", ending {0}", EndDate.Value.ToString("d MMMM yyyy")) : ""));
        }

    }
}
