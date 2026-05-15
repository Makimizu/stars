using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AGR.Apps.Core.Models
{
    public class TokenModel
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("refreshToken")]
        public string RefreshToken { get; set; }
    }
    public class AuthenticationResult : TokenModel
    {
        public bool Success { get; set; }
        public IEnumerable<string> Error { get; set; }
    }
}