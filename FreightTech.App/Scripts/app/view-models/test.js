define(['jquery', 'knockout', 'mapping', 'lodash', 'ajax'], function ($, ko, mapping, _, ajax) {

    var interval;
    function TestViewModel() {
        var self = this;
        var model = {
            orderId: ''
        };
        mapping.fromJS(model, {}, self);

        self.init = function () {
            self.orderId('');

            ajax.get('api/Orders/getorderid').done(function (response) {
                if (response) {
                    self.orderId(response);
                }
            });

        };

        self.start = function () {
            interval = setInterval(self.collectData, 5000);
        };
        self.stop = function () {
            clearInterval(interval);
            self.init();
        };
        self.collectData = function () {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(successFunction, errorFunction);
            } else {
                alert('It seems like Geolocation, which is required for this page, is not enabled in your browser. Please use a browser which supports it.');
            }
            function successFunction(position) {                
                ajax.post('api/Orders/test', {
                    OrderId: self.orderId(),
                    Lat: position.coords.latitude,
                    Long: position.coords.longitude
                });

                //var lat = position.coords.latitude;
                // long = position.coords.longitude;
                //console.log('Your latitude is :' + lat + ' and longitude is ' + long);
            }

            function errorFunction(error) {
                console.log(error);
            }
        };
    };


    return TestViewModel;

});

