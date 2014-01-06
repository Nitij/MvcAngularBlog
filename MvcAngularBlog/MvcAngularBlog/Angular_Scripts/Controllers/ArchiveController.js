;

blogApp.controller('ArchiveCtrl', ['$scope', '$location', 'ArchiveService', 'HelperService', function ($scope, $location, ArchiveService, HelperService) {
    var tempArchiveList = [],
        archive = function (monthString, month, year, articleCount) {
            this.MonthString = monthString;
            this.Month = month;
            this.Year = year;
            this.ArticleCount = articleCount;
        },
        i = 0,
        rawDate,
        month,
        monthValue,
        year,
        previousMonth,
        previousYear,
        articleCount = 0;
    $scope.archiveList = []; //{Month, Year, ArticleCount}
    $scope.Go = GoFunc;

    //lets get all the tags
    ArchiveService.Execute(ArchiveService.OperationType.GetArticleDates).
        then(function (args) {
            tempArchiveList = args.data;
            //now lets convert that archive list into meaningful information
            if (tempArchiveList.length) {
                rawDate = tempArchiveList[0];
                previousMonth = HelperService.GetMonthStringFromRawDate(rawDate);
                monthValue = HelperService.GetMonthFromRawDate(rawDate);
                previousYear = HelperService.GetYearFromRawDate(rawDate);

                tempArchiveList.forEach(function (obj) {
                    rawDate = obj;
                    month = HelperService.GetMonthStringFromRawDate(rawDate);
                    year = HelperService.GetYearFromRawDate(rawDate);
                    if ((month === previousMonth) && (year === previousYear)) {
                        articleCount++;
                    }
                    else {
                        $scope.archiveList.push(new archive(previousMonth, monthValue, previousYear, articleCount));
                        articleCount = 1;
                    }
                    previousMonth = month;
                    previousYear = year;
                    monthValue = HelperService.GetMonthFromRawDate(rawDate);
                });
                //we need to add the last item also
                if (articleCount > 0)
                    $scope.archiveList.push(new archive(previousMonth, monthValue, previousYear, articleCount));
            }
        });

    //http://stackoverflow.com/questions/14201753/angular-jshow-when-to-use-ng-click-to-call-a-route
    function GoFunc(year, month) {
        $location.path('/articles-archive/' + year + "/" + month);
    }
}]);