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
            Assert.That(Enumerable.Empty<string>().IsNullOrEmpty(), Is.True);
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

        [Test]
        public void Chunk_chunk_size_invalid_test()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Enumerable.Range(1, 10)
                .Chunk(0)
                .First());
            Assert.That(ex.ParamName, Is.EqualTo("chunkSize"));
            Assert.That(ex.ActualValue, Is.EqualTo(0));

            ex = Assert.Throws<ArgumentOutOfRangeException>(() => Enumerable.Range(1, 10)
                .Chunk(-10)
                .First());
            Assert.That(ex.ParamName, Is.EqualTo("chunkSize"));
            Assert.That(ex.ActualValue, Is.EqualTo(-10));
        }

        [Test]
        public void Chunk_splits_accurately_test()
        {
            var items = Enumerable.Range(1, 23);
            var chunked = items
                .Chunk(10)
                .ToArray();
            Assert.That(chunked[0], Is.EquivalentTo(new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10}));
            Assert.That(chunked[1], Is.EquivalentTo(new[] {11, 12, 13, 14, 15, 16, 17, 18, 19, 20}));
            Assert.That(chunked[2], Is.EquivalentTo(new[] {21, 22, 23}));
        }

        [Test]
        public void Chunk_splits_exact_border_test()
        {
            var items = Enumerable.Range(1, 23);
            Assert.That(items.Chunk(23), Is.EquivalentTo(new[] {Enumerable.Range(1, 23)}));
        }

        [Test]
        public void Chunk_splits_smaller_than_size_test()
        {
            var items = Enumerable.Range(1, 23);
            Assert.That(items.Chunk(50), Is.EquivalentTo(new[] {Enumerable.Range(1, 23)}));
        }
    }
}