using System;
using System.ComponentModel.DataAnnotations;

namespace card_game.ViewModels
{
    public class SignInDTO
    {
        [Required]
        [MaxLength(10)]
        [MinLength(3)]
        public string UserName { get; set; }
    }
}
