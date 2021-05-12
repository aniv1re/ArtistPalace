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
insert into Artists(TwitterLink, Nickname, FollowersCount, Country, TwitterTag, Type, AcceptCommissions, PricePerHour)
values (@twitterLink, @nickname, @followersCount, @country, @rank, @twitterTag, @type, @acceptCommissions, @pricePerHour)

update SuggestArtists set IsAccepted = 1 where TwitterTag = '@twitterTag'