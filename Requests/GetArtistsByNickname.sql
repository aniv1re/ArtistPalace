USE ArtistPalace

GO

create or alter function GetArtistsByNickname(@nickname nvarchar(150))
returns @tableVar TABLE
(
	Id	int,
	TwitterLink	nvarchar(50),
	TwitterTag	nvarchar(50),
	Nickname	nvarchar(50),
	FollowersCount	int,
	Country	nvarchar(50),
	Rank	nvarchar(50),
	Type	nvarchar(50),
	AcceptCommissions	bit,
	CommissionsLink	nvarchar(50),
	PricePerHour	int
)
begin
	insert @tableVar
	select * from artists
	where Nickname = @nickname or TwitterTag = @nickname
	return
end