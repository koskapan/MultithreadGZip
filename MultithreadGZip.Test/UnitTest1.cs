using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultithreadGZip;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace MultithreadGZip.Test
{
    [TestClass]
    public class UnitTest1
    {
        string startFileName = "pic1.jpg";
        string compressedFileName = "pic1.gz";
        string decompressedFileName = "decompressed_pic1.jpg";

        [TestMethod]
        public void compression_fail_test()
        {
            Assert.IsTrue(File.Exists(startFileName));
            int result = Program.Main(new[] { "compress", startFileName });
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void compression_test()
        {
            Assert.IsTrue(File.Exists(startFileName));
            int result = Program.Main(new[] { "compress", startFileName, compressedFileName });
            Assert.AreEqual(0, result);
            Assert.IsTrue(File.Exists(compressedFileName));
            /*FileInfo startFileInfo = new FileInfo(startFileName);
            FileInfo compressedFileInfo = new FileInfo(compressedFileName);
            Assert.IsTrue(startFileInfo.Length > compressedFileInfo.Length);*/
        }

        [TestMethod]
        public void decompression_test()
        {
            Assert.IsTrue(File.Exists(compressedFileName));
            int result = Program.Main(new[] {"decompress", compressedFileName, decompressedFileName });
            Assert.AreEqual(0, result);
            Assert.IsTrue(File.Exists(decompressedFileName));
            /*FileInfo compressedFileInfo = new FileInfo(compressedFileName);
            FileInfo decompressedFileInfo = new FileInfo(decompressedFileName);
            Assert.IsTrue(compressedFileInfo.Length < decompressedFileInfo.Length);*/

        }


        [TestMethod]
        public void start_and_decompressed_files_hash_are_equal()
        {
            string startFileHash = Encoding.Default.GetString(ComputeFileHash(startFileName));
            string decompressedFileHash = Encoding.Default.GetString(ComputeFileHash(decompressedFileName));
            Assert.AreEqual(startFileHash, decompressedFileHash);
        }

        private byte[] ComputeFileHash(string fileName)
        {
            var md5 = MD5.Create();
            using (var stream = File.OpenRead(fileName))
                return md5.ComputeHash(stream);
        }
    }
}
