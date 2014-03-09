CREATE TABLE [dbo].[tblTagArticle] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [ArticleId] INT           NOT NULL,
    [TagName]   NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ArticleId] FOREIGN KEY ([ArticleId]) REFERENCES [dbo].[tbl_Article] ([ID]),
    CONSTRAINT [FK_TagName] FOREIGN KEY ([TagName]) REFERENCES [dbo].[tbl_Tags] ([Tag_Name])
);

