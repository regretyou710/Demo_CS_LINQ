using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Demo_CS_LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            var patents = PatentData.Patents;
            var inventors = PatentData.Inventors;


            //LINQ
            //遍歷一個集合
            //from 別名 in 變數 select 別名
            var query1 = from inventor in inventors select inventor;
            Print(query1);


            Console.WriteLine("--------------------");


            //IEnumerable擴展方法(標準查詢運算符)
            var query2 = inventors.Select(i => i);
            Print(query2);


            Console.WriteLine("--------------------");


            //使用where過濾，過濾出19世紀的專利
            var query3 = from patent in patents
                         where patent.YearOfPublication.StartsWith("18")
                         select new { patent.Title };
            Print(query3);


            Console.WriteLine("--------------------");


            //使用select投影，輸出文件名及其最後修改時間 
            //在LINQ語句中使用let定義臨時變量
            var query4 = from filesPath in Directory.GetFiles(@"C:\")
                         let fileInfo = new FileInfo(filesPath)
                         select new { fileInfo.Name, fileInfo.LastAccessTime };
            Print(query4);


            Console.WriteLine("--------------------");


            //使用orderBy排序，按照文件大小排序            
            var query5 = from filesPath in Directory.GetFiles(@"C:\Windows")
                         let fileInfo = new FileInfo(filesPath)
                         orderby fileInfo.Length descending
                         select new { fileInfo.Name, fileInfo.Length };
            Print(query5);


            Console.WriteLine("--------------------");


            //使用group...by...into分組
            var query6 = from i in inventors
                         group i by i.Country into groups                         
                         from elements in groups
                         //orderby elements.Country,elements.Name descending
                         select new { 分組中個數 = groups.Count(), groups.Key, elements };
            Print(query6);
            Console.ReadKey();
        }

        public static void Print<T>(IEnumerable<T> source)
        {
            foreach (var item in source)
            {
                Console.WriteLine(item);
            }
        }
    }
}
