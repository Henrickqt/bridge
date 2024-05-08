using Bridge.Products.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Products.Infra.Data
{
    public class BridgeContext : DbContext
    {
        public DbSet<Product> Product { get; set; } = null!;

        public BridgeContext(DbContextOptions<BridgeContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BridgeContext).Assembly);
        }
    }
}
