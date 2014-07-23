using System;
using MissingLinq;
using NUnit.Framework;

namespace MissingLinqTests
{
    [TestFixture]
    public class FuncComparerTests
    {
        [Test]
        public void compare_test()
        {
            var funcComparer = new FuncComparer<int>((x, y) => y.CompareTo(x));
            Assert.That(funcComparer.Compare(1, 2), Is.EqualTo(1));
            Assert.That(funcComparer.Compare(5, -1), Is.EqualTo(-1));
            Assert.That(funcComparer.Compare(4, 4), Is.EqualTo(0));
        }

        [Test]
        public void compare_multiple_test()
        {
            Func<string, string, int> skipX = (str1, str2) => string.Compare(str1, "skipX", StringComparison.Ordinal);
            Func<string, string, int> skipY = (str1, str2) => string.Compare(str2, "skipY", StringComparison.Ordinal);
            var funcComparer = new FuncComparer<string>(
                skipX,
                skipY,
                (str1, str2) => string.Compare(str1, str2, StringComparison.Ordinal));

            Assert.That(funcComparer.Compare("X", "Y"), Is.EqualTo(skipX("X", "Y")));
            Assert.That(funcComparer.Compare("skipX", "Y"), Is.EqualTo(skipY("skipX", "Y")));
            Assert.That(funcComparer.Compare("skipX", "skipY"), Is.EqualTo(string.Compare("skipX", "skipY", StringComparison.Ordinal)));
        }
    }
}
