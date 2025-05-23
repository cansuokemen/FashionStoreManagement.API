using FashionStoreManagement.API.Dtos;
using FashionStoreManagement.API.Entities;
using FashionStoreManagement.API.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace FashionStoreManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductSizesController : ControllerBase
    {
        private readonly IProductSizeService _productSizeService;

        public ProductSizesController(IProductSizeService productSizeService)
        {
            _productSizeService = productSizeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductSize>>> GetAll()
        {
            var result = await _productSizeService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{productId}/{sizeId}")]
        public async Task<ActionResult<ProductSize>> GetByIds(int productId, int sizeId)
        {
            var result = await _productSizeService.GetByIdsAsync(productId, sizeId);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ProductSize>> Create([FromBody] ProductSizeCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _productSizeService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetByIds), new { productId = created.ProductId, sizeId = created.SizeId }, created);
        }

        [HttpDelete("{productId}/{sizeId}")]
        public async Task<IActionResult> Delete(int productId, int sizeId)
        {
            var success = await _productSizeService.DeleteAsync(productId, sizeId);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
