<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>



<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width: 100%;">
        <tr>
            <td style="width: 70%;">
                <div ng-view></div>
            </td>
            <td style="width: 70%; vertical-align: top;">
                <div ng-controller="TagsCtrl">
                    <span style="font-weight: 800">Tags: </span>
                    <br />
                    <div class="tag-container" id="divTagContainer" ng-repeat="tag in tags" >
                        <div class="article-tag-name" ng-click="Go(tag.TagName)">
                            {{tag.TagName}}
                        </div>
                        <div class="article-tag-count" ng-click="Go(tag.TagName)">
                            {{tag.ArticleCount}}
                        </div>
                    </div>
                </div>
                <br />
                <div ng-controller="ArchiveCtrl">
                    <span style="font-weight: 800">Archive: </span>
                    <br />
                    <span ng-repeat="archive in archiveList">
                        <a ng-href='#/articles-archive/{{archive.Year}}/{{archive.Month}}'>{{archive.MonthString}} {{archive.Year}}({{archive.ArticleCount}})</a>
                        <br />
                    </span>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
