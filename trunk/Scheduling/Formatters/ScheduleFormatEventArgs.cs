using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduling.Formatters
{
    public class ScheduleFormatEventArgs : EventArgs
    {
        public ISchedule Schedule { get; private set; }
        public string Text { get; private set; }

        public ScheduleFormatEventArgs(ISchedule schedule, string text)
        {
            Schedule = schedule;
            Text = text;
        }
    }
}
