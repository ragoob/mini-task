using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Core.Plugins
{
   public class WriteLockDisposable : IDisposable
    {
        private readonly ReaderWriterLockSlim _rwLock;

        /// <summary>
        /// Initializes a new instance of the <see cref="WriteLockDisposable"/> class.
        /// </summary>
        /// <param name="rwLock">The readers–writer lock</param>
        public WriteLockDisposable(ReaderWriterLockSlim rwLock)
        {
            _rwLock = rwLock;
            _rwLock.EnterWriteLock();
        }

        void IDisposable.Dispose()
        {
            _rwLock.ExitWriteLock();
        }
    }
}
