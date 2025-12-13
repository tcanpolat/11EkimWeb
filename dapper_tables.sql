create database ETradeDB
use ETradeDB

create table Category(
	CategoryId int primary key identity(1,1),
	Name varchar(50)
)
create table Product(
	ProductId int primary key identity(1,1),
	[Name] varchar(50),
	Price decimal(18,6),
	CategoryId int foreign key references Category(CategoryId) 
)
