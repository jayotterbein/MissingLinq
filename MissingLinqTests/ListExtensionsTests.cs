using System.Collections.Generic;
using MissingLinq;
using NUnit.Framework;

namespace MissingLinqTests
{
    [TestFixture]
    public class ListExtensionsTests
    {
        [Test]
        public void GetAndRemove_removes_single_item()
        {
            var list = new List<int> {1, 2, 5};
            var removedItems = list.GetAndRemove(x => x > 4);
            Assert.That(removedItems, Is.EquivalentTo(new[] {5}));
            Assert.That(list, Is.EquivalentTo(new[] {1, 2}));
        }

        [Test]
        public void GetAndRemove_removes_multiple_items()
        {
            var list = new List<int> {1, 5, 15, 5, 10, 3};
            var removedItems = list.GetAndRemove(x => x >= 5);
            Assert.That(removedItems, Is.EquivalentTo(new[] {5, 15, 5, 10}));
            Assert.That(list, Is.EquivalentTo(new[] {1, 3}));
        }

        [Test]
        public void GetAndRemove_removes_multiple_items_with_index()
        {
            var list = new List<int> { 1, 5, 15, 5, 10, 3 };
            var removedItems = list.GetAndRemove((x, i) => x >= 5 && i <= 2);
            Assert.That(removedItems, Is.EquivalentTo(new[] { 5, 15 }));
            Assert.That(list, Is.EquivalentTo(new[] { 1, 5, 10, 3 }));
        }

        [Test]
        public void RemoveAll_without_index()
        {
            IList<int> list = new List<int> {16, 13, 77, 0};
            list.RemoveAll(x => x == 13);
            Assert.That(list, Is.EquivalentTo(new[] {16, 77, 0}));
        }

        [Test]
        public void RemoveAll_with_index()
        {
            IList<int> list = new List<int> { 16, 13, 77, 0 };
            list.RemoveAll((x, i) => x > 1 && i == 0);
            Assert.That(list, Is.EquivalentTo(new[] { 13, 77, 0 }));
        }
    }
}
