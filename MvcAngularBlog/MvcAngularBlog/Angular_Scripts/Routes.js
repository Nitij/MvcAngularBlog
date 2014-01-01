;

blogApp.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/articles', {
        templateUrl: '../Angular_Views/Articles.html',
        controller: 'MainCtrl'
    });
    $routeProvider.when('/articles-detail/:articleId', {
        templateUrl: '../Angular_Views/Article-Detail.html',
        controller: 'ArticleCtrl'
    });
    $routeProvider.when('/articles-tag/:tagName', {
        templateUrl: '../Angular_Views/Article-Tag.html',
        controller: 'ArticleTagCtrl'
    });
    $routeProvider.when('/articles-archive/:year/:month', {
        templateUrl: '../Angular_Views/Article-Archive.html',
        controller: 'ArticleArchiveCtrl'
    });
    $routeProvider.otherwise({ redirectTo: '/articles' });
}]);