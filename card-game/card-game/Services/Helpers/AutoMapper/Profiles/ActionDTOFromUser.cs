using AutoMapper;
using card_game.ViewModels;

namespace card_game.Services.Helpers.AutoMapper.Profiles
{
    public class ActionDTOFromUser : Profile
    {
        public ActionDTOFromUser()
        {
            CreateMap<User, ActionDTO>();
        }
    }
}