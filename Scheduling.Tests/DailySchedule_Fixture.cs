using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduling.Tests
{
    [TestClass]
    public class DailySchedule_Fixture
    {
        private DateTime _start;

        public DailySchedule_Fixture()
        {
            _start = new DateTime(2010, 4, 20);
        }

        [TestMethod]
        public void Occurrences_before_start()
        {
            // 20/4, 23/4, 26/4, 29/4, 2/5, 5/5, 8/5
            DailySchedule day = new DailySchedule { Frequency = 3 };
            IList<DateTime> occurrences = day.GetOccurrences(_start, new DateTime(2010, 4, 1), new DateTime(2010, 4, 10)).ToList();
            
            Assert.AreEqual(0, occurrences.Count);
        }

        [TestMethod]
        public void Occurrences_before_start_boundry_case()
        {
            // 20/4, 23/4, 26/4, 29/4, 2/5, 5/5, 8/5
            DailySchedule day = new DailySchedule { Frequency = 3 };
            IList<DateTime> occurrences = day.GetOccurrences(_start, new DateTime(2010, 4, 1), new DateTime(2010, 4, 20)).ToList();

            Assert.AreEqual(1, occurrences.Count);
            Assert.AreEqual(occurrences[0], new DateTime(2010, 4, 20));
        }

        [TestMethod]
        public void Occurrences_crossing_start()
        {
            // 20/4, 23/4, 26/4, 29/4, 2/5, 5/5, 8/5
            DailySchedule day = new DailySchedule { Frequency = 3 };
            IList<DateTime> occurrences = day.GetOccurrences(_start, new DateTime(2010, 4, 1), new DateTime(2010, 4, 27)).ToList();

            Assert.AreEqual(3, occurrences.Count);
            Assert.AreEqual(occurrences[0], new DateTime(2010, 4, 20));
            Assert.AreEqual(occurrences[1], new DateTime(2010, 4, 23));
            Assert.AreEqual(occurrences[2], new DateTime(2010, 4, 26));
        }

        [TestMethod]
        public void Occurrences_within_period()
        {
            // 20/4, 23/4, 26/4, 29/4, 2/5, 5/5, 8/5
            DailySchedule day = new DailySchedule { Frequency = 3 };
            IList<DateTime> occurrences = day.GetOccurrences(_start, new DateTime(2010, 4, 21), new DateTime(2010, 5, 2)).ToList();

            Assert.AreEqual(4, occurrences.Count);
            Assert.AreEqual(occurrences[0], new DateTime(2010, 4, 23));
            Assert.AreEqual(occurrences[1], new DateTime(2010, 4, 26));
            Assert.AreEqual(occurrences[2], new DateTime(2010, 4, 29));
            Assert.AreEqual(occurrences[3], new DateTime(2010, 5, 2));
        }

        [TestMethod]
        public void Occurrences_stress_test()
        {
            DailySchedule day = new DailySchedule { Frequency = 1 };
            var d = new DateTime(1990, 1, 1);

            // 1/1/1990 -> 1/1/2010 = 365 * 20 + 5 extra days due to leap years (1992, 1996, 2000, 2004, 2008)
            IList<DateTime> occurrences = day.GetOccurrences(d, d, new DateTime(2009, 12, 31)).ToList();

            Assert.AreEqual(7305, occurrences.Count);
        }

        [TestMethod]
        public void Next_occurrence_before_start()
        {
            // 20/4, 23/4, 26/4, 29/4, 2/5, 5/5, 8/5
            DailySchedule day = new DailySchedule { Frequency = 3 };
            DateTime? next = day.NextOccurrence(_start, new DateTime(2010, 1, 1));

            Assert.IsTrue(next.HasValue);
            Assert.AreEqual(_start, next.Value);
        }

        [TestMethod]
        public void Next_occurrence_during_period()
        {
            // 20/4, 23/4, 26/4, 29/4, 2/5, 5/5, 8/5
            DailySchedule day = new DailySchedule { Frequency = 3 };
            DateTime? next = day.NextOccurrence(_start, new DateTime(2010, 4, 24));

            Assert.IsTrue(next.HasValue);
            Assert.AreEqual(new DateTime(2010, 4, 26), next.Value);
        }
    }
}
