;
//Service for Tags
blogApp.factory("TagService", ['$http', '$q', '$templateCache', function ($http, $q, $templateCache) {
    var OpType = {
        GetAllTags: 1,
        GetArticlesByTagName: 2
    };
    var execute = function (operation, data) {
        var deferred = $q.defer();
        if (operation === OpType.GetAllTags) {
            deferred.resolve($http({
                method: 'GET',
                url: '/api/Tags/',
                cache: $templateCache
            }));
            return deferred.promise;
        }
        else if (operation === OpType.GetArticlesByTagName) {
            deferred.resolve($http({
                method: 'POST',
                url: '/api/Tags/',
                data: JSON.stringify(data),
                headers: { 'Content-Type': 'application/json; charset=utf-8', 'dataType': 'json' },
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
}]);