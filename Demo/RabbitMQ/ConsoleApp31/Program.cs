using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp31
{
    class Program
    {
        static void Main(string[] args)
        {
            //发送者
            var factory = new ConnectionFactory() { HostName = "127.0.0.1", UserName = "admin", Password = "admin" };
            using (var connection = factory.CreateConnection())
            {
                while (Console.ReadLine() != null)
                {
                    using (var channel = connection.CreateModel())
                    {

                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        dic.Add("x-expires", 30000);

                        //DateTime.Now.Second*1000
                        int expires = 3000;//DateTime.Now.Second * 1000;

                        dic.Add("x-message-ttl", expires);//队列上消息过期时间，应小于队列过期时间  
                        dic.Add("x-dead-letter-exchange", "exchange-direct");//过期消息转向路由  
                        dic.Add("x-dead-letter-routing-key", "routing-delay");//过期消息转向路由相匹配routingkey  


                        //创建一个名叫"zzhello"的消息队列
                        channel.QueueDeclare(queue: "zzhello",
                            durable: true,
                            exclusive: false,
                            autoDelete: false,
                            arguments: dic);

                        //内容
                        var message = "Hello World!" + "-" + DateTime.Now.ToString();
                        var body = Encoding.UTF8.GetBytes(message);

                        //向该消息队列发送消息message
                        channel.BasicPublish(exchange: "",
                            routingKey: "zzhello",
                            basicProperties: null,
                            body: body);
                        Console.WriteLine(" [x] Sent {0}", message);
                    }
                }
            }
            Console.WriteLine("结束");
            Console.ReadKey();
        }
    }
}
