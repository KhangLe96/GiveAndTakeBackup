using AutoMapper;
using AutoMapper.Configuration;
using Giveaway.API.Shared.Responses;
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

            cfg.CreateMap<Category, CategoryResponse>();
            cfg.CreateMap<CategoryResponse, Category> ();

            cfg.CreateMap<Post, PostResponse>();
            cfg.CreateMap<PostResponse, Post>();

            Mapper.Initialize(cfg);
        }
    }
}
