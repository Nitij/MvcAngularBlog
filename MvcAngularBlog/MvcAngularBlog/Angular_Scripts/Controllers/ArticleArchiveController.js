;

blogApp.controller('ArticleArchiveCtrl', ['$scope', '$routeParams', 'ArchiveService', 'HelperService', function ($scope, $routeParams, ArchiveService, HelperService) {
    $scope.month = $routeParams.month;
    $scope.year = $routeParams.year;
    $scope.articles = [];
    $scope.GetDateTime = HelperService.GetDateTime;

    //lets get all the tags
    ArchiveService.Execute(ArchiveService.OperationType.GetArticlesByDateRange, { Month: $routeParams.month, Year: $routeParams.year }).
        then(function (args) { $scope.articles = args.data; });
}]);