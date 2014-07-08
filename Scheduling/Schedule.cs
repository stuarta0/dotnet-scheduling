using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.ComponentModel;

namespace Scheduling
{
    /// <remarks>
    /// Represents a schedule that reoccurs over a period of time with a concrete start date and optional end date.
    /// </remarks>
    [DebuggerDisplay("{Frequency} {Type}")]
    public abstract class Schedule : ISchedule, INotifyPropertyChanged
    {
        public Schedule()
        {
            PropertyChanged += delegate { };
            Frequency = 1;
        }

        private int _frequency;
        public int Frequency 
        { 
            get { return _frequency; } 
            set
            {
                if (_frequency == Math.Max(1, value))
                    return;

                _frequency = Math.Max(1, value);
                OnPropertyChanged("Frequency");
            }
        }

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

        public abstract IEnumerable<DateTime> GetOccurrences(DateTime start, DateTime from);

        /// <summary>
        /// Convenience method to get a set range of occurrences between 'from' and 'to' DateTimes.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public IEnumerable<DateTime> GetOccurrences(DateTime start, DateTime from, DateTime to)
        {
            return GetOccurrences(start, from).TakeWhile<DateTime>(dt => dt <= to);
        }

        public abstract void Accept(IScheduleVisitor entity);

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
