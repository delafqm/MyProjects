using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace ConsoleApp22
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.HostName = "127.0.0.1";
            factory.UserName = "admin";
            factory.Password = "admin";

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    //创建队列
                    channel.QueueDeclare("consume-queue-orderOverTime", true, false, false, null);
                    //绑定消费者到哪个队列
                    var consumer = new EventingBasicConsumer(channel);
                    channel.BasicConsume("consume-queue-orderOverTime", false, consumer);
                    consumer.Received += (model, ea) =>
                    {
                        //Thread.Sleep(1000 * 2);
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine("已接收： {0}", message);
                        //设置手动确认
                        channel.BasicAck(ea.DeliveryTag, false);
                    };
                    //手动确认OK
                    consumer.HandleBasicConsumeOk("ok");
                    Console.ReadLine();
                }
            }
        }
    }
}
