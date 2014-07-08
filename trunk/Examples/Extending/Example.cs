using Scheduling;
using Scheduling.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Examples.Extending
{
    class Example
    {
        public Example()
        {
            var olympics = new LeapYearSchedule() { Frequency = 1 };

            var inauguration = new DateTime(1896, 1, 1);
            var from = new DateTime(2000, 12, 31);
            var to = new DateTime(2020, 1, 1);
            foreach (var d in olympics.GetOccurrences(inauguration, from).TakeWhile<DateTime>(dt => dt <= to))
                Console.WriteLine(d);

            foreach (var s in Generate())
            {
                Console.WriteLine(s.GetType().Name);

                // the standard formatter doesn't know about our custom schedule, so we can't call
                // ScheduleFormatter.Format(), and it also won't print if we use the visitor pattern
                // LeapYearSchedule.Accept(ScheduleFormatter())
                var standard = new ScheduleFormatter();
                s.Accept(standard);
                Console.WriteLine("Standard: {0}", standard.Description);

                // our extended formatter will print all existing schedules, including our custom one
                var extended = new ExtendedFormatter();
                s.Accept(extended);
                Console.WriteLine("Extended: {0}", extended.Description);
            }
        }

        private List<ISchedule> Generate()
        {
            return new List<ISchedule>() { 
                new DailySchedule() { Frequency = 1 }, 
                new WeeklySchedule() { Frequency = 1, Wednesday = true }, 
                new MonthlySchedule() { Frequency = 3 },
                new MonthlyDaySchedule() { Frequency = 3 }, 
                new YearlySchedule() { Frequency = 4 },
                new LeapYearSchedule() { Frequency = 2 }
            };
        }
    }
}
