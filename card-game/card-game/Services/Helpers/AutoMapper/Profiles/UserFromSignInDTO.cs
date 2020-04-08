using AutoMapper;
using card_game.ViewModels;

namespace card_game.Services.Helpers.AutoMapper.Profiles
{
    public class UserFromSignInDTO : Profile
    {
        public UserFromSignInDTO()
        {
            CreateMap<SignInDTO, User>();
        }
    }
}