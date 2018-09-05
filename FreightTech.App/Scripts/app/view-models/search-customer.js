define(['jquery', 'knockout', 'mapping', 'lodash', 'ajax'], function ($, ko, mapping, _, ajax) {

    function SearchCustomerViewModel() {
        var self = this;
        var model = {
           customersArray: []
        };
        mapping.fromJS(model, {}, self);

        self.init = function () {

            self.getData();    
        };

        self.getData = function () {
            ajax.get('api/Customer/all').done(function (response) {
                if (response) {

                    $.each(response, function (index, each) {
                        self.customersArray.push(each);
                    });
                }
            });
        };

        self.delete = function (data) {
            if (confirm('Are you sure you want to delete ' + data.FirstName + ' ' + data.LastName)) {
                ajax.delete('api/customer/' + data.UserId).done(function (response) {
                    if (response) {

                        if (response.status) {
                            alert('Customer deleted successfully');
                            self.getData();
                        }
                        else {
                            var message = 'Unable to delete customer. ';
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


    return SearchCustomerViewModel;

});

