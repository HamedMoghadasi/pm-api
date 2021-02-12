using DataAccess.Contracts;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using WebFramework.Api;

namespace PosManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [Route("~/api/Product/create")]
        [HttpPost]
        public async Task<ApiResult> CreateTerminal(ProductDto terminalDto, CancellationToken cancellationToken)
        {
            await _productRepository.Create(terminalDto, cancellationToken);
            return Ok();
        }

    }
}
