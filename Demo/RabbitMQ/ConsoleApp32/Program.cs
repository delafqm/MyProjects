using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp32
{
    class Program
    {
        static void Main(string[] args)
        {

            //接收者
            var factory = new ConnectionFactory() { HostName = "127.0.0.1", UserName = "admin", Password = "admin" };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {

                    string name = "zzhello1";
                    channel.ExchangeDeclare(exchange: "exchange-direct", type: "direct");
                    channel.QueueDeclare(name, true, false, false, null);
                    channel.QueueBind(queue: name, exchange: "exchange-direct", routingKey: "routing-delay");
                    channel.BasicQos(0, 1, false);

                    //回调，当consumer收到消息后会执行该函数
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        //Thread.Sleep(1000 * 5);
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine(ea.RoutingKey);
                        Console.WriteLine(" [x] Received {0}", message + "-" + DateTime.Now.ToString());

                        channel.BasicAck(ea.DeliveryTag, false);
                    };

                    //Console.WriteLine("name:" + name);
                    //消费队列"hello"中的消息
                    channel.BasicConsume(queue: name,
                                         autoAck: false,
                                         consumer: consumer);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }

            Console.ReadKey();
        }
    }
}
