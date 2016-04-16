using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace MultithreadGZip
{
    public class Program
    {
        const int SUCCESS_CODE = 0;
        const int FAIL_CODE = 1;
        const long BUFFER_SIZE = 524288000;
        private delegate int CompressionOperationDelegate(string inF, string outF, long buffSize);

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
                            CompressionOperationDelegate cod;
                            switch (args[0].ToLower())
                            {
                                case "compress":
                                        cod = compressor.Compress;
                                        break;
                                case "decompress":
                                        cod = compressor.Decompress;
                                        break;
                                default:
                                    throw new ArgumentNullException("[operation]");
                            }
                            var res = cod.BeginInvoke(startFileName, endFileName, BUFFER_SIZE, null, null);
                            while (!res.IsCompleted)
                            {
                                Console.Write('.');
                                Thread.Sleep(500);
                            }
                            return cod.EndInvoke(res);
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
