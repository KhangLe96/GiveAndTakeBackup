using AutoMapper;
using AutoMapper.Configuration;
using Giveaway.Data.Models.Database;

namespace Giveaway.API
{
    public static class AutoMapperConfig
    {
        public static void RegisterModel()
        {
            var cfg = new MapperConfigurationExpression();
            cfg.CreateMap<User, Giveaway.API.Shared.Models.DTO.User>();
            cfg.CreateMap<Category, Shared.Models.DTO.Category>();
            Mapper.Initialize(cfg);
        }
    }
}
