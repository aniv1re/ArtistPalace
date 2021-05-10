create table Users (
	Id int IDENTITY(1,1) PRIMARY KEY NOT NULL,
    Email nvarchar(100) NOT NULL,
	PasswordHash nvarchar(max) NULL,
);