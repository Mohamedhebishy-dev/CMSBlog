using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Contracts
{
    public interface IUserService
    {
        Task<ApplicationUser> RegisterAsync(ApplicationUser user);
        Task<ApplicationUser> LoginAsync(ApplicationUser user);
        Task LogoutAsync();
        Task<ApplicationUser> GetUserByIdAsync(string id);
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
    }
}
