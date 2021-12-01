/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
USE [FilmDatabase]
GO
SET IDENTITY_INSERT [dbo].[Country] ON 

INSERT [dbo].[Country] ([Id], [Name]) VALUES (1, N'USA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (2, N'Canada')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (3, N'Mexico')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (1001, N'UK')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (1002, N'China')
SET IDENTITY_INSERT [dbo].[Country] OFF
GO
SET IDENTITY_INSERT [dbo].[Film] ON 

INSERT [dbo].[Film] ([Id], [Name], [ReleaseDate]) VALUES (2, N'Avatar', CAST(N'2009-12-10T00:00:00.000' AS DateTime))
INSERT [dbo].[Film] ([Id], [Name], [ReleaseDate]) VALUES (1002, N'The Dark Knight', CAST(N'2008-07-18T00:00:00.000' AS DateTime))
INSERT [dbo].[Film] ([Id], [Name], [ReleaseDate]) VALUES (1003, N'Napoleon Dynamite', CAST(N'2004-06-11T00:00:00.000' AS DateTime))
INSERT [dbo].[Film] ([Id], [Name], [ReleaseDate]) VALUES (1004, N'Short Circuit', CAST(N'1986-05-09T00:00:00.000' AS DateTime))
INSERT [dbo].[Film] ([Id], [Name], [ReleaseDate]) VALUES (1005, N'Top Gun', CAST(N'1986-04-12T00:00:00.000' AS DateTime))
INSERT [dbo].[Film] ([Id], [Name], [ReleaseDate]) VALUES (1006, N'Army of Darkness', CAST(N'1993-02-19T00:00:00.000' AS DateTime))
INSERT [dbo].[Film] ([Id], [Name], [ReleaseDate]) VALUES (1007, N'Home Alone', CAST(N'1990-11-16T00:00:00.000' AS DateTime))
INSERT [dbo].[Film] ([Id], [Name], [ReleaseDate]) VALUES (1008, N'Twister', CAST(N'1996-05-10T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Film] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [FirstName], [LastName], [CountryId], [FavoriteFilmId]) VALUES (1, N'Kevin', N'Paul', 1, 1002)
INSERT [dbo].[User] ([Id], [FirstName], [LastName], [CountryId], [FavoriteFilmId]) VALUES (2, N'Sarah', N'Smithers', 2, 1007)
INSERT [dbo].[User] ([Id], [FirstName], [LastName], [CountryId], [FavoriteFilmId]) VALUES (3, N'William', N'Dufford', 1, 1005)
INSERT [dbo].[User] ([Id], [FirstName], [LastName], [CountryId], [FavoriteFilmId]) VALUES (1001, N'Mary', N'Rose', 1001, 1003)
INSERT [dbo].[User] ([Id], [FirstName], [LastName], [CountryId], [FavoriteFilmId]) VALUES (1002, N'Zihan', N'Lee', 1002, 1006)
INSERT [dbo].[User] ([Id], [FirstName], [LastName], [CountryId], [FavoriteFilmId]) VALUES (1003, N'James', N'Robin', 2, 1008)
INSERT [dbo].[User] ([Id], [FirstName], [LastName], [CountryId], [FavoriteFilmId]) VALUES (1005, N'Yinuo', N'Shanshan', 1002, NULL)
INSERT [dbo].[User] ([Id], [FirstName], [LastName], [CountryId], [FavoriteFilmId]) VALUES (1006, N'Frank', N'North', 3, 1004)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
