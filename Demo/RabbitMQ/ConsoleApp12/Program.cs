using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

namespace ConsoleApp12
{
    class Program
    {
        static void Main(string[] args)
        {
            //接收者
            Console.WriteLine("Start");
            IConnectionFactory connFactory = new ConnectionFactory//创建连接工厂对象
            {
                HostName = "127.0.0.1",//IP地址
                Port = 5672,//端口号
                UserName = "admin",//用户账号
                Password = "admin"//用户密码
            };
            using (IConnection conn = connFactory.CreateConnection())
            {
                using (IModel channel = conn.CreateModel())
                {
                    channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                    String queueName = String.Empty;
                    if (args.Length > 0)
                        queueName = args[0];
                    else
                        queueName = "queue1";
                    //声明一个队列
                    channel.QueueDeclare(
                      queue: queueName,//消息队列名称
                      durable: false,//是否缓存
                      exclusive: false,
                      autoDelete: false,
                      arguments: null
                       );
                    //创建消费者对象
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        Thread.Sleep(1000 * 5);

                        byte[] message = ea.Body.ToArray();//接收到的消息

                        channel.BasicAck(ea.DeliveryTag, false);
                        Console.WriteLine("接收到信息为:" + Encoding.UTF8.GetString(message));
                    };
                    //消费者开启监听
                    channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
                    Console.ReadKey();
                }
            }
        }
    }
}
