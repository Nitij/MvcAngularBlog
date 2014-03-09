CREATE TABLE [dbo].[tbl_Article] (
    [ID]                  INT             IDENTITY (1, 1) NOT NULL,
    [Article_Title]       NVARCHAR (500)  NOT NULL,
    [Article_Description] NVARCHAR (1000) NOT NULL,
    [Article_Data]        NVARCHAR (MAX)  NOT NULL,
    [Create_Date]         DATETIME2 (7)   NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

