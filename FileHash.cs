using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace HashFiles
{   // Creates a SHA256 hash for the file passed
    class Program
    {
        public static string ToHexString(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes)
                sb.Append(b.ToString("x2").ToLower());

            return sb.ToString();
        }
        static void Main(string[] args)
        {
            // Create File
            String FilePath = @"C:\Users\jtata\Documents\TestFile.txt";
            try
            {
                // Opens the File, reads it then closes
                String fileContents = File.ReadAllText(@"C:\Users\jtata\Documents\TestFile.txt");
                if (File.Exists(FilePath))
                {
                    // Creates Hash of the file using SHA256
                    using (HashAlgorithm hashAlgorithm = SHA256.Create())
                    {
                        // Encode File and computes the Hash Value

                        byte[] plainText = Encoding.UTF8.GetBytes(fileContents);
                        byte[] hash = hashAlgorithm.ComputeHash(plainText);                     

                        Console.WriteLine("The SHA256 hash Value for TestFiles.txt is: " + "\n" + ToHexString(hash));
                    }

                }                
            }
            catch (Exception e)
            {                
                Console.WriteLine(e.Message);
                Console.WriteLine("Check the path to the file again");

            }

            Console.ReadLine();
        }
    }
}
