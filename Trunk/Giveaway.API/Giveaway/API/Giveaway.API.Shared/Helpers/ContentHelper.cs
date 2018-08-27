using Giveaway.Util.Utils;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Giveaway.API.Shared.Helpers
{
    public static class ContentHelper
	{
		private static IHostingEnvironment Environment => ServiceProviderHelper.Current.GetService<IHostingEnvironment>();

        public static IEnumerable<string> ReadDirectory(string dir)
        {
            var path = GetPath(dir);
            return Directory
                .GetFiles(path)
                .Select(x => Path.GetFileName(x));
        }

        public static string GetPath(string dir, string subForlder = null)
        {
            var path = Path.Combine(GetRootPath(), dir);
            if (subForlder == null) return CreateIfNotExist(path);
            path = Path.Combine(path, subForlder);
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

        public static string GetImageUrl(string type, string id, string fileName)
        {
            var pageUri = GetUri();
            return pageUri.Scheme + "://" + pageUri.Host + ":" + pageUri.Port + "/content/" + type + "/" + id + "/" + fileName;
        }

        public static Uri GetUri()
        {
            var contextAccessor = ServiceProviderHelper.Current.GetService<IHttpContextAccessor>();

            if (contextAccessor.HttpContext.Request.IsHttps)
            {
                contextAccessor.HttpContext.Request.Scheme = "https";
            }

            return contextAccessor.HttpContext.Request.GetUri();
        }

        public static string EncodeTo64(string toEncode)
        {

            byte[] toEncodeAsBytes = System.Text.Encoding.ASCII.GetBytes(toEncode);
            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }

		public static string GetPath(string dir)
		{
			var path = Path.Combine(GetRootPath(), dir);
			return CreateIfNotExist(path);
		}
	}
}
