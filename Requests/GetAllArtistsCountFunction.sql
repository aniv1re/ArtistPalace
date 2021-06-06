USE ArtistPalace

GO

create function GetAllArtistsCount()
returns @count table
(
	Count int
)
begin
	insert @count
	select count(*) from Artists
	return
end