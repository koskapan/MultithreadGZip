using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultithreadGZip
{
    public interface IGZipCompressor
    {
        int Compress(string startFileName, string endFileName, long bufferSize);
        IAsyncResult BeginCompress(string startFileName, string endFileName, long bufferSize, AsyncCallback callback, object state);
        int EndCompress(IAsyncResult result);
        int Decompress(string startFileName, string endFileName, long bufferSize);
        IAsyncResult BeginDecompress(string startFileName, string endFileName, long bufferSize, AsyncCallback callback, object state);
        int EndDecompress(IAsyncResult result);
    }
}
