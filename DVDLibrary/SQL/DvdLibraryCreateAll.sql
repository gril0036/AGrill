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

-- Seed Information
USE DvdLibrary
GO

SET IDENTITY_INSERT Dvd ON

INSERT INTO Dvd (dvdId, title, realeaseYear, director, rating, notes)
VALUES (0, 'Abba', 2010, 'Bob Dole', 'PG', 'Good'),
	(1, 'Baab', 2020, 'Bob Ross', 'R', 'Great')

SET IDENTITY_INSERT Dvd OFF

--Stored Procedures
USE DvdLibrary
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdSelectAll')
      DROP PROCEDURE DvdSelectAll
GO

CREATE PROCEDURE DvdSelectAll
AS
	SELECT *
	FROM Dvd
	ORDER BY dvdId
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdSelectById')
      DROP PROCEDURE DvdSelectById
GO

CREATE PROCEDURE DvdSelectById (@id int)
AS
	SELECT *
	FROM Dvd
	WHERE dvdId = @id
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdSelectByTitle')
      DROP PROCEDURE DvdSelectByTitle
GO

CREATE PROCEDURE DvdSelectByTitle (@term varchar(50))
AS
	SELECT *
	FROM Dvd
	Where title = @term
	ORDER BY dvdId
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdSelectByYear')
      DROP PROCEDURE DvdSelectByYear
GO

CREATE PROCEDURE DvdSelectByYear (@term varchar(50))
AS
	SELECT *
	FROM Dvd
	Where realeaseYear = @term
	ORDER BY dvdId
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdSelectByDirector')
      DROP PROCEDURE DvdSelectByDirector
GO

CREATE PROCEDURE DvdSelectByDirector (@term varchar(50))
AS
	SELECT *
	FROM Dvd
	Where director = @term
	ORDER BY dvdId
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdSelectByRating')
      DROP PROCEDURE DvdSelectByRating
GO

CREATE PROCEDURE DvdSelectByRating (@term varchar(50))
AS
	SELECT *
	FROM Dvd
	Where rating = @term
	ORDER BY dvdId
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdInsert')
      DROP PROCEDURE DvdInsert
GO

CREATE PROCEDURE DvdInsert (
	@dvdId int output,
	@title varchar (50),
	@realeaseYear int,
	@director varchar(50),
	@rating varchar(50),
	@notes varchar(200)
)
AS
	INSERT INTO Dvd (title, realeaseYear, director, rating, notes)
	VALUES (@title, @realeaseYear, @director, @rating, @notes)

	SET @dvdId = SCOPE_IDENTITY()
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdDelete')
      DROP PROCEDURE DvdDelete
GO

CREATE PROCEDURE DvdDelete (@id int)
AS
	DELETE 
	FROM Dvd
	WHERE dvdId = @id
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdUpdate')
      DROP PROCEDURE DvdUpdate
GO

CREATE PROCEDURE DvdUpdate (
	@dvdId int,
	@title varchar (50),
	@realeaseYear int,
	@director varchar(50),
	@rating varchar(50),
	@notes varchar(200)
)
AS
	UPDATE Dvd
		SET title = @title,
		realeaseYear = @realeaseYear,
		director = @director,
		rating = @rating,
		notes = @notes
	WHERE dvdId = @dvdId
GO
