CREATE TABLE [dbo].[User]
(
  [Id] INT IDENTITY,
  [FirstName] VARCHAR(50) NOT NULL,
  [LastName] VARCHAR(50) NOT NULL,
  [CountryId] INT NOT NULL,
  [FavoriteFilmId] INT,
  PRIMARY KEY ([Id]),
  CONSTRAINT [FK_User.CountryId] FOREIGN KEY ([CountryId]) REFERENCES [Country]([Id]),
  CONSTRAINT [FK_User.FavoriteFilmId] FOREIGN KEY ([FavoriteFilmId]) REFERENCES [Film]([Id])
)