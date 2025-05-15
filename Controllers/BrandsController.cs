using FashionStoreManagement.API.Dtos;
using FashionStoreManagement.API.Models;
using Microsoft.AspNetCore.Mvc;
using FashionStoreManagement.API.Interfaces;
using FashionStoreManagement.API.Services;


[ApiController]
[Route("api/[controller]")]
public class BrandsController : ControllerBase
{
    private readonly IBrandService _service;
    public BrandsController(IBrandService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var brand = await _service.GetByIdAsync(id);
        if (brand == null) return NotFound();
        return Ok(brand);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BrandCreateDto dto)
    {
        try
        {
            var brand = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = brand.Id }, brand);
        }
        catch (ArgumentException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Brand brand)
    {
        var success = await _service.UpdateAsync(id, brand);
        if (!success) return BadRequest();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _service.DeleteAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}
