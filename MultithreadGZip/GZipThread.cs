using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MultithreadGZip
{
    public class GZipThread : IGzipThread, IDisposable
    {
        ReaderWriterLockSlim _lock;
        Thread innerThread;
        bool disposing;

        public GZipThread(ReaderWriterLockSlim rwLock)
        {
            _lock = rwLock;
            innerThread = new Thread(Start);
        }

        public void Start()
        {
            
        }

        public void Dispose()
        {
            innerThread.Abort();
            _lock.ExitWriteLock();
            _lock.ExitReadLock();
        }
    }
}
