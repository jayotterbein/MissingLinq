using System;
using System.Collections.Generic;
using MissingLinq;
using NUnit.Framework;

namespace MissingLinqTests
{
    [TestFixture]
    public class LazyDisposableTests
    {
        [Test]
        public void Factory_not_called_for_dispose()
        {
            var factoryCalled = 0;
            Func<Model> modelFactory = () =>
            {
                factoryCalled++;
                return new Model();
            };

            var lazyDisposable = new LazyDisposable<Model>(modelFactory);
            lazyDisposable.DisposeValue();
            lazyDisposable.DisposeValue();
            lazyDisposable.DisposeValue();
            lazyDisposable.DisposeValue();
            Assert.That(factoryCalled, Is.EqualTo(0));
        }

        [Test]
        public void Factory_called_once_for_repeated_value_access()
        {
            var factoryCalled = 0;
            Func<Model> modelFactory = () =>
            {
                factoryCalled++;
                return new Model();
            };

            var lazyDisposable = new LazyDisposable<Model>(modelFactory);
            for (var i = 0; i < 10; i++)
            {
                var temp = lazyDisposable.Value;
                Assert.That(temp, Is.Not.Null);
            }
            Assert.That(factoryCalled, Is.EqualTo(1));
        }

        [Test]
        public void Factory_called_for_every_value_access_after_dispose()
        {
            var factoryCalled = 0;
            Func<Model> modelFactory = () =>
            {
                factoryCalled++;
                return new Model();
            };

            var lazyDisposable = new LazyDisposable<Model>(modelFactory);
            for (var i = 0; i < 10; i++)
            {
                var temp = lazyDisposable.Value;
                Assert.That(temp, Is.Not.Null);
                lazyDisposable.DisposeValue();
            }

            Assert.That(factoryCalled, Is.EqualTo(10));
        }

        [Test]
        public void Dispose_calls_underlaying_dispose_only_once()
        {
            var factoryCalled = 0;
            Func<Model> modelFactory = () =>
            {
                factoryCalled++;
                return new Model();
            };

            var lazyDisposable = new LazyDisposable<Model>(modelFactory);
            var models = new List<Model>();
            for (var i = 0; i < 10; i++)
            {
                var lazyLoadedModel = lazyDisposable.Value;
                models.Add(lazyLoadedModel);
                lazyDisposable.DisposeValue();
                lazyDisposable.DisposeValue();
                lazyDisposable.DisposeValue();
                lazyDisposable.DisposeValue();
            }

            Assert.That(models.Count, Is.EqualTo(10), "models not added to testing array");
            foreach (var model in models)
            {
                Assert.That(model.DisposeCallCount, Is.EqualTo(1));
            }
        }

        private class Model : IDisposable
        {
            public int DisposeCallCount;

            public void Dispose()
            {
                DisposeCallCount++;
            }
        }
    }
}