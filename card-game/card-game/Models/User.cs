using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace card_game
{
    public class User 
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<Card> CardsInHand { get; set; }
        public List<Card> HiddenCards { get; set; }
        public List<Card> CardsOnTheTable { get; set; }
    }
}