using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Scheduling.Tests
{
    [TestFixture]
    public class MonthlyDaySchedule_Fixture : TestBase
    {
        private DateTime _start;

        [SetUp]
        public void Setup()
        {
            _start = new DateTime(2010, 4, 1);
        }

        [Test]
        public void Occurrences_before_start()
        {
            // 21/2/2009, 21/8/2010, 21/2/2012, 21/8/2013, 21/2/2015
            MonthlyDaySchedule month = new MonthlyDaySchedule { Frequency = 1 };
            IList<DateTime> occurrences = Convert(month.GetOccurrences(_start, new DateTime(2009, 1, 1), new DateTime(2009, 2, 1)));

            Assert.AreEqual(0, occurrences.Count);
        }

        [Test]
        public void Occurrences_before_start_boundry_case()
        {
            // 21/2/2009, 21/8/2010, 21/2/2012, 21/8/2013, 21/2/2015
            MonthlyDaySchedule month = new MonthlyDaySchedule { Frequency = 1 };
            IList<DateTime> occurrences = Convert(month.GetOccurrences(_start, new DateTime(2009, 1, 1), new DateTime(2010, 4, 1)));

            Assert.AreEqual(1, occurrences.Count);
            Assert.AreEqual(new DateTime(2010, 4, 1), occurrences[0]);
        }

        [Test]
        public void Occurrences_crossing_start()
        {
            // 21/2/2009, 21/8/2010, 21/2/2012, 21/8/2013, 21/2/2015
            MonthlyDaySchedule month = new MonthlyDaySchedule { Frequency = 1 };
            IList<DateTime> occurrences = Convert(month.GetOccurrences(_start, new DateTime(2010, 1, 1), new DateTime(2010, 6, 30)));

            Assert.AreEqual(3, occurrences.Count);
            Assert.AreEqual(new DateTime(2010, 4, 1), occurrences[0]);
            Assert.AreEqual(new DateTime(2010, 5, 6), occurrences[1]);
            Assert.AreEqual(new DateTime(2010, 6, 3), occurrences[2]);
        }

        [Test]
        public void Occurrences_within_period()
        {
            // 21/2/2009, 21/8/2010, 21/2/2012, 21/8/2013, 21/2/2015
            MonthlyDaySchedule month = new MonthlyDaySchedule { Frequency = 1 };
            IList<DateTime> occurrences = Convert(month.GetOccurrences(_start, new DateTime(2010, 12, 1), new DateTime(2011, 3, 1)));

            Assert.AreEqual(3, occurrences.Count);
            Assert.AreEqual(new DateTime(2010, 12, 2), occurrences[0]);
            Assert.AreEqual(new DateTime(2011, 1, 6), occurrences[1]);
            Assert.AreEqual(new DateTime(2011, 2, 3), occurrences[2]);
        }

        [Test]
        public void Occurrences_stress_test()
        {
            var d = new DateTime(1000, 1, 1);
            MonthlyDaySchedule month = new MonthlyDaySchedule { Frequency = 1 };
            IList<DateTime> occurrences = Convert(month.GetOccurrences(d, d, new DateTime(2999, 12, 31)));

            Assert.AreEqual(24000, occurrences.Count);
        }

        [Test]
        public void Next_occurrence_before_start()
        {
            // 21/2/2009, 21/8/2010, 21/2/2012, 21/8/2013, 21/2/2015
            MonthlyDaySchedule month = new MonthlyDaySchedule { Frequency = 1 };
            DateTime? next = month.NextOccurrence(_start, new DateTime(2000, 1, 1));

            Assert.IsTrue(next.HasValue);
            Assert.AreEqual(new DateTime(2010, 4, 1), next.Value);
        }

        [Test]
        public void Next_occurrence_during_period()
        {
            // 21/2/2009, 21/8/2010, 21/2/2012, 21/8/2013, 21/2/2015
            MonthlyDaySchedule month = new MonthlyDaySchedule { Frequency = 1 };
            DateTime? next = month.NextOccurrence(_start, new DateTime(2013, 1, 1));

            Assert.IsTrue(next.HasValue);
            Assert.AreEqual(new DateTime(2013, 1, 3), next.Value);
        }
        


        [Test]
        public void Occurrences_first_friday()
        {
            MonthlyDaySchedule month = new MonthlyDaySchedule { Frequency = 1 };
            IList<DateTime> occurrences = Convert(month.GetOccurrences(_start.AddDays(1), new DateTime(2010, 3, 31), new DateTime(2010, 6, 30)));

            Assert.AreEqual(3, occurrences.Count);
            Assert.AreEqual(new DateTime(2010, 4, 2), occurrences[0]);
            Assert.AreEqual(new DateTime(2010, 5, 7), occurrences[1]);
            Assert.AreEqual(new DateTime(2010, 6, 4), occurrences[2]);
        }

        [Test]
        public void Occurrences_first_monday()
        {
            MonthlyDaySchedule month = new MonthlyDaySchedule { Frequency = 2 };
            IList<DateTime> occurrences = Convert(month.GetOccurrences(_start.AddDays(4), new DateTime(2010, 3, 31), new DateTime(2010, 6, 30)));

            Assert.AreEqual(2, occurrences.Count);
            Assert.AreEqual(new DateTime(2010, 4, 5), occurrences[0]);
            Assert.AreEqual(new DateTime(2010, 6, 7), occurrences[1]);
        }

        [Test]
        public void Occurrences_second_thursday()
        {
            MonthlyDaySchedule month = new MonthlyDaySchedule { Frequency = 1 };
            IList<DateTime> occurrences = Convert(month.GetOccurrences(_start.AddDays(7), new DateTime(2010, 3, 31), new DateTime(2010, 6, 30)));

            Assert.AreEqual(3, occurrences.Count);
            Assert.AreEqual(new DateTime(2010, 4, 8), occurrences[0]);
            Assert.AreEqual(new DateTime(2010, 5, 13), occurrences[1]);
            Assert.AreEqual(new DateTime(2010, 6, 10), occurrences[2]);
        }

        [Test]
        public void Occurrences_second_sunday()
        {
            MonthlyDaySchedule month = new MonthlyDaySchedule { Frequency = 2 };
            IList<DateTime> occurrences = Convert(month.GetOccurrences(_start.AddDays(10), new DateTime(2010, 3, 31), new DateTime(2010, 6, 30)));

            Assert.AreEqual(2, occurrences.Count);
            Assert.AreEqual(new DateTime(2010, 4, 11), occurrences[0]);
            Assert.AreEqual(new DateTime(2010, 6, 13), occurrences[1]);
        }

        [Test]
        public void Occurrences_third_saturday()
        {
            MonthlyDaySchedule month = new MonthlyDaySchedule { Frequency = 1 };
            IList<DateTime> occurrences = Convert(month.GetOccurrences(_start.AddDays(16), new DateTime(2010, 3, 31), new DateTime(2010, 6, 30)));

            Assert.AreEqual(3, occurrences.Count);
            Assert.AreEqual(new DateTime(2010, 4, 17), occurrences[0]);
            Assert.AreEqual(new DateTime(2010, 5, 15), occurrences[1]);
            Assert.AreEqual(new DateTime(2010, 6, 19), occurrences[2]);
        }

        [Test]
        public void Occurrences_third_wednesday()
        {
            MonthlyDaySchedule month = new MonthlyDaySchedule { Frequency = 2 };
            IList<DateTime> occurrences = Convert(month.GetOccurrences(_start.AddDays(20), new DateTime(2010, 3, 31), new DateTime(2010, 6, 30)));

            Assert.AreEqual(2, occurrences.Count);
            Assert.AreEqual(new DateTime(2010, 4, 21), occurrences[0]);
            Assert.AreEqual(new DateTime(2010, 6, 16), occurrences[1]);
        }

        [Test]
        public void Occurrences_fourth_friday()
        {
            MonthlyDaySchedule month = new MonthlyDaySchedule { Frequency = 1 };
            IList<DateTime> occurrences = Convert(month.GetOccurrences(_start.AddDays(22), new DateTime(2010, 3, 31), new DateTime(2010, 6, 30)));

            Assert.AreEqual(3, occurrences.Count);
            Assert.AreEqual(new DateTime(2010, 4, 23), occurrences[0]); // 4th
            Assert.AreEqual(new DateTime(2010, 5, 28), occurrences[1]); // 4th + last
            Assert.AreEqual(new DateTime(2010, 6, 25), occurrences[2]); // 4th + last
        }
        // see last tuesday
        //[Test]
        //public void Occurrences_fourth_tuesday()
        //{
        //    MonthlyDaySchedule month = new MonthlyDaySchedule { StartDate = new DateTime(2010, 4, 27), Frequency = 2, ReoccurrenceType = MonthlyDaySchedule.Type.DayOfWeek };
        //    IList<DateTime> occurrences = Convert(month.GetOccurrences(_start, new DateTime(2010, 3, 31), new DateTime(2010, 6, 30));

        //    Assert.AreEqual(2, occurrences.Count);
        //    Assert.AreEqual(new DateTime(2010, 4, 27), occurrences[0]); // 4th + last
        //    Assert.AreEqual(new DateTime(2010, 6, 22), occurrences[1]); // 4th
        //}
        [Test]
        public void Occurrences_last_saturday()
        {
            MonthlyDaySchedule month = new MonthlyDaySchedule { Frequency = 1 };
            IList<DateTime> occurrences = Convert(month.GetOccurrences(_start.AddDays(23), new DateTime(2010, 3, 31), new DateTime(2010, 6, 30)));

            Assert.AreEqual(3, occurrences.Count);
            Assert.AreEqual(new DateTime(2010, 4, 24), occurrences[0]); // 4th + last
            Assert.AreEqual(new DateTime(2010, 5, 29), occurrences[1]); // last
            Assert.AreEqual(new DateTime(2010, 6, 26), occurrences[2]); // 4th + last
        }

        [Test]
        public void Occurrences_last_tuesday()
        {
            MonthlyDaySchedule month = new MonthlyDaySchedule { Frequency = 2 };
            IList<DateTime> occurrences = Convert(month.GetOccurrences(_start.AddDays(26), new DateTime(2010, 3, 31), new DateTime(2010, 6, 30)));

            Assert.AreEqual(2, occurrences.Count);
            Assert.AreEqual(new DateTime(2010, 4, 27), occurrences[0]); // 4th + last
            Assert.AreEqual(new DateTime(2010, 6, 29), occurrences[1]); // last
        }

        [Test]
        public void Occurrences_last_friday()
        {
            MonthlyDaySchedule month = new MonthlyDaySchedule { Frequency = 1 };
            IList<DateTime> occurrences = Convert(month.GetOccurrences(_start.AddDays(29), new DateTime(2010, 3, 31), new DateTime(2010, 6, 30)));

            Assert.AreEqual(3, occurrences.Count);
            Assert.AreEqual(new DateTime(2010, 4, 30), occurrences[0]); // last
            Assert.AreEqual(new DateTime(2010, 5, 28), occurrences[1]); // 4th + last
            Assert.AreEqual(new DateTime(2010, 6, 25), occurrences[2]); // 4th + last
        }


        [Test]
        public void Occurrences_first_tuesday_with_time_component()
        {
            MonthlyDaySchedule month = new MonthlyDaySchedule { Frequency = 1 };
            IList<DateTime> occurrences = Convert(month.GetOccurrences( 
                new DateTime(2011, 8, 2, 14, 30, 0),
                new DateTime(2011, 8, 17), 
                new DateTime(2011, 11, 1, 11, 0, 0)));

            Assert.AreEqual(2, occurrences.Count);
            Assert.AreEqual(new DateTime(2011, 9, 6, 14, 30, 0), occurrences[0]);
            Assert.AreEqual(new DateTime(2011, 10, 4, 14, 30, 0), occurrences[1]);
        }

        [Test]
        public void Occurrences_first_tuesday_with_time_component_afternoon()
        {
            MonthlyDaySchedule month = new MonthlyDaySchedule { Frequency = 1 };
            IList<DateTime> occurrences = Convert(month.GetOccurrences( 
                new DateTime(2011, 8, 2, 14, 30, 0),
                new DateTime(2011, 8, 17), 
                new DateTime(2011, 11, 1, 15, 0, 0)));

            Assert.AreEqual(3, occurrences.Count);
            Assert.AreEqual(new DateTime(2011, 9, 6, 14, 30, 0), occurrences[0]);
            Assert.AreEqual(new DateTime(2011, 10, 4, 14, 30, 0), occurrences[1]);
            Assert.AreEqual(new DateTime(2011, 11, 1, 14, 30, 0), occurrences[2]);
        }

        [Test]
        public void Occurrences_first_tuesday_without_time_component_afternoon()
        {
            MonthlyDaySchedule month = new MonthlyDaySchedule { Frequency = 1 };
            IList<DateTime> occurrences = Convert(month.GetOccurrences( 
                new DateTime(2011, 8, 2),
                new DateTime(2011, 8, 17), 
                new DateTime(2011, 11, 1)));

            Assert.AreEqual(3, occurrences.Count);
            Assert.AreEqual(new DateTime(2011, 9, 6), occurrences[0]);
            Assert.AreEqual(new DateTime(2011, 10, 4), occurrences[1]);
            Assert.AreEqual(new DateTime(2011, 11, 1), occurrences[2]);
        }
    }
}
