using EvenTer.BLL.DTO.User;
using EvenTer.DAL.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserEntity = EvenTer.DAL.Entities.Users.User;

namespace EvenTer.BLL.Interfaces.User.IServices;

public interface IUserService
{
	Task<AuthResponse> RegisterUser(RegisterDTO registerDTO);
	Task<AuthResponse> LoginUser(LoginDTO loginDTO);
	Task UpdateUser(Guid userId, UpdateUserDTO updateUserDTO);
	Task DeleteUser(Guid userId);
	Task<UserEntity> GetUser(Guid userId);
	Task<IEnumerable<UserEntity>> GetUsers();
	Task<UserEntity> GetUserByEmail(string email);
	Task<UserEntity> GetUserByUsername(string username);
}
