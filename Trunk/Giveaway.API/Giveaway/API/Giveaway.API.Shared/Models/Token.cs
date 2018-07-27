using System;

namespace Giveaway.API.Shared.Models
{
    public class Token
    {
        public string AccessToken { get; set; }
        public DateTimeOffset Expires { get; set; }
        public string Type { get; set; }
        public string RefreshToke { get; set; }
    }
}
