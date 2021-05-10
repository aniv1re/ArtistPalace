create table Artists (
	Id int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	TwitterLink nvarchar(150) NOT NULL,
	TwitterTag nvarchar(150) NULL,
	Nickname nvarchar(150) NULL,
	FollowersCount int NULL,
	Country nvarchar(150) NULL,
	Rank nvarchar(150) NULL,
	Type nvarchar(150) NULL,
	AcceptCommissions bit NULL,
	PricePerHour int NULL,
);