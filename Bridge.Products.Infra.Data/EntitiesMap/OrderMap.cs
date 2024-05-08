using Bridge.Products.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Products.Infra.Data.EntitiesMap
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");
            builder.HasKey(o => o.OrderId);

            builder.Property(o => o.OrderId).HasColumnType("int").UseIdentityColumn(1, 1).IsRequired();
            builder.Property(o => o.OrderStatus).HasConversion<string>().HasColumnType("varchar").HasMaxLength(50).IsRequired();
            builder.Property(o => o.OrderDate).HasColumnType("datetime2").IsRequired();
            builder.Property(o => o.PaymentDate).HasColumnType("datetime2").IsRequired(false);

            builder
                .HasMany(o => o.Products)
                .WithMany(p => p.Orders)
                .UsingEntity(
                    "OrderProduct",
                    o => o.HasOne(typeof(Product)).WithMany().HasForeignKey("ProductId").HasPrincipalKey(nameof(Product.ProductId)),
                    p => p.HasOne(typeof(Order)).WithMany().HasForeignKey("OrderId").HasPrincipalKey(nameof(Order.OrderId)),
                    j => j.HasKey("OrderId", "ProductId"));
        }
    }
}
