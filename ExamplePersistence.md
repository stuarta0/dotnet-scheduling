# Introduction #

An overview of the source code within `Examples.Persistence`

For this library to be useful in any way you really want to be persisting the data somewhere.  The schedules themselves are based on value objects of domain driven design.  Therefore the user of the library needs to provide an entity object to hold the schedule.

By provided your own entity object, you can decide what data types are required for your data store.  If using an ORM such as NHibernate, you can provide mappings and schemes that are relevant to your database.  It allows you to provide additional fields too, like in ExampleCalendar.


# Details #

The class `DbSchedule` is used to compose a number of properties that are generally used with databases:
  * `int Id`
  * `DateTime Created`
  * `DateTime Modified`
  * `ISchedule Schedule`

When using an ORM, the fields in `DbSchedule` and fields for the specific `Schedule` instance can be persisted as:
  * all columns in a single table with a type discriminator column, or
  * individual tables for each concrete schedule type with the entity object representing the root table

The example code shows a trivial example:
```
Repository repo = new FakeRepository();
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
```