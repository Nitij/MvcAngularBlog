;
//Service for Archive
blogApp.factory("ArchiveService", function ($http, $q, $templateCache) {
    var OpType = {
        GetArticleDates: 1,
        GetArticlesByDateRange: 2
    };
    var execute = function (operation, data) {
        var deferred = $q.defer();
        if (operation === OpType.GetArticleDates) {
            deferred.resolve($http({
                method: 'GET',
                url: '/api/Archive/',
                cache: $templateCache
            }));
            return deferred.promise;
        }
        else if (operation === OpType.GetArticlesByDateRange) {
            deferred.resolve($http({
                method: 'POST',
                url: '/api/Archive/',
                data: JSON.stringify(data),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                cache: $templateCache
            }));
            return deferred.promise;
        }
        else { console.log("Unidentified method"); }
    };
    return {
        Execute: execute,
        OperationType: OpType
    };
});