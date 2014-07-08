using Scheduling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Examples.Extending
{
    interface IExtendedSchedule : ISchedule
    {
        void Accept(IExtendedScheduleVisitor entity);
    }
}
