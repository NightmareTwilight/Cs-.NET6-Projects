IF NOT EXISTS(SELECT * FROM sys.databases WHERE name='AssetTracking')
BEGIN
	CREATE DATABASE [AssetTracking]
END
GO
	USE [AssetTracking]
GO
IF OBJECT_ID(N'dbo.Items', N'U') IS NULL
	CREATE TABLE Items (
		Id INT IDENTITY(1,1) PRIMARY KEY,
		Title VARCHAR(64),
		Amount FLOAT,
		Date Date,
		IsIncome BIT,
	);
GO