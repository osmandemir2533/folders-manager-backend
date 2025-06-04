using Folders_Auction_Business.Services;
using Folders_Auction_Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Folders_Auction.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto dto)
        {
            var token = await _authService.RegisterAsync(dto);
            return Ok(new { token });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            var token = await _authService.LoginAsync(dto);
            return Ok(new { token });
        }
    }
}
