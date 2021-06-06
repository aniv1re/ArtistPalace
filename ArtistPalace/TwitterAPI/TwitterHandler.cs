using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ArtistPalace.Models;
using ArtistPalace.ViewModels;
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

        public static async Task<string> GetAvatar(string tag)
        {
            var artist = await UserClient.Users.GetUserAsync(tag);
            return artist.ProfileImageUrl.ToString();
        }

        public static async Task<Artist> GetInfo(string tag)
        {
            var user = await UserClient.Users.GetUserAsync(tag);

            Artist newArtist = new Artist()
            {
                Nickname = user.Name,
                TwitterTag = user.ScreenName,
                FollowersCount = user.FollowersCount,
            };

            return newArtist;
        }
    }
}
