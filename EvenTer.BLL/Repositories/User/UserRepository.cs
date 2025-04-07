using EvenTer.BLL.Interfaces.User.IRepositories;
using EvenTer.DAL.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserEntity = EvenTer.DAL.Entities.Users.User;

namespace EvenTer.BLL.Repositories.User;

public class UserRepository : IUserRepository
{
	private readonly EvenTerDbContext _context;
	public UserRepository(EvenTerDbContext context)
	{
		_context = context;
	}


	public async Task CreateUserAsync(UserEntity userEntity)
	{
		await _context.Users.AddAsync(userEntity);
		await _context.SaveChangesAsync();
	}

	public async Task<IEnumerable<UserEntity>> GetAllUsersAsync()
	{
		return await _context.Users.ToListAsync();
	}

	public async Task<UserEntity> GetUserByEmailAsync(string email)
	{
		return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
	}

	public async Task<UserEntity> GetUserByIdAsync(Guid userId)
	{
		return await _context.Users.FindAsync(userId);
	}

	public async Task<UserEntity> GetUserByUserName(string username)
	{
		return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
	}

	public async Task RemoveUserAsync(Guid userId)
	{
		_context.Users.Remove(await GetUserByIdAsync(userId));
		await _context.SaveChangesAsync();
	}

	public async Task UpdateUserAsync(Guid userId, UserEntity userEntity)
	{
		var user = await GetUserByIdAsync(userId);

		user.FirstName = userEntity.FirstName;
		user.LastName = userEntity.LastName;
		user.Username = userEntity.Username;
		user.Email = userEntity.Email;
		user.Image = userEntity.Image;
		user.UserRole = userEntity.UserRole;
		user.OrganisationName = userEntity.OrganisationName;
		user.HashedPassword = userEntity.HashedPassword;

		await _context.SaveChangesAsync();
	}
}
