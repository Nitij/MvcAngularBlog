;

blogApp.controller('MainCtrl', function ($scope, BlogArticleService, HelperService) {
    $scope.articles = [];
    $scope.GetDateTime = HelperService.GetDateTime;

    //get all the articles from the 'BlogDataService' service that we have injected
    //into this controller
    BlogArticleService.GetData(BlogArticleService.OperationType.GetAllArticles).
        then(function (args) { $scope.articles = args.data; });
});