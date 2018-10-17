using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain;

namespace Project.Infrastructure.EntityConfigurations {
    internal class BusinessTypeConfiguration : EntityTypeConfiguration<Business> {
        public override void Configure(EntityTypeBuilder<Business> builder) {
            base.Configure(builder);
            builder.Property(b => b.No).IsRequired().HasMaxLength(8);
            builder.Property(b => b.Name).IsRequired().HasMaxLength(50);
            builder.Property(b => b.Address).HasMaxLength(100);
        }
    }
}