using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduling
{
    public class OneOffSchedule : Schedule
    {
        public override string Description
        {
            get
            {
                // Occurs on 12 March 2010
                return String.Concat("Occurs on ", StartDate.ToString("d MMMM yyyy"));
            }
        }

        public OneOffSchedule()
            : base()
        {
            Period = Period.None;
        }

        public override IList<DateTime> GetOccurences(DateTime start, DateTime end)
        {
            List<DateTime> list = new List<DateTime>();

            if (StartDate >= start && StartDate <= end)
                list.Add(StartDate);

            return list;
        }
    }
}
