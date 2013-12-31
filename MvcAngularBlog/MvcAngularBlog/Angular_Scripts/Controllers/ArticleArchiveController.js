;

blogApp.controller('ArticleArchiveCtrl', function ($scope, ArchiveService) {
    $scope.tags = [];

    //lets get all the tags
    ArchiveService.Execute(ArchiveService.OperationType.GetArticleDates).
        then(function (args) { $scope.tags = args.data; });
});