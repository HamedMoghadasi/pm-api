using DataAccess.DataAccess;
using DataAccess.Models;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> Create(ProductDto productDto, CancellationToken cancellationToken);
    }
}
