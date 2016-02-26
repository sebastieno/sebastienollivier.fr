CREATE TABLE [dbo].[Posts] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Url]         NVARCHAR (100)  NOT NULL,
    [Title]       NVARCHAR (140) NOT NULL,
    [Description] NVARCHAR (MAX) NOT NULL,
    [Content]     NVARCHAR (MAX) NOT NULL,
    [PublicationDate] DATETIME NULL, 
    [CategoryId] INT NOT NULL, 
    CONSTRAINT [PK_Posts] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [AK_Posts_Url] UNIQUE ([Url]),
	CONSTRAINT [FK_Post_Category] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Categories] ([Id]),
);

