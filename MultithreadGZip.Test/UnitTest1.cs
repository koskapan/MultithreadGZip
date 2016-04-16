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
        string startFileName = "start_file";
        string compressedFileName = "compressed_file";
        string decompressedFileName = "decompressed_file";

        [TestMethod]
        public void compression_fail_test_two_params()
        {
            Assert.IsTrue(File.Exists(startFileName));
            int result = Program.Main(new[] { "compress", startFileName });
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void compression_fail_test_one_param()
        {
            Assert.IsTrue(File.Exists(startFileName));
            int result = Program.Main(new[] { "compress" });
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void compression_fail_test_file_not_exist()
        {
            Assert.IsTrue(File.Exists(startFileName));
            int result = Program.Main(new[] { "compress", "some_file", compressedFileName });
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void compression_test()
        {
            Assert.IsTrue(File.Exists(startFileName));
            int result = Program.Main(new[] { "compress", startFileName, compressedFileName });
            Assert.AreEqual(0, result);
            Assert.IsTrue(File.Exists(compressedFileName));
        }

        [TestMethod]
        public void decompression_test()
        {
            Assert.IsTrue(File.Exists(compressedFileName));
            int result = Program.Main(new[] {"decompress", compressedFileName, decompressedFileName });
            Assert.AreEqual(0, result);
            Assert.IsTrue(File.Exists(decompressedFileName));
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
