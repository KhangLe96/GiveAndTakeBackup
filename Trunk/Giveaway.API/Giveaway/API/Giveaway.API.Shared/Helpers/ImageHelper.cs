using Giveaway.API.Shared.Models;
using Giveaway.Data.EF;
using Giveaway.Util.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Giveaway.API.Shared.Helpers
{
    public static class ImageHelper
    {
        private const float MediumImageScale = 0.65f; // Compared to big image
        private const float SmallImageScale = 0.3f; // Compared to big image

        public static async Task<Avatar> SavePhoto(Guid userId, string avatarUrl, string webRoot, string folderName)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    using (Stream stream = await client.GetStreamAsync(avatarUrl))
                    {
                        var bytes = GetBytes(stream);

                        if (bytes == null)
                        {
                            return null;
                        }

                        return SavePhoto(userId, bytes, webRoot, folderName);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return null;
                }
            }
        }


        public static Avatar SavePhoto(Guid userId, byte[] data, string webRoot, string folderName)
        {
            var userFolder = GenerateUserFolder(userId);
            var path = CreatePath(webRoot, folderName, userFolder);
            CreateDirectory(path);

            var bigImagePath = Path.Combine(path, Const.BigImageSuffix);

            File.WriteAllBytes(bigImagePath, data);

            var pageUri = GetUri();

            return new Avatar
            {
                BigImagePath = pageUri.Scheme + "://" + pageUri.Host + "/" + folderName + "/" + userFolder + "/" + Const.BigImageSuffix,
                MediumImagePath = pageUri.Scheme + "://" + pageUri.Host + "/" + folderName + "/" + userFolder + "/" + Const.BigImageSuffix,
                SmallImagePath = pageUri.Scheme + "://" + pageUri.Host + "/" + folderName + "/" + userFolder + "/" + Const.BigImageSuffix
            };
        }

        public static byte[] GetBytes(Stream input)
        {
            try
            {
                var buffer = new byte[16 * 1024];

                using (var ms = new MemoryStream())
                {
                    int read;
                    while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }

                    return ms.ToArray();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        /// <summary>
        /// Generate a unique photo folder for a user based on user id
        /// The purpose is to hide user id on the link so other people can't recognize
        /// </summary>
        private static string GenerateUserFolder(Guid userId)
        {
            long i = 1;

            foreach (var b in userId.ToByteArray())
            {
                i *= (b + 1);
            }

            return string.Format("{0:x}", i);
        }

        public static Uri GetUri()
        {
            var contextAccessor = ServiceProviderHelper.Current.GetService<IHttpContextAccessor>();

            if (!contextAccessor.HttpContext.Request.IsHttps)
            {
                contextAccessor.HttpContext.Request.Scheme = "https";
            }

            return new Uri(contextAccessor.HttpContext.Request.GetDisplayUrl());
        }

        private static string CreatePath(string webRoot, string folderName, string userFolder = default)
        {
            return userFolder != default ?
                Path.Combine(webRoot, folderName, userFolder) : Path.Combine(webRoot, folderName);
        }

        private static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                var coder = imageIn.RawFormat;
                if (coder == null || (coder != ImageFormat.Jpeg && coder != ImageFormat.Png && coder != ImageFormat.Gif)) coder = ImageFormat.Jpeg;
                imageIn.Save(ms, coder);
                return ms.ToArray();
            }
        }
    }
}
