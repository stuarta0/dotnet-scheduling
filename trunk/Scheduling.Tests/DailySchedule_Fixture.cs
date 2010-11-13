using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Scheduling.Tests
{
    [TestFixture]
    public class DailySchedule_Fixture
    {
        [Test]
        public void Occurrences_before_start()
        {
            // 20/4, 23/4, 26/4, 29/4, 2/5, 5/5, 8/5
            DailySchedule day = new DailySchedule { StartDate = new DateTime(2010, 4, 20), Frequency = 3 };
            IList<DateTime> occurrences = day.GetOccurences(new DateTime(2010, 4, 1), new DateTime(2010, 4, 10));

            Assert.AreEqual(0, occurrences.Count);
        }

        [Test]
        public void Occurrences_before_start_boundry_case()
        {
            // 20/4, 23/4, 26/4, 29/4, 2/5, 5/5, 8/5
            DailySchedule day = new DailySchedule { StartDate = new DateTime(2010, 4, 20), Frequency = 3 };
            IList<DateTime> occurrences = day.GetOccurences(new DateTime(2010, 4, 1), new DateTime(2010, 4, 20));

            Assert.AreEqual(1, occurrences.Count);
            Assert.AreEqual(occurrences[0], new DateTime(2010, 4, 20));
        }

        [Test]
        public void Occurrences_crossing_start()
        {
            // 20/4, 23/4, 26/4, 29/4, 2/5, 5/5, 8/5
            DailySchedule day = new DailySchedule { StartDate = new DateTime(2010, 4, 20), Frequency = 3 };
            IList<DateTime> occurrences = day.GetOccurences(new DateTime(2010, 4, 1), new DateTime(2010, 4, 27));

            Assert.AreEqual(3, occurrences.Count);
            Assert.AreEqual(occurrences[0], new DateTime(2010, 4, 20));
            Assert.AreEqual(occurrences[1], new DateTime(2010, 4, 23));
            Assert.AreEqual(occurrences[2], new DateTime(2010, 4, 26));
        }

        [Test]
        public void Occurrences_within_period()
        {
            // 20/4, 23/4, 26/4, 29/4, 2/5, 5/5, 8/5
            DailySchedule day = new DailySchedule { StartDate = new DateTime(2010, 4, 20), Frequency = 3 };
            IList<DateTime> occurrences = day.GetOccurences(new DateTime(2010, 4, 21), new DateTime(2010, 5, 2));

            Assert.AreEqual(4, occurrences.Count);
            Assert.AreEqual(occurrences[0], new DateTime(2010, 4, 23));
            Assert.AreEqual(occurrences[1], new DateTime(2010, 4, 26));
            Assert.AreEqual(occurrences[2], new DateTime(2010, 4, 29));
            Assert.AreEqual(occurrences[3], new DateTime(2010, 5, 2));
        }

        [Test]
        public void Occurrences_after_end()
        {
            // 20/4, 23/4, 26/4, 29/4, 2/5, 5/5, 8/5
            DailySchedule day = new DailySchedule { StartDate = new DateTime(2010, 4, 20), Frequency = 3, EndDate = new DateTime(2010, 5, 3) };
            IList<DateTime> occurrences = day.GetOccurences(new DateTime(2010, 6, 1), new DateTime(2010, 7, 1));

            Assert.AreEqual(0, occurrences.Count);
        }

        [Test]
        public void Occurrences_after_end_boundry_case()
        {
            // 20/4, 23/4, 26/4, 29/4, 2/5, 5/5, 8/5
            DailySchedule day = new DailySchedule { StartDate = new DateTime(2010, 4, 20), Frequency = 3, EndDate = new DateTime(2010, 5, 3) };
            IList<DateTime> occurrences = day.GetOccurences(new DateTime(2010, 5, 2), new DateTime(2010, 7, 1));

            Assert.AreEqual(1, occurrences.Count);
            Assert.AreEqual(occurrences[0], new DateTime(2010, 5, 2));
        }

        [Test]
        public void Occurrences_crossing_end()
        {
            // 20/4, 23/4, 26/4, 29/4, 2/5, 5/5, 8/5
            DailySchedule day = new DailySchedule { StartDate = new DateTime(2010, 4, 20), Frequency = 3, EndDate = new DateTime(2010, 5, 5) };
            IList<DateTime> occurrences = day.GetOccurences(new DateTime(2010, 5, 1), new DateTime(2010, 7, 1));

            Assert.AreEqual(2, occurrences.Count);
            Assert.AreEqual(occurrences[0], new DateTime(2010, 5, 2));
            Assert.AreEqual(occurrences[1], new DateTime(2010, 5, 5));
        }

        [Test]
        public void Occurrences_stress_test()
        {
            DailySchedule day = new DailySchedule { StartDate = new DateTime(1990, 1, 1), Frequency = 1 };

            // 1/1/1990 -> 1/1/2010 = 365 * 20 + 5 extra days due to leap years (1992, 96, 00, 04, 08)
            IList<DateTime> occurrences = day.GetOccurences(new DateTime(1990, 1, 1), new DateTime(2009, 12, 31));

            Assert.AreEqual(7305, occurrences.Count);
        }

        [Test]
        public void Next_occurrence_before_start()
        {
            // 20/4, 23/4, 26/4, 29/4, 2/5, 5/5, 8/5
            DailySchedule day = new DailySchedule { StartDate = new DateTime(2010, 4, 20), Frequency = 3, EndDate = new DateTime(2010, 5, 3) };
            DateTime? next = day.NextOccurrence(new DateTime(2010, 1, 1));

            Assert.IsTrue(next.HasValue);
            Assert.AreEqual(new DateTime(2010, 4, 20), next.Value);
        }

        [Test]
        public void Next_occurrence_during_period()
        {
            // 20/4, 23/4, 26/4, 29/4, 2/5, 5/5, 8/5
            DailySchedule day = new DailySchedule { StartDate = new DateTime(2010, 4, 20), Frequency = 3, EndDate = new DateTime(2010, 5, 3) };
            DateTime? next = day.NextOccurrence(new DateTime(2010, 4, 24));

            Assert.IsTrue(next.HasValue);
            Assert.AreEqual(new DateTime(2010, 4, 26), next.Value);
        }

        [Test]
        public void Next_occurrence_after_end()
        {
            // 20/4, 23/4, 26/4, 29/4, 2/5, 5/5, 8/5
            DailySchedule day = new DailySchedule { StartDate = new DateTime(2010, 4, 20), Frequency = 3, EndDate = new DateTime(2010, 5, 3) };
            DateTime? next = day.NextOccurrence(new DateTime(2010, 6, 1));

            Assert.IsFalse(next.HasValue);
        }
    }
}
