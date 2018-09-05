requirejs.config({
    baseUrl: FreightTechKeys.baseUrl + '/Scripts/app',
    enforceDefine: true,
    paths: {
        jquery: '../libs/jquery/jquery-3.3.1.min',
        bootstrap: '../../../content/theme/vendor/bootstrap/js/bootstrap.min',
        knockout: '../libs/knockout/knockout-3.4.2',
        mapping: '../libs/knockout/knockout.mapping-latest',
        lodash: '../libs/lodash/lodash.min',
        ajax: '../app/services/ajax',
        viewModel: '../app/view-models/track-orders'
    },
    shim: {
        bootstrap: { deps: ['jquery'], exports: 'jQuery.fn.alert' },
        mapping: { deps: ['knockout'], exports: 'ko.mapping' },
        lodash: { exports: '_' },
    }
});

define(['jquery', 'knockout', 'viewModel']
    , function ($, ko, ViewModel) {

        var node = $('#div-main');
        ko.cleanNode(node[0]);

        var vm = new ViewModel();
        ko.applyBindings(vm, node[0]);
        vm.init();

    });


