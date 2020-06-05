using FullSearchApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FullSearchApp
{
    class Program
    {
        public static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder => { builder.AddConsole(); });

        static void Main(string[] args)
        {
            using (var db = new MyDbContext())
            {
                if (!db.Articles.Any())
                {
                    var articles = new List<Article>{
                        new Article{Title="testing is ok", Abst="this is a test about postgre full text searching"},
                        new Article{Title="tested all bugs", Abst="there is no bug exists in this app"}
                    };

                    db.AddRange(articles);
                    db.SaveChanges();
                }

                #region 英文
                var query = "test";

                var data = db.Articles
                    .Where(p => p.TitleVector.Matches(query) || p.AbstVector.Matches(query))
                    .OrderByDescending(p => p.TitleVector.Rank(EF.Functions.ToTsQuery(query)) * 2.0 + p.AbstVector.Rank(EF.Functions.ToTsQuery(query)))
                    .Select(p => new Article
                    {
                        Title = p.Title,
                        Abst = p.Abst,
                        TitleHL = EF.Functions.ToTsQuery(query).GetResultHeadline(p.Title),
                        AbstHL = EF.Functions.ToTsQuery(query).GetResultHeadline(p.Abst),
                    });
                #endregion

                #region 中文 postgre未安装中文分词zhparser插件
                //var query = "测试";
                //var config = "chinese_zh";

                //var data = db.Articles
                //                    .Where(p => p.TitleVector.Matches(EF.Functions.ToTsQuery(config, query)) ||
                //                        p.AbstVector.Matches(EF.Functions.ToTsQuery(config, query)))
                //                    .OrderByDescending(p => p.TitleVector.Rank(EF.Functions.ToTsQuery(config, query)) * 2.0 +
                //                        p.AbstVector.Rank(EF.Functions.ToTsQuery(config, query)))
                //                    .Select(p => new Article
                //                    {
                //                        Title = p.Title,
                //                        Abst = p.Abst,
                //                        TitleHL = EF.Functions.ToTsQuery(config, query).GetResultHeadline(config, p.Title, ""),
                //                        AbstHL = EF.Functions.ToTsQuery(config, query).GetResultHeadline(config, p.Abst, ""),
                //                    });
                #endregion

                foreach (var article in data)
                {
                    Console.WriteLine($"{article.Title}\t{article.Abst}\t{article.TitleHL}\t{article.AbstHL}");
                }
            }
        }
    }
}
