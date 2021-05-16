use ArtistPalace;

go

create trigger ArtistAdded
on Artists
after INSERT
as
begin
	update LatestAddedArtists
	set ArtistId = @@IDENTITY, AddDate = GETDATE()
	where Id = 1
end