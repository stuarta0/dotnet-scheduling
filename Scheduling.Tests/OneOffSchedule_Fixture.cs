using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Scheduling.Tests
{
    [TestFixture]
    public class OneOffSchedule_Fixture
    {
        [Test]
        public void Occurrences_before()
        {
            // 20/4, 23/4, 26/4, 29/4, 2/5, 5/5, 8/5
            OneOffSchedule once = new OneOffSchedule { StartDate = new DateTime(2010, 4, 20), Frequency = 3 };
            IList<DateTime> occurrences = once.GetOccurences(new DateTime(2010, 4, 1), new DateTime(2010, 4, 10));

            Assert.AreEqual(0, occurrences.Count);
        }

        [Test]
        public void Occurrences_before_boundry_case()
        {
            // 20/4, 23/4, 26/4, 29/4, 2/5, 5/5, 8/5
            OneOffSchedule once = new OneOffSchedule { StartDate = new DateTime(2010, 4, 20), Frequency = 3 };
            IList<DateTime> occurrences = once.GetOccurences(new DateTime(2010, 4, 1), new DateTime(2010, 4, 20));

            Assert.AreEqual(1, occurrences.Count);
            Assert.AreEqual(occurrences[0], new DateTime(2010, 4, 20));
        }

        [Test]
        public void Occurrences_crossing()
        {
            // 20/4, 23/4, 26/4, 29/4, 2/5, 5/5, 8/5
            OneOffSchedule once = new OneOffSchedule { StartDate = new DateTime(2010, 4, 20), Frequency = 3 };
            IList<DateTime> occurrences = once.GetOccurences(new DateTime(2010, 4, 1), new DateTime(2010, 5, 1));

            Assert.AreEqual(1, occurrences.Count);
            Assert.AreEqual(occurrences[0], new DateTime(2010, 4, 20));
        }

        [Test]
        public void Occurrences_after()
        {
            // 20/4, 23/4, 26/4, 29/4, 2/5, 5/5, 8/5
            OneOffSchedule once = new OneOffSchedule { StartDate = new DateTime(2010, 4, 20), Frequency = 3, EndDate = new DateTime(2010, 5, 3) };
            IList<DateTime> occurrences = once.GetOccurences(new DateTime(2010, 6, 1), new DateTime(2010, 7, 1));

            Assert.AreEqual(0, occurrences.Count);
        }

        [Test]
        public void Occurrences_after_boundry_case()
        {
            // 20/4, 23/4, 26/4, 29/4, 2/5, 5/5, 8/5
            OneOffSchedule once = new OneOffSchedule { StartDate = new DateTime(2010, 4, 20), Frequency = 3, EndDate = new DateTime(2010, 5, 3) };
            IList<DateTime> occurrences = once.GetOccurences(new DateTime(2010, 4, 20), new DateTime(2010, 7, 1));

            Assert.AreEqual(1, occurrences.Count);
            Assert.AreEqual(occurrences[0], new DateTime(2010, 4, 20));
        }

        [Test]
        public void Next_occurrence_before()
        {
            // 20/4, 23/4, 26/4, 29/4, 2/5, 5/5, 8/5
            OneOffSchedule once = new OneOffSchedule { StartDate = new DateTime(2010, 4, 20), Frequency = 3, EndDate = new DateTime(2010, 5, 3) };
            DateTime? next = once.NextOccurrence(new DateTime(2010, 1, 1));

            Assert.IsTrue(next.HasValue);
            Assert.AreEqual(new DateTime(2010, 4, 20), next.Value);
        }

        [Test]
        public void Next_occurrence_on_date()
        {
            // 20/4, 23/4, 26/4, 29/4, 2/5, 5/5, 8/5
            OneOffSchedule once = new OneOffSchedule { StartDate = new DateTime(2010, 4, 20), Frequency = 3, EndDate = new DateTime(2010, 5, 3) };
            DateTime? next = once.NextOccurrence(new DateTime(2010, 4, 20));
            
            Assert.IsTrue(next.HasValue);
            Assert.AreEqual(new DateTime(2010, 4, 20), next.Value);
        }

        [Test]
        public void Next_occurrence_after()
        {
            // 20/4, 23/4, 26/4, 29/4, 2/5, 5/5, 8/5
            OneOffSchedule once = new OneOffSchedule { StartDate = new DateTime(2010, 4, 20), Frequency = 3, EndDate = new DateTime(2010, 5, 3) };
            DateTime? next = once.NextOccurrence(new DateTime(2010, 6, 1));

            Assert.IsFalse(next.HasValue);
        }
    }
}
