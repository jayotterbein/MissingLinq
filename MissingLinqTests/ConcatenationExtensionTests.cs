using MissingLinq;
using NUnit.Framework;

namespace MissingLinqTests
{
    [TestFixture]
    public class ConcatenationExtensionTests
    {
        [Test]
        public void Prepend_single()
        {
            Assert.That(
                new[] {5, 6, 10, 1}.Prepend(500),
                Is.EquivalentTo(new[] {500, 5, 6, 10, 1}));
        }

        [Test]
        public void Prepend_list()
        {
            Assert.That(
                new[] {1, 3, 17, -1}.Prepend(1, 3, -1),
                Is.EquivalentTo(new[] {1, 3, -1, 1, 3, 17, -1}));
        }

        [Test]
        public void Append_single()
        {
            Assert.That(
                new[] { -15, 8, 10, -15 }.Append(-15),
                Is.EquivalentTo(new[] { -15, 8, 10, -15, -15 }));
        }

        [Test]
        public void Append_list()
        {
            Assert.That(
                new[] {-10, 3, 5}.Append(new[] {1, 5, 5}),
                Is.EquivalentTo(new[] {-10, 3, 5, 1, 5, 5}));
        }
    }
}
