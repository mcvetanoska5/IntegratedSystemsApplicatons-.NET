IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Authors] (
    [Id] uniqueidentifier NOT NULL,
    [FullName] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Authors] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Members] (
    [Id] uniqueidentifier NOT NULL,
    [FullName] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Members] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Books] (
    [Id] uniqueidentifier NOT NULL,
    [Title] nvarchar(max) NOT NULL,
    [ISBN] nvarchar(max) NOT NULL,
    [IsAvailable] bit NOT NULL,
    [AuthorId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Books] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Books_Authors_AuthorId] FOREIGN KEY ([AuthorId]) REFERENCES [Authors] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Rentals] (
    [Id] uniqueidentifier NOT NULL,
    [MemberId] uniqueidentifier NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Rentals] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Rentals_Members_MemberId] FOREIGN KEY ([MemberId]) REFERENCES [Members] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [BookRentals] (
    [Id] uniqueidentifier NOT NULL,
    [BookId] uniqueidentifier NOT NULL,
    [RentalId] uniqueidentifier NOT NULL,
    [RentedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_BookRentals] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_BookRentals_Books_BookId] FOREIGN KEY ([BookId]) REFERENCES [Books] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_BookRentals_Rentals_RentalId] FOREIGN KEY ([RentalId]) REFERENCES [Rentals] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_BookRentals_BookId] ON [BookRentals] ([BookId]);
GO

CREATE INDEX [IX_BookRentals_RentalId] ON [BookRentals] ([RentalId]);
GO

CREATE INDEX [IX_Books_AuthorId] ON [Books] ([AuthorId]);
GO

CREATE INDEX [IX_Rentals_MemberId] ON [Rentals] ([MemberId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260123232940_InitialCreate', N'8.0.0');
GO

COMMIT;
GO

