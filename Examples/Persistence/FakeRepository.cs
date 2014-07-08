using Scheduling;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Examples.Persistence
{
    /// <summary>
    /// Used for testing purposes. Must call Create() before calling Read().
    /// </summary>
    class FakeRepository : Repository
    {
        private DbSchedule _schedule;

        public FakeRepository()
        { }

        public override DbSchedule Read(int id)
        {
            Console.WriteLine("Repository.Read({0})", id);
            return _schedule;
        }

        public override void Create(DbSchedule schedule)
        {
            Console.WriteLine("Repository.Create({0})", schedule.Id);
            _schedule = schedule;
        }
    }
}
