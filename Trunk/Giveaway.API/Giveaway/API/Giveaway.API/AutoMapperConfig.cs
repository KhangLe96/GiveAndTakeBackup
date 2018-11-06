using AutoMapper;
using AutoMapper.Configuration;
using Giveaway.API.Shared.Models.DTO;
using Giveaway.API.Shared.Requests.Image;
using Giveaway.API.Shared.Requests.Notification;
using Giveaway.API.Shared.Requests.Post;
using Giveaway.API.Shared.Requests.Request;
using Giveaway.API.Shared.Requests.Response;
using Giveaway.API.Shared.Requests.Warning;
using Giveaway.API.Shared.Responses.Category;
using Giveaway.API.Shared.Responses.Image;
using Giveaway.API.Shared.Responses.Notification;
using Giveaway.API.Shared.Responses.Post;
using Giveaway.API.Shared.Responses.ProviceCity;
using Giveaway.API.Shared.Responses.Report;
using Giveaway.API.Shared.Responses.Request;
using Giveaway.API.Shared.Responses.Response;
using Giveaway.API.Shared.Responses.User;
using Giveaway.API.Shared.Responses.Warning;
using Giveaway.Data.Models.Database;

namespace Giveaway.API
{
    public static class AutoMapperConfig
    {
        public static void RegisterModel()
        {
            var cfg = new MapperConfigurationExpression();

            #region User

            cfg.CreateMap<Data.Models.Database.User, Giveaway.API.Shared.Models.DTO.User>();
            cfg.CreateMap<Data.Models.Database.User, UserPostResponse>();
            cfg.CreateMap<Data.Models.Database.User, UserRequestResponse>();
            cfg.CreateMap<Data.Models.Database.User, UserReportResponse>()
                .ForMember(
                    destination => destination.Status,
                    map => map.MapFrom(source => source.EntityStatus.ToString()
                ));

            #endregion

            #region Category

            cfg.CreateMap<Data.Models.Database.Category, Shared.Models.DTO.Category>();
            cfg.CreateMap<Data.Models.Database.Category, CategoryAppResponse>();
            cfg.CreateMap<CategoryAppResponse, Data.Models.Database.Category>();
            cfg.CreateMap<Data.Models.Database.Category, CategoryCmsResponse>();
            cfg.CreateMap<CategoryCmsResponse, Data.Models.Database.Category>();

            #endregion

            #region Post

            cfg.CreateMap<Post, PostAppResponse>()
                .ForMember(
                    destination => destination.Status,
                    map => map.MapFrom(source => source.PostStatus.ToString())
                )
                .ForMember(
                    destination => destination.CommentCount,
                    map => map.MapFrom(source => source.Comments.Count)
                )
                .ForMember(
                    destination => destination.RequestCount,
                    map => map.MapFrom(source => source.Requests.Count)
                );
            cfg.CreateMap<PostAppResponse, Post>();
            cfg.CreateMap<Post, PostCmsResponse>()
                .ForMember(
                    destination => destination.EntityStatus,
                    map => map.MapFrom(source => source.EntityStatus.ToString())
                )
                .ForMember(
                    destination => destination.Status,
                    map => map.MapFrom(source => source.PostStatus.ToString())
                );
            cfg.CreateMap<PostCmsResponse, Post>();
            cfg.CreateMap<Post, PostReportResponse>();
            cfg.CreateMap<PostRequest, Post>();

            #endregion

            #region Image

            cfg.CreateMap<Image, ImageResponse>();
            cfg.CreateMap<ImageRequest, Image>();
            cfg.CreateMap<ImageDTO, Image>();

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

            #region Warning

            cfg.CreateMap<WarningMessage, WarningResponse>();
            cfg.CreateMap<WarningRequest, WarningMessage>();

            #endregion

            #region Request

            cfg.CreateMap<Request, RequestPostResponse>()
                .ForMember(
                    destination => destination.RequestStatus,
                    map => map.MapFrom(source => source.RequestStatus.ToString())
                );
            cfg.CreateMap<RequestPostRequest, Request>();

			#endregion

			#region Response

			cfg.CreateMap<ResponseRequest, Response>();
			cfg.CreateMap<Response, ResponseRequestResponse>();

			#endregion

	        #region Notification

	        cfg.CreateMap<Notification, NotificationResponse>();
	        cfg.CreateMap<NotificationRequest, Notification>();

			#endregion

			Mapper.Initialize(cfg);
        }
    }
}
