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
        }

        //POST /api/game/sign-in
        [HttpPost("sign-in")]
        public async Task<ActionResult> SignIn(SignInDTO signInDto)
        {
            if (ModelState.IsValid)
            {
                await UserService.CreateUserAsync(signInDto);
                return Ok(signInDto);
            }

            return BadRequest();
        }

        //POST /api/game
        [HttpPost]
        public async Task<ActionResult> PlayCards([FromBody] ActionDTO actionDto)
        {
            return Ok();
        }


        //GET /api/game/kriszti
        [HttpGet("{UserName}")]
        public async Task<ActionResult> Game([FromRoute] string UserName)
        {
            var actionDto = new ActionDTO();
            var User = await UserService.FindUserByNameAsync(UserName);
            CardService.NewGameAsync(User.UserId);
            if (User != null)
            {
                actionDto.UserName = User.UserName;
                actionDto.CardsInHand = User.CardsInHand;
                return Ok(actionDto);
            }

            return BadRequest("No user found");

//            var playingVewModel = new PlayDTO();
//
//            playingVewModel.User = await UserService.FindUserByNameAsync(UserName);
//            await CardService.NewGameAsync(playingVewModel.User.UserId);
//            return View(playingVewModel);
        }
    }
}
