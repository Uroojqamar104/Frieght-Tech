define(['jquery', 'knockout', 'mapping', 'lodash', 'ajax'], function ($, ko, mapping, _, ajax) {

    function SearchOrderViewModel() {
        var self = this;
        var model = {
            OrderArray: []
        };
        mapping.fromJS(model, {}, self);

        self.OrderStatus = {
            1: 'New',
            2: 'Pending',
            3: 'Cancelled',
            4: 'Done'
        }

        self.init = function () {

            ajax.get('api/Customer/orders/all').done(function (response) {
                if (response) {

                    $.each(response, function (index, each) {
                        self.OrderArray.push(each);
                    });
                }
            });
        };

        self.geStatusName = function (statusId) {
            return self.OrderStatus[statusId];
        };

    };


    return SearchOrderViewModel;

});

