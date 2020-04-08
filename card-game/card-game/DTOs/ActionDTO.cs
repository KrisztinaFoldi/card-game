using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace card_game.ViewModels
{
    public class ActionDTO
    {
        public string UserName { get; set; }
        public List<Card> CardsInHand { get; set; }
        public List<Card> CardsToPlay { get; set; }
        public bool DrawCards { get; set; }
        public List<Card> CardsInGame { get; set; }
    }
}