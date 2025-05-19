using FashionStoreManagement.API.Dtos;
using FashionStoreManagement.API.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetCart(int userId)
    {
        var items = await _cartService.GetCartItemsAsync(userId);
        return Ok(items);
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart([FromBody] CartItemDto dto)
    {
        try
        {
            await _cartService.AddToCartAsync(dto);
            return Ok("Ürün sepete eklendi.");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateQuantity([FromBody] CartItemDto dto)
    {
        try
        {
            await _cartService.UpdateQuantityAsync(dto);
            return Ok("Sepet güncellendi.");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{userId}/{productId}/{sizeId}")]
    public async Task<IActionResult> RemoveFromCart(int userId, int productId, int sizeId)
    {
        await _cartService.RemoveFromCartAsync(userId, productId, sizeId);
        return Ok("Ürün sepetten çıkarıldı.");
    }
}
