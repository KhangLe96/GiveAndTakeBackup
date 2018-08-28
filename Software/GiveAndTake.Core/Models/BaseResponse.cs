using GiveAndTake.Core.Helpers;
using Newtonsoft.Json;

namespace GiveAndTake.Core.Models
{
    public class BaseResponse
    {
        [JsonIgnore]
        public NetworkStatus NetworkStatus { get; set; }

        public string ErrorMessage { get; set; }

        private string _rawContent;

        public string RawContent
        {
            get => _rawContent;
            set
            {
                _rawContent = value;
                var errorResponse = JsonHelper.Deserialize<ErrorResponse>(value);
                if (errorResponse != default(ErrorResponse))
                {
                    ErrorMessage = errorResponse.Message;
                }
            }
        }

    }
}
