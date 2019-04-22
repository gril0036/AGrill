-- Create Database
USE [Master]
GO

if exists (select * from sys.databases where name = N'DvdLibrary')
begin
    EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'DvdLibrary';
    ALTER DATABASE DvdLibrary SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE DvdLibrary;
end
go

CREATE DATABASE DvdLibrary
GO

USE DvdLibrary
GO

-- Tables
IF EXISTS(SELECT * FROM sys.tables WHERE name='Dvd')
	DROP TABLE Dvd
GO

CREATE TABLE Dvd (
	dvdId int identity(0,1) primary key not null,
	title varchar (50) not null,
	realeaseYear int not null,
	director  varchar (50) not null,
	rating  varchar (50) not null,
	notes varchar(200) not null
)