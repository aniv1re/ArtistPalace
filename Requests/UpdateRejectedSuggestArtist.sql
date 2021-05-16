use ArtistPalace;
go
create procedure UpdateRejectedSuggestArtist (@twitterTag varchar(150))
as
update SuggestArtists set IsRejected = 1 where TwitterTag = @twitterTag