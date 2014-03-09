CREATE TABLE [dbo].[tbl_Comments] (
    [ID]         INT            IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (50)  NOT NULL,
    [Email]      NVARCHAR (150) NOT NULL,
    [Comment]    NVARCHAR (MAX) NOT NULL,
    [CreateDate] DATETIME       NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

