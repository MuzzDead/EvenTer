using EvenTer.BLL.DTO.User;
using EvenTer.BLL.Interfaces.User.IServices;
using EvenTer.BLL.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EvenTer.WebAPI.Controllers.User;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
	private readonly IUserService _service;
	private readonly ICurrentUserService _currentUserService;
	public UserController(IUserService service, ICurrentUserService currentUserService)
	{
		_service = service;
		_currentUserService = currentUserService;
	}

	[Authorize]
	[HttpGet]
	public async Task<IActionResult> GetCurrentUser()
	{
		var userId = _currentUserService.GetUserId(User);
		if (userId == null)
			return Unauthorized();

		var user = await _service.GetUser(userId.Value);
		return Ok(user);
	}

	[Authorize]
	[HttpPut]
	public async Task<IActionResult> UpdateCurrentUser([FromBody] UpdateUserDTO dto)
	{
		var userId = _currentUserService.GetUserId(User);
		if (userId == null)
			return Unauthorized();

		await _service.UpdateUser(userId.Value, dto);
		return NoContent();
	}

	[Authorize]
	[HttpDelete("{userid:guid}")]
	public async Task<IActionResult> DeleteUser(Guid userid)
	{
		await _service.DeleteUser(userid);
		return NoContent();
	}

	[Authorize(Roles = "Admin")]
	[HttpGet("all")]
	public async Task<IActionResult> GetAllUsers()
	{
		var users = await _service.GetUsers();
		return Ok(users);
	}

	[Authorize(Roles = "Admin")]
	[HttpGet("by-email")]
	public async Task<IActionResult> GetUserByEmail([FromQuery] string email)
	{
		var user = await _service.GetUserByEmail(email);
		return Ok(user);
	}

	[HttpGet("by-username")]
	public async Task<IActionResult> GetUserByUsername([FromQuery] string username)
	{
		var user = await _service.GetUserByUsername(username);
		return Ok(user);
	}
}