using Microsoft.AspNetCore.Mvc;
using Trello.Server.Core;
using Trello.Server.Core.DTO;

namespace Trello.Server.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {
        private readonly IAuthService _service;

        public AuthController(IAuthService service) {
            _service = service;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto) {
            try {
                var result = await _service.Login(dto.Email, dto.Password);

                return Ok(result);
            }
            catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO dto) {
            try {
                if (!ModelState.IsValid) {
                    return BadRequest(ModelState);
                }

                await _service.Register(dto.Firstname, dto.Lastname, dto.Email, dto.Password);
                return NoContent();
            }
            catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }
    }
}
