﻿using System;
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
                GZipCommandLineArgs cmdArgs = GZipCommandLineArgs.ParseArgs(args);
                switch (cmdArgs.Method)
                {
                    case CompressorMethods.compress:
                        return compressor.Compress(cmdArgs.StartFileName, cmdArgs.EndFileName, BUFFER_SIZE);
                    case CompressorMethods.decompress:
                        return compressor.Decompress(cmdArgs.StartFileName, cmdArgs.EndFileName, BUFFER_SIZE);
                    default:
                        return FAIL_CODE;
                }
                
            }
            catch (InvalidCastException ex)
            {
                HandleError(ex, "Invalid parameter [method]");
                return FAIL_CODE;
            }
            catch (IndexOutOfRangeException ex)
            {
                HandleError(ex, "Missing parameter [destination_file_name]");
                return FAIL_CODE;
            }
            catch (FileNotFoundException ex)
            {
                HandleError(ex, "Cannot find file: " + args[1] );
                return FAIL_CODE;
            }
            catch (Exception ex)
            {
                HandleError(ex, "");
                return FAIL_CODE;
            }
        }

        private static void HandleError(Exception ex, string message)
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
