
//File:Academic_Semester List Anjular  Controller
emsApp.controller('AdmitCardCtrl', function ($scope, $http, $filter) {
    $scope.ErrorMsg = "";
    $scope.HasError = false;

    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====
    $scope.Init = function (
         getSemesterListByStudentUrl
        , getSemesterWiseAdmitCardDownloadCheckingUrl
        ) {
        $scope.getSemesterListByStudentUrl = getSemesterListByStudentUrl;
        $scope.getSemesterWiseAdmitCardDownloadCheckingUrl = getSemesterWiseAdmitCardDownloadCheckingUrl;
        $scope.getSemesterListByStudent();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

    $scope.SelectedSemesterId = 0;


    //search items

    $scope.getSemesterListByStudent = function () {
        $http.get($scope.getSemesterListByStudentUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.Data != null) {
                    $scope.SemesterList = result.Data;
                    $scope.SelectedSemesterId = $scope.SemesterList[0].Id;
                    $scope.getSemesterWiseAdmitCardDownloadChecking();
                }

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg =  result.Errors.toString()+" Unable to load Data!";
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load data! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };


    $scope.getSemesterWiseAdmitCardDownloadChecking = function () {
        $http.get($scope.getSemesterWiseAdmitCardDownloadCheckingUrl, {
            params: {
                "semesterId": $scope.SelectedSemesterId
            }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.Data != null) {
                    $scope.AdmitCard = result.Data;
                }

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = result.Errors.toString() + " Unable to load Data!";
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load data! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

});



