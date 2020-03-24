using System.Collections.Generic;
using System.Threading.Tasks;

namespace card_game.Services
{
    public interface IUserService
    {
        Task<List<User>> FindAllPlayers(string UserId);
    }
}