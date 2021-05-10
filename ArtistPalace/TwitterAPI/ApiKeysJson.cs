using Newtonsoft.Json;

namespace ArtistPalace.TwitterApi
{
    public class ApiKeysJson
    {
        [JsonProperty("key")]
        public string Key { get; private set; }
        [JsonProperty("secret")]
        public string Secret { get; private set; }
        [JsonProperty("access-token")]
        public string AccessToken { get; private set; }
        [JsonProperty("access-secret")]
        public string AccessSecret { get; private set; }
    }
}
