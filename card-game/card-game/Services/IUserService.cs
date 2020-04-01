using System.Collections.Generic;
using System.Threading.Tasks;
using card_game.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace card_game.Services
{
    public interface IUserService
    {
        Task<List<User>> FindAllPlayersAsync(string UserId);
        Task CreateUserAsync(SignInViewModel viewModel);
        Task<string> FindUserIdByNameAsync(string UserName);
    }
}