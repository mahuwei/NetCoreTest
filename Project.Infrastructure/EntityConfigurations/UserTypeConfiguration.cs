using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain;

namespace Project.Infrastructure.EntityConfigurations {
    internal class UserTypeConfiguration : EntityTypeConfiguration<User> {
        public override void Configure(EntityTypeBuilder<User> builder) {
            base.Configure(builder);
            builder.Property(b => b.WorkNo).IsRequired().HasMaxLength(8);
            builder.Property(b => b.Name).IsRequired().HasMaxLength(30);
            builder.Property(b => b.MobileNo).HasMaxLength(20);
            builder.Property(b => b.Permission).HasMaxLength(100);

            builder.Property(b => b.BusinessId).IsRequired();
            builder.HasOne(b => b.Business)
                .WithMany()
                .HasForeignKey(b => b.BusinessId);
        }
    }
}