using Bl.Contracts;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace CMSBlog.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> GetUserByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> LoginAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task LogoutAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationUser> RegisterAsync(ApplicationUser user,string Paassord)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> RegisterAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }
    }
}
