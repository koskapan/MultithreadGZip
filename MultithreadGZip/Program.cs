using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MultithreadGZip
{
    public class Program
    {
        const int SUCCESS_CODE = 0;
        const int FAIL_CODE = 1;
        const int BUFFER_SIZE = 1024;

        public static int Main(string[] args)
        {
            IGZipCompressor compressor = new GZipCompressor();
            try
            {
                switch (args.Length)
                {
                    case 0:
                        ShowHelp(); ;
                        return FAIL_CODE;
                    case 2:
                        throw new ArgumentNullException("[destination file name]");
                    case 3:
                        {
                            string startFileName = args[1];
                            string endFileName = args[2];
                            if (!File.Exists(startFileName))
                                throw new FileNotFoundException("File not found! " + startFileName);
                            if (!File.Exists(endFileName))
                                throw new FileNotFoundException("File not found! " + endFileName);
                            switch (args[0].ToLower())
                            {
                                case "compress":
                                    return compressor.Compress(startFileName, endFileName, BUFFER_SIZE);
                                case "decompress":
                                    return compressor.Decompress(startFileName, endFileName, BUFFER_SIZE);
                                default:
                                    throw new ArgumentNullException("[operation]");
                            }
                        }
                    default:
                        throw new ArgumentNullException("[operation]");
                }
            }
            catch (Exception ex)
            {
                HandleError(ex);
                return FAIL_CODE;
            }
        }

        private static void HandleError(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        private static void ShowHelp()
        {
            Console.WriteLine(@"
Compress / Decompress single files.
Usage:
    MultithreadGZip.exe [operation] source_file_name destination_file_name

Operations:
    compress
    decompress
                ");
        }
    }
}
