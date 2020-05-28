using MediatR.Pipeline;
using MediatRHandlerClass.IRequests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRHandlerClass
{
    //消息处理前执行
    public class PingValueRequestPreProcessor : IRequestPreProcessor<PingValue>
    {
        public Task Process(PingValue request, CancellationToken cancellationToken)
        {
            Console.WriteLine("请求处理前：" + request.Msg);
            request.Msg = "10";
            //throw new NotImplementedException();

            return Task.CompletedTask;
        }
    }
}
