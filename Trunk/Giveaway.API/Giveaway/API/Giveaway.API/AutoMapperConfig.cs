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
            cfg.CreateMap<Giveaway.API.Shared.Models.Avatar, Avatar>();

            cfg.CreateMap<Avatar, Giveaway.API.Shared.Models.Avatar>();
            cfg.CreateMap<Admin, Giveaway.API.Shared.Models.DTO.Admin>();
            cfg.CreateMap<Avatar, Giveaway.API.Shared.Models.DTO.Avatar>();
            cfg.CreateMap<Setting, Giveaway.API.Shared.Models.DTO.Setting>();
            cfg.CreateMap<SuperAdmin, Giveaway.API.Shared.Models.DTO.SuperAdmin>();
            cfg.CreateMap<Giveaway.Data.Models.Database.User, Giveaway.API.Shared.Models.DTO.User>();
            Mapper.Initialize(cfg);
        }
    }
}
