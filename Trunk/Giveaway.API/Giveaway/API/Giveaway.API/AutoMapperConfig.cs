using AutoMapper;
using AutoMapper.Configuration;
using Giveaway.API.Shared.Requests;
using Giveaway.API.Shared.Responses;
using Giveaway.API.Shared.Responses.Post;
using Giveaway.API.Shared.Responses.Report;
using Giveaway.API.Shared.Responses.User;
using Giveaway.Data.Models.Database;

namespace Giveaway.API
{
    public static class AutoMapperConfig
    {
        public static void RegisterModel()
        {
            var cfg = new MapperConfigurationExpression();

            #region User

            cfg.CreateMap<User, Giveaway.API.Shared.Models.DTO.User>();
            cfg.CreateMap<User, UserPostResponse>();

            #endregion

            #region Category

            cfg.CreateMap<Category, Shared.Models.DTO.Category>();
            cfg.CreateMap<Category, CategoryResponse>();
            cfg.CreateMap<CategoryResponse, Category>();

            #endregion

            #region Post

            cfg.CreateMap<Post, PostAppResponse>()
                .ForMember(
                    destination => destination.Status,
                    map => map.MapFrom(source => source.PostStatus.ToString()
                ));
            cfg.CreateMap<PostAppResponse, Post>();
            cfg.CreateMap<Post, PostCmsResponse>()
                .ForMember(
                    destination => destination.EntityStatus,
                    map => map.MapFrom(source => source.EntityStatus.ToString()
                ));
            cfg.CreateMap<PostCmsResponse, Post>();
            cfg.CreateMap<Post, PostReportResponse>();
            cfg.CreateMap<PostRequest, Post>();

            #endregion

            #region Image

            cfg.CreateMap<Image, ImageResponse>();
            cfg.CreateMap<ImageRequest, Image>();

            #endregion

            #region ProvinceCity

            cfg.CreateMap<ProvinceCity, ProvinceCityResponse>();

            #endregion

            #region Report

            cfg.CreateMap<Report, ReportResponse>()
                .ForMember(
                    destination => destination.WarningNumber,
                    map => map.MapFrom(source => source.User.WarningMessages.Count)
                );

            #endregion

            Mapper.Initialize(cfg);
        }
    }
}
