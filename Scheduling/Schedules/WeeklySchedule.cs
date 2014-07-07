using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Scheduling
{
    public class WeeklySchedule : Schedule
    {
        #region Local Variables

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

        #endregion

        //public override IList<DateTime> GetOccurences(DateTime start, DateTime end)
        public override IEnumerable<DateTime> GetOccurrences(DateTime start, DateTime from)
        {
            DateTime cur;
            if (from <= start)
            {
                from = start;
                cur = from;
            }
            else
            {
                // the requested from is within the schedule, so calculate the corresponding from based on 
                // how far through two of the scheduled dates we are

                // if the parameter from time is after the schedule from time, perform calculations starting tomorrow
                if (from.TimeOfDay > start.TimeOfDay)
                    from = from.AddDays(1);

                // now wipe out time component as we will use the schedule's start time of day and we don't want
                // the time of day during our following calculations
                from = from.Date;

                int rollover = ((int)Math.Floor((from - start.Date).TotalDays)) % (Frequency * 7);
                if (rollover > 0)
                    cur = from.AddDays(-rollover);
                else
                    cur = from;

                // now restore time to what start says it should be
                from = from.Add(start.TimeOfDay);
                cur = cur.Add(start.TimeOfDay);
            }
            
            List<sbyte> offsets = new List<sbyte>();
            if (Sunday) offsets.Add((sbyte)(DayOfWeek.Sunday - start.DayOfWeek));
            if (Monday) offsets.Add((sbyte)(DayOfWeek.Monday - start.DayOfWeek));
            if (Tuesday) offsets.Add((sbyte)(DayOfWeek.Tuesday - start.DayOfWeek));
            if (Wednesday) offsets.Add((sbyte)(DayOfWeek.Wednesday - start.DayOfWeek));
            if (Thursday) offsets.Add((sbyte)(DayOfWeek.Thursday - start.DayOfWeek));
            if (Friday) offsets.Add((sbyte)(DayOfWeek.Friday - start.DayOfWeek));
            if (Saturday) offsets.Add((sbyte)(DayOfWeek.Saturday - start.DayOfWeek));

            // add dates to the list until we reach the requested end
            while (true)
            {
                foreach (sbyte b in offsets)
                {
                    if (cur.AddDays(b) >= from)
                        yield return cur.AddDays(b);
                }

                cur = cur.AddDays(Frequency * 7);
            }
        }
    }
}
