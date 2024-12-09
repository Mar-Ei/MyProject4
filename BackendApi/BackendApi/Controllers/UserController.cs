using Domain.Interfaces;
using BackendApi.Models;
using Microsoft.AspNetCore.Mvc;
using BackendApi.Contracts;
using BackendApi.Contracts.User;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _userService.GetAll());
        }


       
        [HttpGet(template:"{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _userService.GetById(id);
            var response = new GetUserResponse()
            {
                Id = result.UserId,
                Email = result.Email,
                Password = result.PasswordHash, 
                CreatedAt = DateTime.Now,
            };

            return Ok(response);
        }



   

        // POST api/<UsersController>
        [HttpPost]
        public async Task<IActionResult> Add(CreateUserRequest request)
        {
            var userDto = new BackendApi.Models.User()
            {
                Email = request.Email,
                PasswordHash = request.Password,
            };
            await _userService.Create(userDto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(BackendApi.Models.User user)
        {
            await _userService.Update(user);
            return Ok();
        }


      
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.Delete(id);
            return Ok();
        }
    }
}
