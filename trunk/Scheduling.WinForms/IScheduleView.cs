using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduling.WinForms
{
    public interface IScheduleView
    {
        /// <summary>
        /// Fired when the CurrentSchedule object changes.
        /// </summary>
        event EventHandler CurrentScheduleChanged;

        /// <summary>
        /// The current schedule bound to the view.
        /// </summary>
        Schedule CurrentSchedule { get; set; }
    }
}
