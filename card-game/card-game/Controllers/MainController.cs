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
           
            return RedirectToAction(nameof(MainController.Game), "Main", new { signInViewModel.UserName});
        }

        [HttpGet("/game/{UserName}")]
        public async Task<ActionResult> Game(string UserName)
        {

            var playingVewModel = new PlayingViewModel();

            playingVewModel.User = await UserService.FindUserByNameAsync(UserName);
            await CardService.NewGameAsync(playingVewModel.User.UserId);
            return View(playingVewModel);
        }
    }
}
