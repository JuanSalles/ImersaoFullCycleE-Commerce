using AuthServer.Application.DTOs;
using AuthServer.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ITokenService _tokenService;

    public UserController(IAuthService authService, ITokenService tokenService)
    {
        _authService = authService;
        _tokenService = tokenService;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> RegisterAsync(CreateUserDTO dto)
    {

        IdentityResult result = await _authService.Register(dto.Username, dto.Password, dto.Email);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Created("User Created", null);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(loginDTO login)
    {
        try
        {
            await _authService.Login(login.Username, login.Password);

            var token = await _tokenService.GenerateToken(login.Username);

            return Ok(new { AuthToken = token });

        }
        catch (Exception ex)
        {
            return Unauthorized(ex.Message);
        }

    }

    [Authorize]
    [HttpGet("acesso")]
    public async Task<IActionResult> IsAuthenticated()
    {
        return Ok("");
    }
}
