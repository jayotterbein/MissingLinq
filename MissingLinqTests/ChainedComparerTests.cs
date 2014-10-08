using System.Collections.Generic;
using MissingLinq;
using NUnit.Framework;

namespace MissingLinqTests
{
    [TestFixture]
    public class ChainedComparerTests
    {
        [Test]
        public void Multiple_compare_tests()
        {
            var firstComparer = new FirstComparer();
            var secondComparer = new SecondComparer();
            var chainComparer = new ChainedComparer<int>(firstComparer, secondComparer);

            Assert.That(chainComparer.Compare(2, 4), Is.EqualTo(0));
            Assert.That(chainComparer.Compare(0, 2), Is.EqualTo(secondComparer.Compare(0, 2)));
            Assert.That(chainComparer.Compare(0, 0), Is.EqualTo(firstComparer.Compare(0, 0)));
        }

        private class FirstComparer : IComparer<int>
        {
            public int Compare(int x, int y)
            {
                return (x + 2).CompareTo(y);
            }
        }

        private class SecondComparer : IComparer<int>
        {
            public int Compare(int x, int y)
            {
                return (x*2).CompareTo(y);
            }
        }
    }
}