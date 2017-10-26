using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;

namespace ManualTest
{
    class Program
    {
        static void Main(string[] args)
        {

            string macAdress = GetMacAddress();
            string hash = GetHashString(macAdress);
            Console.WriteLine("result : " + hash);

            System.IO.File.WriteAllText(@".\Result.txt", hash);

            Console.ReadLine();

        }
        private static string GetMacAddress()
        {
            string macAddress = string.Empty;

            var interfaces = NetworkInterface.GetAllNetworkInterfaces();
            NetworkInterface nic = interfaces.Where(n => n.NetworkInterfaceType == NetworkInterfaceType.Ethernet).FirstOrDefault();
            return nic.GetPhysicalAddress().ToString();
        }


        public static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = MD5.Create();  //or use SHA256.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
    }
}
