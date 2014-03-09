CREATE TABLE [dbo].[tblUserArticle] (
    [Id]        INT IDENTITY (1, 1) NOT NULL,
    [UserID]    INT NOT NULL,
    [ArticleID] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UserId] FOREIGN KEY ([UserID]) REFERENCES [dbo].[UserProfile] ([UserId]),
    CONSTRAINT [ArticleId] FOREIGN KEY ([ArticleID]) REFERENCES [dbo].[tbl_Article] ([ID])
);

