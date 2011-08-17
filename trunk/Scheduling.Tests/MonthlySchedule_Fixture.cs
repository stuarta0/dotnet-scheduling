using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Scheduling.Tests
{
    [TestFixture]
    public class MonthlySchedule_Fixture
    {
        [Test]
        public void Occurrences_before_start()
        {
            // 21/2/2009, 21/8/2010, 21/2/2012, 21/8/2013, 21/2/2015
            MonthlySchedule month = new MonthlySchedule { StartDate = new DateTime(2009, 2, 21), Frequency = 18 };
            IList<DateTime> occurrences = month.GetOccurences(new DateTime(2009, 1, 1), new DateTime(2009, 2, 1));

            Assert.AreEqual(0, occurrences.Count);
        }

        [Test]
        public void Occurrences_before_start_boundry_case()
        {
            // 21/2/2009, 21/8/2010, 21/2/2012, 21/8/2013, 21/2/2015
            MonthlySchedule month = new MonthlySchedule { StartDate = new DateTime(2009, 2, 21), Frequency = 18 };
            IList<DateTime> occurrences = month.GetOccurences(new DateTime(2009, 1, 1), new DateTime(2009, 2, 21));

            Assert.AreEqual(1, occurrences.Count);
            Assert.AreEqual(new DateTime(2009, 2, 21), occurrences[0]);
        }

        [Test]
        public void Occurrences_crossing_start()
        {
            // 21/2/2009, 21/8/2010, 21/2/2012, 21/8/2013, 21/2/2015
            MonthlySchedule month = new MonthlySchedule { StartDate = new DateTime(2009, 2, 21), Frequency = 18 };
            IList<DateTime> occurrences = month.GetOccurences(new DateTime(2008, 1, 1), new DateTime(2010, 12, 31));

            Assert.AreEqual(2, occurrences.Count);
            Assert.AreEqual(new DateTime(2009, 2, 21), occurrences[0]);
            Assert.AreEqual(new DateTime(2010, 8, 21), occurrences[1]);
        }

        [Test]
        public void Occurrences_within_period()
        {
            // 21/2/2009, 21/8/2010, 21/2/2012, 21/8/2013, 21/2/2015
            MonthlySchedule month = new MonthlySchedule { StartDate = new DateTime(2009, 2, 21), Frequency = 18 };
            IList<DateTime> occurrences = month.GetOccurences(new DateTime(2010, 1, 1), new DateTime(2014, 12, 31));

            Assert.AreEqual(3, occurrences.Count);
            Assert.AreEqual(new DateTime(2010, 8, 21), occurrences[0]);
            Assert.AreEqual(new DateTime(2012, 2, 21), occurrences[1]);
            Assert.AreEqual(new DateTime(2013, 8, 21), occurrences[2]);
        }

        [Test]
        public void Occurrences_after_end()
        {
            // 21/2/2009, 21/8/2010, 21/2/2012, 21/8/2013, 21/2/2015
            MonthlySchedule month = new MonthlySchedule { StartDate = new DateTime(2009, 2, 21), Frequency = 18, EndDate = new DateTime(2015, 6, 30) };
            IList<DateTime> occurrences = month.GetOccurences(new DateTime(2015, 7, 30), new DateTime(2020, 1, 1));

            Assert.AreEqual(0, occurrences.Count);
        }

        [Test]
        public void Occurrences_after_end_boundry_case()
        {
            // 21/2/2009, 21/8/2010, 21/2/2012, 21/8/2013, 21/2/2015
            MonthlySchedule month = new MonthlySchedule { StartDate = new DateTime(2009, 2, 21), Frequency = 18, EndDate = new DateTime(2015, 6, 30) };
            IList<DateTime> occurrences = month.GetOccurences(new DateTime(2015, 2, 21), new DateTime(2020, 1, 1));

            Assert.AreEqual(1, occurrences.Count);
            Assert.AreEqual(new DateTime(2015, 2, 21), occurrences[0]);
        }

        [Test]
        public void Occurrences_crossing_end()
        {
            // 21/2/2009, 21/8/2010, 21/2/2012, 21/8/2013, 21/2/2015
            MonthlySchedule month = new MonthlySchedule { StartDate = new DateTime(2009, 2, 21), Frequency = 18, EndDate = new DateTime(2015, 6, 30) };
            IList<DateTime> occurrences = month.GetOccurences(new DateTime(2014, 1, 1), new DateTime(2020, 1, 1));

            Assert.AreEqual(1, occurrences.Count);
            Assert.AreEqual(occurrences[0], new DateTime(2015, 2, 21));
        }

        [Test]
        public void Occurrences_stress_test()
        {
            MonthlySchedule month = new MonthlySchedule { StartDate = new DateTime(1400, 1, 1), Frequency = 1 };

            // 1/1/1900 (Monday) -> 31/12/1999 (Friday) = 36524 total days; 7 days in every 21 for 100 years; 36524 - 14 days = 36510 / 3 = 12170
            IList<DateTime> occurrences = month.GetOccurences(new DateTime(1400, 1, 1), new DateTime(2399, 12, 31));

            Assert.AreEqual(12000, occurrences.Count);
        }

        [Test]
        public void Next_occurrence_before_start()
        {
            // 21/2/2009, 21/8/2010, 21/2/2012, 21/8/2013, 21/2/2015
            MonthlySchedule month = new MonthlySchedule { StartDate = new DateTime(2009, 2, 21), Frequency = 18, EndDate = new DateTime(2015, 6, 30) };
            DateTime? next = month.NextOccurrence(new DateTime(2000, 1, 1));

            Assert.IsTrue(next.HasValue);
            Assert.AreEqual(new DateTime(2009, 2, 21), next.Value);
        }

        [Test]
        public void Next_occurrence_during_period()
        {
            // 21/2/2009, 21/8/2010, 21/2/2012, 21/8/2013, 21/2/2015
            MonthlySchedule month = new MonthlySchedule { StartDate = new DateTime(2009, 2, 21), Frequency = 18, EndDate = new DateTime(2015, 6, 30) };
            DateTime? next = month.NextOccurrence(new DateTime(2013, 1, 1));

            Assert.IsTrue(next.HasValue);
            Assert.AreEqual(new DateTime(2013, 8, 21), next.Value);
        }

        [Test]
        public void Next_occurrence_after_end()
        {
            // 21/2/2009, 21/8/2010, 21/2/2012, 21/8/2013, 21/2/2015
            MonthlySchedule month = new MonthlySchedule { StartDate = new DateTime(2009, 2, 21), Frequency = 18, EndDate = new DateTime(2015, 6, 30) };
            DateTime? next = month.NextOccurrence(new DateTime(2020, 1, 1));

            Assert.IsFalse(next.HasValue);
        }



        [Test]
        public void Occurrences_before_start_using_day_of_week()
        {
            // 21/2/2009, 21/8/2010, 21/2/2012, 21/8/2013, 21/2/2015
            MonthlySchedule month = new MonthlySchedule { StartDate = new DateTime(2010, 4, 1), Frequency = 1, ReoccurrenceType = MonthlySchedule.Type.DayOfWeek };
            IList<DateTime> occurrences = month.GetOccurences(new DateTime(2009, 1, 1), new DateTime(2009, 2, 1));

            Assert.AreEqual(0, occurrences.Count);
        }

        [Test]
        public void Occurrences_before_start_boundry_case_using_day_of_week()
        {
            // 21/2/2009, 21/8/2010, 21/2/2012, 21/8/2013, 21/2/2015
            MonthlySchedule month = new MonthlySchedule { StartDate = new DateTime(2010, 4, 1), Frequency = 1, ReoccurrenceType = MonthlySchedule.Type.DayOfWeek };
            IList<DateTime> occurrences = month.GetOccurences(new DateTime(2009, 1, 1), new DateTime(2010, 4, 1));

            Assert.AreEqual(1, occurrences.Count);
            Assert.AreEqual(new DateTime(2010, 4, 1), occurrences[0]);
        }

        [Test]
        public void Occurrences_crossing_start_using_day_of_week()
        {
            // 21/2/2009, 21/8/2010, 21/2/2012, 21/8/2013, 21/2/2015
            MonthlySchedule month = new MonthlySchedule { StartDate = new DateTime(2010, 4, 1), Frequency = 1, ReoccurrenceType = MonthlySchedule.Type.DayOfWeek };
            IList<DateTime> occurrences = month.GetOccurences(new DateTime(2010, 1, 1), new DateTime(2010, 6, 30));

            Assert.AreEqual(3, occurrences.Count);
            Assert.AreEqual(new DateTime(2010, 4, 1), occurrences[0]);
            Assert.AreEqual(new DateTime(2010, 5, 6), occurrences[1]);
            Assert.AreEqual(new DateTime(2010, 6, 3), occurrences[2]);
        }

        [Test]
        public void Occurrences_within_period_using_day_of_week()
        {
            // 21/2/2009, 21/8/2010, 21/2/2012, 21/8/2013, 21/2/2015
            MonthlySchedule month = new MonthlySchedule { StartDate = new DateTime(2010, 4, 1), Frequency = 1, ReoccurrenceType = MonthlySchedule.Type.DayOfWeek };
            IList<DateTime> occurrences = month.GetOccurences(new DateTime(2010, 12, 1), new DateTime(2011, 3, 1));

            Assert.AreEqual(3, occurrences.Count);
            Assert.AreEqual(new DateTime(2010, 12, 2), occurrences[0]);
            Assert.AreEqual(new DateTime(2011, 1, 6), occurrences[1]);
            Assert.AreEqual(new DateTime(2011, 2, 3), occurrences[2]);
        }

        [Test]
        public void Occurrences_after_end_using_day_of_week()
        {
            // 21/2/2009, 21/8/2010, 21/2/2012, 21/8/2013, 21/2/2015
            MonthlySchedule month = new MonthlySchedule { StartDate = new DateTime(2010, 4, 1), Frequency = 1, EndDate = new DateTime(2015, 6, 30), ReoccurrenceType = MonthlySchedule.Type.DayOfWeek };
            IList<DateTime> occurrences = month.GetOccurences(new DateTime(2015, 7, 30), new DateTime(2020, 1, 1));

            Assert.AreEqual(0, occurrences.Count);
        }

        [Test]
        public void Occurrences_after_end_boundry_case_using_day_of_week()
        {
            // 21/2/2009, 21/8/2010, 21/2/2012, 21/8/2013, 21/2/2015
            MonthlySchedule month = new MonthlySchedule { StartDate = new DateTime(2010, 4, 1), Frequency = 1, EndDate = new DateTime(2015, 6, 30), ReoccurrenceType = MonthlySchedule.Type.DayOfWeek };
            IList<DateTime> occurrences = month.GetOccurences(new DateTime(2015, 6, 4), new DateTime(2020, 1, 1));

            Assert.AreEqual(1, occurrences.Count);
            Assert.AreEqual(new DateTime(2015, 6, 4), occurrences[0]);
        }

        [Test]
        public void Occurrences_crossing_end_using_day_of_week()
        {
            // 21/2/2009, 21/8/2010, 21/2/2012, 21/8/2013, 21/2/2015
            MonthlySchedule month = new MonthlySchedule { StartDate = new DateTime(2010, 4, 1), Frequency = 1, EndDate = new DateTime(2015, 6, 30), ReoccurrenceType = MonthlySchedule.Type.DayOfWeek };
            IList<DateTime> occurrences = month.GetOccurences(new DateTime(2015, 4, 1), new DateTime(2020, 1, 1));

            Assert.AreEqual(3, occurrences.Count);
            Assert.AreEqual(new DateTime(2015, 4, 2), occurrences[0]);
            Assert.AreEqual(new DateTime(2015, 5, 7), occurrences[1]);
            Assert.AreEqual(new DateTime(2015, 6, 4), occurrences[2]);
        }

        [Test]
        public void Occurrences_stress_test_using_day_of_week()
        {
            MonthlySchedule month = new MonthlySchedule { StartDate = new DateTime(1000, 1, 1), Frequency = 1, ReoccurrenceType = MonthlySchedule.Type.DayOfWeek };
            IList<DateTime> occurrences = month.GetOccurences(new DateTime(1000, 1, 1), new DateTime(2999, 12, 31));

            Assert.AreEqual(24000, occurrences.Count);
        }

        [Test]
        public void Next_occurrence_before_start_using_day_of_week()
        {
            // 21/2/2009, 21/8/2010, 21/2/2012, 21/8/2013, 21/2/2015
            MonthlySchedule month = new MonthlySchedule { StartDate = new DateTime(2010, 4, 1), Frequency = 1, EndDate = new DateTime(2015, 6, 30), ReoccurrenceType = MonthlySchedule.Type.DayOfWeek };
            DateTime? next = month.NextOccurrence(new DateTime(2000, 1, 1));

            Assert.IsTrue(next.HasValue);
            Assert.AreEqual(new DateTime(2010, 4, 1), next.Value);
        }

        [Test]
        public void Next_occurrence_during_period_using_day_of_week()
        {
            // 21/2/2009, 21/8/2010, 21/2/2012, 21/8/2013, 21/2/2015
            MonthlySchedule month = new MonthlySchedule { StartDate = new DateTime(2010, 4, 1), Frequency = 1, EndDate = new DateTime(2015, 6, 30), ReoccurrenceType = MonthlySchedule.Type.DayOfWeek };
            DateTime? next = month.NextOccurrence(new DateTime(2013, 1, 1));

            Assert.IsTrue(next.HasValue);
            Assert.AreEqual(new DateTime(2013, 1, 3), next.Value);
        }

        [Test]
        public void Next_occurrence_after_end_using_day_of_week()
        {
            // 21/2/2009, 21/8/2010, 21/2/2012, 21/8/2013, 21/2/2015
            MonthlySchedule month = new MonthlySchedule { StartDate = new DateTime(2010, 4, 1), Frequency = 1, EndDate = new DateTime(2015, 6, 30), ReoccurrenceType = MonthlySchedule.Type.DayOfWeek };
            DateTime? next = month.NextOccurrence(new DateTime(2020, 1, 1));

            Assert.IsFalse(next.HasValue);
        }





        [Test]
        public void Occurrences_day_of_week_first_friday()
        {
            MonthlySchedule month = new MonthlySchedule { StartDate = new DateTime(2010, 4, 2), Frequency = 1, ReoccurrenceType = MonthlySchedule.Type.DayOfWeek };
            IList<DateTime> occurrences = month.GetOccurences(new DateTime(2010, 3, 31), new DateTime(2010, 6, 30));

            Assert.AreEqual(3, occurrences.Count);
            Assert.AreEqual(new DateTime(2010, 4, 2), occurrences[0]);
            Assert.AreEqual(new DateTime(2010, 5, 7), occurrences[1]);
            Assert.AreEqual(new DateTime(2010, 6, 4), occurrences[2]);
        }

        [Test]
        public void Occurrences_day_of_week_first_monday()
        {
            MonthlySchedule month = new MonthlySchedule { StartDate = new DateTime(2010, 4, 5), Frequency = 2, ReoccurrenceType = MonthlySchedule.Type.DayOfWeek };
            IList<DateTime> occurrences = month.GetOccurences(new DateTime(2010, 3, 31), new DateTime(2010, 6, 30));

            Assert.AreEqual(2, occurrences.Count);
            Assert.AreEqual(new DateTime(2010, 4, 5), occurrences[0]);
            Assert.AreEqual(new DateTime(2010, 6, 7), occurrences[1]);
        }

        [Test]
        public void Occurrences_day_of_week_second_thursday()
        {
            MonthlySchedule month = new MonthlySchedule { StartDate = new DateTime(2010, 4, 8), Frequency = 1, ReoccurrenceType = MonthlySchedule.Type.DayOfWeek };
            IList<DateTime> occurrences = month.GetOccurences(new DateTime(2010, 3, 31), new DateTime(2010, 6, 30));

            Assert.AreEqual(3, occurrences.Count);
            Assert.AreEqual(new DateTime(2010, 4, 8), occurrences[0]);
            Assert.AreEqual(new DateTime(2010, 5, 13), occurrences[1]);
            Assert.AreEqual(new DateTime(2010, 6, 10), occurrences[2]);
        }

        [Test]
        public void Occurrences_day_of_week_second_sunday()
        {
            MonthlySchedule month = new MonthlySchedule { StartDate = new DateTime(2010, 4, 11), Frequency = 2, ReoccurrenceType = MonthlySchedule.Type.DayOfWeek };
            IList<DateTime> occurrences = month.GetOccurences(new DateTime(2010, 3, 31), new DateTime(2010, 6, 30));

            Assert.AreEqual(2, occurrences.Count);
            Assert.AreEqual(new DateTime(2010, 4, 11), occurrences[0]);
            Assert.AreEqual(new DateTime(2010, 6, 13), occurrences[1]);
        }

        [Test]
        public void Occurrences_day_of_week_third_saturday()
        {
            MonthlySchedule month = new MonthlySchedule { StartDate = new DateTime(2010, 4, 17), Frequency = 1, ReoccurrenceType = MonthlySchedule.Type.DayOfWeek };
            IList<DateTime> occurrences = month.GetOccurences(new DateTime(2010, 3, 31), new DateTime(2010, 6, 30));

            Assert.AreEqual(3, occurrences.Count);
            Assert.AreEqual(new DateTime(2010, 4, 17), occurrences[0]);
            Assert.AreEqual(new DateTime(2010, 5, 15), occurrences[1]);
            Assert.AreEqual(new DateTime(2010, 6, 19), occurrences[2]);
        }

        [Test]
        public void Occurrences_day_of_week_third_wednesday()
        {
            MonthlySchedule month = new MonthlySchedule { StartDate = new DateTime(2010, 4, 21), Frequency = 2, ReoccurrenceType = MonthlySchedule.Type.DayOfWeek };
            IList<DateTime> occurrences = month.GetOccurences(new DateTime(2010, 3, 31), new DateTime(2010, 6, 30));

            Assert.AreEqual(2, occurrences.Count);
            Assert.AreEqual(new DateTime(2010, 4, 21), occurrences[0]);
            Assert.AreEqual(new DateTime(2010, 6, 16), occurrences[1]);
        }

        [Test]
        public void Occurrences_day_of_week_fourth_friday()
        {
            MonthlySchedule month = new MonthlySchedule { StartDate = new DateTime(2010, 4, 23), Frequency = 1, ReoccurrenceType = MonthlySchedule.Type.DayOfWeek };
            IList<DateTime> occurrences = month.GetOccurences(new DateTime(2010, 3, 31), new DateTime(2010, 6, 30));

            Assert.AreEqual(3, occurrences.Count);
            Assert.AreEqual(new DateTime(2010, 4, 23), occurrences[0]); // 4th
            Assert.AreEqual(new DateTime(2010, 5, 28), occurrences[1]); // 4th + last
            Assert.AreEqual(new DateTime(2010, 6, 25), occurrences[2]); // 4th + last
        }
        // see last tuesday
        //[Test]
        //public void Occurrences_day_of_week_fourth_tuesday()
        //{
        //    MonthlySchedule month = new MonthlySchedule { StartDate = new DateTime(2010, 4, 27), Frequency = 2, ReoccurrenceType = MonthlySchedule.Type.DayOfWeek };
        //    IList<DateTime> occurrences = month.GetOccurences(new DateTime(2010, 3, 31), new DateTime(2010, 6, 30));

        //    Assert.AreEqual(2, occurrences.Count);
        //    Assert.AreEqual(new DateTime(2010, 4, 27), occurrences[0]); // 4th + last
        //    Assert.AreEqual(new DateTime(2010, 6, 22), occurrences[1]); // 4th
        //}
        [Test]
        public void Occurrences_day_of_week_last_saturday()
        {
            MonthlySchedule month = new MonthlySchedule { StartDate = new DateTime(2010, 4, 24), Frequency = 1, ReoccurrenceType = MonthlySchedule.Type.DayOfWeek };
            IList<DateTime> occurrences = month.GetOccurences(new DateTime(2010, 3, 31), new DateTime(2010, 6, 30));

            Assert.AreEqual(3, occurrences.Count);
            Assert.AreEqual(new DateTime(2010, 4, 24), occurrences[0]); // 4th + last
            Assert.AreEqual(new DateTime(2010, 5, 29), occurrences[1]); // last
            Assert.AreEqual(new DateTime(2010, 6, 26), occurrences[2]); // 4th + last
        }

        [Test]
        public void Occurrences_day_of_week_last_tuesday()
        {
            MonthlySchedule month = new MonthlySchedule { StartDate = new DateTime(2010, 4, 27), Frequency = 2, ReoccurrenceType = MonthlySchedule.Type.DayOfWeek };
            IList<DateTime> occurrences = month.GetOccurences(new DateTime(2010, 3, 31), new DateTime(2010, 6, 30));

            Assert.AreEqual(2, occurrences.Count);
            Assert.AreEqual(new DateTime(2010, 4, 27), occurrences[0]); // 4th + last
            Assert.AreEqual(new DateTime(2010, 6, 29), occurrences[1]); // last
        }

        [Test]
        public void Occurrences_day_of_week_last_friday()
        {
            MonthlySchedule month = new MonthlySchedule { StartDate = new DateTime(2010, 4, 30), Frequency = 1, ReoccurrenceType = MonthlySchedule.Type.DayOfWeek };
            IList<DateTime> occurrences = month.GetOccurences(new DateTime(2010, 3, 31), new DateTime(2010, 6, 30));

            Assert.AreEqual(3, occurrences.Count);
            Assert.AreEqual(new DateTime(2010, 4, 30), occurrences[0]); // last
            Assert.AreEqual(new DateTime(2010, 5, 28), occurrences[1]); // 4th + last
            Assert.AreEqual(new DateTime(2010, 6, 25), occurrences[2]); // 4th + last
        }


        [Test]
        public void Occurrences_day_of_month_first_with_time_component()
        {
            MonthlySchedule month = new MonthlySchedule { StartDate = new DateTime(2011, 8, 1, 14, 30, 0), Frequency = 1, ReoccurrenceType = MonthlySchedule.Type.DayOfMonth };
            IList<DateTime> occurrences = month.GetOccurences(new DateTime(2011, 8, 17), new DateTime(2011, 10, 1, 11, 0, 0));

            Assert.AreEqual(1, occurrences.Count);
            Assert.AreEqual(new DateTime(2011, 9, 1, 14, 30, 0), occurrences[0]);
        }

        [Test]
        public void Occurrences_day_of_week_first_tuesday_with_time_component()
        {
            MonthlySchedule month = new MonthlySchedule { StartDate = new DateTime(2011, 8, 2, 14, 30, 0), Frequency = 1, ReoccurrenceType = MonthlySchedule.Type.DayOfWeek };
            IList<DateTime> occurrences = month.GetOccurences(new DateTime(2011, 8, 17), new DateTime(2011, 11, 1, 11, 0, 0));

            Assert.AreEqual(2, occurrences.Count);
            Assert.AreEqual(new DateTime(2011, 9, 6, 14, 30, 0), occurrences[0]);
            Assert.AreEqual(new DateTime(2011, 10, 4, 14, 30, 0), occurrences[1]);
        }

        [Test]
        public void Occurrences_day_of_week_first_tuesday_with_time_component_afternoon()
        {
            MonthlySchedule month = new MonthlySchedule { StartDate = new DateTime(2011, 8, 2, 14, 30, 0), Frequency = 1, ReoccurrenceType = MonthlySchedule.Type.DayOfWeek };
            IList<DateTime> occurrences = month.GetOccurences(new DateTime(2011, 8, 17), new DateTime(2011, 11, 1, 15, 0, 0));

            Assert.AreEqual(3, occurrences.Count);
            Assert.AreEqual(new DateTime(2011, 9, 6, 14, 30, 0), occurrences[0]);
            Assert.AreEqual(new DateTime(2011, 10, 4, 14, 30, 0), occurrences[1]);
            Assert.AreEqual(new DateTime(2011, 11, 1, 14, 30, 0), occurrences[2]);
        }

        [Test]
        public void Occurrences_day_of_week_first_tuesday_without_time_component_afternoon()
        {
            MonthlySchedule month = new MonthlySchedule { StartDate = new DateTime(2011, 8, 2), Frequency = 1, ReoccurrenceType = MonthlySchedule.Type.DayOfWeek };
            IList<DateTime> occurrences = month.GetOccurences(new DateTime(2011, 8, 17), new DateTime(2011, 11, 1));

            Assert.AreEqual(3, occurrences.Count);
            Assert.AreEqual(new DateTime(2011, 9, 6), occurrences[0]);
            Assert.AreEqual(new DateTime(2011, 10, 4), occurrences[1]);
            Assert.AreEqual(new DateTime(2011, 11, 1), occurrences[2]);
        }
    }
}
