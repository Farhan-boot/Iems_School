var emsApp = angular.module('emsApp', [
   //'ui.bootstrap.datetimepicker', //js in bundle
    //'ngGrid', //js in bundle
    //'smart-table',//js in bundle
    'filters',
    'ngFileUpload',
    //'ngImgCrop',//js in bundle
    'chieffancypants.loadingBar',
    'ngSanitize',
    'ui.select', //use for ui-select
    'datetime'//don't delete, its for datetime format
   //'xdan.datetimepicker',//don't delete working with 
]).run(function ($http) {
    //adding token header for anti forgery protection
    $http.defaults.headers.common['X-XSRF-Token'] = angular.element('input[name="__RequestVerificationToken"]').attr('value');

    //setting datetime format for ng datetime, and datetimepicker binding input[type=datetime]
    angular.element("#datetimepicker,.datetimepicker,.datetime-picker").attr('datetime', 'dd/MM/yyyy HH:mm').datetimepicker({
        //value:dateFormat(this.val, "dd/mm/yyyy"),//'2015/04/15 05:03',
        //formatTime: 'H:i',
        //formatDate: 'd.m.Y',
        //formatDate: 'Y/m/d',
        format: 'd/m/Y H:i',
        timepicker: true,
        step: 15,
        //dayOfWeekStart: 1,
        //lang: 'en'
        //disabledDates: ['1986/01/08','1986/01/09','1986/01/10'],
        //startDate: '1986/01/05'
    });
    angular.element("#datepicker,.datepicker,.date-picker").attr('datetime', 'dd/MM/yyyy').datetimepicker({
        format: 'd/m/Y',
        timepicker: false
    });
    angular.element("#timepicker,.timepicker,.time-picker").attr('datetime', 'HH:mm').datetimepicker({
        formatTime: 'H:i',
        format: 'H:i',
        timepicker: true,
        step: 10,
        datepicker: false
    });
   
}).config([
        'cfpLoadingBarProvider', function (cfpLoadingBarProvider) {
            //added from http://chieffancypants.github.io/angular-loading-bar/#
            cfpLoadingBarProvider.includeSpinner = true;
            cfpLoadingBarProvider.includeBar = true;
            //cfpLoadingBarProvider.latencyThreshold = 500;
            //for config custom, cfpLoadingBar
            //ignoreLoadingBar: true
            //cfpLoadingBarProvider.parentSelector = '#loading-bar-container';
            //cfpLoadingBarProvider.spinnerTemplate = '<div><span  class="fa fa-spinner">Custom Loading Message...</div>';
            //cfpLoadingBarProvider.spinnerTemplate = '<div><span  class="fa fa-spinner">Loading...</div>';
        cfpLoadingBarProvider.spinnerTemplate =
            '<div id="loader" class="loading-spinner" style=""><i class="fa fa-refresh fa-spin fa-5x fa-fw margin-bottom" style="font-size: 100px;"></i></div>';
    }
]).config(['$locationProvider', function ($locationProvider) {
    $locationProvider.html5Mode(true).hashPrefix('!');//solved adding extra slash/ in url
}]);

//'ngTable', "ngResource"
//Custom zero padding left
angular.module('filters', []).filter('zpad', function () {
    return function (input, n) {
        if (input === undefined)
            input = "";
        if (input.length >= n)
            return input;
        var zeros = "0".repeat(n);
        return (zeros + input).slice(-1 * n);
    };
});

//work for fire on press enter key 
emsApp.directive('ngEnter', function () {
    return function (scope, element, attrs) {
        element.bind("keydown keypress", function (event) {
            if(event.which === 13) {
                scope.$apply(function (){
                    scope.$eval(attrs.ngEnter);
                });
                event.preventDefault();
            }
        });
    };
});

