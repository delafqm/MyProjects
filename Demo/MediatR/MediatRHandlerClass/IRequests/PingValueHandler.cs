using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRHandlerClass.IRequests
{
    //处理类型Ping，返回值是string型
    public class PingValueHandler : IRequestHandler<PingValue, string>
    {
        public Task<string> Handle(PingValue request, CancellationToken cancellationToken)
        {
            //这里可以对实体Ping进行处理
            if (request.Msg == "1")
            {
                return Task.FromResult($"Pong Value - {request.Msg} - {DateTime.Now}");
            }


            //异步完成，返回字符串：Pong
            return Task.FromResult($"Pong - {request.Msg}");
        }
    }

    //同步的消息处理
    //public class PingValueHandler : RequestHandler<PingValue, string>
    //{
    //    protected override string Handle(PingValue request)
    //    {
    //        return "Pong";
    //    }
    //}
}
