using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRHandlerClass.IRequests
{
    public class PingNoValueHandler : AsyncRequestHandler<PingNoValue>
    {
        protected override async Task Handle(PingNoValue request, CancellationToken cancellationToken)
        {
            //System.Threading.Thread.Sleep(5000); ////等5秒
            await Task.Delay(5000);

            Console.WriteLine($"Pong No Value - {request.Msg} - {DateTime.Now} - 当前线程：{Thread.CurrentThread.ManagedThreadId}");

            //return Task.CompletedTask;
        }
    }
}
