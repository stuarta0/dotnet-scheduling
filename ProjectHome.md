For my work in software programming the occasional spec comes along to allow scheduling of events.  Based on calendars from other software, this small library provides the classes to represent reoccurring appointments/events.  In particular:
  * Daily event that can occur every X days
  * Weekly event that happens on particular days of the week every X weeks
  * Monthly event that can be set up in two ways:
    * On day Y of the month every X months, or
    * On the Zth day of the Yth week every X months
  * Yearly event occurring on a particular D/M every X years

What this library is not:
  * CRON
  * Task Scheduler
  * A realtime timer/event library
  * A hamburger

The primary goals of this library are to be reusable, modular, extensible, fast and complete.  Borrowing from the Python mindset, the library attempts to be 'batteries included'.  Providing alternate implementations for parts or all of the library is (or should be) trivial for those cases where the library needs to be extended.

Getting occurrences of any schedule is achieved using `yield return` so it's up to the caller to decide how many values are required.  Calculating occurrences should take O(n) where n = the number of occurrences calculated.

NUnit tests are also provided for every schedule type covering date boundaries and frequency checks.

Sample usage (detailed examples are provided in the source):
```
// An event which occurs on Mondays and Thursdays every fortnight starting on 1 Apr, 2010
// Thu 1/4 -> Mon 12/4, Thu 15/4 -> Mon 26/4, Thu 29/4
var week = new WeeklySchedule { 
    Frequency = 2, 
    Monday = true, 
    Thursday = true };

foreach (var d in week.GetOccurrences(new DateTime(2010, 4, 1))
        .TakeWhile<DateTime>(dt => dt <= new DateTime(2010, 4, 28)))
    // do something with date d
```

Note: I don't expect to update this regularly as calendars don't change often.  Feel free to submit issues or suggestions to improve the library.