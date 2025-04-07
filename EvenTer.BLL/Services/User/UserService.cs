using EvenTer.BLL.DTO.User;
using EvenTer.BLL.Interfaces.User.IRepositories;
using EvenTer.BLL.Interfaces.User.IServices;
using EvenTer.BLL.Services.Auth;
using EvenTer.DAL.Enums.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserEntity = EvenTer.DAL.Entities.Users.User;

namespace EvenTer.BLL.Services.User;

public class UserService : IUserService
{
	private readonly IUserRepository _repository;
	private readonly JwtTokenService _jwtTokenService;
	private readonly IPasswordHasher<UserEntity> _passwordHasher;
	public UserService(IUserRepository repository, JwtTokenService jwtTokenService, IPasswordHasher<UserEntity> passwordHasher)
	{
		_repository = repository;
		_jwtTokenService = jwtTokenService;
		_passwordHasher = passwordHasher;
	}


	public async Task DeleteUser(Guid userId)
	{
		await _repository.RemoveUserAsync(userId);
	}

	public async Task<UserEntity> GetUser(Guid userId)
	{
		return await _repository.GetUserByIdAsync(userId);
	}

	public Task<UserEntity> GetUserByEmail(string email)
	{
		ArgumentException.ThrowIfNullOrEmpty(email);

		return _repository.GetUserByEmailAsync(email);
	}

	public async Task<UserEntity> GetUserByUsername(string username)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(username);

		return await _repository.GetUserByUserName(username);
	}

	public async Task<IEnumerable<UserEntity>> GetUsers()
	{
		return await _repository.GetAllUsersAsync();
	}

	public async Task<AuthResponse> LoginUser(LoginDTO loginDTO)
	{
		var user = await _repository.GetUserByEmailAsync(loginDTO.Email);
		if (user == null)
			throw new Exception("No user with this email address was found.");

		var result = _passwordHasher.VerifyHashedPassword(user, user.HashedPassword, loginDTO.Password);
		if (result != PasswordVerificationResult.Success)
			throw new Exception("Incorrect password.");

		var token = _jwtTokenService.GenerateToken(user.Id, user.Username, user.UserRole.ToString());

		return new AuthResponse
		{
			Token = token,
			Username = user.Username,
			Role = user.UserRole.ToString()
		};
	}

	public async Task<AuthResponse> RegisterUser(RegisterDTO registerDTO)
	{
		if (registerDTO.Password != registerDTO.ConfirmPassword)
			throw new Exception("Passwords do not match.");

		var existingUser = await _repository.GetUserByEmailAsync(registerDTO.Email);
		if (existingUser != null)
			throw new Exception("A user with this email already exists.");

		var newUser = new UserEntity
		{
			Id = Guid.NewGuid(),
			Image = registerDTO.Image,
			Username = registerDTO.Username,
			FirstName = registerDTO.FirstName,
			LastName = registerDTO.LastName,
			Email = registerDTO.Email,
			OrganisationName = registerDTO.OrganisationName,
			UserRole = UserRole.User,
			CreatedAt = DateTime.UtcNow,
			IsActive = true
		};

		newUser.HashedPassword = _passwordHasher.HashPassword(newUser, registerDTO.Password);

		await _repository.CreateUserAsync(newUser);

		var token = _jwtTokenService.GenerateToken(newUser.Id, newUser.Username, newUser.UserRole.ToString());

		return new AuthResponse
		{
			Token = token,
			Username = newUser.Username,
			Role = newUser.UserRole.ToString()
		};
	}

	public async Task UpdateUser(Guid userId, UpdateUserDTO updateUserDTO)
	{
		await _repository.UpdateUserAsync(userId, await MapToEntity(userId, updateUserDTO));
	}

	private async Task<UserEntity> MapToEntity(Guid userId, UpdateUserDTO updateUserDTO)
	{
		var user = await _repository.GetUserByIdAsync(userId);

		return new UserEntity
		{
			Username = updateUserDTO.Username,
			FirstName = updateUserDTO.FirstName,
			LastName = updateUserDTO.LastName,
			Email = updateUserDTO.Email,
			Image = updateUserDTO.Image,
			HashedPassword = user.HashedPassword,
			UserRole = user.UserRole,
			OrganisationName = user.OrganisationName
		};
	}
}
