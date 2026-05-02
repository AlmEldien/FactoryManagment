using FactoryManagment.Application.Abstractions.Services;
using FactoryManagment.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FactoryManagment.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto, CancellationToken cancellationToken)
    {
        var result = await authService.LoginAsync(dto, cancellationToken);
        if (result == null)
        {
            return Unauthorized(new { message = "Invalid email or password" });
        }

        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto, CancellationToken cancellationToken)
    {
        var result = await authService.RegisterAsync(dto, cancellationToken);

        if (result.Response is null)
        {
            var message = result.Errors?[0] ?? "Registration failed";
            return BadRequest(new { message, errors = result.Errors });
        }

        return Ok(result.Response);
    }

}
