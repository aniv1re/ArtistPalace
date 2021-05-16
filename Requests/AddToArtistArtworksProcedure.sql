use ArtistPalace;

go

create procedure AddToArtistArtworks
(
	@artworkLink nvarchar(MAX),
    @artistId int
)
as
insert into ArtistArtworks(ArtworkLink, ArtistId)
values (@artworkLink, @artistId)