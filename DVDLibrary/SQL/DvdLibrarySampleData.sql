-- Seed Information
USE DvdLibrary
GO

SET IDENTITY_INSERT Dvd ON

INSERT INTO Dvd (dvdId, title, realeaseYear, director, rating, notes)
VALUES (0, 'Abba', 2010, 'Bob Dole', 'PG', 'Good'),
	(1, 'Baab', 2020, 'Bob Ross', 'R', 'Great')

SET IDENTITY_INSERT Dvd OFF