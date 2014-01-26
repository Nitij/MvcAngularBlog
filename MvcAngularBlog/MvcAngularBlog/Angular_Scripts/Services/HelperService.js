;
//Service for miscelleneous helper functions which need to be shared across controllers
blogApp.factory("HelperService", function () {
    //Convert raw date data into meaningful date and time representation
    function GetDateTimeFunc(data) {
        var date = new Date(Date.parse(data));
        var weekDay = GetWeekDayString(date.getDay());
        var month = GetMonthString(date.getMonth());
        var ampm = date.getHours() >= 12 ? 'PM' : 'AM';
        return weekDay + " " + date.getDate() + ", " + month + " " +
            date.getFullYear() + ", " + date.getHours() + ":" + date.getMinutes() + " " + ampm;
    }

    //Returns the month string
    function GetMonthString(month) {
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
    function GetWeekDayString(day) {
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
    function GetMonthStringFromRawDateFunc(date) {
        var date = new Date(date);
        return GetMonthString(date.getMonth());
    }

    //Returns year string from raw date
    function GetYearFromRawDateFunc(date) {
        var date = new Date(date);
        return date.getFullYear();
    }

    //Returns the month value from raw date
    function GetMonthFromRawDateFunc(date) {
        var date = new Date(date);
        return date.getMonth() + 1;
    }

    //Returns date from raw date
    function GetDayFromRawDateFunc(date) {
        var date = new Date(date);
        return date.getDate();
    }

    return {
        GetDateTime: GetDateTimeFunc,
        GetMonthFromRawDate: GetMonthFromRawDateFunc,
        GetMonthStringFromRawDate: GetMonthStringFromRawDateFunc,
        GetYearFromRawDate: GetYearFromRawDateFunc,
        GetDayFromRawDate: GetDayFromRawDateFunc
    };
});