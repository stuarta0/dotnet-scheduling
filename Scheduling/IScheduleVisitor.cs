using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduling
{
    public interface IScheduleVisitor
    {
        /// <summary>
        /// Visit a schedule to perform some logic with it.
        /// </summary>
        /// <param name="schedule"></param>
        void Visit(ISchedule schedule);
    }
}
