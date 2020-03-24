using System.Collections.Generic;

namespace card_game
{
    public class Deck
    {
        public long DeckId { get; set; }
        public List<Card> CardsInDeck { get; set; }
        public List<Card> CardsOutOfGame { get; set; }
        public List<Card> CardsInGame { get; set; }
    }
}