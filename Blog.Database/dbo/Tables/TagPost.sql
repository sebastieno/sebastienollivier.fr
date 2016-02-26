CREATE TABLE [dbo].[TagPost] (
    [TagId]        INT NOT NULL,
    [PostId] INT NOT NULL,
    CONSTRAINT [PK_TagPost] PRIMARY KEY CLUSTERED ([TagId] ASC, [PostId] ASC),
    CONSTRAINT [FK_TagPost_Post] FOREIGN KEY ([PostId]) REFERENCES [dbo].[Posts] ([Id]),
    CONSTRAINT [FK_TagPost_Tag] FOREIGN KEY ([TagId]) REFERENCES [dbo].[Tags] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_TagPost_Post]
    ON [dbo].[TagPost]([PostId] ASC);

