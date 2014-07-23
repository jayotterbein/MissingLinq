using MissingLinq;
using NUnit.Framework;

namespace MissingLinqTests
{
    [TestFixture]
    public class FuncEqualityTests
    {
        [Test]
        public void equality_test()
        {
            var funcEquality = new FuncEqualityComparer<int>((x, y) => (x + 1) == y);
            Assert.That(funcEquality.Equals(7, 7), Is.False);
            Assert.That(funcEquality.Equals(7, 8), Is.True);
        }
    }
}
