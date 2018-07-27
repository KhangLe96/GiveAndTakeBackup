using System.IO;
using Giveaway.API.Shared.Helpers;
using Giveaway.Data.EF;
using Giveaway.Util.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace Giveaway.API.Shared.Extensions
{
	/// <summary>
	/// Extends IApplicationBuilder
	/// </summary>
	public static class ApplicationBuilderExt
	{
		private static IHostingEnvironment Environment => ServiceProviderHelper.Current.GetService<IHostingEnvironment>();

		/// <summary>
		/// Configures the static content
		/// </summary>
		/// <param name="app"></param>
		public static void UseContent(this IApplicationBuilder app)
		{
			app.UseStaticFiles();
			var path = ContentHelper.GetRootPath();
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}

			var staticFilePath = Path.Combine(Environment.WebRootPath, Const.StaticFilesFolder);
			if (!Directory.Exists(staticFilePath))
			{
				Directory.CreateDirectory(staticFilePath);
			}

			app.UseStaticFiles(new StaticFileOptions()
			{
				FileProvider = new PhysicalFileProvider(
					Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", Const.StaticFilesFolder)),
				RequestPath = new PathString("/images")
			});
		}
	}
}
