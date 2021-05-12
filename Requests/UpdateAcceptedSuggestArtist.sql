use ArtistPalace;
go
create procedure UpdateAcceptedSuggestArtist (@twitterTag varchar(150))
as
update SuggestArtists set IsAccepted = 1 where TwitterTag = '@twitterTag'