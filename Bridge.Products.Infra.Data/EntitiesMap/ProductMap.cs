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
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");
            builder.HasKey(p => p.ProductId);

            builder.Property(p => p.ProductId).HasColumnType("int").UseIdentityColumn(1, 1).IsRequired();
            builder.Property(p => p.Name).HasColumnType("varchar").HasMaxLength(100).IsRequired();
            builder.Property(p => p.Price).HasColumnType("decimal(12,2)").IsRequired();
        }
    }
}
