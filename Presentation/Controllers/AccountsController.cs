using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using Presentation.Services;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController(IAccountService accountService) : ControllerBase
{
    private readonly IAccountService _accountService = accountService;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest model)
    {
        var result = await _accountService.RegisterAsync(model.Email, model.Password, model.FirstName, model.LastName, model.StreetName, model.StreetNumber, model.PostalCode, model.City);
        if (!result.Success) return BadRequest(result.Error);

        return Ok(new { message = "User created. Verification email will be sent." });
    }
}

