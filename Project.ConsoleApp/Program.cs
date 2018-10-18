using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using Project.Domain;
using Project.Infrastructure;

namespace Project.ConsoleApp {
    internal class Program {
        private static void Main(string[] args) {
            Console.WriteLine("Hello World!");
            //RowVersionTest();

            #region NLog

            //var servicesProvider = NLogTest.BuildDi();
            //var runner = servicesProvider.GetRequiredService<NLogTest>();
            //runner.DoAction("Action1");
            //LogManager.Shutdown();

            #endregion


            #region SerilogTest

            SerilogTest.Test();

            #endregion

            Console.ReadLine();
            // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
        }

        private static void RowVersionTest() {
            var rowCount = 0;
            ulong rowFlag;

            using (var dc = new ProjectContext()) {
                dc.Database.Migrate();
                if (dc.Businesses.Any() == false) {
                    var business = new Business {
                        Id = Guid.NewGuid(), Status = 0, LastChange = DateTime.Now,
                        No = "001", Name = "测试商户"
                    };
                    dc.Businesses.Add(business);
                    rowCount = dc.SaveChanges();
                    rowFlag = BitConverter.ToUInt64(business.RowFlag);
                    Console.WriteLine($"新增记录：{rowCount}行，行标识:{rowFlag}");
                }
            }

            Business businessInDb;
            using (var dc = new ProjectContext()) {
                Console.WriteLine("\n读取然后直接修改，查看行标识....");
                businessInDb = dc.Businesses.First();
                rowFlag = BitConverter.ToUInt64(businessInDb.RowFlag);
                Console.WriteLine(
                    $"读取记录，行标识：{rowFlag}");

                businessInDb.Status++;
                rowCount = dc.SaveChanges();
                var rowFlagUpdate = BitConverter.ToUInt64(businessInDb.RowFlag);
                Console.WriteLine(
                    $"修改记录：{rowCount}行，行标识:{rowFlagUpdate}，读取时和修改后的行标识相同：{rowFlagUpdate == rowFlag}");
            }

            using (var dc = new ProjectContext()) {
                Console.WriteLine("\n重建连接后，直接修改(DbSet.Update)，查看行标识....");
                businessInDb.Status++;
                var rowFlagSource = BitConverter.ToUInt64(businessInDb.RowFlag);
                var changeTracking = dc.Businesses.Update(businessInDb);
                rowCount = dc.SaveChanges();
                rowFlag = BitConverter.ToUInt64(businessInDb.RowFlag);

                Console.WriteLine(
                    $"修改记录：{rowCount}行，行标识:{rowFlagSource}，读取时和修改后的行标识相同：{rowFlagSource == rowFlag}，\n不相同说明可以使用这种办法修改，但由于修改前需要判断行标识，所以没什么意义！");
                rowFlag = BitConverter.ToUInt64(changeTracking.Entity.RowFlag);
                Console.WriteLine(
                    $"修改记录：{rowCount}行，行标识:{rowFlagSource}，读取时和changeTracking的行标识相同：{rowFlagSource == rowFlag}");
            }

            using (var dc = new ProjectContext()) {
                Console.WriteLine("\n重建连接后，直接修改(DbSet.Update)，查看行标识....");
                var businssSource = dc.Businesses.Find(businessInDb.Id);
                businessInDb.Status++;
                var rowFlagSource = BitConverter.ToUInt64(businessInDb.RowFlag);
                //var changeTracking = dc.Businesses.Update(businessInDb);
                dc.Entry(businssSource).CurrentValues.SetValues(businessInDb);
                rowCount = dc.SaveChanges();
                rowFlag = BitConverter.ToUInt64(businessInDb.RowFlag);

                Console.WriteLine(
                    $"修改记录：{rowCount}行，行标识:{rowFlagSource}，读取时和修改后的行标识相同：{rowFlagSource == rowFlag}");
                rowFlag = BitConverter.ToUInt64(businssSource.RowFlag);
                Console.WriteLine(
                    $"修改记录：{rowCount}行，行标识:{rowFlagSource}，读取时和(Entry(businssSource).CurrentValues.SetValues(businessInDb)的行标识相同：{rowFlagSource == rowFlag} \n 应该是不相同的，以前就一直用这种办法进行修改。");
            }
        }
    }
}