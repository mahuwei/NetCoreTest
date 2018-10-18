using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Project.Domain;
using Project.Infrastructure.EntityConfigurations;

namespace Project.Infrastructure {
    public class ProjectContext : DbContext {
        public ProjectContext() { }

        public ProjectContext(DbContextOptions<ProjectContext> options)
            : base(options) { }

        public DbSet<Business> Businesses { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void
            OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (optionsBuilder.IsConfigured) {
                base.OnConfiguring(optionsBuilder);
                return;
            }

            optionsBuilder.UseSqlServer(
                "Data Source=(local);Initial Catalog=EFTest;user id=sa;password=estep;");
            // mySql
            //optionsBuilder.UseMySql("Data Source=47.98.58.222;port=3306;Initial Catalog=FBTest;user id=root;password=zkhk2017;Character Set=utf8");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            //将实现了IEntityTypeConfiguration<Entity>接口的模型配置类加入到modelBuilder中，进行注册
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(q =>
                    q.GetInterface(typeof(IEntityTypeConfiguration<>).FullName) !=
                    null);
            foreach (var type in typesToRegister) {
                if (type == typeof(EntityTypeConfiguration<>))
                    continue;
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }
        }
    }
}