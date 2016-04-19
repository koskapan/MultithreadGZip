﻿using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace MultithreadGZip
{
    public class GZipCompressor : IGZipCompressor
    {
        /*some master comment*/
        private int PROCESSORS_COUNT;
        public GZipCompressor()
        {
            PROCESSORS_COUNT = Environment.ProcessorCount;
        }
        /*comment*/
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
    }
}
