using FactoryManagment.Application.Features.Auth.Commands.Login;
using FactoryManagment.Application.Features.Auth.Commands.RefreshToken;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FactoryManagment.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(ISender sender) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var result = await sender.Send(command);
        if (result == null)
        {
            return Unauthorized("Invalid email or password.");
        }

        return Ok(result);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
    {
        var result = await sender.Send(command);
        if (result == null)
        {
            return Unauthorized("Invalid or expired refresh token.");
        }

        return Ok(result);
    }
}
