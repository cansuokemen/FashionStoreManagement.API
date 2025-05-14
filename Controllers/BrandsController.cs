using FashionStoreManagement.API.Data;
using FashionStoreManagement.API.Dtos;
using FashionStoreManagement.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FashionStoreManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public BrandsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> GetBrands()
        {
            return await _context.Brands.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetBrand(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null) return NotFound();
            return brand;
        }

        [HttpPost]
        public async Task<ActionResult<Brand>> CreateBrand([FromBody] BrandCreateDto dto)
        {
            var brand = new Brand
            {
                Name = dto.Name
            };

            _context.Brands.Add(brand);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.InnerException?.Message.Contains("IX_Brands_Name") == true)
            {
                return Conflict("Bu marka adı zaten kayıtlı.");
            }

            return CreatedAtAction(nameof(GetBrand), new { id = brand.Id }, brand);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBrand(int id, Brand brand)
        {
            if (id != brand.Id) return BadRequest();
            _context.Entry(brand).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null) return NotFound();
            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
