using System.Collections.Generic;
using System.Threading.Tasks;
using card_game.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace card_game.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _appDbContext;
        //private readonly UserManager<User> _userManager;

        public UserService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            //_userManager = userManager;
        }


        public async Task CreateUserAsync(SignInViewModel viewModel)
        { 
        //    var User = new User { UserName = viewModel.NickName };
            await _appDbContext.Users.AddAsync(new User{UserName = viewModel.NickName });
            await _appDbContext.SaveChangesAsync();
            //var result = await _userManager.CreateAsync(User);

    
        }

        public async Task<List<User>> FindAllPlayersAsync(string UserId)
        {
            var Players = new List<User>
            {
                await _appDbContext.Users.FindAsync(UserId),
                await _appDbContext.Users.FindAsync("firstOpponent"),
                await _appDbContext.Users.FindAsync("secondOpponent")
            };
            
            return Players;
        }

        public async Task<string> FindUserIdByNameAsync(string UserName)
        {
            var User = await _appDbContext.Users.FirstOrDefaultAsync(x =>
            
                x.UserName == UserName
            );

            return User.UserId;
        }
    }
}