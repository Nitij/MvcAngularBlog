;
//Service for comments
blogApp.factory("BlogCommentsService", function ($http, $q, $templateCache) {
    var OpType = {
        AddNewComment: 1,
        GetAllComments: 2
    };
    var execute = function (operation, data) {
        var deferred = $q.defer();
        if (operation === OpType.AddNewComment) {
            deferred.resolve($http({
                method: 'POST',
                url: '/api/BlogComments/',
                data: JSON.stringify(data),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                cache: $templateCache
            }));
            return deferred.promise;
        }
        else if (operation === OpType.GetAllComments) {
            deferred.resolve($http({
                method: 'GET',
                url: '/api/BlogComments/' + data,
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