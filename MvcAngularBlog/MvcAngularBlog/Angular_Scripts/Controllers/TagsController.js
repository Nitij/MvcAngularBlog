;

blogApp.controller('TagsCtrl', function ($scope, TagService) {
    $scope.tags = [];

    //lets get all the tags
    TagService.Execute(TagService.OperationType.GetAllTags).
        then(function (args) { $scope.tags = args.data; });
});