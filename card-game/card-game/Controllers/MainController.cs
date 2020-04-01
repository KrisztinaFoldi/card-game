using System.Threading.Tasks;
using card_game.Services;
using card_game.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace card_game.Controllers
{
    public class MainController : Controller
    {
        private readonly ICardService CardService;
        private readonly IUserService UserService;
        //private readonly UserManager<User> UserManager;

        public MainController(ICardService cardService, IUserService userService)
        {
            CardService = cardService;
            UserService = userService;
            //UserManager = userManager;
        }


        [HttpGet("/")]
        public ActionResult Home()
        {
            return View();
        }

        [HttpPost("/")]
        public async Task<ActionResult> SignIn(SignInViewModel signInViewModel)
        {
            if (!ModelState.IsValid)
            {
               return RedirectToAction("Home");
            }

            await UserService.CreateUserAsync(signInViewModel);
            var UserId = await UserService.FindUserIdByNameAsync(signInViewModel.NickName);
            return RedirectToAction("Game");
        }

        [HttpGet("/game")]
        public ActionResult Game()
        {
            return View();
        }
    }
}
