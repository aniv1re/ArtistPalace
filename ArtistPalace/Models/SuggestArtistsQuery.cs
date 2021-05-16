namespace ArtistPalace.Models
{
    public class SuggestArtistsQuery
    {
        public string Tag { get; set; }
        public string Type { get; set; }
        public string AcceptCommissions { get; set; }
        public string PricePerHour { get; set; }
        public string ArtworkLink { get; set; }
    }
}