use ArtistPalace;

go

create procedure AddToArtists
(
	@twitterLink varchar(150),
	@nickname varchar(150),
	@followersCount int,
	@country varchar(150),
	@rank varchar(150),
	@twitterTag varchar(150),
	@type varchar(150),
	@acceptCommissions bit,
	@pricePerHour int
)
as
begin
	insert into Artists(TwitterLink, Nickname, FollowersCount, Country, Rank, TwitterTag, Type, AcceptCommissions, PricePerHour)
	values (@twitterLink, @nickname, @followersCount, @country, @rank, @twitterTag, @type, @acceptCommissions, @pricePerHour)
end
begin
	insert into ArtistArtworks(ArtworkLink, ArtistId)
	values (@artworkLink, @@IDENTITY)
end