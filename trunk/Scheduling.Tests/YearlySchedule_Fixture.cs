using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Scheduling.Tests
{
    [TestFixture]
    public class YearlySchedule_Fixture : TestBase
    {
        private DateTime _start;

        [SetUp]
        public void Setup()
        {
            _start = new DateTime(1983, 11, 3);
        }

        [Test]
        public void Occurrences_before_start()
        {
            // 3/11/1983, 3/11/1990, 3/11/1997, 3/11/2004, 3/11/2011, 3/11/2018, 3/11/2026
            YearlySchedule year = new YearlySchedule { Frequency = 7 };
            IList<DateTime> occurrences = Convert(year.GetOccurrences(_start, new DateTime(1900, 1, 1), new DateTime(1950, 1, 1)));

            Assert.AreEqual(0, occurrences.Count);
        }

        [Test]
        public void Occurrences_before_start_boundry_case()
        {
            // 3/11/1983, 3/11/1990, 3/11/1997, 3/11/2004, 3/11/2011, 3/11/2018, 3/11/2026
            YearlySchedule year = new YearlySchedule { Frequency = 7 };
            IList<DateTime> occurrences = Convert(year.GetOccurrences(_start, new DateTime(1900, 1, 1), new DateTime(1983, 11, 3)));

            Assert.AreEqual(1, occurrences.Count);
            Assert.AreEqual(new DateTime(1983, 11, 3), occurrences[0]);
        }

        [Test]
        public void Occurrences_crossing_start()
        {
            // 3/11/1983, 3/11/1990, 3/11/1997, 3/11/2004, 3/11/2011, 3/11/2018, 3/11/2026
            YearlySchedule year = new YearlySchedule { Frequency = 7 };
            IList<DateTime> occurrences = Convert(year.GetOccurrences(_start, new DateTime(1970, 1, 1), new DateTime(1990, 11, 3)));

            Assert.AreEqual(2, occurrences.Count);
            Assert.AreEqual(new DateTime(1983, 11, 3), occurrences[0]);
            Assert.AreEqual(new DateTime(1990, 11, 3), occurrences[1]);
        }

        [Test]
        public void Occurrences_within_period()
        {
            // 3/11/1983, 3/11/1990, 3/11/1997, 3/11/2004, 3/11/2011, 3/11/2018, 3/11/2026
            YearlySchedule year = new YearlySchedule { Frequency = 7 };
            IList<DateTime> occurrences = Convert(year.GetOccurrences(_start, new DateTime(1985, 3, 1), new DateTime(2005, 1, 1)));

            Assert.AreEqual(3, occurrences.Count);
            Assert.AreEqual(new DateTime(1990, 11, 3), occurrences[0]);
            Assert.AreEqual(new DateTime(1997, 11, 3), occurrences[1]);
            Assert.AreEqual(new DateTime(2004, 11, 3), occurrences[2]);
        }

        [Test]
        public void Occurrences_stress_test()
        {
            var d = new DateTime(900, 1, 1);
            YearlySchedule year = new YearlySchedule { Frequency = 1 };

            // 1/1/1900 (Monday) -> 31/12/1999 (Friday) = 36524 total days; 7 days in every 21 for 100 years; 36524 - 14 days = 36510 / 3 = 12170
            IList<DateTime> occurrences = Convert(year.GetOccurrences(d, d, new DateTime(2899, 12, 31)));

            Assert.AreEqual(2000, occurrences.Count);
        }

        [Test]
        public void Occurrences_with_time()
        {
            // 3/11/1983, 3/11/1990, 3/11/1997, 3/11/2004, 3/11/2011, 3/11/2018, 3/11/2026
            YearlySchedule year = new YearlySchedule { Frequency = 7 };
            IList<DateTime> occurrences = Convert(year.GetOccurrences(
                new DateTime(1983, 11, 3, 18, 0, 0),
                new DateTime(2004, 11, 3, 14, 0, 0),
                new DateTime(2015, 12, 31)));

            Assert.AreEqual(2, occurrences.Count);
            Assert.AreEqual(new DateTime(2004, 11, 3, 18, 0, 0), occurrences[0]);
            Assert.AreEqual(new DateTime(2011, 11, 3, 18, 0, 0), occurrences[1]);
        }

        [Test]
        public void Next_occurrence_before_start()
        {
            // 3/11/1983, 3/11/1990, 3/11/1997, 3/11/2004, 3/11/2011, 3/11/2018, 3/11/2026
            YearlySchedule year = new YearlySchedule { Frequency = 7 };
            DateTime? next = year.NextOccurrence(_start, new DateTime(1957, 12, 7));

            Assert.IsTrue(next.HasValue);
            Assert.AreEqual(new DateTime(1983, 11, 3), next.Value);
        }

        [Test]
        public void Next_occurrence_during_period()
        {
            // 3/11/1983, 3/11/1990, 3/11/1997, 3/11/2004, 3/11/2011, 3/11/2018, 3/11/2026
            YearlySchedule year = new YearlySchedule { Frequency = 7 };
            DateTime? next = year.NextOccurrence(_start, new DateTime(1995, 1, 1));

            Assert.IsTrue(next.HasValue);
            Assert.AreEqual(new DateTime(1997, 11, 3), next.Value);
        }
    }
}
