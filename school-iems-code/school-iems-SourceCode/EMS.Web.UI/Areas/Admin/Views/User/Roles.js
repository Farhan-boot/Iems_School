emsApp.controller('UserRoleListCtrl', function ($scope, $filter, $http) {
    $scope.Init = function (rootUrl, url, urlView) {
        $scope.RootUrl = rootUrl;
        $scope.url = url;
        $scope.urlView = urlView;
    };

    $http.get('http://localhost/ems/api/UserRole/Get/').success(function (data) {
        $scope.rowCollection= data;

    });

    $scope.itemsByPage = 15;

    $scope.predicates = ['Id', 'Name', 'Description'];
    $scope.selectedPredicate = $scope.predicates[0];

    $scope.getters = {
        Name: function (value) {
            //this will sort by the length of the name string
            return value.Name.length;
        }
    }
});



