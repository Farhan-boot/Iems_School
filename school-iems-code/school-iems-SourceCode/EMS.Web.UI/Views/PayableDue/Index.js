
//File: Aca_SemesterResult Anjular  Controller
emsApp.controller('PublicPayableDueCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.StudentUserName = "";
    $scope.Student = null;
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;

    $scope.getSemesterPayableDueByStudentUserName = function () {
        if ($scope.StudentUserName == null || $scope.StudentUserName === "") {
            return;
        }

        $http.get($scope.getSemesterPayableDueByStudentUserNameUrl, {
            params: {
                "studentUserName": $scope.StudentUserName
            }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;

                $scope.Student = result.Data;
                $scope.ErrorMsg = "";
                updateUrlQuery('sid', $scope.StudentUserName);

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg =result.Errors.toString();
                $scope.Student = null;
                //alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Please try again later!";
            log(JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString());
        });
    };


    //======Custom property and Functions Start=======================================================  

    $scope.bKashPaymentProcessModalShow = function () {
        $('#bKashPaymentProcessModal').modal('show');
    };
    //======Custom Variabales goes hare=====

    $scope.Init = function (
        StudentUserName
        , getSemesterPayableDueByStudentUserNameUrl
    ) {
        $scope.StudentUserName = StudentUserName;
        $scope.getSemesterPayableDueByStudentUserNameUrl = getSemesterPayableDueByStudentUserNameUrl;
        $scope.getSemesterPayableDueByStudentUserName();
    };

});

