using MediatR.Pipeline;
using MediatRHandlerClass.IRequests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRHandlerClass
{
    //消息处理后执行
    public class PingValueRequestPostProcessor : IRequestPostProcessor<PingValue, string>
    {
        public Task Process(PingValue request, string response, CancellationToken cancellationToken)
        {
            //throw new NotImplementedException();

            Console.WriteLine("请求处理后：" + response);

            return Task.CompletedTask;
        }
    }
}
