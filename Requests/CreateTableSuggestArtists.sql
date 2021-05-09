create table SuggestArtists (
	Id int not null IDENTITY(1,1) PRIMARY KEY,
	TwitterTag varchar(150) not null,
	Type varchar(150) not null,
	AcceptCommissions bit,
	PricePerHour int,
	IsAccepted bit
);