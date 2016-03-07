# Introduction #

An overview of the source code within `Examples.Formatting`

One of the important parts of the library is being able to get a decent description for a schedule.  Not only that, but if end users have their say, you'll want custom descriptions for a multitude of special cases too. That's where the formatter comes in.


# Details #

Within the library is `Formatters.ScheduleFormatter`.  It provides the base implementation of getting descriptions for schedules.  To allow clean object-oriented behaviour, the visitor pattern is used to avoid if/else hell.

If the type of schedule is known, getting a description is a one-liner:
```
new ScheduleFormatter().Format(schedule)
```

`ScheduleFormatter` can be inherited, allowing the user to override specific description behaviour where necessary.

If the type of schedule is unknown, it's best to use the visitor pattern via the `Accept()` method.  Since this function has no return type, the description is stored within the formatter object (if only one schedule description is required).  If multiple descriptions are generated, use the `DescriptionChanged` event handler.  Both options are shown below:
```
var formatter = new ScheduleFormatter();
formatter.DescriptionChanged += (sender, args) => {
    args.Text; // description of schedule when schedule.Accept() called
};

// this will fire the DescriptionChanged event above
schedule.Accept(formatter);

// alternatively, the description property will contain the last generated description
formatter.Description; 
```

Another benefit to using this formatter object is that we can extend it and provide custom descriptions based on our current spec.  By using the existing `ScheduleFormatter` we get all the default behaviour with our required changes:
```
public class CustomFormatter : ScheduleFormatter
{
    public override string Format(WeeklySchedule s)
    {
        if (s.Frequency == 2)
            return "Fortnightly";
        return base.Format(s);
    }
}
```