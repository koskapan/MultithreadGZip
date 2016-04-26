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
        private static IGZipCancellationToken cancelToken;
        private delegate int CompressionOperationDelegate(string inF, string outF, IGZipCancellationToken cancellToken);

        public static int Main(string[] args)
        {
            GZipCancellationTokenSource cancelTokenSource = new GZipCancellationTokenSource();
            cancelToken = cancelTokenSource.Token;
            IGZipCompressor compressor = new GZipCompressor(Properties.Settings.Default.BUFFER_SIZE, Properties.Settings.Default.THREADS_COUNT);
            Console.CancelKeyPress += Console_CancelKeyPress;
            try
            {
                GZipCommandLineArgs cmdArgs = GZipCommandLineArgs.ParseArgs(args);
                CompressionOperationDelegate cod;
                if (cmdArgs.Method == CompressorMethods.compress)
                {
                    cod = compressor.Compress;
                }
                else
                {
                    cod = compressor.Decompress;
                }
                IAsyncResult operationResult = cod.BeginInvoke(cmdArgs.StartFileName, cmdArgs.EndFileName, cancelToken, null, null);
                while (!operationResult.IsCompleted && !cancelToken.IsCancelled)
                {
                    Console.Write('.');
                    Thread.Sleep(500);
                }
                return cod.EndInvoke(operationResult);                
            }
            catch (Exception ex)
            {
                HandleError(ex);
                return FAIL_CODE;
            }
        }

        private static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            e.Cancel = true;
            cancelToken.IsCancelled = true;
        }
        
        private static void HandleError(Exception ex)
        {
            Console.WriteLine("\r\n" + ex.Message);
            ShowHelp();
        }

        private static void ShowHelp()
        {
            Console.WriteLine(@"
Compress / Decompress single file.
Usage:
    MultithreadGZip.exe [operation] [source_file_name] [destination_file_name]

Operations:
    compress
    decompress
                ");
        }
    }
}
