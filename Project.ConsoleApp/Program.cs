using System;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project.Infrastructure;

namespace Project.ConsoleApp {
    internal class Program {
        private static void Main(string[] args) {
            Console.WriteLine("Hello World!");

            using (var dc = new ProjectContext()) {
                dc.Database.Migrate();
                //var business = new Business {Id = Guid.NewGuid(),Status = 0,LastChange = DateTime.Now,No = "001",Name = "测试商户"};
                //dc.Businesses.Add(business);
                //var rowCount = dc.SaveChanges();

                var business = dc.Businesses.First();
                var rowVersion = BitConverter.ToInt64(business.RowFlag, 0);
                var getNow = DateTime.FromBinary(rowVersion);
                var s = new NumberToBytesConverter<UInt64>();
                

                Console.WriteLine(
                    $"变更记录数:{1} 新增记录行标识：{rowVersion}");
            }

            Console.ReadLine();
        }
    }
}