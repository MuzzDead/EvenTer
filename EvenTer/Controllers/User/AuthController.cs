using EvenTer.BLL.DTO.User;
using EvenTer.BLL.Interfaces.User.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EvenTer.WebAPI.Controllers.User;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
	private readonly IUserService _userService;
	public AuthController(IUserService userService)
	{
		_userService = userService;
	}

	[HttpPost("register")]
	public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
	{
		var response = await _userService.RegisterUser(registerDTO);
		return Ok(response);
	}

	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
	{
		var response = await _userService.LoginUser(loginDTO);
		return Ok(response);
	}

}
