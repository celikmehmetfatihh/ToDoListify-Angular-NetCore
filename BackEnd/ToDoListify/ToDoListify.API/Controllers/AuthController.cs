using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoListify.API.Models.Domain;
using ToDoListify.API.Models.DTO;
using ToDoListify.API.Repositories.Implementation;
using ToDoListify.API.Repositories.Interface;

namespace ToDoListify.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly ITokenRepositorty tokenRepositorty;

        public AuthController(IUserRepository userRepository, ITokenRepositorty tokenRepositorty)
        {
            this.userRepository = userRepository;
            this.tokenRepositorty = tokenRepositorty;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var user = await userRepository.GetUserByEmail(request.Email);
            if (user == null || user.Password != request.Password)
            {
                ModelState.AddModelError("", "Email or Password Inccorect");
                return ValidationProblem(ModelState);
            }

            var jwtToken = tokenRepositorty.CreateJwtToken(user);

            var response = new LoginResponseDto
            {
                Email = request.Email,
                Username = await userRepository.GetUsernameByEmail(request.Email),
                Token = jwtToken
            };

            // Return the userId to the frontend
            return Ok(response);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var existingUser = await userRepository.GetUserByEmail(request.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("", "A user with this email already exists.");
                return ValidationProblem(ModelState);

            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = request.Username,
                Email = request.Email,
                Password = request.Password,
            };

            await userRepository.AddUserAsync(user);

            // Return a success response
            return Ok(new { Message = "User registered successfully", UserId = user.Id });
        }

        [HttpGet("GetUserIdByEmail")]
        public async Task<IActionResult> GetUserIdByEmail([FromQuery] string email)
        {
            var user = await userRepository.GetUserByEmail(email);
            if (user == null)
            {
                return NotFound("User not found");
            }
            return Ok(user.Id);
        }
    }
}
