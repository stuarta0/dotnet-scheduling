using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduling.Tests
{
    [TestClass]
    public class WeeklySchedule_Fixture
    {
        private DateTime _start;

        public WeeklySchedule_Fixture()
        {
            _start = new DateTime(2010, 4, 1);
        }

        [TestMethod]
        public void Occurrences_before_start()
        {
            // 1/4 -> 12/4, 15/4 -> 26/4, 29/4
            WeeklySchedule week = new WeeklySchedule { Frequency = 2, Monday = true, Thursday = true };
            IList<DateTime> occurrences = week.GetOccurrences(_start, new DateTime(2010, 3, 1), new DateTime(2010, 3, 31)).ToList();
            
            Assert.AreEqual(0, occurrences.Count);
        }

        [TestMethod]
        public void Occurrences_before_start_boundry_case()
        {
            // 1/4 -> 12/4, 15/4 -> 26/4, 29/4
            WeeklySchedule week = new WeeklySchedule { Frequency = 2, Monday = true, Thursday = true };
            IList<DateTime> occurrences = week.GetOccurrences(_start, new DateTime(2010, 3, 1), new DateTime(2010, 4, 1)).ToList();

            Assert.AreEqual(1, occurrences.Count);
            Assert.AreEqual(occurrences[0], new DateTime(2010, 4, 1));
        }

        [TestMethod]
        public void Occurrences_crossing_start()
        {
            // 1/4 -> 12/4, 15/4 -> 26/4, 29/4
            WeeklySchedule week = new WeeklySchedule { Frequency = 2, Monday = true, Thursday = true };
            IList<DateTime> occurrences = week.GetOccurrences(_start, new DateTime(2010, 3, 20), new DateTime(2010, 4, 28)).ToList();

            Assert.AreEqual(4, occurrences.Count);
            Assert.AreEqual(occurrences[0], new DateTime(2010, 4, 1));
            Assert.AreEqual(occurrences[1], new DateTime(2010, 4, 12));
            Assert.AreEqual(occurrences[2], new DateTime(2010, 4, 15));
            Assert.AreEqual(occurrences[3], new DateTime(2010, 4, 26));
        }

        [TestMethod]
        public void Occurrences_within_period()
        {
            // 1/4 -> 12/4, 15/4 -> 26/4, 29/4
            WeeklySchedule week = new WeeklySchedule { Frequency = 2, Monday = true, Thursday = true };
            IList<DateTime> occurrences = week.GetOccurrences(_start, new DateTime(2010, 4, 9), new DateTime(2010, 4, 28)).ToList();

            Assert.AreEqual(3, occurrences.Count);
            Assert.AreEqual(occurrences[0], new DateTime(2010, 4, 12));
            Assert.AreEqual(occurrences[1], new DateTime(2010, 4, 15));
            Assert.AreEqual(occurrences[2], new DateTime(2010, 4, 26));
        }

        [TestMethod]
        public void Occurrences_stress_test()
        {
            var d = new DateTime(1900, 1, 1);
            WeeklySchedule week = new WeeklySchedule { Frequency = 3, Sunday = true, Monday = true, Tuesday = true, Wednesday = true, Thursday = true, Friday = true, Saturday = true };

            // 1/1/1900 (Monday) -> 31/12/1999 (Friday) = 36524 total days; 7 days in every 21 for 100 years; 36524 - 14 days = 36510 / 3 = 12170
            IList<DateTime> occurrences = week.GetOccurrences(d, d, new DateTime(1999, 12, 31)).ToList();

            Assert.AreEqual(12178, occurrences.Count);
        }

        [TestMethod]
        public void Next_occurrence_before_start()
        {
            // 1/4 -> 12/4, 15/4 -> 26/4, 29/4
            WeeklySchedule week = new WeeklySchedule {Frequency = 2, Monday = true, Thursday = true };
            DateTime? next = week.NextOccurrence(_start, new DateTime(2010, 1, 1));

            Assert.IsTrue(next.HasValue);
            Assert.AreEqual(new DateTime(2010, 4, 1), next.Value);
        }

        [TestMethod]
        public void Next_occurrence_during_period()
        {
            // 1/4 -> 12/4, 15/4 -> 26/4, 29/4
            WeeklySchedule week = new WeeklySchedule { Frequency = 2, Monday = true, Thursday = true };
            DateTime? next = week.NextOccurrence(_start, new DateTime(2010, 4, 2));

            Assert.IsTrue(next.HasValue);
            Assert.AreEqual(new DateTime(2010, 4, 12), next.Value);
        }

        [TestMethod]
        public void Occurrences_with_time()
        {
            // 17/8 14:30, 19/8 14:30 -> 22/8 14:30
            WeeklySchedule week = new WeeklySchedule { Frequency = 1, Monday = true, Wednesday = true, Friday = true };
            IList<DateTime> occurrences = week.GetOccurrences(
                new DateTime(2011, 8, 1, 14, 30, 0), 
                new DateTime(2011, 8, 17, 0, 0, 0), 
                new DateTime(2011, 8, 24, 11, 0, 0)).ToList();

            Assert.AreEqual(3, occurrences.Count);
            Assert.AreEqual(occurrences[0], new DateTime(2011, 8, 17, 14, 30, 0));
            Assert.AreEqual(occurrences[1], new DateTime(2011, 8, 19, 14, 30, 0));
            Assert.AreEqual(occurrences[2], new DateTime(2011, 8, 22, 14, 30, 0));
        }

        [TestMethod]
        public void Occurrences_without_time()
        {
            // 17/8, 19/8 -> 22/8, 24/8
            WeeklySchedule week = new WeeklySchedule { Frequency = 1, Monday = true, Wednesday = true, Friday = true };
            IList<DateTime> occurrences = week.GetOccurrences(
                new DateTime(2011, 8, 1), 
                new DateTime(2011, 8, 17),
                new DateTime(2011, 8, 24)).ToList();

            Assert.AreEqual(4, occurrences.Count);
            Assert.AreEqual(occurrences[0], new DateTime(2011, 8, 17));
            Assert.AreEqual(occurrences[1], new DateTime(2011, 8, 19));
            Assert.AreEqual(occurrences[2], new DateTime(2011, 8, 22));
            Assert.AreEqual(occurrences[3], new DateTime(2011, 8, 24));
        }
    }
}
