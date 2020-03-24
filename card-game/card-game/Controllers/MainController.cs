using card_game.Services;
using Microsoft.AspNetCore.Mvc;

namespace card_game.Controllers
{
    public class MainController : Controller
    {
        private readonly ICardService CardService;
        private readonly IUserService UserService;

        public MainController(ICardService cardService, IUserService userService)
        {
            CardService = cardService;
            UserService = userService;
        }


        // GET
        [HttpGet("/")]
        public ActionResult Index()
        {
            return View();
        }
    }
}