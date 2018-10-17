using System;
using Microsoft.EntityFrameworkCore;
using Project.Domain;
using Project.Infrastructure;

namespace Project.ConsoleApp {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");

            using (var dc = new ProjectContext()) {
                dc.Database.Migrate();
                var business = new Business {Id = Guid.NewGuid(),Status = 0,LastChange = DateTime.Now,No = "001",Name = "测试商户"};
                dc.Businesses.Add(business);
                var rowCount = dc.SaveChanges();
                Console.WriteLine($"变更记录数:{rowCount} 新增记录行标识：{business.RowFlag}");
            }

            Console.ReadLine();
        }
    }
}
