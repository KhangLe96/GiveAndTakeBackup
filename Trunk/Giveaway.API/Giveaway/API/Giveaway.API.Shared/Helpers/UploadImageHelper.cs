using Giveaway.API.Shared.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Giveaway.API.Shared.Helpers
{
    public class UploadImageHelper
    {
        public static string PostBase64Image(ImageBase64Request request)
        {
            try
            {
                var bytes = Convert.FromBase64String(request.File);
                if ((request.Type == "Post") && request.File.Length > 0)
                {
                    var path = ContentHelper.GetPath("Post", request.Id.ToString());

                    string fileName = Guid.NewGuid() + ".jpg";
                    var filePath = Path.Combine(path, fileName);
                    if (bytes.Length > 0)
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            stream.Write(bytes, 0, bytes.Length);
                            stream.Flush();
                        }
                    }
                    string url = ContentHelper.GetImageUrl(request.Type, request.Id, fileName);

                    return url;
                }
            }
            catch (Exception e)
            {
                
            }

            return "";
        }
    }
}
