CREATE TABLE [dbo].[tbl_ArticleComment] (
    [Id]         INT IDENTITY (1, 1) NOT NULL,
    [Article_Id] INT NOT NULL,
    [Comment_Id] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Article] FOREIGN KEY ([Article_Id]) REFERENCES [dbo].[tbl_Article] ([ID]),
    CONSTRAINT [FK_Comment] FOREIGN KEY ([Comment_Id]) REFERENCES [dbo].[tbl_Comments] ([ID])
);

