using System.Collections.Generic;
using System.Threading.Tasks;

namespace card_game.Services
{
    public interface ICardService
    {
        Task NewGameAsync(string UserId);
        Task CreateDeckAsync();
        Task DealAsync(List<User> PlayersInGame);
        Task PutDownFourCardAsync(List<User> PlayersInGame, List<Card> CardsToPutDown);
        //Task<List<string>> ShowPlayersCardsInHandAsync(string UserId);
        Task CreateSymbolsAsync();
    }
}