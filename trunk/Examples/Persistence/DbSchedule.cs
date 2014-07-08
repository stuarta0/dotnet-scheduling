using Scheduling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Examples.Persistence
{
    /// <summary>
    /// An example of how you might persist a schedule.  
    /// 
    /// With Domain Driven Design, the schedule is considered a value object, therefore it's properties 
    /// would be persisted with the entity object (this class).
    /// </summary>
    public class DbSchedule
    {
        /// <summary>
        /// Primary key of schedule in database.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The actual schedule.  Depending on the database mapping this could either be stored in one table, or create many tables with 1-to-1 key representing each schedule type.
        /// </summary>
        public ISchedule Schedule { get; set; }

        /// <summary>
        /// Date created for row in database.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Date modified for row in database.
        /// </summary>
        public DateTime Modified { get; set; }

        public DbSchedule()
        { }
    }
}
