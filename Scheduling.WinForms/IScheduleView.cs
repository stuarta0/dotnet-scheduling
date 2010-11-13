using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduling.WinForms
{
    public interface IScheduleView
    {
        Schedule CurrentSchedule { get; set; }
    }
}
