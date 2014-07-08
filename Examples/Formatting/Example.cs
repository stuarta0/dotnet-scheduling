using Scheduling;
using Scheduling.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Examples.Formatting
{
    class Example
    {
        public Example()
        {
            // create the schedule from which we will get it's description
            var schedule = new WeeklySchedule()
            {
                Frequency = 2,
                Monday = true,
                Friday = true
            };

            // one-liner print
            SimplePrint(schedule);

            // set up a custom formatter and print the schedule, returning the formatter
            var formatter = AdvancedPrint(schedule);

            // print an entire list of schedules using the custom formatter
            BulkPrint(formatter);
        }

        private void SimplePrint(WeeklySchedule schedule)
        {
            // create the standard formatter that will be used to generate the schedule description
            Console.WriteLine(new ScheduleFormatter().Format(schedule));
        }

        private IScheduleVisitor AdvancedPrint(ISchedule schedule)
        {
            // Now try using our custom formatter to generate the description using the Description property.
            // Using our custom formatter will now produce different text saying "Fortnightly" instead of 
            // the standard "Every 2 weeks"
            var formatter = new CustomFormatter(DateTime.Now);

            // there are three options when using the formatter:
            // 1) handle the description changed event, which will be fired each time a schedule
            //    accepts the formatter (good for looping over lists of schedules), or
            // 2) if it's only called for one schedule, use formatter.Description after it's been
            //    accepted as this property will have the last Description generated, or
            // 3) call formatter.Format() if you know the type of your schedule object.
            formatter.DescriptionChanged += (sender, args) =>
            {
                Console.WriteLine(args.Text);
            };

            // visiting the schedule with the formatter will fire the DescriptionChanged event,
            // causing the text output to be printed to the console.
            schedule.Accept(formatter);

            // we'll reuse this formatter
            return formatter;
        }

        private void BulkPrint(IScheduleVisitor formatter)
        {
            // now format a whole list of schedules, remembering that the description event will
            // fire as each schedule is visited
            var schedules = new List<Schedule>() { 
                new DailySchedule() { Frequency = 1 }, 
                new WeeklySchedule() { Frequency = 1, Wednesday = true }, 
                new MonthlySchedule() { Frequency = 3 },
                new MonthlyDaySchedule() { Frequency = 3 }, 
                new YearlySchedule() { Frequency = 4 }
            };

            foreach (var s in schedules)
                s.Accept(formatter);
        }
    }
}
