
//File:Accounts_AdmissionSummaryReport List Anjular  Controller
emsApp.controller('MonthlySalaryDetailsReportCtrl', function ($scope, $http, $filter, cfpLoadingBar) {

    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
    $scope.IsShowJoiningDate = false;
    $scope.IsShowLengthOfService = false;
    $scope.IsShowNextPromotionDate = false;

    $scope.SelectedPayrollId = 0;
    $scope.SelectedSalaryTemplateId = 0;

    $scope.SelectedPayroll = null;
    $scope.SelectedSalaryTemplate = null;

    $scope.Report = null;


    $scope.loadPage = function () {
        $scope.getMonthlySalaryDetailsReportExtraDataExtra();
    };

    $scope.onChangeFilter = function () {
        $scope.getMonthlySalaryDetailsReport();
    };

    $scope.getMonthlySalaryDetailsReportExtraDataExtra = function () {
        $http.get($scope.getMonthlySalaryDetailsReportExtraUrl, {
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

                if (result.DataExtra.SalaryTemplateList != null)
                    $scope.SalaryTemplateList = result.DataExtra.SalaryTemplateList;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Monthly Salary Details! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Monthly Salary Details! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.getMonthlySalaryDetailsReport = function () {
        $scope.SelectedPayrollId = 0;
        $scope.SelectedSalaryTemplateId = 0;

        if ($scope.SelectedPayroll != null)
            $scope.SelectedPayrollId = $scope.SelectedPayroll.Id;

        if ($scope.SelectedSalaryTemplate != null)
            $scope.SelectedSalaryTemplateId = $scope.SelectedSalaryTemplate.Id;

        $http.get($scope.getMonthlySalaryDetailsReportUrl, {
            params: {
                "payrollId": $scope.SelectedPayrollId,
                "salaryTemplateId": $scope.SelectedSalaryTemplateId
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.Report = result.Data;

                log(result.Data);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable Get Monthly Salary Details Report! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
                $scope.PartWiseBillingDtlList = [];
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.PartWiseBillingDtlList = [];
            $scope.ErrorMsg = "Unable Get Monthly Salary Details Report! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };

    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====


    $scope.Init = function (
        getMonthlySalaryDetailsReportExtraUrl
        , getMonthlySalaryDetailsReportUrl
    ) {
        $scope.getMonthlySalaryDetailsReportExtraUrl = getMonthlySalaryDetailsReportExtraUrl;
        $scope.getMonthlySalaryDetailsReportUrl = getMonthlySalaryDetailsReportUrl;
        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