/** Usage: on enter focus next
  <input next-focus >
  <input next-focus >
  Upon pressing ENTER key the directive will switch focus to the next available attrib next-focus.
  eg:http://stackoverflow.com/questions/18086865/angularjs-move-focus-to-next-control-on-enter
**/
emsApp.directive('nextFocus', function () {
    return {
        restrict: 'A',
        link: function($scope,elem,attrs) {
            elem.bind('keydown', function(e) {
                var code = e.keyCode || e.which;
                if (code === 13) {
                    //e.preventDefault();
                    $(elem).css('border', '2px solid #5bb15b');
                    //log($(elem).val());
                    //log($('[focus]').val());
                    var select = $('[next-focus]');
                    if (select != null && select.length > 1) {
                        var nextIndex = select.index($(elem)) + 1; //select next element index
                        if (nextIndex < select.length) {
                            select[nextIndex].focus();
                            //$(select[nextIndex]).css({ 'border': '2px solid red', 'background-color': 'red' });
                        }
                    }
                }
            });
        }
    }
});
//https://gist.github.com/yrezgui/5653591
//used in class note upload
emsApp.filter('filesize', function () {
    var units = [
      'bytes',
      'KB',
      'MB',
      'GB',
      'TB',
      'PB'
    ];
    return function (bytes, precision) {
        if (isNaN(parseFloat(bytes)) || !isFinite(bytes)) {
            return '?';
        }
        var unit = 0;
        while (bytes >= 1024) {
            bytes /= 1024;
            unit++;
        }
        return bytes.toFixed(+precision) + ' ' + units[unit];
    };
});
//https://jsfiddle.net/karthikjsf/hdbszpo4/1/
//count total over a field in List
//<b>  {{ filtered|total:'age' }}</b>
emsApp.filter('total', function () {
    return function (input, property) {
        if (!input) return;
        var i = input.length;
        var total = 0;
        while (i--)
            total += Number(input[i][property]);
        return total;
    }
});

/**
 * AngularJS default filter with the following expression:
 * "person in people | filter: {name: $select.search, age: $select.search}"
 * performs an AND between 'name: $select.search' and 'age: $select.search'.
 * We want to perform an OR.
 */
emsApp.filter('orFilter', function () {
    return function (items, props) {
        var out = [];
        if (angular.isArray(items)) {
            var keys = Object.keys(props);

            items.forEach(function (item) {
                var itemMatches = false;

                for (var i = 0; i < keys.length; i++) {
                    var prop = keys[i];

                    if (item[prop]==null) {
                        continue;
                    }
                    var text = props[prop].toLowerCase();
                    if (item[prop].toString().toLowerCase().indexOf(text) !== -1) {
                        itemMatches = true;
                        break;
                    }
                }

                if (itemMatches) {
                    out.push(item);
                }
            });
        } else {
            // Let the output be the input untouched
            out = items;
        }

        return out;
    };
});

//emsApp.directive('pageSelect', function () {
//    return {
//        restrict: 'E',
//        template: '<input type="text" class="select-page" ng-model="inputPage" ng-change="selectPage(inputPage)">',
//        link: function (scope, element, attrs) {
//            scope.$watch('currentPage', function (c) {
//                scope.inputPage = c;
//            });
//        }
//    }
//});


/// <input type="text"  file-Model="dd/MM/yyyy" ng-model="date" class="form-control" style="width: 150px" />
//emsApp.directive('fileModel', ['$parse', function ($parse) {
//    return {
//        require: '?ngModel',
//        //require: 'ngModel',
//        restrict: 'A',
//        scope: {
//          ngModel: '='
//        },
//        link: function (scope, element, attrs, ngModel) {
//            var modelGetter2 = $parse(attrs.fileModel);
//            var modelGetter = $parse(attrs['ngModel']);
//            //console.log(modelGetter(scope));
//            var modelSetter = modelGetter.assign;
//            // This is how you can use it to set the value 'bar' on the given scope.
//            modelSetter(scope, 'dfrdftdft');

//            //console.log(modelGetter(scope));
//            attrs.$observe("ngModel", function () {
//                console.log(scope.ngModel);
//               // validMinMax(parser.getDate());
//            });

//            //console.log(attrs.fileModel); 
//            //console.log(attrs.datetime);
//            //console.log(modelSetter);
//            //console.log("model:"+scope.ngModel);
//            //element.val("456789");
//            //scope.ngModel = "Cikap";
//            //ngModel.$setViewValue("Pavel");
     
//            //scope.$watch(attrs.ngModel, function () {
//            //    console.log("Changed to " + scope[attrs.ngModel]);
//            //});
//            element.bind('change', function () {
//                scope.$apply(function () {
//                    //modelSetter(scope, element[0].files[0]);
//                    //element.val("456789");
//                    //ngModel.$setViewValue("Pavel");
//                    scope.ngModel = "Pavel";
//                });
//            });
//        }
//    };
//}]);


