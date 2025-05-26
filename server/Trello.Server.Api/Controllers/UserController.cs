using Microsoft.AspNetCore.Mvc;
using Trello.Server.Core;
using Trello.Server.Core.DTO;

namespace Trello.Server.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service) {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll() {
            try {
                IEnumerable<User> users = await _service.GetAll();

                return Ok(users);
            }
            catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(Guid id) {
            try {
                var user =  await _service.GetById(id);

                return Ok(user);
            }
            catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<User>> Update(Guid id, [FromBody] UserDTO dto) {
            try {
                var user = await _service.GetById(id);

                user.Firstname = dto.Firstname;
                user.Lastname = dto.Lastname;
                user.Email = dto.Email;

                await _service.Update(id, user);
                return NoContent();
            }
            catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id) {
            try {
                await _service.Delete(id);
                return NoContent();
            }
            catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }
    }
}
