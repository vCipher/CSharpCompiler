using System;
using System.Threading;

namespace CSharpCompiler.Utility
{
    public struct LazyWrapper<T>
    {
        private bool _isInited;
        private T _value;

        public T GetValue(ref object syncLock, Func<T> factory)
        {
            return LazyInitializer.EnsureInitialized(ref _value, ref _isInited, ref syncLock, factory);
        }
    }
}
