using System.Security.Cryptography;
using System.Text;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }

        public string CalculateMd5Hash(byte[] fileData)
        {
            string hashMd5;
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(fileData);
                var sb = new StringBuilder();
                foreach (var t in hash)
                {
                    sb.Append(t.ToString("X2"));
                }
                hashMd5 = sb.ToString();
            }
            return hashMd5;
        }

    }


}