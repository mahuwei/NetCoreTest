using System;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Project.Domain;
using Project.Domain.Validations;
using Project.Infrastructure;
using Xunit;

namespace Project.DataTest {
    public class BusinessTest {
        [Fact]
        public void ValidationTest() {
            var business =  new Business {
                Id = Guid.NewGuid(), Status = 0, LastChange = DateTime.Now,
                No = "A1111", Name = ""
            };

            var bv = new BusinessValidator();
            var result = bv.Validate(business);
            Assert.False(result.IsValid);
            
            Assert.Equal("商户名称只能输入中文、数字、字符。",result.Errors.First().ErrorMessage);
            Assert.Equal(1, result.Errors.Count);
        }

        [Fact]
        public void Add() {
            var options = new DbContextOptionsBuilder<ProjectContext>()
                .UseInMemoryDatabase("Add_to_database")
                .Options;

            var no = "001";
            var name = "测试商户";
            using (var dc = new ProjectContext(options)) {
                var business = new Business {
                    Id = Guid.NewGuid(), Status = 0, LastChange = DateTime.Now,
                    No = no, Name = name
                };
                dc.Businesses.Add(business);
                var rowCount = dc.SaveChanges();
                var cc = "内存数据库，不支持：RowVersion";
                Console.WriteLine(
                    $"变更记录数:{rowCount} 新增记录行标识：{(business.RowFlag != null ? Encoding.UTF8.GetString(business.RowFlag) : cc)}");
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