using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EvenTer.BLL.Interfaces.User.IServices;

public interface ICurrentUserService
{
	Guid? GetUserId(ClaimsPrincipal user);
}
