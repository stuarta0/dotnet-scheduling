# Introduction #

An overview of the source code within `Examples.Calendar`

Schedules are defined as a type and frequency, but often you'll want a defined start and potential end date.  This would be the case if you were setting up appointments/events like in Microsoft Outlook or Google Calendar.


# Details #

To achieve this behaviour, create a class `CalendarSchedule` with properties for:
  * `DateTime StartDate`
  * `DateTime? EndDate`
  * `ISchedule Schedule`

Requests for occurrences can be routed through the `CalendarSchedule` class, which can use its `StartDate` property as the value for the call to `Schedule.GetOccurrences()`.  It can also limit the range returned based on the `EndDate` property and also convert the enumerator to a more convenient `List<>` object.