using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    /// <summary>
    /// OperationType Class
    /// </summary>
    public static class OperationType
    {
        //add the mappings for operation type and its corresponding sql query text
        public static readonly Dictionary<String, String> Map
            = new Dictionary<String, String>
            {
                {ReadAllArticles, OperationText.ReadAllArticles},
                {ReadArticleById, OperationText.ReadArticleById},
                {AddNewComment, OperationText.AddNewComment},
                {AddCommentArticleRelation, OperationText.AddCommentArticleRelation},
                {GetAllComments, OperationText.GetAllComments},
                {GetArticlesByUser, OperationText.GetArticlesByUser},
                {UpdateArticleById, OperationText.UpdateArticleById},
                {AddNewArticle, OperationText.AddNewArticle},
                {AddArticleUserRelation, OperationText.AddArticleUserRelation},
                {GetUserIdByUserName, OperationText.GetUserIdByUserName},
                {DeleteArticle, OperationText.DeleteArticle},
                {DeleteArticleUserRelation, OperationText.DeleteArticleUserRelation},
                {SelectTag, OperationText.SelectTag},
                {AddNewTag, OperationText.AddNewTag},
                {AddArticleTagRelation, OperationText.AddArticleTagRelation},
                {RemoveArticleTagRelationByArticleID, OperationText.RemoveArticleTagRelationByArticleID},
                {GetArticleTags, OperationText.GetArticleTags},
                {GetAllTags, OperationText.GetAllTags},
                {GetArticlesByTagName, OperationText.GetArticlesByTagName},
                {GetArticleDates, OperationText.GetArticleDates},
				{GetArticlesByDateRange, OperationText.GetArticlesByDateRange}
            };

        public const String ReadAllArticles = "ReadAllArticles";
        public const String ReadArticleById = "ReadArticleById";
        public const String AddNewComment = "AddNewComment";
        public const String AddCommentArticleRelation = "AddCommentArticleRelation";
        public const String GetAllComments = "GetAllComments";
        public const String GetArticlesByUser = "GetArticlesByUser";
        public const String UpdateArticleById = "UpdateArticleById";
        public const String AddNewArticle = "AddNewArticle";
        public const String AddArticleUserRelation = "AddArticleUserRelation";
        public const String GetUserIdByUserName = "GetUserIdByUserName";
        public const String DeleteArticle = "DeleteArticle";
        public const String DeleteArticleUserRelation = "DeleteArticleUserRelation";
        public const String SelectTag = "SelectTag";
        public const String AddNewTag = "AddNewTag";
        public const String AddArticleTagRelation = "AddArticleTagRelation";
        public const String RemoveArticleTagRelationByArticleID = "RemoveArticleTagRelationByArticleID";
        public const String GetArticleTags = "GetArticleTags";
        public const String GetAllTags = "GetAllTags";
        public const String GetArticlesByTagName = "GetArticlesByTagName";
        public const String GetArticleDates = "GetArticleDates";
        public const String GetArticlesByDateRange = "GetArticlesByDateRange";
    }

    /// <summary>
    /// Class to provide custom exception messages
    /// </summary>
    public static class Exceptions
    {
        public const String Error = "Some error has occured";
    }

    /// <summary>
    /// OperationText Class
    /// </summary>
    public static class OperationText
    {
        /// <summary>
        /// Read All Articles
        /// </summary>
        public const String ReadAllArticles = "SELECT * FROM tbl_Article ORDER BY Create_Date desc";

        /// <summary>
        /// Read Article By Id
        /// </summary>
        public const String ReadArticleById = "SELECT u.UserId as 'UserId', u.UserName as 'UserName', a.ID, a.Article_Title, a.Article_Description," +
                                                "a.Article_Data, a.Create_Date from tbl_Article as a" +
                                                " INNER JOIN tblUserArticle ua on ua.ArticleID = a.ID" +
                                                " INNER JOIN UserProfile u on u.UserId = ua.UserID" +
                                                " WHERE a.ID = @articleId";

        /// <summary>
        /// Add New Comment
        /// </summary>
        public const String AddNewComment = "INSERT INTO tbl_Comments(Name, Email, Comment, CreateDate) VALUES(@name, @email, @comment, @createDate); SELECT SCOPE_IDENTITY()";

        /// <summary>
        /// Add Comment Article Relation
        /// </summary>
        public const String AddCommentArticleRelation = "INSERT INTO tbl_ArticleComment(Article_Id, Comment_Id) VALUES(@articleId, @commentId)";

        /// <summary>
        /// Get All Comments
        /// </summary>
        public const String GetAllComments = "SELECT a.ID as 'ArticleId', c.Id as 'CommentId', c.Name, c.Email, c.Comment, c.CreateDate FROM tbl_Comments as c" +
                                               " INNER JOIN tbl_ArticleComment ac ON c.ID = ac.Comment_Id" +
                                               " INNER JOIN tbl_Article a ON ac.Article_Id = a.ID" +
                                               " WHERE a.ID = @articleId";

        /// <summary>
        /// Get Articles By User
        /// </summary>
        public const String GetArticlesByUser = "SELECT a.ID, u.UserId as 'UserId', u.UserName as 'UserName', a.Article_Title as 'Title', a.Article_Description as 'Description'," +
                                                "a.Article_Data as 'Data', a.Create_Date as 'CreateDate' from tbl_Article as a" +
                                                " INNER JOIN tblUserArticle ua on ua.ArticleID = a.ID" +
                                                " INNER JOIN UserProfile u on u.UserId = ua.UserID" +
                                                " WHERE u.UserName = @userName";

        /// <summary>
        /// Update Article By Id
        /// </summary>
        public const String UpdateArticleById = "UPDATE tbl_Article SET Article_Title=@title, Article_Description=@description, Article_Data=@data" +
                                                " WHERE ID=@articleId";

        /// <summary>
        /// Add New Article
        /// </summary>
        public const String AddNewArticle = "INSERT INTO tbl_Article(Article_Title, Article_Description, Article_Data, Create_Date)" +
                                            " VALUES(@title, @description, @data, @createDate); SELECT SCOPE_IDENTITY()";

        /// <summary>
        /// Add Article User Relation
        /// </summary>
        public const String AddArticleUserRelation = "INSERT INTO tblUserArticle(UserId, ArticleId) VALUES(@userId, @articleId)";

        /// <summary>
        /// Get User Id By User Name
        /// </summary>
        public const String GetUserIdByUserName = "SELECT UserId from UserProfile WHERE UserName=@userName";

        /// <summary>
        /// Delete Article
        /// </summary>
        public const String DeleteArticle = "DELETE FROM tbl_Article WHERE ID = @articleId";

        /// <summary>
        /// Delete Article User Relation
        /// </summary>
        public const String DeleteArticleUserRelation = "DELETE FROM tblUserArticle WHERE ArticleID = @articleId";

        /// <summary>
        /// Select Tag
        /// </summary>
        public const String SelectTag = "SELECT Tag_Name FROM tbl_Tags WHERE Tag_Name = @tagName";

        /// <summary>
        /// Add New Tag
        /// </summary>
        public const String AddNewTag = "INSERT INTO tbl_Tags(Tag_Name) VALUES(@tagName)";

        /// <summary>
        /// Add Article Tag Relation
        /// </summary>
        public const String AddArticleTagRelation = "INSERT INTO tblTagArticle(ArticleId, TagName) VALUES(@ArticleId, @tagName)";

        /// <summary>
        /// Remove Article Tag Relation By Article ID
        /// </summary>
        public const String RemoveArticleTagRelationByArticleID = "DELETE FROM tblTagArticle WHERE ArticleId=@articleId";

        /// <summary>
        /// Get Article Tags
        /// </summary>
        public const String GetArticleTags = "SELECT * FROM tblTagArticle WHERE ArticleId=@articleId";

        /// <summary>
        /// Get All Tags
        /// </summary>
        public const String GetAllTags = "SELECT TagName, Count(*) as 'ArticleCount' From tblTagArticle Group By TagName";

        /// <summary>
        /// Get Articles By Tag Name
        /// </summary>
        public const String GetArticlesByTagName = "SELECT a.ID, u.UserId as 'UserId', u.UserName as 'UserName', a.Article_Title as 'Title'," +
                                                    "a.Article_Description as 'Description',a.Article_Data as 'Data'," +
                                                    "a.Create_Date as 'CreateDate' from tbl_Article as a " +
                                                    "INNER JOIN tblUserArticle ua on ua.ArticleID = a.ID " +
                                                    "INNER JOIN UserProfile u on u.UserId = ua.UserID " +
                                                    "INNER JOIN tblTagArticle ta on ta.ArticleId = a.ID " +
                                                    "WHERE ta.TagName = @tagName ORDER BY a.Create_Date DESC";

        /// <summary>
        /// Get Article Dates
        /// </summary>
        public const String GetArticleDates = "SELECT Create_Date FROM tbl_Article ORDER BY Create_Date DESC";

        /// <summary>
        /// Get Articles By Date Range
        /// </summary>
        public const String GetArticlesByDateRange = "SELECT a.ID, u.UserId as 'UserId', u.UserName as 'UserName', a.Article_Title as 'Title', a.Article_Description as 'Description'," +
                                                "a.Article_Data as 'Data', a.Create_Date as 'CreateDate' from tbl_Article as a" +
                                                " INNER JOIN tblUserArticle ua on ua.ArticleID = a.ID" +
                                                " INNER JOIN UserProfile u on u.UserId = ua.UserID" +
                                                " WHERE (a.Create_Date >= @startDate AND a.Create_Date < @endDate)";
    }
}
