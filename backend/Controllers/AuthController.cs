using backend.DTOs;
using backend.Models;
using backend.Repositories;
using Microsoft.AspNetCore.Mvc;
using backend.Services;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        private readonly JwtService _jwtService;

        public AuthController(UserRepository userRepository, JwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            if (_userRepository.EmailExists(request.Email))
            {
                return BadRequest("Email already exists.");
            }

            _userRepository.CreateUser(request);

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            User? user = _userRepository.GetUserByEmail(request.Email);

            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            bool isPasswordCorrect =
                BCrypt.Net.BCrypt.Verify(
                    request.Password,
                    user.PasswordHash
                );

            if (!isPasswordCorrect)
            {
                return Unauthorized("Invalid email or password.");
            }

            string token = _jwtService.GenerateToken(user);

            return Ok(new
            {
                Token = token
            });
        }
    }
}