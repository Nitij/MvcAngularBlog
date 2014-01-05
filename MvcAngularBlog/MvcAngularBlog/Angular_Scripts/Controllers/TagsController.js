;

blogApp.controller('TagsCtrl', ['$scope', '$location', 'TagService', function ($scope, $location, TagService) {
    $scope.tags = [];
    $scope.Go = GoFunc;

    //lets get all the tags
    TagService.Execute(TagService.OperationType.GetAllTags).
        then(function (args) { $scope.tags = args.data; });

    //http://stackoverflow.com/questions/14201753/angular-jshow-when-to-use-ng-click-to-call-a-route
    function GoFunc(tagName) {
        $location.path('/articles-tag/' + tagName);
    }
}]);