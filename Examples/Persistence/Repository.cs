using Scheduling;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Examples.Persistence
{
    /// <summary>
    /// Uses tables to store each schedule-specific details.  Another option is to use one table with a discriminator columns.
    /// All ORM's will have these options, this class shows a manual implementation of what an ORM can provide.
    /// </summary>
    class Repository
    {
        public Repository()
        { }

        #region Helpers

        private bool GetBoolean(SqlDataReader reader, string column)
        {
            return reader.GetBoolean(reader.GetOrdinal(column));
        }

        private int GetInt32(SqlDataReader reader, string column)
        {
            return reader.GetInt32(reader.GetOrdinal(column));
        }

        private bool IsDBNull(SqlDataReader reader, string column)
        {
            return reader.IsDBNull(reader.GetOrdinal(column));
        }

        #endregion

        public virtual DbSchedule Read(int id)
        {
            using (var conn = new SqlConnection("connection string"))
            {
                using (var cmd = conn.CreateCommand())
                {
                    // Left join to the individual tables. The correct table which will define the type of schedule we need
                    // will produce a column with joined Id != null.
                    cmd.CommandText = @"SELECT s.Id, s.DateCreated, s.DateModified, s.Frequency, sd.*, sw.*, sm.*, smd.*, sy.*
FROM [Schedules] s
LEFT OUTER JOIN [ScheduleDay] sd ON s.Id = sd.Id
LEFT OUTER JOIN [ScheduleWeek] sw ON s.Id = sw.Id
LEFT OUTER JOIN [ScheduleMonth] sm ON s.Id = sw.Id
LEFT OUTER JOIN [ScheduleMonthByDay] smd = s.Id = smd.Id
LEFT OUTER JOIN [ScheduleYear] sy ON s.Id = sy.Id
WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("Id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new DbSchedule()
                            {
                                Id = reader.GetInt32(0),
                                Created = reader.GetDateTime(1),
                                Modified = reader.GetDateTime(2),
                                Schedule = Generate(reader)
                            };
                        }
                    }
                }
            }

            return null;
        }

        public virtual void Create(DbSchedule schedule)
        {
            using (var conn = new SqlConnection("connection string"))
            {
                using (var cmd = conn.CreateCommand())
                {
                    // assume triggers update date created and modified
                    cmd.CommandText = @"INSERT INTO [Schedules] (Id, Frequency) VALUES (@Id, @Frequency)";
                    cmd.Parameters.AddWithValue("Id", schedule.Id);
                    cmd.Parameters.AddWithValue("Frequency", schedule.Schedule.Frequency);
                    cmd.ExecuteNonQuery();

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("Id", schedule.Id);

                    // now commit the correct ID into the correct table
                    if (schedule.Schedule is DailySchedule)
                        cmd.CommandText = "INSERT INTO [ScheduleDay] (Id) VALUES (@Id)";
                    else if (schedule.Schedule is WeeklySchedule)
                    {
                        var week = (WeeklySchedule)schedule.Schedule;
                        cmd.CommandText = "INSERT INTO [ScheduleWeek] (Id, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday) VALUES (@Id, @Monday, @Tuesday, @Wednesday, @Thursday, @Friday, @Saturday, @Sunday)";
                        cmd.Parameters.AddWithValue("@Monday", week.Monday);
                        cmd.Parameters.AddWithValue("@Tuesday", week.Tuesday);
                        cmd.Parameters.AddWithValue("@Wednesday", week.Wednesday);
                        cmd.Parameters.AddWithValue("@Thursday", week.Thursday);
                        cmd.Parameters.AddWithValue("@Friday", week.Friday);
                        cmd.Parameters.AddWithValue("@Saturday", week.Saturday);
                        cmd.Parameters.AddWithValue("@Sunday", week.Sunday);
                    }
                    else if (schedule.Schedule is MonthlyDaySchedule)
                        cmd.CommandText = "INSERT INTO [ScheduleMonthByDay] (Id) VALUES (@Id)";
                    else if (schedule.Schedule is MonthlySchedule)
                        cmd.CommandText = "INSERT INTO [ScheduleMonth] (Id) VALUES (@Id)";
                    else if (schedule.Schedule is YearlySchedule)
                        cmd.CommandText = "INSERT INTO [ScheduleYear] (Id) VALUES (@Id)";

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private ISchedule Generate(SqlDataReader reader)
        {
            ISchedule result;

            if (!IsDBNull(reader, "sd.Id"))
                result = new DailySchedule();
            else if (!IsDBNull(reader, "sw.Id"))
            {
                var week = new WeeklySchedule();
                week.Monday = GetBoolean(reader, "Monday");
                week.Tuesday = GetBoolean(reader, "Tuesday");
                week.Wednesday = GetBoolean(reader, "Wednesday");
                week.Thursday = GetBoolean(reader, "Thursday");
                week.Friday = GetBoolean(reader, "Friday");
                week.Saturday = GetBoolean(reader, "Saturday");
                week.Sunday = GetBoolean(reader, "Sunday");
                result = week;
            }
            else if (!IsDBNull(reader, "sm.Id"))
                result = new MonthlySchedule();
            else if (!IsDBNull(reader, "smd.Id"))
                result = new MonthlyDaySchedule();
            else if (!IsDBNull(reader, "sy.Id"))
                result = new YearlySchedule();
            else
                return null;

            result.Frequency = GetInt32(reader, "Frequency");
            return result;
        }
    }
}
