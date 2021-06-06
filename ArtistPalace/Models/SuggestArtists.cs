namespace ArtistPalace.Models
{
    public class SuggestArtists
    {
        public int Id { get; set; }
        public string TwitterTag { get; set; }
        public string Type { get; set; }
        public bool AcceptCommissions { get; set; }
        public int? PricePerHour { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsRejected { get; set; }
    }
}