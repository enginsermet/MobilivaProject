using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobilivaProject.Entities;

namespace MobilivaProject.Configurations
{
    public class OrderDetailsMapping : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasOne(a => a.Order)
                .WithMany(a => a.OrderDetails)
                .HasForeignKey(a => a.OrderId);
        }
    }


}
