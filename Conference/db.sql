create database [Conference]
go
use [Conference]
go
create table [Role]
(
	Id int primary key identity,
	Name nvarchar(15) not null
)
go
create table [City]
(
	Id int primary key identity,
	Name nvarchar(100) not null
)
go
create table [Country]
(
	Id int primary key identity,
	Name nvarchar(100) not null,
	EnglishName nvarchar(100) not null,
	Code nvarchar(2) not null,
	NumCode smallint not null
)
go
create table [Event]
(
	Id int primary key identity,
	Name nvarchar(max) not null,
	Direction nvarchar(100) not null,
	Date date not null,
	Days smallint not null,
	CityId int foreign key references [City](Id) not null
)
go
create table [Participant]
(
	Id int primary key identity,
	Surname nvarchar(100) not null,
	Name nvarchar(100) not null,
	Patronymic nvarchar(100) not null,
	Gender nvarchar(1) not null,
	Email nvarchar(max) not null,
	Birthdate date not null,
	CountryId int foreign key references [Country](Id) not null,
	Phone nvarchar(20) not null,
	Direction nvarchar(50) null,
	Event nvarchar(max) null,
	Password nvarchar(max) not null,
	Photo nvarchar(max) not null,
	RoleId int foreign key references [Role](Id) not null
)