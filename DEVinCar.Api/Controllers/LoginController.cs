using DEVinCar.Api.Services;
using DEVinCar.Domain.DTOs;
using DEVinCar.Domain.Interfaces.Services;
using DEVinCar.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DEVinCar.Api.Controllers;

[ApiController]
[Route("api/login")]
[Authorize]
public class LoginController : ControllerBase
{
    private readonly ILoginService _service;

    public LoginController(ILoginService service)
    {
        _service = service;
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult Login([FromBody] LoginDTO loginDto)
    {
        User user = _service.CheckLogin(loginDto.Email, loginDto.Password);

        var token = TokenService.GenerateToken(user);

        return Ok(new { token });
    }
}
