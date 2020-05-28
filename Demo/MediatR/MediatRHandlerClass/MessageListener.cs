using MediatR;
using MediatRHandlerClass.INotifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRHandlerClass
{
    /// <summary>
    /// 消息发布监听程序，这个监听程序不需要注册，继承接口INotificationHandler<INotification>，有新消息发布就自动执行
    /// </summary>
    public class MessageListener : INotificationHandler<INotification>
    {
        public Task Handle(INotification notification, CancellationToken cancellationToken)
        {

            if (notification.GetType().FullName == typeof(PingNotification).FullName)
            {
                var newtype = notification as PingNotification;
                Console.WriteLine($"接收到新的消息：{newtype.Msg}");
            }

            return Task.CompletedTask;
        }
    }
}
