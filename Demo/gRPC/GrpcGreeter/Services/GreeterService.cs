using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace GrpcGreeter
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }
        public override Task<HelloReply> GetName(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply { Message = "name:" + request.Name });
        }

        //一元方式
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            //Thread.Sleep(5000);

            return Task.FromResult(new HelloReply
            {
                Message = "回复： " + new Random().Next().ToString() + request.Name
            });
        }

        //客户端流
        public override async Task<HelloReply> SayHelloClientStream(IAsyncStreamReader<HelloRequest> requestStream, ServerCallContext context)
        {
            string msg = string.Empty;
            //var current = new HelloRequest();
            while (await requestStream.MoveNext())
            {
                msg += requestStream.Current.Name;
            }

            var task = new Task<HelloReply>(() =>
            {
                var reply = new HelloReply()
                {
                    Message = "回复:" + msg//current.Name
                };
                return reply;
            });

            task.Start();

            var result = await task;

            return result;
        }

        //服务端流
        public override async Task SayHelloServerStream(HelloRequest request, IServerStreamWriter<HelloReply> responseStream, ServerCallContext context)
        {
            for (int i = 0; i < 3; i++)
            {
                await responseStream.WriteAsync(new HelloReply() { Message = "回复:" + request.Name + i.ToString() });
            }

        }

        //双向流
        public override async Task SayHelloStream(IAsyncStreamReader<HelloRequest> requestStream,
            IServerStreamWriter<HelloReply> responseStream,
            ServerCallContext context)
        {
            while (await requestStream.MoveNext())
            {
                await responseStream.WriteAsync(new HelloReply() { Message = "回复:" + requestStream.Current.Name });
            }
        }
    }
}
