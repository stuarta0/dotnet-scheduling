using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Scheduling.Tests
{
    [TestFixture]
    public class WeeklySchedule_Fixture
    {
        [Test]
        public void Occurrences_before_start()
        {
            // 1/4 -> 12/4, 15/4 -> 26/4, 29/4
            WeeklySchedule week = new WeeklySchedule { StartDate = new DateTime(2010, 4, 1), Frequency = 2, Monday = true, Thursday = true };
            IList<DateTime> occurrences = week.GetOccurences(new DateTime(2010, 3, 1), new DateTime(2010, 3, 31));
            
            Assert.AreEqual(0, occurrences.Count);
        }

        [Test]
        public void Occurrences_before_start_boundry_case()
        {
            // 1/4 -> 12/4, 15/4 -> 26/4, 29/4
            WeeklySchedule week = new WeeklySchedule { StartDate = new DateTime(2010, 4, 1), Frequency = 2, Monday = true, Thursday = true };
            IList<DateTime> occurrences = week.GetOccurences(new DateTime(2010, 3, 1), new DateTime(2010, 4, 1));

            Assert.AreEqual(1, occurrences.Count);
            Assert.AreEqual(occurrences[0], new DateTime(2010, 4, 1));
        }

        [Test]
        public void Occurrences_crossing_start()
        {
            // 1/4 -> 12/4, 15/4 -> 26/4, 29/4
            WeeklySchedule week = new WeeklySchedule { StartDate = new DateTime(2010, 4, 1), Frequency = 2, Monday = true, Thursday = true };
            IList<DateTime> occurrences = week.GetOccurences(new DateTime(2010, 3, 20), new DateTime(2010, 4, 28));

            Assert.AreEqual(4, occurrences.Count);
            Assert.AreEqual(occurrences[0], new DateTime(2010, 4, 1));
            Assert.AreEqual(occurrences[1], new DateTime(2010, 4, 12));
            Assert.AreEqual(occurrences[2], new DateTime(2010, 4, 15));
            Assert.AreEqual(occurrences[3], new DateTime(2010, 4, 26));
        }

        [Test]
        public void Occurrences_within_period()
        {
            // 1/4 -> 12/4, 15/4 -> 26/4, 29/4
            WeeklySchedule week = new WeeklySchedule { StartDate = new DateTime(2010, 4, 1), Frequency = 2, Monday = true, Thursday = true };
            IList<DateTime> occurrences = week.GetOccurences(new DateTime(2010, 4, 9), new DateTime(2010, 4, 28));

            Assert.AreEqual(3, occurrences.Count);
            Assert.AreEqual(occurrences[0], new DateTime(2010, 4, 12));
            Assert.AreEqual(occurrences[1], new DateTime(2010, 4, 15));
            Assert.AreEqual(occurrences[2], new DateTime(2010, 4, 26));
        }

        [Test]
        public void Occurrences_after_end()
        {
            // 1/4 -> 12/4, 15/4 -> 26/4, 29/4
            WeeklySchedule week = new WeeklySchedule { StartDate = new DateTime(2010, 4, 1), Frequency = 2, Monday = true, Thursday = true, EndDate = new DateTime(2010, 5, 1) };
            IList<DateTime> occurrences = week.GetOccurences(new DateTime(2010, 6, 1), new DateTime(2010, 7, 1));

            Assert.AreEqual(0, occurrences.Count);
        }

        [Test]
        public void Occurrences_after_end_boundry_case()
        {
            // 1/4 -> 12/4, 15/4 -> 26/4, 29/4
            WeeklySchedule week = new WeeklySchedule { StartDate = new DateTime(2010, 4, 1), Frequency = 2, Monday = true, Thursday = true, EndDate = new DateTime(2010, 5, 1) };
            IList<DateTime> occurrences = week.GetOccurences(new DateTime(2010, 4, 29), new DateTime(2010, 7, 1));

            Assert.AreEqual(1, occurrences.Count);
            Assert.AreEqual(occurrences[0], new DateTime(2010, 4, 29));
        }

        [Test]
        public void Occurrences_crossing_end()
        {
            // 1/4 -> 12/4, 15/4 -> 26/4, 29/4
            WeeklySchedule week = new WeeklySchedule { StartDate = new DateTime(2010, 4, 1), Frequency = 2, Monday = true, Thursday = true, EndDate = new DateTime(2010, 5, 1) };
            IList<DateTime> occurrences = week.GetOccurences(new DateTime(2010, 4, 20), new DateTime(2010, 7, 1));

            Assert.AreEqual(2, occurrences.Count);
            Assert.AreEqual(occurrences[0], new DateTime(2010, 4, 26));
            Assert.AreEqual(occurrences[1], new DateTime(2010, 4, 29));
        }

        [Test]
        public void Occurrences_stress_test()
        {
            WeeklySchedule week = new WeeklySchedule { StartDate = new DateTime(1900, 1, 1), Frequency = 3, Sunday = true, Monday = true, Tuesday = true, Wednesday = true, Thursday = true, Friday = true, Saturday = true };

            // 1/1/1900 (Monday) -> 31/12/1999 (Friday) = 36524 total days; 7 days in every 21 for 100 years; 36524 - 14 days = 36510 / 3 = 12170
            IList<DateTime> occurrences = week.GetOccurences(new DateTime(1900, 1, 1), new DateTime(1999, 12, 31));

            Assert.AreEqual(12178, occurrences.Count);
        }

        [Test]
        public void Next_occurrence_before_start()
        {
            // 1/4 -> 12/4, 15/4 -> 26/4, 29/4
            WeeklySchedule week = new WeeklySchedule { StartDate = new DateTime(2010, 4, 1), Frequency = 2, Monday = true, Thursday = true, EndDate = new DateTime(2010, 5, 1) };
            DateTime? next = week.NextOccurrence(new DateTime(2010, 1, 1));

            Assert.IsTrue(next.HasValue);
            Assert.AreEqual(new DateTime(2010, 4, 1), next.Value);
        }

        [Test]
        public void Next_occurrence_during_period()
        {
            // 1/4 -> 12/4, 15/4 -> 26/4, 29/4
            WeeklySchedule week = new WeeklySchedule { StartDate = new DateTime(2010, 4, 1), Frequency = 2, Monday = true, Thursday = true, EndDate = new DateTime(2010, 5, 1) };
            DateTime? next = week.NextOccurrence(new DateTime(2010, 4, 2));

            Assert.IsTrue(next.HasValue);
            Assert.AreEqual(new DateTime(2010, 4, 12), next.Value);
        }

        [Test]
        public void Next_occurrence_after_end()
        {
            // 1/4 -> 12/4, 15/4 -> 26/4, 29/4
            WeeklySchedule week = new WeeklySchedule { StartDate = new DateTime(2010, 4, 1), Frequency = 2, Monday = true, Thursday = true, EndDate = new DateTime(2010, 5, 1) };
            DateTime? next = week.NextOccurrence(new DateTime(2010, 6, 1));

            Assert.IsFalse(next.HasValue);
        }
    }
}
