;

blogApp.controller('ArticleTagCtrl', ['$scope', '$routeParams', 'TagService', 'HelperService',
    function ($scope, $routeParams, TagService, HelperService) {
        $scope.tagName = $routeParams.tagName;
        $scope.articles = [];
        $scope.GetDateTime = HelperService.GetDateTime;

        //lets get all the articles
        TagService.Execute(TagService.OperationType.GetArticlesByTagName, $scope.tagName).
            then(function (args) { $scope.articles = args.data; });
    }]);