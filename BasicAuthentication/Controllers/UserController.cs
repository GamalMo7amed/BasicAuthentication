using BasicAuthentication.Models;
using BasicAuthentication.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BasicAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "BasicAuthentication")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> Get()
        {
            var users =  await _userService.GetUsersAsync();
            var usersDto = users.Select(u => new UserDTO
            {
                Id = u.Id,
                FirstName = u.FirstName,    
                LastName = u.LastName,
                Email = u.Email,
                Password = u.Password,
            }).ToList();
            return Ok(usersDto);
        }
    
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> Get(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound("User Not Found!");
            var userDto = new UserDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password
            };
            return Ok(userDto);
        }
        [HttpPost]
        public async Task<ActionResult<UserDTO>> Create([FromBody] UserDTO userDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var user = new User
            {
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Email = userDTO.Email,
                Password = userDTO.Password,
            };
            user = await _userService.CreateUserAsync(user);
            userDTO.Id = user.Id;
            return CreatedAtAction(nameof(Get), new { id = user.Id }, userDTO);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserDTO userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (id != userDto.Id)
                return BadRequest("ID in URL doesn't match ID in payload.");
            var user = new User
            {
                Id = userDto.Id,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                Password = userDto.Password
            };
           var updated = await _userService.UpdateUserAsync(user);
            if (!updated)
                return NotFound();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _userService.DeleteUserAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
    }
}
