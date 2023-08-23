using Grpc.Core;
using Proto.Interfaces;
using System;

namespace ProtobufTest
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        private readonly Random _random;

        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
            _random = new Random();
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
