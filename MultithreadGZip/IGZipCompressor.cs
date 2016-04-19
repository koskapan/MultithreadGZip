using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultithreadGZip
{
    public interface IGZipCompressor
    {
        int Compress(string startFileName, string endFileName, long bufferSize);
        int Decompress(string startFileName, string endFileName, long bufferSize);
    }
}
