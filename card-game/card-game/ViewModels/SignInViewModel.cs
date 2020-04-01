using System;
using System.ComponentModel.DataAnnotations;

namespace card_game.ViewModels
{
    public class SignInViewModel
    {
        [Required]
        [MaxLength(10)]
        [MinLength(3)]
        public string NickName { get; set; }
    }
}
