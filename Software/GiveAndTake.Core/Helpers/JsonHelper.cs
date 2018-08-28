using System;
using System.Diagnostics;
using Newtonsoft.Json;

namespace GiveAndTake.Core.Helpers
{
    public static class JsonHelper
    {
        public static string Serialize(object obj)          
        {
            return JsonConvert.SerializeObject(obj);        
        }

        public static T Deserialize<T>(string value)        
        {
            var result = default(T);

            try
            {
                result = JsonConvert.DeserializeObject<T>(value, new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.IsoDateFormat
                });
            }
            catch (Exception e)
            {
                Debug.WriteLine("Deserialize fail with data: " + value);
                Debug.WriteLine("Exeption: " + e.Message);
            }

            return result;
        }
    }
}
