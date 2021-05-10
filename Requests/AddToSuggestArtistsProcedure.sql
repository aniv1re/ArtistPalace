use ArtistPalace;


go
create procedure AddToSuggestArtists(@twitterTag varchar(150),
									  @type varchar(150),
									  @acceptCommissions bit,
									  @pricePerHour int,
									  @isAccepted bit)
as
insert into CheckoutArtists(TwitterTag, Type, AcceptCommissions, PricePerHour, IsAccepted)
values (@twitterTag, @type, @acceptCommissions, @pricePerHour, @isAccepted)