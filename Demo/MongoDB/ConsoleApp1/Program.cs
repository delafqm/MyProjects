using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ConsoleApp1
{
    class Program
    {
        //IMongoClient client = null;
        //IMongoDatabase database = null;

        static void Main(string[] args)
        {
            //InsertData();
            //QueryData1();
            //EditData();
            DeleteData();
        }

        //插入新数据
        static void InsertData()
        {

            // 1.建立数据库连接
            // 使用连接字符串连接
            var client = new MongoClient("mongodb://localhost:27017");
            // 制定多个地址和端口，让程序自动选择一个进行连接。
            //var client = new MongoClient("mongodb://localhost:27017,localhost:27018,localhost:27019");

            // 2.获取数据库foo，如果没有则新建
            var database = client.GetDatabase("foo");

            // 3.获取数据集Collection，获取表bar，如果没有则新建
            // 这里需要数据类型，如果没有可以先使用<BsonDocument>类型，这里用<MyDocument>
            var collection = database.GetCollection<BsonDocument>("bar");
            //var collection = database.GetCollection<MyDocument>("bar");

            // 4.插入数据

            // 定义数据
            var document = new BsonDocument
            {
                { "name", "MongoDB" },
                { "type", "Database" },
                { "count", 1 },
                { "info", new BsonDocument
                    {
                        { "x", 203 },
                        { "y", 102 }
                    }}
            };

            //var document = new MyDocument()
            //{
            //    name = "MSsql",
            //    type = "Database",
            //    count = 2,
            //    info = new MyDocumentInfo()
            //    {
            //        x = 100,
            //        y = 200
            //    }
            //};

            //同步插入数据
            collection.InsertOne(document);
            //异步插入数据
            //await collection.InsertOneAsync(document);

            // 多条数据
            //var documents = Enumerable.Range(0, 100).Select(i => new BsonDocument("counter", i));
            // 批量插入
            //collection.InsertMany(documents);
            // 批量异步插入
            //await collection.InsertManyAsync(documents);


            var count = collection.Count(new BsonDocument());  //产生一个空BsonDocument的过滤器，指对该类型的文档进行计数。

            Console.WriteLine(string.Format("已插入{0}条数据", count));


            
        }

        //查找数据
        static void QueryData()
        {
            // 1.建立数据库连接
            var client = new MongoClient("mongodb://localhost:27017");

            // 2.获取数据库foo，如果没有则新建
            var database = client.GetDatabase("foo");

            Console.WriteLine("查第一条数据");
            // 3.获取数据集Collection，获取表bar，如果没有则新建
            var collection = database.GetCollection<BsonDocument>("bar");
            var document1 = collection.Find(new BsonDocument()).FirstOrDefault();
            
            Console.WriteLine(document1.ToString());


            Console.WriteLine("查全部数据");
            //var documents = collection.Find(new BsonDocument()).ToList();     //同步
            //var document2 = collection.Find(new BsonDocument()).ToCursor();   //同步
            //await collection.Find(new BsonDocument()).ForEachAsync(d => Console.WriteLine(d));    //异步
            foreach (var item in collection.Find(new BsonDocument()).ToCursor().ToEnumerable())
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("使用筛选数据");
            //创建筛选器
            var filter = Builders<BsonDocument>.Filter.Eq("name", "MongoDB");
            //使用筛选器进行查找
            var document2 = collection.Find(filter).First();
            Console.WriteLine(document2);

            
        }

        static void QueryData1()
        {
            // 1.建立数据库连接
            var client = new MongoClient("mongodb://localhost:27017");

            // 2.获取数据库foo，如果没有则新建
            var database = client.GetDatabase("foo");

            Console.WriteLine("查第一条数据");
            // 3.获取数据集Collection，获取表bar，如果没有则新建
            var collection = database.GetCollection<MyDocument>("bar");

            //分页查询
            var documents = collection.Find<MyDocument>(_ => true).Skip(2).Limit(2).ToList();
            //var documents = collection.Find<MyDocument>(u => u.id == "").ToList();

            Console.WriteLine("使用条件查询");
            foreach (var item in documents)
            {
                Console.WriteLine(item.id);
            }
        }

        static void EditData()
        {
            // 1.建立数据库连接
            var client = new MongoClient("mongodb://localhost:27017");

            // 2.获取数据库foo，如果没有则新建
            var database = client.GetDatabase("foo");

            //Console.WriteLine("查第一条数据");
            // 3.获取数据集Collection，获取表bar，如果没有则新建
            var collection = database.GetCollection<MyDocument>("bar");

            //设置更新的数据
            var update = Builders<MyDocument>.Update.Set(p => p.name, "MySql");

            //更新指定条件的一条数据
            var result = collection.UpdateOne<MyDocument>(u => u.counter == 5, update);

            Console.WriteLine(result.ModifiedCount);
        }

        static void DeleteData()
        {
            // 1.建立数据库连接
            var client = new MongoClient("mongodb://localhost:27017");

            // 2.获取数据库foo，如果没有则新建
            var database = client.GetDatabase("foo");

            //Console.WriteLine("查第一条数据");
            // 3.获取数据集Collection，获取表bar，如果没有则新建
            var collection = database.GetCollection<MyDocument>("bar");

            //使用_id来筛选数据
            var filter = Builders<MyDocument>.Filter.Eq("_id", new ObjectId("5ed7121c5e3dec3ebcbd86f9"));

            //使用筛选器获取数据集合中的第一条
            var items = collection.Find(filter).FirstOrDefault();

            //筛选出数据并删除
            var result1 = collection.FindOneAndDelete(filter);

            Console.WriteLine(result1.id);

            //删除指定条件的一条数据
            var result = collection.DeleteOne<MyDocument>(u => u.counter == 6);

            Console.WriteLine(result.DeletedCount);
        }
    }

    class MyDocument
    {
        public object id { get; set; }

        public int mid { get; set; }

        public string name { get; set; }

        public string type { get; set; }

        public int count { get; set; }

        public MyDocumentInfo info { get; set; }

        public int counter { get; set; }
    }

    class MyDocumentInfo
    {
        public int x { get; set; }
        public int y { get; set; }
    }
}
