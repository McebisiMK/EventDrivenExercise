using EventDrivenExercise.Common.DTOs;
using EventDrivenExercise.Core.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace EventDrivenExercise.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(UserDTO user)
        {
            try
            {
                var addedUser = await _userService.Add(user);

                return Ok(addedUser);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(UserDTO user)
        {
            try
            {
                var updatedUser = await _userService.Update(user);

                return Ok(updatedUser);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _userService.Delete(id);

                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}