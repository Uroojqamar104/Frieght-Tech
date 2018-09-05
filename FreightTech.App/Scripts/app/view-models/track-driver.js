define(['jquery', 'knockout', 'mapping', 'lodash', 'ajax'], function ($, ko, mapping, _, ajax) {

    //var index = 0;
    function SearchDriverViewModel(driverId) {
        var self = this;
        var model = {
            driverId: driverId,
            driverName: '',
            mapElement: '',
            currentLat: 24.9191437,
            currentLong: 67.1020151,
            positions: ko.observableArray(),
            isFetching: false,
            fetchInterval: 1000

        };
        mapping.fromJS(model, {}, self);

        self.init = function () {

            ajax.get('api/driver/' + self.driverId()).done(function (response) {
                if (response) {
                    self.driverName(response.FirstName + ' ' + response.LastName);
                    setTimeout(self.initializeMap, 2000);
                }
                else {
                    alert('No driver found!');
                    self.driverName('');
                }
            });
        };

        self.startTracking = function () {
            setInterval(self.fetchLocation, self.fetchInterval());
        };
        self.fetchLocation = function () {
            if (self.isFetching()) {
                return;
            }

            self.isFetching(true);
            ajax.get('api/driver/' + self.driverId() + '/location/current').done(function (response) {
            debugger
                if (response) {
                    //index = response.DriverId;
                    var result = [response.Latitude, response.Longitude];
                    self.transition(result);
                    self.isFetching(false);
                }
                else {
                    alert('No data found!.')
                }
            });
        };
        self.initializeMap = function () {
            var latlng = new google.maps.LatLng(self.currentLat(), self.currentLong());
            var myOptions = {
                zoom: 16,
                center: latlng,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };

            self.mapElement(new google.maps.Map(document.getElementById("map"), myOptions));

            marker = new google.maps.Marker({
                position: latlng,
                map: self.mapElement(),
                title: "Latitude:" + self.currentLat() + " | Longitude:" + self.currentLong(),
                icon: '/Content/images/map/truck1_24x24.png'
            });

            //google.maps.event.addListener(self.mapElement(), 'click', function (event) {
            //    var result = [event.latLng.lat(), event.latLng.lng()];
            //    self.transition(result);
            //});
        };
        self.transition = function (result) {
            i = 0;
            deltaLat = (result[0] - self.currentLat()) / numDeltas;
            deltaLng = (result[1] - self.currentLong()) / numDeltas;
            self.moveMarker();
        }

        self.moveMarker = function () {

            //var a = self.currentLat();
            //var b = self.currentLong();

            //a += deltaLat;
            //b += deltaLng;

            //self.currentLat(a);
            //self.currentLong(b);

            self.currentLat((self.currentLat() + deltaLat));
            self.currentLong((self.currentLong() + deltaLng));

            var latlng = new google.maps.LatLng(self.currentLat(), self.currentLong());
            marker.setTitle("Latitude:" + self.currentLat() + " | Longitude:" + self.currentLong());
            marker.setPosition(latlng);
            if (i != numDeltas) {
                i++;
                setTimeout(self.moveMarker, delay);
            }

            var centerPosition = new google.maps.LatLng(self.currentLat(), self.currentLong());
            self.mapElement().setCenter(centerPosition);

        }

    };


    return SearchDriverViewModel;

});




//Load google map


var numDeltas = 100;
var delay = 10; //milliseconds
var i = 0;
var deltaLat;
var deltaLng;


