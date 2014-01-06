;

//Article List Item Template
blogApp.directive('articleListItem', function () {
    var t = [];
    t.push('<span>');
    t.push('<a class="article-list-title" ng-href="#/articles-detail/{{article.ID}}">{{article.Title}}</a>');
    t.push('</span><br />');
    t.push('<span class="article-list-date" >{{GetDateTime(article.CreateDate)}}</span><br />');
    t.push('{{article.Description}}');
    t.push('<br />');
    t.push('<a class="article-read-more-button" ng-href="#/articles-detail/{{article.ID}}">Read More</a>');
    t.push('<br /><br />');

    t = t.join('');
    return {
        template: t
    };
});

//Article Detail DateTime Template
blogApp.directive('articleDetailDatetime', function ()
{
    var t = [];
    t.push('<div id="divMonth" class="article-month-div">{{GetMonthStringFromRawDate(article.CreateDate)}}</div>');
    t.push('<div id="divDate" class="article-date-div">{{GetDayFromRawDate(article.CreateDate)}}</div>');
    t.push('<div id="divYear" class="article-year-div">{{GetYearFromRawDate(article.CreateDate)}}</div>');

    t = t.join('');
    return {
        template: t
    };
});

//Article Title Template
blogApp.directive('articleTitle', function ()
{
    var t = [];
    t.push('<span class="article-title">{{article.Title}}</span>');

    t = t.join('');
    return {
        template: t
    };
});

blogApp.directive('userArticleListItem', function ()
{
    var t = [];
    t.push('<td>{{article.Title}}</td>');
    t.push('<td><a href="" ng-click="EditArticle(article.ID)">Edit</a></td>');
    t.push('<td><a href="" ng-click="DeleteArticle(article.ID)">Delete</a></td>');

    t = t.join('');
    return {
        template: t
    };
});