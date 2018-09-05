define(['jquery', 'knockout', 'mapping', 'lodash', 'ajax'], function ($, ko, mapping, _, ajax) {

    function requestsDriverViewModel() {
        var self = this;
        var model = {
            driversArray1: []
        };
        mapping.fromJS(model, {}, self);

        self.init = function () {
            self.fetchAll();
            
        };
        self.fetchAll = function () {
            ajax.get('api/driver/all' + '?isAccepted=false').done(function (response) {
                if (response) {
                  
                    self.driversArray1.removeAll();
                    $.each(response, function (index, each) {
                        self.driversArray1.push(each);
                    });
                }
            });
        };

        self.approveDriver = function (data) {

            ajax.post('api/driver/' + data.driver.UserId + '/approve').done(function (response) {
                if (response.status) {
                    self.fetchAll();
                    alert('Driver approved successfully');
                }
                else {
                    alert(response.message)
                }


            });
        };

    };


    return requestsDriverViewModel;

});



