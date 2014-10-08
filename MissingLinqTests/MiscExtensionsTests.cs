using System;
using System.Collections.Generic;
using System.Linq;
using MissingLinq;
using NUnit.Framework;

namespace MissingLinqTests
{
    [TestFixture]
    public class MiscExtensionsTests
    {
        [Test]
        public void IsNullOrEmpty()
        {
            Assert.That(((IEnumerable<int>) null).IsNullOrEmpty(), Is.True);
            Assert.That(Enumerable.Empty<string>()
                .IsNullOrEmpty(), Is.True);
            Assert.That(new Int16[0].IsNullOrEmpty(), Is.True);
        }

        [Test]
        public void None()
        {
            Assert.That(new[] {1, 3, 5}.None(x => x < 0), Is.True);
            Assert.That(new[] {"x", "y", "z"}.None(x => x == "a"), Is.True);
            Assert.That(((IEnumerable<int>) null).None(x => true), Is.True);
        }

        [Test]
        public void IndexOf()
        {
            Assert.That(new[] {0, 1, 15, 500, -1, 11, 3}.IndexOf(x => x < 0), Is.EqualTo(4));
            Assert.That(new[] {0, 1, 15, 500, -1, 11, 3}.IndexOf(x => x < -100), Is.EqualTo(-1));
        }

        [Test]
        public void DistincyByTest()
        {
            var items = new[]
            {
                new {Key = 1, Value = "1"},
                new {Key = 2, Value = "2"},
                new {Key = 1, Value = "not distinct"}
            };

            var distinctItems = items.DistinctBy((x, y) => x.Key == y.Key);
            Assert.That(distinctItems, Is.EquivalentTo(items.Take(2)));
        }
    }
}