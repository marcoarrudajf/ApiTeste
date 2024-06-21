
using ApiTeste.Domain;
using ApiTeste.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ApiTeste.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers([FromQuery] List<int>? ids = null)
        {
            var users = await _userService.GetUsers(ids);
            if (users == null)
            {
                return NoContent();
            }

            return Ok(users);
        }

        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetUser(id);

            if (user == null)
            {
                return NoContent();
            }

            return Ok(user);
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(UserDto user)
        {
            var idUser = await _userService.AddUser(user);

            return Ok(idUser);
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromQuery] int id, [FromBody] UserDto user)
        {
            var success = await _userService.UpdateUser(id, user);

            if (!success)
            {
                return BadRequest("Não é possível atualizar o usuário.");
            };

            return Ok("Usuario  atualizado com sucesso!");
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            var success = await _userService.DeleteUser(id);

            if (!success)
            {
                return BadRequest("Não é possível excluir o usuário.");
            }
            return Ok();
        }       
    }
}