
//File:ExamSection_ProgramWiseGradeSummarySheetApiController List Anjular  Controller
emsApp.controller('ProgramWiseGradeSummaryCtrl', function ($scope, $http, $filter, cfpLoadingBar) {

    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;

    $scope.SelectedProgramId = 0;
    $scope.SelectedSemesterId = 0;


    $scope.SelectedProgram = null;
    $scope.SelectedSemester = null;

    $scope.ProgramWiseGradeSummaryList = [];

    $scope.loadPage = function () {
        $scope.getProgramWiseGradeSummarySheetDataExtra();
    };

    $scope.onChangeFilter = function () {
        $scope.ProgramWiseGradeSummaryList = [];
        //$scope.getProgramWiseGradeSummarySheet();
    };

    $scope.getProgramWiseGradeSummarySheetDataExtra = function () {
        $http.get($scope.getProgramWiseGradeSummarySheetDataExtraUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;

                //DropDown Option Tables
                if (result.DataExtra.ProgramList != null)
                    $scope.ProgramList = result.DataExtra.ProgramList;

                if (result.DataExtra.SemesterList != null)
                    $scope.SemesterList = result.DataExtra.SemesterList;

                $scope.SelectedSemester = $scope.SemesterList[0];

                if (result.DataExtra.GradingPolicyOptionList != null)
                    $scope.GradingPolicyOptionList = result.DataExtra.GradingPolicyOptionList;

                log($scope.GradingPolicyOptionList);


            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Program Wise Grade Summary Sheet! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Program Wise Grade Summary Sheet! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.getProgramWiseGradeSummarySheet = function () {
        $scope.SelectedProgramId = 0;
        $scope.SelectedSemesterId = 0;


        if ($scope.SelectedProgram != null)
            $scope.SelectedProgramId = $scope.SelectedProgram.Id;

        if ($scope.SelectedSemester != null)
            $scope.SelectedSemesterId = $scope.SelectedSemester.Id;


        $http.get($scope.getProgramWiseGradeSummarySheetUrl, {
            params: {
                "programId": $scope.SelectedProgramId,
                "semesterId": $scope.SelectedSemesterId
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.ProgramWiseGradeSummaryList = result.Data;
                $scope.GrandTotalGradeSummary = result.DataExtra.GrandTotalGradeSummary;
                
                log($scope.GrandTotalGradeSummary);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable Get Program Wise Grade Summary Sheet! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
                $scope.ProgramWiseGradeSummaryList = [];
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ProgramWiseGradeSummaryList = [];
            $scope.ErrorMsg = "Unable Get Program Wise Grade Summary Sheet! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };

    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====


    $scope.Init = function (
        getProgramWiseGradeSummarySheetDataExtraUrl
        , getProgramWiseGradeSummarySheetUrl
        ) {
        $scope.getProgramWiseGradeSummarySheetDataExtraUrl = getProgramWiseGradeSummarySheetDataExtraUrl;

        $scope.getProgramWiseGradeSummarySheetUrl = getProgramWiseGradeSummarySheetUrl;
        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



