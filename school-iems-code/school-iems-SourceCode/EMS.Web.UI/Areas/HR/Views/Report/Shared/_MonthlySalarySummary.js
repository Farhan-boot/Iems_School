
//File:Accounts_AdmissionSummaryReport List Anjular  Controller
emsApp.controller('MonthlySalarySummaryReportCtrl', function ($scope, $http, $filter, cfpLoadingBar) {

    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;

    $scope.SelectedPayrollId = 0;

    $scope.SelectedPayroll = null;

    $scope.Report = null;


    $scope.loadPage = function () {
        $scope.getMonthlySalarySummaryReportExtraDataExtra();
    };

    $scope.onChangeFilter = function () {
        $scope.getMonthlySalarySummaryReport();
    };

    $scope.getMonthlySalarySummaryReportExtraDataExtra = function () {
        $http.get($scope.getMonthlySalarySummaryReportExtraUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;

                //DropDown Option Tables
                if (result.DataExtra.PayrollList != null) {
                    $scope.PayrollList = result.DataExtra.PayrollList;

                    if (result.DataExtra.SelectedPayrollId != null) {
                        var selectedPayroll = $scope.PayrollList.filter(x => x.Id === result.DataExtra.SelectedPayrollId, true);
                        $scope.SelectedPayroll = selectedPayroll[0];
                    }
                }
                
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Monthly Salary Summary! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Monthly Salary Summary! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.getMonthlySalarySummaryReport = function () {
        $scope.SelectedPayrollId = 0;

        if ($scope.SelectedPayroll != null)
            $scope.SelectedPayrollId = $scope.SelectedPayroll.Id;

        $http.get($scope.getMonthlySalarySummaryReportUrl, {
            params: {
                "payrollId": $scope.SelectedPayrollId
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.Report = result.Data;

                log(result.Data);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable Get Monthly Salary Summary Report! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
                $scope.PartWiseBillingDtlList = [];
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.PartWiseBillingDtlList = [];
            $scope.ErrorMsg = "Unable Get Monthly Salary Summary Report! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };

    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====


    $scope.Init = function (
        getMonthlySalarySummaryReportExtraUrl
        , getMonthlySalarySummaryReportUrl
    ) {
        $scope.getMonthlySalarySummaryReportExtraUrl = getMonthlySalarySummaryReportExtraUrl;
        $scope.getMonthlySalarySummaryReportUrl = getMonthlySalarySummaryReportUrl;
        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



