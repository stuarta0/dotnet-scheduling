using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduling
{
    public interface IScheduleVisitor
    {
        void Visit(DailySchedule schedule);
        void Visit(WeeklySchedule schedule);
        void Visit(MonthlySchedule schedule);
        void Visit(MonthlyDaySchedule schedule);
        void Visit(YearlySchedule schedule);
    }
}
