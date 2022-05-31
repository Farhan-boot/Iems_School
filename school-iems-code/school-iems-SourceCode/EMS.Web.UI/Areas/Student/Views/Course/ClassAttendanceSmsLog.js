
//File:Academic_Semester List Anjular  Controller
emsApp.controller('ClassAttendanceSmsLogCtrl', function ($scope, $http, $filter) {
    $scope.SemesterList = [];
    $scope.SelectedSemester = [];

    $scope.ErrorMsg = "";
    $scope.HasError = false;

    $scope.loadPage = function () {
        $scope.getSemesterListByStudent();
    }

    $scope.getSemesterListByStudent = function () {

        $http.get($scope.getSemesterListByStudentUrl, {
            params: {}
        }).success(function (result) {
            if (!result.HasError) {
                $scope.SemesterList = result.Data;
                //console.log(result.DataExtra.Date);
                //console.log($scope.date);
                if ($scope.SemesterList != null && $scope.SemesterList.length > 0) {
                    $scope.SelectedSemester = $scope.SemesterList[0];
                    $scope.getClassAttendanceSmsLogListBySemesterId($scope.SelectedSemester);
                }

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Semester list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }

        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Semester list! " + "Status: " + status.toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);

        });
    };

    $scope.onChangeSemester = function () {
        if ($scope.SelectedSemester != null)
        { $scope.getClassAttendanceSmsLogListBySemesterId($scope.SelectedSemester) }
    };

    $scope.getClassAttendanceSmsLogListBySemesterId = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }

        $http.get($scope.getClassAttendanceSmsLogListBySemesterIdUrl, {
            params: { "id": obj.Id }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.ClassAttendanceSmsLogList = result.Data;
                //console.log(result.DataExtra.Date);
                //console.log($scope.date);


            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Class Attendance SMS Log list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }

        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Class Attendance SMS Log list! " + "Status: " + status.toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);

        });
    };

    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====
    $scope.Init = function (
        getSemesterListByStudentUrl
        , getClassAttendanceSmsLogListBySemesterIdUrl

    ) {
        $scope.getSemesterListByStudentUrl = getSemesterListByStudentUrl;
        $scope.getClassAttendanceSmsLogListBySemesterIdUrl = getClassAttendanceSmsLogListBySemesterIdUrl;
        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 



});



