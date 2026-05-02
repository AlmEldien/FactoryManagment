using FactoryManagment.Application.Abstractions.Services;
using FactoryManagment.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FactoryManagment.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var result = await authService.LoginAsync(request.Email, request.Password, cancellationToken);
        if (result == null)
        {
            return Unauthorized("Invalid email or password.");
        }

        return Ok(result);
    }
}
