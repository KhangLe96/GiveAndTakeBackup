using Giveaway.API.Shared.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace Giveaway.API.Shared.Helpers
{
    public class UploadImageHelper
    {
        public static List<string> PostBase64Image(ImageBase64Request request)
        {
            var urls = new List<string>();
            try
            {
                var bytes = Convert.FromBase64String(request.File);
                if ((request.Type == "Post") && request.File.Length > 0)
                {
                    var path = ContentHelper.GetPath("Post", request.Id.ToString());

                    StreamWrite(bytes, path, out var fileName);

                    string url = ContentHelper.GetImageUrl(request.Type, request.Id, fileName);
                    urls.Add(url);

                    //Resize the original image above
                    string localUrl = ContentHelper.GetLocalImageUrl(request.Type, request.Id, fileName);
                    Image image = Image.FromFile(localUrl);
                    image = ContentHelper.Resize(image, image.Width/2, image.Height/2, true);
                    bytes = ImageHelper.ImageToByteArray(image);

                    StreamWrite(bytes, path, out fileName);

                    url = ContentHelper.GetImageUrl(request.Type, request.Id, fileName);
                    urls.Add(url);

                    return urls;
                }
            }
            catch (Exception e)
            {

            }

            return urls;
        }

        private static void StreamWrite(byte[] bytes, string path, out string fileName)
        {
            fileName = Guid.NewGuid() + ".jpg";
            var filePath = Path.Combine(path, fileName);
            if (bytes.Length > 0)
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Flush();
                }
            }
        }
    }
}
