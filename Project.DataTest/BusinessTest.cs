using System;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Project.Domain;
using Project.Infrastructure;
using Xunit;

namespace Project.DataTest {
    public class BusinessTest {
        [Fact]
        public void Add() {
            var options = new DbContextOptionsBuilder<ProjectContext>()
                .UseInMemoryDatabase("Add_to_database")
                .Options;

            var no = "001";
            var name = "�����̻�";
            using (var dc = new ProjectContext(options)) {
                var business = new Business {
                    Id = Guid.NewGuid(), Status = 0, LastChange = DateTime.Now,
                    No = no, Name = name
                };
                dc.Businesses.Add(business);
                var rowCount = dc.SaveChanges();
                var cc = "�ڴ����ݿ⣬��֧�֣�RowVersion";
                Console.WriteLine(
                    $"�����¼��:{rowCount} ������¼�б�ʶ��{(business.RowFlag != null ? Encoding.UTF8.GetString(business.RowFlag) : cc)}");
            }

            using (var dc = new ProjectContext(options)) {
                Assert.Equal(1, dc.Businesses.Count());
                var business = dc.Businesses.First();
                Assert.Equal(no, business.No);
                Assert.Equal(name, business.Name);
            }
        }
    }
}