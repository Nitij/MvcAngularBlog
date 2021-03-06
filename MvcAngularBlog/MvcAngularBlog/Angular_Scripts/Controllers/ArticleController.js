﻿;

blogApp.controller('ArticleCtrl', ['$scope', '$routeParams', '$sce', 'BlogArticleService', 'BlogCommentsService', 'HelperService',
    function ($scope, $routeParams, $sce, BlogArticleService, BlogCommentsService, HelperService) {
        $scope.GetDateTime = HelperService.GetDateTime;
        $scope.GetDayFromRawDate = HelperService.GetDayFromRawDate;
        $scope.GetMonthStringFromRawDate = HelperService.GetMonthStringFromRawDate;
        $scope.GetYearFromRawDate = HelperService.GetYearFromRawDate;
        $scope.articleId = $routeParams.articleId;
        $scope.article = {};
        $scope.name = "";
        $scope.email = "";
        $scope.comment = "";
        $scope.comments = [];
        $scope.AddNewComment = function () {
            var currentdate = new Date(),
                datetime = currentdate.getFullYear() + "-"
                    + (currentdate.getMonth() + 1) + "-"
                    + currentdate.getDate() + " "
                    + currentdate.getHours() + ":"
                    + currentdate.getMinutes() + ":"
                    + currentdate.getSeconds(),
                commentObj =
                {
                    Name: $scope.name,
                    Email: $scope.email,
                    Comment: $scope.comment,
                    CreateDate: datetime,
                    ArticleId: $scope.articleId
                };

            BlogCommentsService.Execute(BlogCommentsService.OperationType.AddNewComment, commentObj).
            then(function (args) {
                console.log("Comment Added!");
                $scope.comments.push(
                    {
                        ID: "",
                        ArticleID: $scope.articleId,
                        Name: $scope.name,
                        Email: $scope.email,
                        Comment: $scope.comment,
                        CreateDate: datetime
                    });
                ClearCommentData();
            });
        };

        //Clears the comment data
        function ClearCommentData() {
            $scope.name = "";
            $scope.email = "";
            $scope.comment = "";
        }

        //get the article by its ID from the 'BlogDataService' service that we have injected
        //into this controller
        BlogArticleService.GetData(BlogArticleService.OperationType.GetArticleById, $scope.articleId).
            then(function (args) { $scope.article = args.data; });

        //get all the comments for this article here
        BlogCommentsService.Execute(BlogCommentsService.OperationType.GetAllComments, $scope.articleId).
            then(function (args) {
                $scope.comments = args.data;
            });
    }]);