define(['jquery', 'knockout', 'mapping', 'lodash', 'ajax'], function ($, ko, mapping, _, ajax) {

    function SearchFeedbackViewModel() {
        var self = this;
        var model = {
           FeedbackArray: []
        };
        mapping.fromJS(model, {}, self);

        self.init = function () {

            ajax.get('api/Customer/feedback/all').done(function (response) {
                if (response) {

                    $.each(response, function (index, each) {
                        self.FeedbackArray.push(each);
                    });
                }
            });
        };

    };


    return SearchFeedbackViewModel;

});

