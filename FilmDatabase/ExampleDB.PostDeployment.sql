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

INSERT [dbo].[Country] ([Id], [Name]) VALUES (1, N'United States')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (2, N'Canada')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (3, N'Mexico')
SET IDENTITY_INSERT [dbo].[Country] OFF
GO
SET IDENTITY_INSERT [dbo].[Film] ON 

INSERT [dbo].[Film] ([Id], [Name], [ReleaseDate]) VALUES (2, N'The Evil Dead', CAST(N'1981-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[Film] ([Id], [Name], [ReleaseDate]) VALUES (4, N'Evil Dead 2', CAST(N'1987-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[Film] ([Id], [Name], [ReleaseDate]) VALUES (5, N'Sunset Avenue', CAST(N'1999-01-01T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Film] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [FirstName], [LastName], [CountryId], [FavoriteFilmId]) VALUES (1, N'Kevin', N'Malone', 3, NULL)
INSERT [dbo].[User] ([Id], [FirstName], [LastName], [CountryId], [FavoriteFilmId]) VALUES (2, N'Sarah', N'Smithers', 2, 2)
INSERT [dbo].[User] ([Id], [FirstName], [LastName], [CountryId], [FavoriteFilmId]) VALUES (3, N'William', N'Dufford', 1, 4)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
