using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DataAccess.Contracts;
using DataAccess.DataAccess;
using DataAccess.Models;

namespace DataAccess.Repositories
{
    public class ProductRepository:Repository<Product>,IProductRepository
    {
        public ProductRepository(TmsManagementContext dbContext) : base(dbContext)
        {
        }
        public async Task<Product> Create(ProductDto productDto, CancellationToken cancellationToken)
        {
            var data = new Product()
            {
                ProductModel = productDto.ProductModel,
                SerialNumber = productDto.SerialNumber,
                CustomerId = productDto.CustomerId
            };
            try
            {
                await base.AddAsync(data, cancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return data;
        }
	}
}
