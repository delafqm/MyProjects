using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRHandlerClass.INotifications
{
    //消息处理程序2：处理PingNotification消息
    public class PingNotificationHandler2 : INotificationHandler<PingNotification>
    {
        public async Task Handle(PingNotification notification, CancellationToken cancellationToken)
        {
            //System.Threading.Thread.Sleep(5000); ////等5秒

            await Task.Delay(3000);

            Console.WriteLine($"消息处理者2 - {notification.Msg} - {DateTime.Now} - 当前线程：{Thread.CurrentThread.ManagedThreadId}");

            //return Task.CompletedTask;
        }
    }
}
