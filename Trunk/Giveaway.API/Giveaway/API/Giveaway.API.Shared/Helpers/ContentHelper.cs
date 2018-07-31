using System.Collections.Generic;
using System.IO;
using System.Linq;
using Giveaway.Util.Utils;
using Microsoft.AspNetCore.Hosting;

namespace Giveaway.API.Shared.Helpers
{
	internal static class ContentHelper
	{
		private static IHostingEnvironment Environment => ServiceProviderHelper.Current.GetService<IHostingEnvironment>();

		public static IEnumerable<string> ReadDirectory(string dir)
		{
			var path = GetPath(dir);
			return Directory
				.GetFiles(path)
				.Select(x => Path.GetFileName(x));
		}

		public static string GetPath(string dir)
		{
			var path = Path.Combine(GetRootPath(), dir);
			return CreateIfNotExist(path);
		}

		public static string GetRootPath()
		{
			var path = Path.Combine(Environment.ContentRootPath, "Content");
			return CreateIfNotExist(path);
		}

		private static string CreateIfNotExist(string path)
		{
			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);
			return path;
		}
	}
}
