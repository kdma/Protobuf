using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace ProtoPublic.Service;

public class GreeterService : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> _logger;
    private readonly Random _random;

    public GreeterService(ILogger<GreeterService> logger)
    {
        _logger = logger;
        _random = new Random();
    }
    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        return Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name
        });
    }

    public override async Task<HelloReply2> SayHello2(HelloRequest request, ServerCallContext context)
    {
        if (_random.Next(0,1000) < 500)
        {
            return new HelloReply2()
            {
                Data = new HelloReply()
                {
                    Message = "Hello" + request.Name
                }
            };
        }
        else
        {
            return new HelloReply2()
            {
                Error = new ErrorResponse()
                {
                    ErrorCode = "error",
                    ErrorMessage = "message",
                    IsBusinessError = true,
                    WasSuccessful = false
                }
            };
        }
    }
}
