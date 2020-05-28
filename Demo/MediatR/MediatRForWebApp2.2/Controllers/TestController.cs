using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using MediatRHandlerClass;
using MediatRHandlerClass.INotifications;
using MediatRHandlerClass.IRequests;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MediatRForWebApp2._2.Controllers
{
    [Route("api/Test")]
    [ApiController]
    public class TestController : Controller
    {
        private IMediator _mediator;

        public TestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //单播有返回值
        [HttpGet("SendPingValue")]
        public async Task<string> GetSendPingValue()
        {
            ////有返回值的Request消息
            var response = await _mediator.Send(new PingValue() { Msg = "1" });

            //发布策略
            //默认情况下，MediatR的消息发布是一个一个执行的，即便是返回Task的情况，也是使用await等待上一个执行完成后才进行下一个的调用。
            // Send和Publish都一样

            return $"{response} - {DateTime.Now}";
        }

        //单播无返回值
        [HttpGet("SendPingNoValue")]
        public Task<string> GetPingNoValue()
        {
            //无返回值的Request消息
            _mediator.Send(new PingNoValue() { Msg = "2" });

            return Task.FromResult($"{DateTime.Now}");
        }

        //多播
        [HttpGet("Publish")]
        public Task<string> GetPingNotification()
        {
            //发布一个PingNotification的消息，不等待执行结束
            _mediator.Publish(new PingNotification() { Msg = "3" });

            //等待执行结束
            //await _mediator.Publish(new PingNotification() { Id = 3 });

            return Task.FromResult($"{DateTime.Now}");
            //return $"{DateTime.Now}";
        }
    }
}
