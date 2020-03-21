using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace card_game
{
    public class User : IdentityUser
    {
        public List<Card> CardsInHand { get; set; }
        public List<Card> HiddenCards { get; set; }
        public List<Card> CardsOnTheTable { get; set; }
    }
}