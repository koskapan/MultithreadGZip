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
        public GZipCompressor()
        {
            PROCESSORS_COUNT = Environment.ProcessorCount;
        }

        public int Process(GZipCommandLineArgs args)
        {
            switch (args.Method)
            {
                case CompressorMethods.compress:
                    return Compress(args.StartFileName, args.EndFileName, 1024);
                case CompressorMethods.decompress:
                    return Compress(args.StartFileName, args.EndFileName, 1024);
                default:
                    return 1;
            }
        }

        private int Compress(string startFileName, string endFileName, long bufferSize)
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

        private int Decompress(string startFileName, string endFileName, long bufferSize)
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
    }
}
