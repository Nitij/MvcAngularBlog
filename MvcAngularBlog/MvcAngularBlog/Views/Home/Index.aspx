<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>



<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width:100%;">
        <tr>
            <td style="width:70%;">
                <div ng-view></div>
            </td>
            <td style="width:70%;vertical-align:top;">
                <div ng-controller="TagsCtrl">
                    <span style="font-weight:800">Tags: </span>
                    <br />
                    <span ng-repeat="tag in tags">
                        <a ng-href='#/articles-tag/{{tag.TagName}}'">{{tag.TagName}}({{tag.ArticleCount}})</a>
                        <br />
                    </span>
                </div><br />
                <div ng-controller="ArchiveCtrl">
                    <span style="font-weight:800">Archive: </span>
                    <br />
                    <span ng-repeat="archive in archiveList">
                        <a ng-href='#/articles-archive/{{archive.Year}}/{{archive.Month}}'">{{archive.MonthString}} {{archive.Year}}({{archive.ArticleCount}})</a>
                        <br />
                    </span>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
