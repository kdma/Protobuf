using System.Diagnostics;
using Grpc.Net.Client;
using Proto.Interfaces;

namespace ConsoleApp1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                //wait for server
                await Task.Delay(5000);

                Debug.WriteLine("START");
                using var channel = GrpcChannel.ForAddress("http://localhost:5001");
                var client = new Greeter.GreeterClient(channel);
                var reply = await client.SayHelloAsync(new HelloRequest
                {
                    Name = "Worker"
                });
                Debug.WriteLine($"Greeting: {reply.Message} -- {DateTime.Now}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }


}