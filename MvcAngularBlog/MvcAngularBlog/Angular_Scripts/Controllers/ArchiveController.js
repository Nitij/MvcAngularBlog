;

blogApp.controller('ArchiveCtrl', ['$scope', 'ArchiveService', 'HelperService', function ($scope, ArchiveService, HelperService) {
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
}]);