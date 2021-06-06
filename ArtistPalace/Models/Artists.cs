namespace ArtistPalace.Models
{
    public class Artist
    {
        public int Id { get; set; }
        public string TwitterLink { get; set; }
        public string TwitterPfp { get; set; }
        public string TwitterTag { get; set; }
        public string Nickname { get; set; }
        public int? FollowersCount { get; set; }
        public string Lang { get; set; }
        public string Rank { get; set; }
        public string Type { get; set; }
        public bool? AcceptCommissions { get; set; }
        public int? PricePerHour { get; set; }
    }
}