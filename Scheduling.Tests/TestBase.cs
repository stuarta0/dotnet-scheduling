using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduling.Tests
{
    public class TestBase
    {
        public List<DateTime> Convert(IEnumerable<DateTime> occurrences)
        {
            var l = new List<DateTime>();
            foreach (var d in occurrences)
                l.Add(d);
            return l;
        }
    }
}
