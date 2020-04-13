using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public GameController(ICardService cardService, IUserService userService, IMapper mapper)
        {
            CardService = cardService;
            UserService = userService;
            _mapper = mapper;
        }

        //POST /api/game/sign-in
        [HttpPost("sign-in")]
        public async Task<ActionResult> SignIn(SignInDTO signInDto)
        {
            if (ModelState.IsValid)
            {
                await UserService.CreateUserAsync(signInDto);
                var User = await UserService.FindUserByNameAsync(signInDto.UserName);
                await CardService.NewGameAsync(User.UserId);
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
            var User = await UserService.FindUserByNameAsync(UserName);
            if (User != null)
            {
                var actionDto = _mapper.Map<User, ActionDTO>(User);
                return Ok(actionDto);
            }

            return BadRequest("No user found");
        }
    }
}
