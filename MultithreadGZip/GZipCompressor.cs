using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace MultithreadGZip
{
    public class GZipCompressor : IGZipCompressor
    {
        private int PROCESSORS_COUNT;
        delegate int CompressionOpearationDelegate(string inFile, string outFile, long buffSize);
        public GZipCompressor()
        {
            PROCESSORS_COUNT = Environment.ProcessorCount;
        }

        public int Compress(string startFileName, string endFileName, long bufferSize)
        {
            try
            {
                using (var fsInput = File.OpenRead(startFileName))
                {
                    using (var fsOutput = File.Create(endFileName))
                    {
                        using (var gzipStream = new GZipStream(fsOutput, CompressionMode.Compress))
                        {
                            var buffer = new Byte[bufferSize];
                            int h;
                            while ((h = fsInput.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                gzipStream.Write(buffer, 0, h);
                            }
                        }
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int Decompress(string startFileName, string endFileName, long bufferSize)
        {
            try
            {
                using (var fsInput = File.OpenRead(startFileName))
                {
                    using (var fsOutput = File.Create(endFileName))
                    {
                        using (var gzipStream = new GZipStream(fsInput, CompressionMode.Decompress))
                        {
                            var buffer = new Byte[bufferSize];
                            int h;
                            while ((h = gzipStream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                fsOutput.Write(buffer, 0, h);
                            }
                        }
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IAsyncResult BeginCompress(string startFileName, string endFileName, long bufferSize, AsyncCallback callback, object state)
        {
            CompressionOpearationDelegate cod = Compress;
            return cod.BeginInvoke(startFileName, endFileName, bufferSize, callback, state);
        }

        public IAsyncResult BeginDecompress(string startFileName, string endFileName, long bufferSize, AsyncCallback callback, object state)
        {
            CompressionOpearationDelegate cod = Decompress;
            return cod.BeginInvoke(startFileName, endFileName, bufferSize, callback, state);
        }

        public int EndCompress(IAsyncResult result)
        {
            if (result == null) throw new ArgumentNullException("result");
            CompressionOpearationDelegate cod = result.AsyncState as CompressionOpearationDelegate;
            if (cod == null) throw new InvalidCastException("result state is not a proper delegate");
            return cod.EndInvoke(result);
        }

        public int EndDecompress(IAsyncResult result)
        {
            if (result == null) throw new ArgumentNullException("result");
            CompressionOpearationDelegate cod = result.AsyncState as CompressionOpearationDelegate;
            if (cod == null) throw new InvalidCastException("result state is not a proper delegate");
            return cod.EndInvoke(result);
        }
    }
}
