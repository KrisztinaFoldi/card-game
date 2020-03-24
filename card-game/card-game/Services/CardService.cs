using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace card_game.Services
{
    public class CardService : ICardService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserService _userService;

        public CardService(AppDbContext appDbContext, IUserService userService)
        {
            _appDbContext = appDbContext;
            _userService = userService;
        }

        public async Task NewGameAsync()
        {
            await CreateDeckAsync();
            var Players = await _userService.FindAllPlayers("userIdComingFromHttpContext");
            await DealAsync(Players);
        }

        public async Task CreateDeckAsync()
        {
            var newDeckCreated = new Deck {DeckId = 1};

            for (var k = 0; k < 2; k++)
            {
                for (var i = 0; i < 4; i++)
                {
                    for (var j = 2; j < 15; j++)
                    {
                        var newCardToAdd = new Card();
                        if (i == 0)
                            newCardToAdd.Symbol = "hearts";
                        
                        else if (i == 1)
                            newCardToAdd.Symbol = "diamonds";
                        
                        else if (i == 2)
                            newCardToAdd.Symbol = "spades";
                        
                        else if (i == 3)
                            newCardToAdd.Symbol = "clubs";
                        
                        newCardToAdd.Number = j;
                        newDeckCreated.CardsInDeck.Add(newCardToAdd);
                        await _appDbContext.Cards.AddAsync(newCardToAdd);
                       
                    }
                }
            }

            await _appDbContext.Decks.AddAsync(newDeckCreated);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DealAsync(List<User> PlayersInGame)
        {
            var Deck = await _appDbContext.Decks.FindAsync(1);
            var random = new Random();
            
            foreach (var Player in PlayersInGame)
            {
                var User = await _appDbContext.Users.FindAsync(Player.Id);
                
                for (int i = 0; i < 12;)
                {
                    var IdOfCardToFind = random.Next(1, 120);
                    var CardToDealToPlayer = await _appDbContext.Cards.FindAsync(IdOfCardToFind);
                    if (Deck.CardsInDeck.Contains(CardToDealToPlayer))
                    {
                        if (User.CardsInHand.Count < 9)
                            User.CardsInHand.Add(CardToDealToPlayer);
                        else
                            User.HiddenCards.Add(CardToDealToPlayer);

                        Deck.CardsInDeck.Remove(CardToDealToPlayer);
                        i++;
                    }
                }
            }

            await _appDbContext.SaveChangesAsync();
        }

        public async Task PutDownFourCardAsync(List<User> PlayersInGame, List<Card> CardsToPutDown)
        {
            foreach (var Player in PlayersInGame)
            {
                if (Player.Id == "firstOpponent" || Player.Id == "secondOpponent")
                {
                    var cards = Player.CardsInHand;
                    cards.Sort();
                    cards.Reverse();
                    for (int i = 0; i < 4; i++)
                    {
                        Player.CardsOnTheTable.Add(cards[i]);
                    }
                }
                
                else
                {
                    foreach (var Card in CardsToPutDown)
                    {
                        Player.CardsOnTheTable.Add(Card);
                    }
                }

                await _appDbContext.SaveChangesAsync();

            }
        }
        
    }
}