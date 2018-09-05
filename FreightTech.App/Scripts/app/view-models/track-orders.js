define(['jquery', 'knockout', 'mapping', 'lodash', 'ajax'], function ($, ko, mapping, _, ajax) {

    function TrackActiveOrdersViewModel() {
        var self = this;
        var model = {
            ordersArray: [],
            trackingUrl: FreightTechKeys.baseUrl + '/Driver/Track/'
        };
        mapping.fromJS(model, {}, self);

        self.OrderStatus = {
            'New': 1,
            'Pending': 2,
            'Cancelled': 3,
            'Done': 4
        };

        self.init = function () {

            ajax.get('api/orders/all' + '?statusId=' + self.OrderStatus.Pending).done(function (response) {
                if (response) {
                    $.each(response, function (index, each) {
                        self.ordersArray.push(each);
                    });
                }
            });
        };

    };


    return TrackActiveOrdersViewModel;

});



