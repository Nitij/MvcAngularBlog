<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MvcAngularBlog.Models.LocalPasswordModel>" %>

<asp:Content ID="manageTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Manage Account
</asp:Content>

<asp:Content ID="manageContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        //lets initialize our nice little text editor here until we find a 
        //suitable area to place this code
        bkLib.onDomLoaded(function () { nicEditors.allTextAreas() });
    </script>
    <hgroup class="title">
        <h1>Your Account</h1>
    </hgroup>

    <p class="message-success"><%: (string)ViewBag.StatusMessage %></p>

    <p>You're logged in as <strong><%: User.Identity.Name %></strong>.</p>

    <a href="" onclick="ActivateArticlesTab(); return false;">Manage Articles</a>&nbsp;
    <a href="" onclick="ActivateChangePasswordTab(); return false;">Change Password</a>
    
    <div id="divManageArticles" ng-controller="UserCtrl">
        <table>
            <tr user-article-list-item ng-repeat="article in articles"></tr>
        </table>
        <br />
        <a href="" ng-click="CreateNewArticle()">Create New Article</a>

        <br />
        <br />
        <div id="divEditArticle" style="display: {{GetEditMode()}};">
            <label for="txtArticleTitle">Title: </label>
            <input type="text" id="txtArticleTitle" ng-model="currentArticle.Title" class="edit-title"/>
            <br />
            <label for="txtDescription">Description: </label>            
            <input type="text" id="txtDescription" ng-model="currentArticle.Description" class="edit-description"/>
            <textarea style="width: 700px; height: 500px;" id="txtArticleEditor"></textarea>
            <br />
            <label for="txtTags">Tags: </label>
            <input type="text" id="txtTags" ng-model="currentArticle.Tags" class="edit-tags"/>
            <br />
            <a href="" ng-click="SaveArticle()">Save</a>
            <a href="" ng-click="CloseArticleEditing()">Done Editing</a>
        </div>
    </div>
    <div id="divChangePassword" style="display:none;">
        <% Html.RenderPartial("_ChangePasswordPartial"); %>
    </div>

    <script type="text/javascript">
        var $divChangePassword = $('#divChangePassword');
        var $divManageArticles = $('#divManageArticles');
        var currentVisibleSection = "articles";

        function ActivateArticlesTab() {
            $divChangePassword.hide(400);
            $divManageArticles.show(400);
        }

        function ActivateChangePasswordTab() {
            $divChangePassword.show(400);
            $divManageArticles.hide(400);
        }

    </script>
</asp:Content>
