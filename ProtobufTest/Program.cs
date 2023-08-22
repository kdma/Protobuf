using System.Net;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Proto.Interfaces.Service;

namespace ProtobufTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.Listen(IPAddress.Any, 1337);
                serverOptions.Listen(IPAddress.Any, 5001, (opt) =>
                {
                    opt.Protocols = HttpProtocols.Http2;
                });
            });
            builder.Services.AddGrpc(opt =>
            {
            });
            var app = builder.Build();
            app.MapGet("/", () => "Hello World!");
            app.MapGrpcService<GreeterService>();
            app.Run();
        }
    }
}