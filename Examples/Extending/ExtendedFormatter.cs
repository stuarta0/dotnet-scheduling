using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Examples.Extending
{
    class ExtendedFormatter : Scheduling.Formatters.ScheduleFormatter, IExtendedScheduleVisitor
    {
        public virtual void Visit(LeapYearSchedule schedule) { OnDescriptionChanged(schedule, Format(schedule)); }

        public virtual string Format(LeapYearSchedule schedule)
        {
            if (schedule.Frequency == 1)
                return "Every leap year";
            else
                return String.Format("Every {0} leap years", schedule.Frequency);
        }
    }
}
