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

        public async Task NewGameAsync(string UserId)
        {
            await CreateSymbolsAsync();
            await CreateDeckAsync();
            await _userService.CreateOpponentsAsync();
            var Players = await _userService.FindAllPlayersAsync(UserId);
            await DealAsync(Players);
            //return await ShowPlayersCardsInHandAsync(UserId);
        }

        public async Task CreateDeckAsync()
        {
            var newDeckCreated = new Deck {DeckId = 1, CardsInDeck = new List<Card>()};

            for (var k = 0; k < 2; k++)
            {
                for (var i = 0; i < 4; i++)
                {
                    for (var j = 2; j < 15; j++)
                    {
                        var newCardToAdd = new Card {SymbolId = i + 1, Number = j};

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
            long deckId = 1;
            var Deck = await _appDbContext.Decks.FindAsync(deckId);
            var random = new Random();
           
            
            foreach (var Player in PlayersInGame)
            {
                var User = await _appDbContext.Users.FindAsync(Player.UserId);
                User.CardsInHand = new List<Card>();
                User.HiddenCards = new List<Card>();

                for (int i = 0; i < 12;)
                {
                    long IdOfCardToFind = random.Next(1, 120);
                    var CardToDealToPlayer = await _appDbContext.Cards.FindAsync(IdOfCardToFind);
                    if (Deck.CardsInDeck.Contains(CardToDealToPlayer))
                    {
                        if (User.CardsInHand.Count < 8)
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
                if (Player.UserId == "firstOpponent" || Player.UserId == "secondOpponent")
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

        public async Task CreateSymbolsAsync()
        {
            var Hearts = new Symbol { SymbolName = "hearts"};
            await _appDbContext.Symbols.AddAsync(Hearts);
            var Diamonds = new Symbol { SymbolName = "diamonds"};
            await _appDbContext.Symbols.AddAsync(Diamonds);
            var Spades = new Symbol { SymbolName = "spades"};
            await _appDbContext.Symbols.AddAsync(Spades);
            var Clubs = new Symbol { SymbolName = "clubs"};
            await _appDbContext.Symbols.AddAsync(Clubs);
            await _appDbContext.SaveChangesAsync();
        }

        //public async Task<List<string>> ShowPlayersCardsInHandAsync(string UserId)
        //{
        //    var User = await _appDbContext.Users.FindAsync(UserId);
        //    var result = new List<string>();
        //    foreach(var Card in User.CardsInHand){
        //        var temp = "";
        //        temp  += Card.Symbol;
        //        temp += Card.Number.ToString();
        //        result.Add(temp);
        //    }
        //    return result;
        //}
    }
}