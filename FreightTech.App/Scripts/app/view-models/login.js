define(['jquery', 'knockout', 'mapping', 'lodash', 'ajax'], function ($, ko, mapping, _, ajax) {

    function LoginViewModel() {
        var self = this;
        var model = {
            email: 'abc@gmail.com',
            password: 'abc123',
            isProcessing: false
        };
        mapping.fromJS(model, {}, self);

        self.init = function () {
            var deferred = $.Deferred();

            self.fillList();
            setTimeout(function () {
                deferred.resolve();
            }, 10);
            return deferred.promise();
        };
        self.login = function () {
            if (self.email() == '' || self.password() == '') {
                return;
            }
            self.isProcessing(true);

            ajax.post('Account/Login', { Email: self.email(), Password: self.password() }, true)
                .done(function (response) {
                    self.isProcessing(false);
                    if (response) {
                        if (response.status) {
                            
                            localStorage.setItem('access_token', response.tokenData.AccessToken);
                            window.location = FreightTechKeys.baseUrl + "/home";
                        }
                        else {
                            alert(response.error);
                        }
                    }
                    else {
                        alert('Phir sy error agea hai :P');
                    }
                })
        };

    };


    return LoginViewModel;
});