define(['jquery', 'knockout', 'mapping', 'lodash', 'ajax'], function ($, ko, mapping, _, ajax) {

    var interval;
    function TrackingViewModel() {
        var self = this;
        var model = {
            orderId: ''
        };
        mapping.fromJS(model, {}, self);

        self.init = function () {
            self.orderId('');



        };

        self.start = function () {
            //interval = setInterval(self.collectData, 5000);
        };
        self.stop = function () {
            //clearInterval(interval);
            //self.init();
        };
        self.collectData = function () {
            if (self.orderId().trim() == '') {
                return;
            }
            ajax.get('api/Orders/getbyorderid?orderId=' + self.orderId().trim()).done(function (response) {
                if (response) {
                    $.each(response, function (index, each) {
                        each.lat = parseFloat(each.lat);
                        each.lng = parseFloat(each.lng);
                    });
                    self.initMap(response);
                }
            });
        };

        self.initMap = function (locations) {
            var map = new google.maps.Map(document.getElementById('map'), {
                zoom: 20,
                center: { lat: locations[0].lat, lng: locations[0].lng },
                mapTypeId: 'roadmap'
            });

            var flightPlanCoordinates = locations;
            var flightPath = new google.maps.Polyline({
                path: flightPlanCoordinates,
                geodesic: true,
                strokeColor: '#FF0000',
                strokeOpacity: 1.0,
                strokeWeight: 2
            });

            flightPath.setMap(map);
        }
    };


    return TrackingViewModel;

});

