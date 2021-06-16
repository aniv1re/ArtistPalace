use ArtistPalace;

go

create procedure AddToArtists
(
	@twitterLink varchar(150),
	@nickname varchar(150),
	@followersCount int,
	@lang varchar(150),
	@rank varchar(150),
	@twitterTag varchar(150),
	@type varchar(150),
	@acceptCommissions bit,
	@pricePerHour int
)
as
begin
	insert into Artists(TwitterLink, Nickname, FollowersCount, Lang, Rank, TwitterTag, Type, AcceptCommissions, PricePerHour)
	values (@twitterLink, @nickname, @followersCount, @Lang, @rank, @twitterTag, @type, @acceptCommissions, @pricePerHour)
end