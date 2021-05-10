using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;

namespace ArtistPalace.TwitterApi
{
    public class TwitterHandler
    {
        public static TwitterClient UserClient { get; set; }

        public static async void SetConnection()
        {
            var json = String.Empty;
            using (var fs = File.OpenRead(Environment.CurrentDirectory + @"\ApiKeys.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync().ConfigureAwait(false);
            var keyJson = JsonConvert.DeserializeObject<ApiKeysJson>(json);

            UserClient = new TwitterClient(keyJson.Key, keyJson.Secret, keyJson.AccessToken, keyJson.AccessSecret);
        }

        public static string GetAvatar(string tag)
        {
            return UserClient.Users.GetUserAsync(tag).Result.ProfileImageUrl;
        }
        
        /*public static async Task<IWebhookSubscription[]> Fill()
        {
            return userClient.AccountActivity.GetAccountActivitySubscriptionsAsync("maconee_").Result.Subscriptions;
        }

        @for (int i = 0; i < TwitterHandler.Fill().Result.Length; i++)
        {
            <p>TwitterHandler.userClient.UsersV2.GetUserByIdAsync(TwitterHandler.Fill().Result[i].UserId).Result.User.Name</p>
        }*/
    }
}
