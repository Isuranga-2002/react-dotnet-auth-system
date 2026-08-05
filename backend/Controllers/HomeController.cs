using backend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public HomeController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("db-test")]
        public IActionResult TestDatabase()
        {
            string result = _userRepository.TestDatabaseConnection();

            return Ok(result);
        }
    }
}