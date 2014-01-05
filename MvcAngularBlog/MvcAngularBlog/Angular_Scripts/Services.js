;
//Service for articles
blogApp.factory("BlogArticleService", function ($http, $q, $templateCache) {
    var OpType = {
        GetAllArticles: 1,
        GetArticleByTagName: 2,
        GetArticleById: 3,
        GetArticlesByUser: 4,
        UpdateArticle: 5,
        CreateNewArticle: 6,
        DeleteArticle: 7
    };
    var getData = function (operation, data) {
        var deferred = $q.defer();
        if (operation === OpType.GetAllArticles) {
            deferred.resolve($http({
                method: 'GET',
                url: '/api/BlogArticles/',
                cache: $templateCache
            }));
            return deferred.promise;
        }
        else if (operation === OpType.GetArticleById) {
            deferred.resolve($http({
                method: 'GET',
                url: '/api/BlogArticles/' + data,
                cache: $templateCache
            }));
            return deferred.promise;
        }
        else if (operation === OpType.GetArticlesByUser) {
            deferred.resolve($http({
                method: 'GET',
                url: '/api/UserArticles/',
                cache: $templateCache
            }));
            return deferred.promise;
        }
    };
    var execute = function (operation, data) {
        var deferred = $q.defer();
        if (operation === OpType.UpdateArticle) {
            deferred.resolve($http({
                method: 'PUT',
                url: '/api/UserArticles/',
                data: JSON.stringify(data),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                cache: $templateCache
            }));
            return deferred.promise;
        }
        else if (operation === OpType.CreateNewArticle) {
            deferred.resolve($http({
                method: 'POST',
                url: '/api/UserArticles/',
                data: JSON.stringify(data),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                cache: $templateCache
            }));
            return deferred.promise;
        }
        else if (operation === OpType.DeleteArticle) {
            deferred.resolve($http({
                method: 'DELETE',
                url: '/api/UserArticles/' + data,
                cache: $templateCache
            }));
            return deferred.promise;
        }
        else { console.log("Unidentified method"); }
    };
    return {
        GetData: getData,
        Execute: execute,
        OperationType: OpType
    };
});

//Service for comments
blogApp.factory("BlogCommentsService", function ($http, $q, $templateCache) {
    var OpType = {
        AddNewComment: 1,
        GetAllComments: 2
    };
    var execute = function (operation, data) {
        var deferred = $q.defer();
        if (operation === OpType.AddNewComment) {
            deferred.resolve($http({
                method: 'POST',
                url: '/api/BlogComments/',
                data: JSON.stringify(data),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                cache: $templateCache
            }));
            return deferred.promise;
        }
        else if (operation === OpType.GetAllComments) {
            deferred.resolve($http({
                method: 'GET',
                url: '/api/BlogComments/' + data,
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

//Service for Tags
blogApp.factory("TagService", function ($http, $q, $templateCache) {
    var OpType = {
        GetAllTags: 1,
        GetArticlesByTagName: 2
    };
    var execute = function (operation, data) {
        var deferred = $q.defer();
        if (operation === OpType.GetAllTags) {
            deferred.resolve($http({
                method: 'GET',
                url: '/api/Tags/',
                cache: $templateCache
            }));
            return deferred.promise;
        }
        else if (operation === OpType.GetArticlesByTagName) {
            deferred.resolve($http({
                method: 'POST',
                url: '/api/Tags/',
                data: JSON.stringify(data),
                headers: { 'Content-Type': 'application/json; charset=utf-8', 'dataType': 'json' },
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

//Service for miscelleneous helper functions which need to be shared across controllers
blogApp.factory("HelperService", function () {
    //Convert raw date data into meaningful date and time representation
    function GetDateTimeFunc (data) {
        var date = new Date(Date.parse(data));
        var weekDay = GetWeekDayString(date.getDay());
        var month = GetMonthString(date.getMonth());
        var ampm = date.getHours() >= 12 ? 'PM' : 'AM';
        return weekDay + " " + date.getDate() + ", " + month + " " +
            date.getFullYear() + ", " + date.getHours() + ":" + date.getMinutes() + " " + ampm;
    }

    //Returns the month string
    function GetMonthString (month) {
        switch (month) {
            case 0:
                return 'January';
                break;
            case 1:
                return 'February';
                break;
            case 2:
                return 'March';
                break;
            case 3:
                return 'April';
                break;
            case 4:
                return 'May';
                break;
            case 5:
                return 'June';
                break;
            case 6:
                return 'July';
                break;
            case 7:
                return 'August';
                break;
            case 8:
                return 'September';
                break;
            case 9:
                return 'October';
                break;
            case 10:
                return 'November';
                break;
            case 11:
                return 'December';
                break;
        }
    }

    //Returns the week day string
    function GetWeekDayString (day) {
        switch (day) {
            case 0:
                return 'Sunday';
                break;
            case 1:
                return 'Monday';
                break;
            case 2:
                return 'Tuesday';
                break;
            case 3:
                return 'Wednesday';
                break;
            case 4:
                return 'Thursday';
                break;
            case 5:
                return 'Friday';
                break;
            case 6:
                return 'Saturday';
                break;
        }
    }

    //Returns month string from raw date
    function GetMonthStringFromRawDateFunc (date) {
        var date = new Date(date);
        return GetMonthString(date.getMonth());
    }

    //Returns year string from raw date
    function GetYearFromRawDateFunc (date) {
        var date = new Date(date);
        return date.getFullYear();
    }

    //Returns the month value from raw date
    function GetMonthFromRawDateFunc (date) {
        var date = new Date(date);
        return date.getMonth() + 1;
    }

    //Returns date from raw date
    function GetDayFromRawDateFunc (date) {
        var date = new Date(date);
        return date.getDate();
    }

    return {
        GetDateTime: GetDateTimeFunc,
        GetMonthFromRawDate: GetMonthFromRawDateFunc,
        GetMonthStringFromRawDate: GetMonthStringFromRawDateFunc,
        GetYearFromRawDate: GetYearFromRawDateFunc,
        GetYearFromRawDate: GetYearFromRawDateFunc,
        GetDayFromRawDate: GetDayFromRawDateFunc
    };
});

//Service for Archive
blogApp.factory("ArchiveService", function ($http, $q, $templateCache) {
    var OpType = {
        GetArticleDates: 1,
        GetArticlesByDateRange: 2
    };
    var execute = function (operation, data) {
        var deferred = $q.defer();
        if (operation === OpType.GetArticleDates) {
            deferred.resolve($http({
                method: 'GET',
                url: '/api/Archive/',
                cache: $templateCache
            }));
            return deferred.promise;
        }
        else if (operation === OpType.GetArticlesByDateRange) {
            deferred.resolve($http({
                method: 'POST',
                url: '/api/Archive/',
                data: JSON.stringify(data),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
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