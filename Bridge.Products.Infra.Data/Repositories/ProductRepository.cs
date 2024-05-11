using Bridge.Products.Application.Interfaces.Repositories;
using Bridge.Products.Domain.Entities;
using Bridge.Products.Infra.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Products.Infra.Data.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(BridgeContext context) : base(context)
        {
        }
    }
}
