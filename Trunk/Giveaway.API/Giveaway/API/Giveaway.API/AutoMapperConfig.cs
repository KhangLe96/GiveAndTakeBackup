using AutoMapper;
using AutoMapper.Configuration;
using Giveaway.API.Shared.Requests;
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
            cfg.CreateMap<CategoryResponse, Category>();

            cfg.CreateMap<Post, PostResponse>()
                .ForMember(
                    destination => destination.Status,
                    map => map.MapFrom(source => source.PostStatus.ToString()))
                .ForMember(
                    destination => destination.EntityStatus,
                    map => map.MapFrom(source => source.EntityStatus.ToString()));
            cfg.CreateMap<PostResponse, Post>();
            cfg.CreateMap<PostRequest, Post>();

            cfg.CreateMap<Image, ImageResponse>();
            cfg.CreateMap<ImageRequest, Image>();

            cfg.CreateMap<ProvinceCity, ProvinceCityResponse>();

            Mapper.Initialize(cfg);
        }
    }
}
