using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Scheduling
{
    public class WeeklySchedule : Schedule 
    {
        private bool _sunday, _monday, _tuesday, _wednesday, _thursday, _friday, _saturday;
        public virtual bool Sunday
        {
            get { return _sunday; }
            set
            {
                if (_sunday == value)
                    return;

                _sunday = value;
                OnPropertyChanged("Sunday");
            }
        }

        public virtual bool Monday
        {
            get { return _monday; }
            set
            {
                if (_monday == value)
                    return;

                _monday = value;
                OnPropertyChanged("Monday");
            }
        }

        public virtual bool Tuesday
        {
            get { return _tuesday; }
            set
            {
                if (_tuesday == value)
                    return;

                _tuesday = value;
                OnPropertyChanged("Tuesday");
            }
        }

        public virtual bool Wednesday
        {
            get { return _wednesday; }
            set
            {
                if (_wednesday == value)
                    return;

                _wednesday = value;
                OnPropertyChanged("Wednesday");
            }
        }

        public virtual bool Thursday
        {
            get { return _thursday; }
            set
            {
                if (_thursday == value)
                    return;

                _thursday = value;
                OnPropertyChanged("Thursday");
            }
        }

        public virtual bool Friday
        {
            get { return _friday; }
            set
            {
                if (_friday == value)
                    return;

                _friday = value;
                OnPropertyChanged("Friday");
            }
        }

        public virtual bool Saturday
        {
            get { return _saturday; }
            set
            {
                if (_saturday == value)
                    return;

                _saturday = value;
                OnPropertyChanged("Saturday");
            }
        }


        public override string Description
        {
            get
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
                if (Frequency == 1)
                {
                    if (Saturday && Sunday && !(Monday || Tuesday || Wednesday || Thursday || Friday))
                        sb.Append("Weekends");
                    else if (Monday && Tuesday && Wednesday && Thursday && Friday && !(Saturday || Sunday))
                        sb.Append("Weekdays");
                    else
                    {
                        sb.Append("Weekly");
                        append = true;
                    }
                }
                else
                {
                    sb.AppendFormat("Every {0} weeks", Frequency);
                    append = true;
                }

                if (append && TotalDays > 0)
                {
                    sb.Append(" on ");
                    if (Saturday && Sunday && !(Monday || Tuesday || Wednesday || Thursday || Friday))
                        sb.Append("weekends");
                    else if (Monday && Tuesday && Wednesday && Thursday && Friday && !(Saturday || Sunday))
                        sb.Append("weekdays");
                    else if (TotalDays == 7)
                        sb.Append("all days");
                    else
                    {
                        List<string> days = new List<string>();
                        if (Sunday) days.Add("Sunday");
                        if (Monday) days.Add("Monday");
                        if (Tuesday) days.Add("Tuesday");
                        if (Wednesday) days.Add("Wednesday");
                        if (Thursday) days.Add("Thursday");
                        if (Friday) days.Add("Friday");
                        if (Saturday) days.Add("Saturday");
                        sb.Append(Strings.Join(", ", " and ", days));
                    }
                }

                return String.Concat(
                    sb.ToString(),
                    (EndDate.HasValue ? String.Format(", ending {0}", EndDate.Value.ToString("d MMMM yyyy")) : ""));
            }
        }

        public virtual byte TotalDays
        {
            get
            {
                return (byte)((Sunday ? 1 : 0) +
                    (Monday ? 1 : 0) +
                    (Tuesday ? 1 : 0) +
                    (Wednesday ? 1 : 0) +
                    (Thursday ? 1 : 0) +
                    (Friday ? 1 : 0) +
                    (Saturday ? 1 : 0));
            }
        }

        public WeeklySchedule()
            : base()
        {
            Period = Period.Week;
        }

        public override IList<DateTime> GetOccurences(DateTime start, DateTime end)
        {
            List<DateTime> list = new List<DateTime>();
            DateTime cur, lastPeriod;
            int rollover;

            // check bounds to end immediately if necessary
            if (end < StartDate || (EndDate.HasValue && start > EndDate))
                return list;
            else if (start < StartDate)
            {
                // modify start if the requested start is before the scheduled start
                start = StartDate;
                cur = start;
            }
            else
            {
                // the requested start is within the schedule, so calculate the corresponding start based on 
                // how far through two of the scheduled dates we are
                rollover = ((int)Math.Floor((start - StartDate).TotalDays)) % (Frequency * 7);
                if (rollover > 0)
                    cur = start.AddDays((Frequency * 7) - rollover);
                else
                    cur = start;
            }

            // limit the end to either the parameter value or when this schedule ends
            if (EndDate.HasValue && EndDate < end)
                end = EndDate.Value;

            // we must also ensure that the loop to calculate the days goes all the way to the next 
            // whole reoccurrence just in case there are days earlier in the week that would still be 
            // included even if the reoccurrence day is later in the week
            rollover = ((int)Math.Floor((end - StartDate).TotalDays)) % (Frequency * 7);
            if (rollover > 0)
                lastPeriod = end.AddDays((Frequency * 7) - rollover);
            else
                lastPeriod = start;

            List<sbyte> offsets = new List<sbyte>();
            if (Sunday) offsets.Add((sbyte)(DayOfWeek.Sunday - StartDate.DayOfWeek));
            if (Monday) offsets.Add((sbyte)(DayOfWeek.Monday - StartDate.DayOfWeek));
            if (Tuesday) offsets.Add((sbyte)(DayOfWeek.Tuesday - StartDate.DayOfWeek));
            if (Wednesday) offsets.Add((sbyte)(DayOfWeek.Wednesday - StartDate.DayOfWeek));
            if (Thursday) offsets.Add((sbyte)(DayOfWeek.Thursday - StartDate.DayOfWeek));
            if (Friday) offsets.Add((sbyte)(DayOfWeek.Friday - StartDate.DayOfWeek));
            if (Saturday) offsets.Add((sbyte)(DayOfWeek.Saturday - StartDate.DayOfWeek));

            // add dates to the list until we reach the requested end
            while (cur <= lastPeriod)
            {
                foreach (sbyte b in offsets)
                {
                    if (cur.AddDays(b) >= start && cur.AddDays(b) <= end)
                        list.Add(cur.AddDays(b));
                }

                cur = cur.AddDays(Frequency * 7);
            }

            return list;
        }
    }
}
