using System.Collections.Generic;
using System.Threading.Tasks;

namespace card_game.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _appDbContext;

        public UserService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public async Task<List<User>> FindAllPlayers(string UserId)
        {
            var Players = new List<User>
            {
                await _appDbContext.Users.FindAsync(UserId),
                await _appDbContext.Users.FindAsync("firstOpponent"),
                await _appDbContext.Users.FindAsync("secondOpponent")
            };
            
            return Players;
        }
    }
}