namespace card_game.Services
{
    public class CardService : ICardService
    {
        private readonly AppDbContext _appDbContext;

        public CardService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void NewGame()
        {
            CreateDeck();
        }

        public void CreateDeck()
        {
            Deck newDeckCreated = new Deck();

            for (int k = 0; k < 2; k++)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 2; j < 15; j++)
                    {
                        if (i == 0)
                        {
                            var newCardToAdd = new Card {Symbol = "hearts", Number = j};
                            newDeckCreated.CardsInDeck.Add(newCardToAdd);
                        }
                        else if (i == 1)
                        {
                            var newCardToAdd = new Card {Symbol = "diamonds", Number = j};
                            newDeckCreated.CardsInDeck.Add(newCardToAdd);
                        }
                        else if (i == 2)
                        {
                            var newCardToAdd = new Card {Symbol = "spades", Number = j};
                            newDeckCreated.CardsInDeck.Add(newCardToAdd);
                        }
                        else if (i == 3)
                        {
                            var newCardToAdd = new Card {Symbol = "clubs", Number = j};
                            newDeckCreated.CardsInDeck.Add(newCardToAdd);
                        }
                    }

                }
            }
        }
    }
}