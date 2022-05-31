
//File:Accounts_AdmissionSummaryReport List Anjular  Controller
emsApp.controller('YearlySalarySummaryReportCtrl', function ($scope, $http, $filter, cfpLoadingBar) {

    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;


    $scope.startPayroll = null;
    $scope.endPayroll = null;


    $scope.Report = null;


    $scope.loadPage = function () {
        $scope.getYearlySalarySummaryReportExtraDataExtra();
    };

    $scope.onChangeFilter = function () {
        $scope.getYearlySalarySummaryReport();
    };

    $scope.getYearlySalarySummaryReportExtraDataExtra = function () {
        $http.get($scope.getYearlySalarySummaryReportExtraUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;

                //DropDown Option Tables
                if (result.DataExtra.PayrollList != null) {
                    $scope.PayrollList = result.DataExtra.PayrollList;
                }
                
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Yearly Salary Summary! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Yearly Salary Summary! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.getYearlySalarySummaryReport = function () {

        if ($scope.startPayroll == null || $scope.endPayroll == null) {
            alertError("Please Select Both Payroll First");
            return;
        }

        $http.get($scope.getYearlySalarySummaryReportUrl, {
            params: {
                "startPayrollId": $scope.startPayroll.Id,
                "endPayrollId": $scope.endPayroll.Id
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.Report = result.Data;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable Get Yearly Salary Summary Report! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
                $scope.Report = null;
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.PartWiseBillingDtlList = [];
            $scope.ErrorMsg = "Unable Get Yearly Salary Summary Report! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };

    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====


    $scope.Init = function (
        getYearlySalarySummaryReportExtraUrl
        , getYearlySalarySummaryReportUrl
    ) {
        $scope.getYearlySalarySummaryReportExtraUrl = getYearlySalarySummaryReportExtraUrl;
        $scope.getYearlySalarySummaryReportUrl = getYearlySalarySummaryReportUrl;
        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



