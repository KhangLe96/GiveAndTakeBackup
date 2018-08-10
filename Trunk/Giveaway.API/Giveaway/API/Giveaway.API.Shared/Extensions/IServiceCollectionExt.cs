using System;
using System.Linq;
using System.Reflection;
using Giveaway.API.Shared.Responses.Post;
using Giveaway.API.Shared.Services.APIs;
using Giveaway.API.Shared.Services.APIs.Realizations;
using Giveaway.API.Shared.Services.Facebook;
using Giveaway.Data.EF.Extensions;
using Giveaway.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Giveaway.API.Shared.Extensions
{
	public static class IServiceCollectionExt
	{
		public static IServiceCollection AddWebDataLayer(this IServiceCollection services)
		{
			services.AddSingleton<IFacebookClient, FacebookClient>();
            services.AddSingleton(typeof(IPostService<>), typeof(PostService<>));

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
			var interfaceAssemblies = new [] { typeof(IServiceCollectionExt).GetTypeInfo().Assembly};
			
			foreach (var assembly in assemblies.Where(m => m.FullName.Contains("Giveaway")))
			{
				foreach (var interfaceAssembly in interfaceAssemblies)
				{
					services.AddSingletonsByConvention(interfaceAssembly, assembly, x => x.Name.EndsWith("Service"));
				}
			}
			
			return services;
		}
	}
}
