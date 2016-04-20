using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultithreadGZip
{
    public class GZipCancellationTokenSource
    {
        public IGZipCancellationToken Token;

        public GZipCancellationTokenSource()
        {
            Token = new GZipCancellationToken();
        }
    }
}
