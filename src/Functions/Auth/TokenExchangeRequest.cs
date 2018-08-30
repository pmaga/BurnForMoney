using Newtonsoft.Json;

namespace BurnForMoney.Functions.Auth
{
    public class TokenExchangeRequest
    {
        public int ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Code { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, new JsonSettings());
        }
    }
}