using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Scheduling.Tests
{
    [TestFixture]
    public class MonthlySchedule_Fixture : TestBase
    {
        private DateTime _start;

        [SetUp]
        public void Setup()
        {
            _start = new DateTime(2009, 2, 21);
        }

        [Test]
        public void Occurrences_before_start()
        {
            // 21/2/2009, 21/8/2010, 21/2/2012, 21/8/2013, 21/2/2015
            MonthlySchedule month = new MonthlySchedule { Frequency = 18 };
            IList<DateTime> occurrences = Convert(month.GetOccurrences(_start, new DateTime(2009, 1, 1), new DateTime(2009, 2, 1)));

            Assert.AreEqual(0, occurrences.Count);
        }

        [Test]
        public void Occurrences_before_start_boundry_case()
        {
            // 21/2/2009, 21/8/2010, 21/2/2012, 21/8/2013, 21/2/2015
            MonthlySchedule month = new MonthlySchedule { Frequency = 18 };
            IList<DateTime> occurrences = Convert(month.GetOccurrences(_start, new DateTime(2009, 1, 1), new DateTime(2009, 2, 21)));

            Assert.AreEqual(1, occurrences.Count);
            Assert.AreEqual(new DateTime(2009, 2, 21), occurrences[0]);
        }

        [Test]
        public void Occurrences_crossing_start()
        {
            // 21/2/2009, 21/8/2010, 21/2/2012, 21/8/2013, 21/2/2015
            MonthlySchedule month = new MonthlySchedule { Frequency = 18 };
            IList<DateTime> occurrences = Convert(month.GetOccurrences(_start, new DateTime(2008, 1, 1), new DateTime(2010, 12, 31)));

            Assert.AreEqual(2, occurrences.Count);
            Assert.AreEqual(new DateTime(2009, 2, 21), occurrences[0]);
            Assert.AreEqual(new DateTime(2010, 8, 21), occurrences[1]);
        }

        [Test]
        public void Occurrences_within_period()
        {
            // 21/2/2009, 21/8/2010, 21/2/2012, 21/8/2013, 21/2/2015
            MonthlySchedule month = new MonthlySchedule { Frequency = 18 };
            IList<DateTime> occurrences = Convert(month.GetOccurrences(_start, new DateTime(2010, 1, 1), new DateTime(2014, 12, 31)));

            Assert.AreEqual(3, occurrences.Count);
            Assert.AreEqual(new DateTime(2010, 8, 21), occurrences[0]);
            Assert.AreEqual(new DateTime(2012, 2, 21), occurrences[1]);
            Assert.AreEqual(new DateTime(2013, 8, 21), occurrences[2]);
        }

        [Test]
        public void Occurrences_stress_test()
        {
            var d = new DateTime(1400, 1, 1);
            MonthlySchedule month = new MonthlySchedule { Frequency = 1 };

            // 1/1/1900 (Monday) -> 31/12/1999 (Friday) = 36524 total days; 7 days in every 21 for 100 years; 36524 - 14 days = 36510 / 3 = 12170
            IList<DateTime> occurrences = Convert(month.GetOccurrences(d, d, new DateTime(2399, 12, 31)));

            Assert.AreEqual(12000, occurrences.Count);
        }

        [Test]
        public void Next_occurrence_before_start()
        {
            // 21/2/2009, 21/8/2010, 21/2/2012, 21/8/2013, 21/2/2015
            MonthlySchedule month = new MonthlySchedule { Frequency = 18 };
            DateTime? next = month.NextOccurrence(_start, new DateTime(2000, 1, 1));

            Assert.IsTrue(next.HasValue);
            Assert.AreEqual(new DateTime(2009, 2, 21), next.Value);
        }

        [Test]
        public void Next_occurrence_during_period()
        {
            // 21/2/2009, 21/8/2010, 21/2/2012, 21/8/2013, 21/2/2015
            MonthlySchedule month = new MonthlySchedule { Frequency = 18 };
            DateTime? next = month.NextOccurrence(_start, new DateTime(2013, 1, 1));

            Assert.IsTrue(next.HasValue);
            Assert.AreEqual(new DateTime(2013, 8, 21), next.Value);
        }

        [Test]
        public void Occurrences_day_of_month_first_with_time_component()
        {
            var month = new MonthlySchedule { Frequency = 1 };
            IList<DateTime> occurrences = Convert(month.GetOccurrences(
                new DateTime(2011, 8, 1, 14, 30, 0),
                new DateTime(2011, 8, 17), 
                new DateTime(2011, 10, 1, 11, 0, 0)));

            Assert.AreEqual(1, occurrences.Count);
            Assert.AreEqual(new DateTime(2011, 9, 1, 14, 30, 0), occurrences[0]);
        }
    }
}
