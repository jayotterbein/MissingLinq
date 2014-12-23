using System;
using System.Threading;

namespace MissingLinq
{
    public class LazyDisposable<T>
        where T : IDisposable
    {
        private readonly Func<Lazy<T>> _lazyFactory;
        private Lazy<T> _lazy;

        public LazyDisposable(Func<T> valueFactory)
        {
            _lazyFactory = () => new Lazy<T>(valueFactory);
            _lazy = _lazyFactory();
        }

        public LazyDisposable(Func<T> valueFactory, LazyThreadSafetyMode threadSafetyMode)
        {
            _lazyFactory = () => new Lazy<T>(valueFactory, threadSafetyMode);
            _lazy = _lazyFactory();
        }

        public LazyDisposable(Func<T> valueFactory, bool isThreadSafe)
        {
            _lazyFactory = () => new Lazy<T>(valueFactory, isThreadSafe);
            _lazy = _lazyFactory();
        }

        public LazyDisposable()
        {
            _lazyFactory = () => new Lazy<T>();
            _lazy = _lazyFactory();
        }

        public LazyDisposable(LazyThreadSafetyMode threadSafetyMode)
        {
            _lazyFactory = () => new Lazy<T>(threadSafetyMode);
            _lazy = _lazyFactory();
        }

        public LazyDisposable(bool isThreadSafe)
        {
            _lazyFactory = () => new Lazy<T>(isThreadSafe);
            _lazy = _lazyFactory();
        }

        public T Value
        {
            get { return _lazy.Value; }
        }

        public bool IsValueCreated
        {
            get { return _lazy.IsValueCreated; }
        }

        /// <summary>
        ///     Disposes of the inner value if it exists.
        ///     This can be called multiple times without a performance penalty.
        ///     This will still allow <see cref="Value" /> to be called and a new value to be generated when needed.
        /// </summary>
        public void DisposeValue()
        {
            if (_lazy.IsValueCreated)
            {
                _lazy.Value.Dispose();
                _lazy = _lazyFactory();
            }
        }
    }
}