;
'use strict';

/* jasmine specs for services go here */

describe('Testing HelperService', function () {
    var $injector = angular.injector(['mvcBlog']);
    var myService = $injector.get('HelperService');

    it('Helper service should be defined', function () {
        expect(myService).toBeDefined();
    });

    it('Should convert raw date to year.', function () {
        expect(myService.GetYearFromRawDate('2013-12-30T22:05:22')).toEqual(2013);
    });

    it('Should convert raw date to month.', function () {
        expect(myService.GetMonthFromRawDate('2013-12-30T22:05:22')).toEqual(12);
    });

    it('Should convert raw date to month string.', function () {
        expect(myService.GetMonthStringFromRawDate('2013-12-30T22:05:22')).toEqual('December');
    });

    it('Should convert raw date to day.', function () {
        expect(myService.GetDayFromRawDate('2013-12-28T18:05:22')).toEqual(28);
    });
})