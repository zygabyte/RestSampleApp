using Microsoft.AspNetCore.Mvc;
using RestSampleApp.Models;
using RestSampleApp.Services.UserService;

namespace RestSampleApp.Controllers
{
    [ApiController]
    [Route("/api/user")]
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<long>> CreateUser([FromBody] UserRequest userRequest)
        {
            var result = await userService.CreateAsync(userRequest);
            return result > 0 ? Ok(result) : BadRequest("Error creating user");
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserResponse>> GetUsers()
        {
            var result = userService.GetAllAsync();
            return Ok(result);
        }
        
        [HttpPost("login")]
        public async Task<ActionResult<IEnumerable<UserResponse>>> LogInAsync([FromBody] UserRequest userRequest)
        {
            var result = await userService.LoginAsync(userRequest);
            return result ? Ok(result) : BadRequest("Invalid credentials");
        }
    }
}
