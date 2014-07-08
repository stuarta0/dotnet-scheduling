using Scheduling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Examples.Persistence
{
    public class Example
    {
        public Example()
        {
            // repo that should have our CRUD operations for schedules
            // (currently only implements Create and Read)
            Repository repo = new FakeRepository();

            // create our new schedule that will be persisted
            var schedule = new DbSchedule()
            {
                Id = 1,
                Schedule = new WeeklySchedule()
                {
                    Frequency = 2,
                    Monday = true,
                    Friday = true
                }
            };

            // create the schedule in our repository
            repo.Create(schedule);

            // now read it back out
            schedule = repo.Read(1);

            // test the occurrences; it should produce ~2 occurrences
            var now = DateTime.Now;
            var end = now.AddMonths(2);
            foreach (var d in schedule.Schedule.GetOccurrences(now, now.AddMonths(1)).TakeWhile<DateTime>(dt => dt <= end))
                Console.WriteLine(d);
        }
    }
}
