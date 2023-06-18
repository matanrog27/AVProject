using AntiVirus;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;




namespace AV
{
    public class FileScanner
    {
        private static List<byte[]> badFiles = new List<byte[]>();
        private static List<byte[]> badFiles_unhash = new List<byte[]>();

        private static HashSet<byte[]> goodFiles = new HashSet<byte[]>();
        /// <summary>
        /// Returns true if the file is a virus
        /// 
        /// </summary>
        /// <param name="filename">File to scan</param>
        /// <returns></returns>
        /// 
        // 1 - good |0-unknown |-1 - bad
        public static int Scan(string filename)
        {
            try {
                byte[] file_bytes = File.ReadAllBytes(filename);

                byte[] hash;
                using (var md5 = System.Security.Cryptography.MD5.Create())
                {
                    md5.TransformFinalBlock(file_bytes, 0, file_bytes.Length);
                    hash = md5.Hash;
                }
                foreach (byte[] byteArray in goodFiles)
                {
                    if (byteArray.SequenceEqual(hash))
                    {
                        return 1;
                    }
                }
                foreach (byte[] virus in badFiles)
                {

                    //check static signature
                    if (CompareBytes(hash, virus))
                    {
                        return -1;
                    }

                }
                foreach (byte[] virus in badFiles_unhash)
                {

                    //check static signature
                    if (Huristic.huristic_check(file_bytes, virus))
                    {
                        return -2;
                    }

                }
                return 0;
            }
            catch (Exception ex)
            {
                //using (StreamWriter writer = new StreamWriter(DirectoryWatcher.logPath, true))
                //{
                //    // Write an initial message or header
                //    writer.WriteLine(DateTime.Now + $" failed to scan file: {ex.Message}");
                //}
                return -3;
            }

             
        }
     

        private static bool CompareBytes(byte[] lhs, byte[] rhs)
        {
            if (lhs.Length != rhs.Length)
            {
                return false;
            }

            for (int i = 0; i < lhs.Length; ++i)
            {
                if (lhs[i] != rhs[i])
                {
                    return false;
                }
            }

            return true;
        }



        public void GenerateLists()
        {
            // Example usage:
            string goodFilesTextPath = @"C:\Users\majd4\OneDrive\מסמכים\GitHub\AVProject\avproject\white list.txt";
            string directoryPath = @"C:\Users\majd4\OneDrive\מסמכים\GitHub\AVProject\avproject\blacklist";
             badFiles = GetBadFiles(directoryPath);
             goodFiles = GetGoodFiles(goodFilesTextPath); 
        }

        // read md5 hashes frm text file and add them to list of byte array
        static HashSet<byte[]> GetGoodFiles(string textPath)
        {
            HashSet<byte[]> goodFiles = new HashSet<byte[]>();

            // Read the text file line by line
            foreach (string line in File.ReadLines(textPath))
            {
                // Convert each line (MD5 hash) to a byte array
                byte[] md5HashBytes = ConvertHexStringToByteArray(line);
                goodFiles.Add(md5HashBytes);
            }

            return goodFiles;
        }
        //convert hexadecimal string (md5 hash) to byte array 
        static byte[] ConvertHexStringToByteArray(string hexString)
        {
            int length = hexString.Length;
            byte[] byteArray = new byte[length / 2];

            for (int i = 0; i < length; i += 2)
            {
                byteArray[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
            }

            return byteArray;
        }
        static List<byte[]> GetBadFiles(string directoryPath)
        {
            List<byte[]> badFiles = new List<byte[]>();
            DirectoryInfo directory = new DirectoryInfo(directoryPath);
            foreach (FileInfo file in directory.GetFiles())
            {
                // Determine if this is a "bad" file based on some criteria (e.g. file size)
                //add the bad file as byte array
                badFiles_unhash.Add(File.ReadAllBytes(file.FullName));

                // Compute the MD5 hash of the file

                byte[] hash;
                    using (var md5 = MD5.Create())
                    {
                        using (var stream = File.OpenRead(file.FullName))
                        {
                            hash = md5.ComputeHash(stream);
                        }
                    }
                    
                    badFiles.Add(hash);
                
            }
            return badFiles;
        }


    }
}
