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
    [DebuggerDisplay("Every {Frequency} {Period}, starting {StartDate}")]
    public abstract class Schedule : INotifyPropertyChanged
    {
        private DateTime _start;

        /// <summary>
        /// Gets or sets when this schedule starts.
        /// </summary>
        public virtual DateTime StartDate
        {
            get { return _start; }
            set
            {
                if (_start == value)
                    return;

                _start = value;
                OnPropertyChanged("StartDate");
            }
        }

        private DateTime? _end;

        /// <summary>
        /// Gets or sets when this schedule ends.  A null value indicates this schedule continues indefinitely.
        /// </summary>
        public virtual DateTime? EndDate
        {
            get { return _end; }
            set
            {
                if (_end == value)
                    return;

                _end = value;
                OnPropertyChanged("EndDate");
            }
        }

        private int _frequency;

        /// <summary>
        /// Gets or sets the frequency of the reoccurrence.
        /// </summary>
        public virtual int Frequency
        {
            get { return _frequency; }
            set
            {
                if (_frequency == value)
                    return;

                // ensure frequency cannot be less than 1
                _frequency = Math.Max(1, value);
                OnPropertyChanged("Frequency");
            }
        }

        private Period _period;

        /// <summary>
        /// Gets the period that this schedule reoccurs.
        /// </summary>
        public virtual Period Period
        {
            get { return _period; }
            protected set
            {
                if (_period == value)
                    return;

                _period = value;
                OnPropertyChanged("Period");
            }
        }

        /// <summary>
        /// Gets a human-readable description of this schedule.
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// Default constructor for a schedule starting today with no end date.
        /// </summary>
        public Schedule()
        {
            StartDate = DateTime.Today;
            EndDate = null;
            Frequency = 1;
            Period = Period.None;
        }

        /// <summary>
        /// Gets a list of DateTime objects indicating every occurrence of the schedule between the requested dates.
        /// </summary>
        /// <param name="start">The start of the requested range (inclusive of this date).</param>
        /// <param name="end">The end of the requested range (inclusive of this date).</param>
        /// <returns>A list of DateTime objects for every occurrence of this schedule.  An empty list indicates the schedule does not fall on any of the dates in the requested range.</returns>
        public abstract IList<DateTime> GetOccurences(DateTime start, DateTime end);

        /// <summary>
        /// Calculates the next occurrence from today.
        /// </summary>
        /// <returns>The date of the next occurrence or null if there are no occurrences.</returns>
        public virtual DateTime? NextOccurrence()
        {
            return NextOccurrence(DateTime.Now.Date);
        }

        /// <summary>
        /// Calculates the next occurrence from a given date.
        /// </summary>
        /// <param name="from">The date from which the next occurrence will be found.</param>
        /// <returns>The date of the next occurrence after <code>from</code> or null if there are no occurrences.</returns>
        public virtual DateTime? NextOccurrence(DateTime from)
        {
            if (from < StartDate)
                from = StartDate;

            // ensure we cover at least one period - this might not be as efficient since we'll be 
            // calculating dates that we don't need but in the grand scheme of things the number of
            // items will be negligble
            IList<DateTime> occurrences = GetOccurences(from, from.AddDays(Frequency * (int)Period));
            if (occurrences.Count > 0)
                return occurrences[0];

            return null;
        }

        /// <summary>
        /// Gets the description of the schedule.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Description;
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

            // changes to any property will probably cause the description to be invalidated
            if (propertyName != "Description")
                OnPropertyChanged("Description");
        }

        #endregion
    }
}
