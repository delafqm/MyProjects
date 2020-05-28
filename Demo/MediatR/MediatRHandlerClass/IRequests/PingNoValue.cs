using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediatRHandlerClass.IRequests
{
    //单播，没有返回值
    public class PingNoValue : IRequest
    {
        public string Msg { get; set; }
    }
}
