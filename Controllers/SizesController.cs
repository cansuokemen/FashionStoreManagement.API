using FashionStoreManagement.API.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using FashionStoreManagement.API.Entities;


[Route("api/[controller]")]
[ApiController]
public class SizesController : ControllerBase
{
    private readonly ISizeService _sizeService;

    public SizesController(ISizeService sizeService)
    {
        _sizeService = sizeService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Size>>> GetAll()
    {
        var sizes = await _sizeService.GetAllAsync();
        return Ok(sizes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Size>> GetById(int id)
    {
        var size = await _sizeService.GetByIdAsync(id);
        if (size == null) return NotFound();
        return Ok(size);
    }
}
