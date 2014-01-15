;
//Service for user
blogApp.factory("BlogUserService", function ($http, $q, $templateCache) {
    var OpType = {
        GetCurrentUser: 1
    };
    var execute = function (operation, data) {
        var deferred = $q.defer();
        if (operation === OpType.GetCurrentUser) {
            deferred.resolve($http({
                method: 'GET',
                url: '/api/BlogUser/',
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
