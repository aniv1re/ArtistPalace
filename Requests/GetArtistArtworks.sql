USE ArtistPalace

GO

create function GetArtistsArtworks()
returns @artworks table
(
	Id int,
    ArtworkLink nvarchar(MAX),
    ArtistId int
)
begin
	insert @artworks
	select * from ArtistArtworks
	return
end