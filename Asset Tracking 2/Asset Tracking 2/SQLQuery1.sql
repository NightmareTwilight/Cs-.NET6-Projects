IF NOT EXISTS(SELECT * FROM sys.databases WHERE name='AssetTracking')
BEGIN
	CREATE DATABASE [AssetTracking]
END
GO
	USE [AssetTracking]
GO
IF OBJECT_ID(N'dbo.Offices', N'U') IS NULL
	CREATE TABLE Offices (
		Id INT IDENTITY(1,1) PRIMARY KEY,
		Name VARCHAR(64),
		Currency VARCHAR(64),
		Conversion FLOAT
	);
GO
IF OBJECT_ID(N'dbo.PCs', N'U') IS NULL
	CREATE TABLE PCs (
		Id INT IDENTITY(1,1) PRIMARY KEY,
		Name VARCHAR(64),
		Currency VARCHAR(64),
		Conversion FLOAT
	);
GO