define(['jquery', 'knockout', 'mapping', 'lodash', 'ajax'], function ($, ko, mapping, _, ajax) {

    function DashboardViewModel() {
        var self = this;
        var model = {
            accessToken: localStorage.getItem('access_token'),
            pendingOrdersCount: 0,
            completedOrdersCount: 0,
            pendingDriversCount: 0,
            totalFeedbackCount: 0
        };
        mapping.fromJS(model, {}, self);

        self.init = function () {
            self.getStats();
            // var deferred = $.Deferred();

            //self.fillList();
            //setTimeout(function () {
            //    deferred.resolve();
            //}, 10);
            //return deferred.promise();
        };
        self.getStats = function () {

            ajax.get('api/home/get/stats').done(function (response) {
                if (response) {
                    self.pendingOrdersCount(response.pendingOrdersCount);
                    self.completedOrdersCount(response.completedOrdersCount);
                    self.pendingDriversCount(response.pendingDriversCount);
                    self.totalFeedbackCount(response.totalFeedbackCount);
                }
            });
        };
        self.fillList = function () {

            ajax.get('api/Orders/Get2').done(function (response) {
                debugger

            });

            ajax.post('api/Orders/post2').done(function (response) {
                debugger

            });

            //ajax.get('api/Orders/Get1').done(function (response) {
            //});
        };

    };


    return DashboardViewModel;
});