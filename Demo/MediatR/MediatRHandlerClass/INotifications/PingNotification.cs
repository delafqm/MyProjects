using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediatRHandlerClass.INotifications
{
    //多播将消息发布给多个处理程序，消息的处理没有返回值。
    public class PingNotification : INotification
    {
        public string Msg { get; set; }
    }
}
