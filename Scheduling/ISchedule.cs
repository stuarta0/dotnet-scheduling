using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduling
{
    public interface ISchedule
    {
        int Frequency { get; set; }

        /// <summary>
        /// Get the next occurrence of this schedule if this schedule started on 'start' DateTime and we wanted the next occurrence after 'from' DateTime.
        /// </summary>
        /// <param name="start">The start DateTime that the schedule begins.</param>
        /// <param name="from">The DateTime that we want to start determining the next occurrence.</param>
        /// <returns>The first occurrence after 'from' DateTime.</returns>
        DateTime? NextOccurrence(DateTime start, DateTime from);

        /// <summary>
        /// Yield occurrences starting from start DateTime.
        /// </summary>
        /// <example>
        /// // assumes schedule returns yearly; number of iterations will be number of years between 1/1/2000 and Today.
        /// foreach (var d in GetOccurrences(new DateTime(2000, 1, 1))
        /// {
        ///     // break if we reach today (otherwise it may continue indefinitely)
        ///     if (d > DateTime.Today)
        ///         break;
        ///         
        ///     // do something with DateTime d
        /// }
        /// </example>
        /// <param name="start">The start DateTime to use to begin calculating this schedule.</param>
        /// <returns>An enumerable of all occurrences of this schedule from start DateTime.</returns>
        IEnumerable<DateTime> GetOccurrences(DateTime start);

        /// <summary>
        /// Yield occurrences after 'from' DateTime when the schedule started from 'start' DateTime.
        /// </summary>
        /// <example>
        /// // assumes schedule returns daily; number of iterations will be number of days between 10/10/2010 and Today.
        /// foreach (var d in GetOccurrences(new DateTime(2000, 1, 1), new DateTime(2010, 10, 10))
        /// {
        ///     // break if we reach today (otherwise it may continue indefinitely)
        ///     if (d > DateTime.Today)
        ///         break;
        ///         
        ///     // do something with DateTime d
        /// }
        /// </example>
        /// <param name="start">The start DateTime that this schedule starts at.</param>
        /// <param name="from">The DateTime from which occurrences will be retrieved from.</param>
        /// <returns>An enumerable of all occurrences of this schedule from a DateTime.</returns>
        IEnumerable<DateTime> GetOccurrences(DateTime start, DateTime from);

        /// <summary>
        /// Using the visitor pattern, calls entity.Visit(this);
        /// </summary>
        void Accept(IScheduleVisitor entity);
    }
}
