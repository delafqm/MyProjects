using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediatRHandlerClass.IRequests
{
    //单播，有返回值，返回值string
    public class PingValue : IRequest<string>
    {
        public string Msg { get; set; }
    }
}
