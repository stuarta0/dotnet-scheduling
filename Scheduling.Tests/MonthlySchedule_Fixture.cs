using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Scheduling.Tests
{
    [TestClass]
    public class MonthlySchedule_Fixture
    {
        private DateTime _start;

        public MonthlySchedule_Fixture()
        {
            _start = new DateTime(2009, 2, 21);
        }

        [TestMethod]
        public void Occurrences_before_start()
        {
            // 21/2/2009, 21/8/2010, 21/2/2012, 21/8/2013, 21/2/2015
            MonthlySchedule month = new MonthlySchedule { Frequency = 18 };
            IList<DateTime> occurrences = month.GetOccurrences(_start, new DateTime(2009, 1, 1), new DateTime(2009, 2, 1)).ToList();

            Assert.AreEqual(0, occurrences.Count);
        }

        [TestMethod]
        public void Occurrences_before_start_boundry_case()
        {
            // 21/2/2009, 21/8/2010, 21/2/2012, 21/8/2013, 21/2/2015
            MonthlySchedule month = new MonthlySchedule { Frequency = 18 };
            IList<DateTime> occurrences = month.GetOccurrences(_start, new DateTime(2009, 1, 1), new DateTime(2009, 2, 21)).ToList();

            Assert.AreEqual(1, occurrences.Count);
            Assert.AreEqual(new DateTime(2009, 2, 21), occurrences[0]);
        }

        [TestMethod]
        public void Occurrences_crossing_start()
        {
            // 21/2/2009, 21/8/2010, 21/2/2012, 21/8/2013, 21/2/2015
            MonthlySchedule month = new MonthlySchedule { Frequency = 18 };
            IList<DateTime> occurrences = month.GetOccurrences(_start, new DateTime(2008, 1, 1), new DateTime(2010, 12, 31)).ToList();

            Assert.AreEqual(2, occurrences.Count);
            Assert.AreEqual(new DateTime(2009, 2, 21), occurrences[0]);
            Assert.AreEqual(new DateTime(2010, 8, 21), occurrences[1]);
        }

        [TestMethod]
        public void Occurrences_within_period()
        {
            // 21/2/2009, 21/8/2010, 21/2/2012, 21/8/2013, 21/2/2015
            MonthlySchedule month = new MonthlySchedule { Frequency = 18 };
            IList<DateTime> occurrences = month.GetOccurrences(_start, new DateTime(2010, 1, 1), new DateTime(2014, 12, 31)).ToList();

            Assert.AreEqual(3, occurrences.Count);
            Assert.AreEqual(new DateTime(2010, 8, 21), occurrences[0]);
            Assert.AreEqual(new DateTime(2012, 2, 21), occurrences[1]);
            Assert.AreEqual(new DateTime(2013, 8, 21), occurrences[2]);
        }

        [TestMethod]
        public void Occurrences_stress_test()
        {
            var d = new DateTime(1400, 1, 1);
            MonthlySchedule month = new MonthlySchedule { Frequency = 1 };

            // 1/1/1900 (Monday) -> 31/12/1999 (Friday) = 36524 total days; 7 days in every 21 for 100 years; 36524 - 14 days = 36510 / 3 = 12170
            IList<DateTime> occurrences = month.GetOccurrences(d, d, new DateTime(2399, 12, 31)).ToList();

            Assert.AreEqual(12000, occurrences.Count);
        }

        [TestMethod]
        public void Next_occurrence_before_start()
        {
            // 21/2/2009, 21/8/2010, 21/2/2012, 21/8/2013, 21/2/2015
            MonthlySchedule month = new MonthlySchedule { Frequency = 18 };
            DateTime? next = month.NextOccurrence(_start, new DateTime(2000, 1, 1));

            Assert.IsTrue(next.HasValue);
            Assert.AreEqual(new DateTime(2009, 2, 21), next.Value);
        }

        [TestMethod]
        public void Next_occurrence_during_period()
        {
            // 21/2/2009, 21/8/2010, 21/2/2012, 21/8/2013, 21/2/2015
            MonthlySchedule month = new MonthlySchedule { Frequency = 18 };
            DateTime? next = month.NextOccurrence(_start, new DateTime(2013, 1, 1));

            Assert.IsTrue(next.HasValue);
            Assert.AreEqual(new DateTime(2013, 8, 21), next.Value);
        }

        [TestMethod]
        public void Occurrences_day_of_month_first_with_time_component()
        {
            var month = new MonthlySchedule { Frequency = 1 };
            IList<DateTime> occurrences = month.GetOccurrences(
                new DateTime(2011, 8, 1, 14, 30, 0),
                new DateTime(2011, 8, 17),
                new DateTime(2011, 10, 1, 11, 0, 0)).ToList();

            Assert.AreEqual(1, occurrences.Count);
            Assert.AreEqual(new DateTime(2011, 9, 1, 14, 30, 0), occurrences[0]);
        }

        [TestMethod]
        public void Occurrences_day_of_month_last_with_time_component()
        {
            var month = new MonthlySchedule { Frequency = 1 };
            IList<DateTime> occurrences = month.GetOccurrences(
                new DateTime(2020, 1, 31, 14, 30, 0),
                new DateTime(2020, 1, 31),
                new DateTime(2021, 1, 1)).ToList();

            Assert.AreEqual(12, occurrences.Count);
            Assert.AreEqual(new DateTime(2020, 1, 31, 14, 30, 0), occurrences[0]);
            Assert.AreEqual(new DateTime(2020, 2, 29, 14, 30, 0), occurrences[1]);
            Assert.AreEqual(new DateTime(2020, 3, 31, 14, 30, 0), occurrences[2]);
            Assert.AreEqual(new DateTime(2020, 4, 30, 14, 30, 0), occurrences[3]);
        }
    }
}
