using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Grpc.Core;
using GrpcGreeter;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //http1方式要加下面这个段，不然有异常
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);//使用HTTP方式

            //服务端地址
            using var channel = GrpcChannel.ForAddress("http://localhost:50051");
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(
                              new HelloRequest { Name = "GreeterClient123" });

            var reply1 = await client.GetNameAsync(new HelloRequest { Name = "我是谁？" });
            Console.WriteLine();
            Console.WriteLine("Greeting: " + reply.Message);
            Console.WriteLine();
            Console.WriteLine("回复：" + reply1.Message);
            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

            await run1();
        }

        static async Task run1()
        {
            ////http1方式要加下面这个段，不然有异常
            //AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            var channel = GrpcChannel.ForAddress("http://localhost:50051");
            var helloClient = new Greeter.GreeterClient(channel);
            Console.WriteLine("一元调用开始-------");
            //一元调用(同步方法）
            Console.WriteLine("发送：一元同步调用");
            var reply = helloClient.SayHello(new HelloRequest { Name = "一元同步调用" });
            Console.WriteLine($"{reply.Message}");
            //一元调用(异步方法）
            Console.WriteLine("发送：一元异步调用");
            var reply2 = helloClient.SayHelloAsync(new HelloRequest { Name = "一元异步调用" }).GetAwaiter().GetResult();
            Console.WriteLine($"{reply2.Message}");
            Console.WriteLine("一元调用结束-------");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("服务端流开始-------");
            //服务端流
            Console.WriteLine("发送：服务端流");
            var reply3 = helloClient.SayHelloServerStream(new HelloRequest { Name = "服务端流" });
            while (await reply3.ResponseStream.MoveNext())
            {
                Console.WriteLine(reply3.ResponseStream.Current.Message);
            }
            Console.WriteLine("服务端流结束-------");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("客户端流开始-------");
            //客户端流
            using (var call = helloClient.SayHelloClientStream())
            {
                for (var i = 0; i < 3; i++)
                {
                    Console.WriteLine("发送：" + "客户端流" + i.ToString());
                    await call.RequestStream.WriteAsync(new HelloRequest { Name = "客户端流" + i.ToString() });
                }
                await call.RequestStream.CompleteAsync();
                var reply4 = await call;
                Console.WriteLine($"{reply4.Message}");
            }
            Console.WriteLine("客户端流结束-------");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("双向流开始-------");
            //双向流
            using (var call = helloClient.SayHelloStream())
            {
                //Console.WriteLine("启动后台任务以接收消息:输入quit退出");
                //接收
                var readTask = Task.Run(async () =>
                {
                    await foreach (var response in call.ResponseStream.ReadAllAsync())
                    {
                        Console.WriteLine(response.Message);
                    }
                });

                ////连续发送
                //Console.WriteLine("启动后台任务以接收消息");
                //for (var i = 0; i < 3; i++)
                //{
                //    Thread.Sleep(1000);//等待1秒，等服务器回复
                //    Console.WriteLine("发送：" + "双向流" + i.ToString());
                //    await call.RequestStream.WriteAsync(new HelloRequest { Name = "双向流" + i.ToString() });
                //}

                //手动输入发送
                Console.WriteLine("启动后台任务以接收消息:输入quit退出");
                while (true)
                {
                    string input = Console.ReadLine();
                    if (input == "quit")
                    {
                        break;
                    }
                    await call.RequestStream.WriteAsync(new HelloRequest { Name = input });
                }

                await call.RequestStream.CompleteAsync();
                await readTask;
            }
            Console.WriteLine("双向流结束-------");



            Console.ReadKey();
        }
    }
}
