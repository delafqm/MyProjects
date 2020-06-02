using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp21
{
    class Program
    {
        private static string USER_NAME = "admin";//用户名
        private static string PASSWORD = "admin";//密码

        //队列名称
        //****==================订单延时队列=======================*****//
        //订单延时队列
        private static string DELAY_QUEUE_NAME = "delay-queue-orderOverTime";
        //订单延时消费队列
        private static string CONSUME_QUEUE_NAME = "consume-queue-orderOverTime";
        //订单延时队列死信交换的交换器名称
        private static string EXCHANGENAME = "exchange-orderOverTime";
        //订单延时队列死信的交换器路由key
        private static string ROUTINGKEY = "routingKey-orderOverTime";

        static void Main(string[] args)
        {

            var RAB_FACTORY = new ConnectionFactory();
            RAB_FACTORY.HostName = "127.0.0.1";
            RAB_FACTORY.Port = 5672;
            RAB_FACTORY.UserName = USER_NAME;
            RAB_FACTORY.Password = PASSWORD;

            using (var connection = RAB_FACTORY.CreateConnection())
            {
                using (var delayChannel = connection.CreateModel())
                {    //延时队列连接通道
                    var consumerChannel = connection.CreateModel();//消费队列连接通道
                    consumerChannel.ExchangeDeclare(EXCHANGENAME, "direct");//创建交换器
                    Dictionary<string, object> arg = new Dictionary<string, object>();
                    //配置死信交换器
                    arg.Add("x-dead-letter-exchange", EXCHANGENAME); //交换器名称
                                                                     //死信交换路由key （交换器可以将死信交换到很多个其他的消费队列，可以用不同的路由key 来将死信路由到不同的消费队列去）
                    arg.Add("x-dead-letter-routing-key", ROUTINGKEY);
                    delayChannel.QueueDeclare(DELAY_QUEUE_NAME, true, false, false, arg);

                    /**
                     直接在发送端创建接收的消费队列
                     */
                    consumerChannel.QueueDeclare(CONSUME_QUEUE_NAME, true, false, false, null);
                    //参数1:绑定的队列名  参数2:绑定至哪个交换器  参数3:绑定路由key
                    consumerChannel.QueueBind(CONSUME_QUEUE_NAME, EXCHANGENAME, ROUTINGKEY);
                    //最多接受条数 0为无限制，每次消费消息数(根据实际场景设置)，true=作用于整channel,false=作用于具体的消费者
                    consumerChannel.BasicQos(0, 1, false);  //这里设置在多个消息者轮留接收消息

                    while (true)
                    {
                        var input = Console.ReadLine();
                        if (input == "exit")
                        {
                            break;
                        }
                        var body = Encoding.UTF8.GetBytes(input + "秒");
                        var properties = delayChannel.CreateBasicProperties();
                        properties.DeliveryMode = 2;
                        properties.Expiration = "1000"; //设置消息的过期时间
                        delayChannel.BasicPublish("", DELAY_QUEUE_NAME, properties, body);
                    }
                }
            }

        }
    }
}
