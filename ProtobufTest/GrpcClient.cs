using Grpc.Net.Client;
using ProtoPublic;

namespace ProtobufTest
{
    public class GRpcHostedClient : BackgroundService
    {
        private readonly ILogger<GRpcHostedClient> _logger;
        private readonly string _url;
        public GRpcHostedClient(ILogger<GRpcHostedClient> logger, IConfiguration configuration)
        {
            _logger = logger;
            _url = "http://localhost:1337";
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
            try
            {
                await Task.Delay(10000);

                using var channel = GrpcChannel.ForAddress(_url);
                var client = new Greeter.GreeterClient(channel);
                while (!stoppingToken.IsCancellationRequested)
                {
                    var reply = await client.SayHelloAsync(new HelloRequest
                    {
                        Name = "Worker"
                    });
                    _logger.LogInformation($"Greeting: {reply.Message} -- {DateTime.Now}");
                    await Task.Delay(1000, stoppingToken);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
           
        }
    }
}
