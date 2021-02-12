using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DataAccess.DataAccess;
using DataAccess.Models;

namespace DataAccess.Contracts
{
    public interface IProductRepository:IRepository<Product>
    {
        Task<Product> Create(ProductDto productDto, CancellationToken cancellationToken);
    }
}
