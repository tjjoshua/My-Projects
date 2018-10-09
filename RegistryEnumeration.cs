using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RegistryDict
{
    class Program
    {
        public static string ToHexString(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes)
                sb.Append(b.ToString("x2").ToLower());

            return sb.ToString();
        }

        /* This Program prints out the ValueNames of the HKEY_LOCAL_MACHINE Subkeys */
        static void printKeys()
        {
            RegistryKey rkey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\ACPI");

            String[] MyStringArray = rkey.GetSubKeyNames();                          

            foreach (String s in MyStringArray)
            {
                HashAlgorithm hashAlgorithm = SHA256.Create();
                byte[] plainText = Encoding.UTF8.GetBytes(s);
                
                RegistryKey productKey = rkey.OpenSubKey(s);

                if (productKey != null)
                {
                    foreach (var value in productKey.GetValueNames())
                    {                       
                        Console.WriteLine("\tValues: " + value);
                        
                        RegistryKey productKeyValue = productKey.OpenSubKey(value);
                    }
                }
            }

            Console.ReadLine();            
        }
        static void Main(string[] args)
        {
            printKeys();
        }
    }
}
