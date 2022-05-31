
//File:Academic_Semester List Anjular  Controller
emsApp.controller('EmployeeDashboardCtrl', function ($scope, $http, $filter) {
    $scope.SemesterList = [];
    $scope.SelectedSemester =[];
    
    $scope.ErrorMsg = "";
    $scope.HasError = false;

    $scope.loadPage = function () {
        $scope.getSemesterListByFaculty();
    }

    $scope.getSemesterListByFaculty = function () {
        
        $http.get($scope.getSemesterListByFacultyUrl, {
            params: { }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.SemesterList = result.Data;
                //console.log(result.DataExtra.Date);
                //console.log($scope.date);
                if ($scope.SemesterList != null && $scope.SemesterList.length > 0) {
                    $scope.SelectedSemester = $scope.SemesterList[0];
                    $scope.getClassListBySemesterId($scope.SelectedSemester);
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
        if ($scope.SelectedSemester!= null)
        { $scope.getClassListBySemesterId($scope.SelectedSemester) }
    };

    $scope.getClassListBySemesterId = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
       
        $http.get($scope.getClassListBySemesterIdUrl, {
            params: { "id": obj.Id }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.ClassList = result.Data;
                //console.log(result.DataExtra.Date);
                //console.log($scope.date);
                

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Class list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }

        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Class list! " + "Status: " + status.toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);

        });
    };

    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====
    $scope.Init = function (
         getSemesterListByFacultyUrl
         , getClassListBySemesterIdUrl
         
        ) {
        $scope.getSemesterListByFacultyUrl = getSemesterListByFacultyUrl;
        $scope.getClassListBySemesterIdUrl = getClassListBySemesterIdUrl;
        //$scope.testurl = testurl;
        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 



});



