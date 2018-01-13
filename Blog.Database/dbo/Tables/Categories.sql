CREATE TABLE [dbo].[Categories] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (50) NOT NULL,
	[Code] NVARCHAR (50) NOT NULL,
    [ImageUrl] NVARCHAR(200) NULL, 
    CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED ([Id] ASC)
);

