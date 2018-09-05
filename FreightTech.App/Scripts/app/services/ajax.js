define(['jquery'], function ($) {

    return (function () {
        var appBaseUrl = FreightTechKeys.appBaseUrl;
        var apiBaseUrl = FreightTechKeys.apiBaseUrl;
        var token = localStorage.getItem("access_token");
        var headers = {};

        return {
            get: function (endpoint, isAppCall) {
                var baseUrl = isAppCall ? appBaseUrl : apiBaseUrl;
                if (!isAppCall) {
                    headers = { "Authorization": 'Bearer ' + token };
                }

                return $.ajax({
                    url: (baseUrl + endpoint),
                    dataType: 'json',
                    headers: headers
                });
            },
            post: function (endpoint, data, isAppCall) {
                var baseUrl = isAppCall ? appBaseUrl : apiBaseUrl;
                if (!isAppCall) {
                    headers = { "Authorization": 'Bearer ' + token };
                }

                return $.ajax({
                    url: (baseUrl + '/' + endpoint),
                    type: 'POST',
                    data: JSON.stringify(data),
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    headers: headers
                });
            },
            put: function (endpoint, data, isAppCall) {
                var baseUrl = isAppCall ? appBaseUrl : apiBaseUrl;
                if (!isAppCall) {
                    headers = { "Authorization": 'Bearer ' + token };
                }

                return $.ajax({
                    url: (baseUrl + '/' + endpoint),
                    type: 'PUT',
                    data: JSON.stringify(data),
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    headers: headers
                });
            },
            delete: function (endpoint, data, isAppCall) {
                var baseUrl = isAppCall ? appBaseUrl : apiBaseUrl;
                if (!isAppCall) {
                    headers = { "Authorization": 'Bearer ' + token };
                }

                return $.ajax({
                    url: (baseUrl + '/' + endpoint),
                    data: JSON.stringify(data),
                    contentType: 'application/json; charset=utf-8',
                    type: 'DELETE',
                    headers: headers
                });
            },
            head: function (endpoint, data, isAppCall) {
                var baseUrl = isAppCall ? appBaseUrl : apiBaseUrl;
                if (!isAppCall) {
                    headers = { "Authorization": 'Bearer ' + token };
                }

                return $.ajax({
                    url: (baseUrl + '/' + endpoint),
                    data: data,
                    dataType: 'json',
                    type: 'HEAD'
                });
            },
            jsonp: function (endpoint, data, isAppCall) {
                var baseUrl = isAppCall ? appBaseUrl : apiBaseUrl;

                return $.ajax({
                    url: String.format('{0}/{1}?token={2}', baseUrl, endpoint, token),
                    data: data,
                    dataType: 'jsonp',
                    success: function (response) {
                        if (!("StatusCode" in response) == 0 && response["StatusCode"] >= 400) {
                            sendException(response["StatusCode"], this.type, response["ErrorMessage"], this.url, false);
                            return;
                        }
                    }
                });
            }
        };

    }());

});