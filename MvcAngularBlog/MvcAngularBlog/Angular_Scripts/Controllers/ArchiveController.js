;

blogApp.controller('ArchiveCtrl', function ($scope, ArchiveService, HelperService) {
    var tempArchiveList = [],
        archive = function (month, year, articleCount) {
            this.Month = month;
            this.Year = year;
            this.ArticleCount = articleCount;
        },
        i = 0,
        rawDate,
        month,
        year,
        articleCount = 1;
    $scope.archiveList = []; //{Month, Year, ArticleCount}

    //lets get all the tags
    ArchiveService.Execute(ArchiveService.OperationType.GetArticleDates).
        then(function (args) {
            tempArchiveList = args.data;

            //now lets convert that archive list into meaningful information
            for (; i < tempArchiveList.length; i++) {
                rawDate = tempArchiveList[i];
                month = HelperService.GetMonthFromRawDate(rawDate);
                year = HelperService.GetYearFromRawDate(rawDate);
                $scope.archiveList.push(new archive(month, year, articleCount++));

                //reset article count
                articleCount = 1;
            }
            debugger;
        });
});