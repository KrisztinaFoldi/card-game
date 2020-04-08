using System;
using System.Collections.Generic;

namespace card_game.ViewModels
{
    public class PlayDTO
    {
        public User User { get; set; }
        public Deck Deck { get; set; }
        public List<Card> CardsToDrop { get; set; }

    }
}