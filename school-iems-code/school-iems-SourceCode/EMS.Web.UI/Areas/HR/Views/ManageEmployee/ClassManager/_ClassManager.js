
//File:Academic_Semester List Anjular  Controller
emsApp.controller('HrEmployeeClassCtrl', function ($scope, $http,$rootScope, $filter) {
    $scope.SemesterList = [];
    $scope.SelectedSemester =[];
    
    $scope.ClassErrorMsg = "";
    $scope.ClassHasError = false;

    //$rootScope.$on('test', function (obj) {
    //    alertSuccess("Successfully Data Passed!" + testData);
    //});

    $rootScope.$on('test', function (event, data) {
        //console.log(data); // 'Data to send'
        alertSuccess("Successfully Data Passed! " + data);
    });

    $scope.loadClassPage = function () {
        $scope.getSemesterListByFaculty();
    }

    $scope.getSemesterListByFaculty = function () {
        
        $http.get($scope.getSemesterListByFacultyUrl, {
            params: { "id": $scope.EmpId }
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
                $scope.ClassHasError = true;
                $scope.ClassErrorMsg = "Unable to get Semester list! " + result.Errors.toString();
                //alertError($scope.ErrorMsg);
            }
            
        }).error(function (result, status) {
            $scope.ClassHasError = true;
            $scope.ClassErrorMsg = "Unable to get Semester list! " + "Status: " + status.toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ClassErrorMsg);
            
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
            params: { "empId": $scope.EmpId, "semesterId": obj.Id }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.ClassList = result.Data;
                //console.log(result.DataExtra.Date);
                //console.log($scope.date);
            } else {
                $scope.ClassHasError = true;
                $scope.ClassErrorMsg = "Unable to get Assigned Class list! " + result.Errors.toString();
                //alertError($scope.ClassErrorMsg);
            }

        }).error(function (result, status) {
            $scope.ClassHasError = true;
            $scope.ClassErrorMsg = "Unable to get Assigned Class list! " + "Status: " + status.toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ClassErrorMsg);

        });
    };

    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====
    $scope.Init = function (
        empId
         ,getSemesterListByFacultyUrl
         , getClassListBySemesterIdUrl
         
        ) {
        $scope.EmpId = empId;
        $scope.getSemesterListByFacultyUrl = getSemesterListByFacultyUrl;
        $scope.getClassListBySemesterIdUrl = getClassListBySemesterIdUrl;
        //$scope.testurl = testurl;
        $scope.loadClassPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 



});



