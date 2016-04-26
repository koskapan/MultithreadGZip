using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultithreadGZip
{
    public enum CompressorMethods { compress, decompress }

    public class GZipCommandLineArgs
    {
        public CompressorMethods Method { get; private set; }
        public string StartFileName { get; private set; }
        public string EndFileName { get; private set; }

        public GZipCommandLineArgs(CompressorMethods method, string startFileName, string endFileName)
        {
            Method = method;
            StartFileName = startFileName;
            EndFileName = endFileName;
        }

        public static GZipCommandLineArgs ParseArgs(string[] args)
        {
            try
            {
                CompressorMethods method = (CompressorMethods)Enum.Parse(typeof(CompressorMethods), args[0]);
                return new GZipCommandLineArgs(method, args[1], args[2]);
            }
            catch (InvalidCastException ex)
            {
                throw new InvalidCastException("Can't find command " + args[0], ex);
            }
            catch (IndexOutOfRangeException ex)
            {
                throw new IndexOutOfRangeException("Missing parameter [destination_file_name]", ex);
            }
        }
    }
}