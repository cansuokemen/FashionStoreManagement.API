using FashionStoreManagement.API.Dtos;
using FashionStoreManagement.API.Entities;
using FashionStoreManagement.API.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace FashionStoreManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> Create([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _userService.CreateAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Update(int id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _userService.UpdateAsync(id, user);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userService.DeleteAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] UserRegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _userService.RegisterAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login([FromBody] UserLoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var found = await _userService.LoginAsync(dto);
            if (found == null)
                return Unauthorized("Geçersiz e-posta veya şifre.");

            return Ok(found);
        }

    }
}
