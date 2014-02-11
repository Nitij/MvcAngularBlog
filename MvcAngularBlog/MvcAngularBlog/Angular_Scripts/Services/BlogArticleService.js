;
//Service for articles
blogApp.factory("BlogArticleService", function ($http, $q, $templateCache) {
    var OpType = {
        GetAllArticles: 1,
        GetArticleByTagName: 2,
        GetArticleById: 3,
        GetArticlesByUser: 4,
        UpdateArticle: 5,
        CreateNewArticle: 6,
        DeleteArticle: 7
    };
    var getData = function (operation, data) {
        var deferred = $q.defer();
        if (operation === OpType.GetAllArticles) {
            deferred.resolve($http({
                method: 'GET',
                url: '/api/BlogArticles/',
                cache: $templateCache
            }));
            return deferred.promise;
        }
        else if (operation === OpType.GetArticleById) {
            deferred.resolve($http({
                method: 'GET',
                url: '/api/BlogArticles/' + data
            }));
            return deferred.promise;
        }
        else if (operation === OpType.GetArticlesByUser) {
            deferred.resolve($http({
                method: 'GET',
                url: '/api/UserArticles/',
                cache: $templateCache
            }));
            return deferred.promise;
        }
    };
    var execute = function (operation, data) {
        var deferred = $q.defer();
        if (operation === OpType.UpdateArticle) {
            deferred.resolve($http({
                method: 'PUT',
                url: '/api/UserArticles/',
                data: JSON.stringify(data),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                cache: $templateCache
            }));
            return deferred.promise;
        }
        else if (operation === OpType.CreateNewArticle) {
            deferred.resolve($http({
                method: 'POST',
                url: '/api/UserArticles/',
                data: JSON.stringify(data),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                cache: $templateCache
            }));
            return deferred.promise;
        }
        else if (operation === OpType.DeleteArticle) {
            deferred.resolve($http({
                method: 'DELETE',
                url: '/api/UserArticles/' + data,
                cache: $templateCache
            }));
            return deferred.promise;
        }
        else { console.log("Unidentified method"); }
    };
    return {
        GetData: getData,
        Execute: execute,
        OperationType: OpType
    };
});