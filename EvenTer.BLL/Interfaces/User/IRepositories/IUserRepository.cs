using EvenTer.DAL.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserEntity = EvenTer.DAL.Entities.Users.User;

namespace EvenTer.BLL.Interfaces.User.IRepositories;

public interface IUserRepository
{
	Task CreateUserAsync(UserEntity userEntity);
	Task UpdateUserAsync(Guid userId, UserEntity userEntity);
	Task RemoveUserAsync(Guid userId);
	Task<IEnumerable<UserEntity>> GetAllUsersAsync();
	Task<UserEntity> GetUserByIdAsync(Guid userId);
	Task<UserEntity> GetUserByUserName(string username);
	Task<UserEntity> GetUserByEmailAsync(string email);
}
