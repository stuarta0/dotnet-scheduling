using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduling
{
    public enum Period
    {
        None = 0,

        /// <summary>
        /// A single day.
        /// </summary>
        Day = 1,

        /// <summary>
        /// One week representing 7 days.
        /// </summary>
        Week = 7,

        /// <summary>
        /// One month representing 30 days.
        /// </summary>
        Month = 30,

        /// <summary>
        /// One year representing 365 days.
        /// </summary>
        Year = 365
    }
}
