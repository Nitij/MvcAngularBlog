;

blogApp.controller('UserCtrl', ['$scope', '$routeParams', 'BlogArticleService', 'BlogUserService',
    function ($scope, $routeParams, BlogArticleService, BlogUserService) {
    $scope.articles = [];
    $scope.currentArticle = {};
    $scope.currentUser = {};
    $scope.editMode = false;
    $scope.opType = null;
    $scope.EditArticle = EditArticleFunc;
    $scope.GetEditDisplay = GetEditDisplayFunc;
    $scope.CloseArticleEditing = CloseArticleEditingFunc;
    $scope.SaveArticle = SaveArticleFunc;
    $scope.GetEditMode = GetEditModeFunc;
    $scope.CreateNewArticle = CreateNewArticleFunc;
    $scope.DeleteArticle = DeleteArticleFunc;

    //lets first get all the articles for this user
    BlogArticleService.GetData(BlogArticleService.OperationType.GetArticlesByUser).
        then(function (args) { $scope.articles = args.data; });
    //get the current user's info here
    BlogUserService.Execute(BlogUserService.OperationType.GetCurrentUser).
        then(function (args) { $scope.currentUser = args.data[0]; });

    function EditArticleFunc(articleId) {
        var myNiceEditor = nicEditors.findEditor('txtArticleEditor');
        $scope.opType = "Edit";
        $scope.editMode = true;
        BlogArticleService.GetData(BlogArticleService.OperationType.GetArticleById, articleId).
        then(function (args) {
            $scope.currentArticle = args.data[0];
            myNiceEditor.setContent($scope.currentArticle.Data);
        });        
    }

    function GetEditDisplayFunc() {
        if ($scope.editMode) return "block";
        else return "none";
    }

    function CloseArticleEditingFunc() {
        var myNiceEditor = nicEditors.findEditor('txtArticleEditor');
        $scope.editMode = false;
        $scope.currentArticle = {};
        myNiceEditor.setContent('');
    }

    function SaveArticleFunc() {
        var myNiceEditor = nicEditors.findEditor('txtArticleEditor');
        $scope.currentArticle.Data = myNiceEditor.getContent();
        //we need to remove any blank spaces before and after the commas in the tags string
        if ($scope.currentArticle.Tags === '' | $scope.currentArticle.Tags === undefined)
            $scope.currentArticle.Tags = ',';
        //handle ', '
        while ($scope.currentArticle.Tags.indexOf(', ') !== -1) {
            $scope.currentArticle.Tags = $scope.currentArticle.Tags.replace(', ', ',');
        }
        //handle ' ,'
        while ($scope.currentArticle.Tags.indexOf(' ,') !== -1) {
            $scope.currentArticle.Tags = $scope.currentArticle.Tags.replace(' ,', ',');
        }

        if ($scope.opType === "Edit") {
            BlogArticleService.Execute(BlogArticleService.OperationType.UpdateArticle, $scope.currentArticle).
            then(function (args) {
                //lets update our local collection
                var i = 0;
                for (; i < $scope.articles.length; i++) {
                    if ($scope.articles[i].ID === $scope.currentArticle.ID)
                        $scope.articles[i].Title = $scope.currentArticle.Title;
                }
                console.log('Article Saved!');
            });
        }
        else if ($scope.opType === "Create") {
            //we dont want to create new article everytime user click on the save link,
            //so only create on first click then edit afterwards
            $scope.opType = "Edit";
            //sync the user name
            $scope.currentArticle.UserName = $scope.currentUser.UserName;

            BlogArticleService.Execute(BlogArticleService.OperationType.CreateNewArticle, $scope.currentArticle).
            then(function (args) {
                $scope.currentArticle = args.data;
                $scope.articles.push($scope.currentArticle);
                console.log('New Article Added!');
            });
        }
    }

    function DeleteArticleFunc(articleId) {
        if (articleId === $scope.currentArticle.ID) {
            $scope.editMode = false;
        }
        BlogArticleService.Execute(BlogArticleService.OperationType.DeleteArticle, articleId).
        then(function (args) {
            //lets update our local collection
            var i = 0;
            for (; i < $scope.articles.length; i++) {
                if ($scope.articles[i].ID === articleId)
                    $scope.articles.splice(i, 1);
            }
            console.log('Article Deleted!');
        });
    }

    function GetEditModeFunc() {
        if ($scope.editMode) return "block";
        else return "none";
    }

    function CreateNewArticleFunc() {
        $scope.editMode = true;
        $scope.opType = "Create";
    }
}]);