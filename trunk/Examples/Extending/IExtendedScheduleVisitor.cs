using Scheduling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Examples.Extending
{
    interface IExtendedScheduleVisitor : IScheduleVisitor
    {
        void Visit(LeapYearSchedule schedule);
    }
}
