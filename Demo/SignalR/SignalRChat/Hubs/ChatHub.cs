using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage1(string user, string message)
        {
            var abc = Context;
            if (message == "over")
            {
                await Clients.All.SendAsync("over", "完成");
                //return await Task.CompletedTask;
                return;
            }
            message += ",ok";
            //发送到客户端，接收事件
            //await Clients.All.SendAsync("ReceiveMessage1", user, message);
            //向特定用户发送信息
            //await Clients.User(user).SendAsync("ReceiveMessage1", user, message);

            await Clients.Group("SignalR Users").SendAsync("ReceiveMessage1", user, message);
        }

        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnConnectedAsync();
        }
    }
}
