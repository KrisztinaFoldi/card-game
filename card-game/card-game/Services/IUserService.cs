using System.Collections.Generic;
using System.Threading.Tasks;
using card_game.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace card_game.Services
{
    public interface IUserService
    {
        Task<List<User>> FindAllPlayersAsync(string UserId);
        Task CreateUserAsync(SignInDTO signInDto);
        Task<User> FindUserByNameAsync(string UserName);
        Task CreateOpponentsAsync();
    }
}