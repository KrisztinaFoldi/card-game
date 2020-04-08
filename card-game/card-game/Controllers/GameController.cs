using System.Threading.Tasks;
using card_game.Services;
using card_game.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace card_game.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly ICardService CardService;
        private readonly IUserService UserService;
        //private readonly UserManager<User> UserManager;

        public GameController(ICardService cardService, IUserService userService)
        {
            CardService = cardService;
            UserService = userService;
            //UserManager = userManager;
        }


//        [HttpGet]
//        public ActionResult Home()
//        {
//            return View();
//        }

        [HttpPost]
        public async Task<ActionResult> SignIn(SignInDTO signInDto)
        {
            if (ModelState.IsValid)
            {
                await UserService.CreateUserAsync(signInDto);
                return Ok();
                
            }
            
            return BadRequest();
        }

        [HttpGet("/{UserName}")]
        public async Task<ActionResult> Game([FromRoute]string UserName, PlayDTO playDto)
        {

            var playingVewModel = new PlayDTO();

            playingVewModel.User = await UserService.FindUserByNameAsync(UserName);
            await CardService.NewGameAsync(playingVewModel.User.UserId);
            return View(playingVewModel);
        }

        //[HttpPost("/game/{UserName}")]
        //public async Task<ActionResult> DropCards(string UserName, PlayingViewModel playingViewModel)
        //{

        //}
    }
}
