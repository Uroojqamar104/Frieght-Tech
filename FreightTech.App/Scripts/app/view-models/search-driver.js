define(['jquery', 'knockout', 'mapping', 'lodash', 'ajax'], function ($, ko, mapping, _, ajax) {

    function SearchDriverViewModel() {
        var self = this;
        var model = {
            driversArray: [],
            trackingUrl: FreightTechKeys.baseUrl + '/Driver/Track/'
        };
        mapping.fromJS(model, {}, self);

        self.init = function () {
            self.getData();
        };

        self.getData = function () {
            ajax.get('api/driver/all' + '?isAccepted=true').done(function (response) {
                if (response) {
                    self.driversArray.removeAll();
                    $.each(response, function (index, each) {
                        self.driversArray.push(each);
                    });
                }
            });
        };

        self.delete = function (data) {
            if (confirm('Are you sure you want to delete ' + data.driver.FirstName + ' ' + data.driver.LastName)) {
                ajax.delete('api/driver/' + data.driver.UserId).done(function (response) {
                    if (response) {

                        if (response.status) {
                            alert('Driver deleted successfully');
                            self.getData();
                        }
                        else {
                            var message = 'Unable to delete driver. ';
                            if (response.errors && response.errors.length > 0) {
                                message += response.errors[0];
                            }
                            alert(message);

                        }
                    }
                });
            }

        };

    };


    return SearchDriverViewModel;

});



