using System.Security.Cryptography;
using System.Text;
using Grpc.Net.Client;
using Proto.Interfaces;

namespace ConsoleApp1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //var readAllBytes = File.ReadAllBytes(@"C:\Users\FrancoMartinelli\Desktop\large.jpg");
            //var length = readAllBytes.Length;
            //var hash =  CalculateMd5Hash(readAllBytes);
            //Console.WriteLine(hash);
            while (true)
            {
                await TestComm();
            }
        }

        public static string CalculateMd5Hash(byte[] fileData)
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

    private static async Task TestComm()
        {
            try
            {
                using var channel = GrpcChannel.ForAddress("http://localhost:1337");
                var client = new Greeter.GreeterClient(channel);
                var reply = await client.SayHello2Async(new HelloRequest
                {
                    Name = "Worker"
                });
                switch (reply.ResultDtoCase)
                {
                    case HelloReply2.ResultDtoOneofCase.None:
                        break;
                    case HelloReply2.ResultDtoOneofCase.Data:
                        Console.WriteLine("DATA");
                        break;
                    case HelloReply2.ResultDtoOneofCase.Error:
                        Console.WriteLine("Error");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                await Task.Delay(1000);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}