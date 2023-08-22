using Grpc.Net.Client;
using Proto.Interfaces;

namespace ConsoleApp1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            while (true)
            {
                 await TestComm();
            }
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