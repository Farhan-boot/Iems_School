
//File:Accounts_AdmissionSummaryReport List Anjular  Controller
emsApp.controller('PreviousEducationCtrl', function ($scope, $http, $filter, cfpLoadingBar) {

    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;

    $scope.SelectedProgramId = 0;
    $scope.SelectedSemesterId = 0;

    $scope.SelectedProgram = null;
    $scope.SelectedSemester = null;

    $scope.StudentList = [];

    $scope.loadPage = function () {
        $scope.getPreviousEducationDataExtra();

    };

     $scope.onChangeFilter = function () {
         $scope.getPreviousEducation();
     };

     $scope.getPreviousEducationDataExtra = function () {
        $http.get($scope.getPreviousEducationDataExtraUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;

                //DropDown Option Tables
                if (result.DataExtra.ProgramList != null)
                    $scope.ProgramList = result.DataExtra.ProgramList;
                //log($scope.ProgramList);
                if (result.DataExtra.SemesterList != null) {
                    $scope.SemesterList = result.DataExtra.SemesterList;

                    $scope.SelectedSemesterId = $scope.SemesterList[0].Id;
                    $scope.SelectedSemester = $scope.SemesterList[0];
                }
                    

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Previous Education Report! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Previous Education Report! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.getPreviousEducation = function () {
        $scope.SelectedProgramId = 0;
        $scope.SelectedSemesterId = 0;

        if ($scope.SelectedProgram != null)
            $scope.SelectedProgramId = $scope.SelectedProgram.Id;

        if ($scope.SelectedSemester != null)
            $scope.SelectedSemesterId = $scope.SelectedSemester.Id;

        $http.get($scope.getPreviousEducationUrl, {
            params: {
                "programId": $scope.SelectedProgramId,
                "semesterId": $scope.SelectedSemesterId,
                "endDate": $scope.EndDate
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;

                $scope.StudentList = result.Data;

                log(result.Data);

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable Get Previous Education Report Report! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
                $scope.Report = [];
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable Get Previous Education Report Report! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };

    //======Custom property and Functions Start=======================================================  


    //======Custom Variables goes hare=====


    $scope.Init = function (
        getPreviousEducationDataExtraUrl
        , getPreviousEducationUrl
        ) {
        $scope.getPreviousEducationDataExtraUrl = getPreviousEducationDataExtraUrl;
        $scope.getPreviousEducationUrl = getPreviousEducationUrl;
        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



