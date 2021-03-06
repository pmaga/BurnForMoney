using BurnForMoney.Functions.Strava.Configuration;
using Newtonsoft.Json;

namespace BurnForMoney.Functions.Strava.External.Strava.Api.Auth
{
    public class TokenExchangeRequest
    {
        public int ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Code { get; set; }
        public string GrantType { get; } = "authorization_code";

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, new JsonSettings());
        }
    }
}