using System.Reflection;
using AutoMapper;
using card_game.Services.Helpers.AutoMapper.Profiles;
using Microsoft.Extensions.DependencyInjection;

namespace card_game.Services.Helpers.AutoMapper
{
    public static class AutoMapperSetup
    {
        public static void SetUpAutoMapper(this IServiceCollection serviceCollection)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserFromSignInDTO());
                cfg.AddProfile(new ActionDTOFromUser());
            });
        }
    }
}