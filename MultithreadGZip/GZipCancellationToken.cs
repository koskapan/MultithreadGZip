using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultithreadGZip
{
    public class GZipCancellationToken : IGZipCancellationToken
    {
        public bool IsCancelled { get; set; }

    }
}
