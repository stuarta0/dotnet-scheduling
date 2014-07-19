using Scheduling;
using Scheduling.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Examples.Calendar
{
    class Example
    {
        public Example()
        {
            var now = DateTime.Now;
            var calendar = new CalendarSchedule()
            {
                StartDate = now,
                EndDate = now.AddMonths(2),
                Schedule = new DailySchedule()
                {
                    Frequency = 5
                }
            };

            // see Examples.Formatting for further details
            Console.WriteLine("Starting {0:d MMMM yyyy}, {1}, ending {2:d MMMM yyyy}", calendar.StartDate, 
                new ScheduleFormatter().Format((DailySchedule)calendar.Schedule), calendar.EndDate);
            

            // occurrences.Length = number of 5 day blocks within 1 month period (from between 1 to 2 months from now).
            // Since GetOccurrences() starts 1 month from now, but has an EndDate 2 months from now, it will only return
            // a small subset even though the following call asks for up to 100 years time (EndDate takes precedence)
            var from = now.AddMonths(1);
            var to = now.AddYears(100);
            var occurrences = calendar.GetOccurrences(from, to);

            Console.WriteLine("Occurrences between {0} and {1}:", from, to);
            foreach (var d in occurrences)
                Console.WriteLine(d);
        }
    }
}
