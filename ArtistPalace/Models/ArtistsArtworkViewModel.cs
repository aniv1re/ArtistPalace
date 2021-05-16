using System.Collections.Generic;
using ArtistPalace.Models;

namespace ArtistPalace
{
    public class ArtistsArtworkViewModel
    {
        public IEnumerable<Artist> Artists { get; set; }
        public IEnumerable<ArtistArtworks> Artworks { get; set; }
    }
}