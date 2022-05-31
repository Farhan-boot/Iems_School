
//File:BillAndPaymen Anjular  Controller
emsApp.controller('ConfirmationCtrl', function ($scope, $http, $filter) {
    $scope.ErrorMsg = "";
    $scope.HasError = false;

    $scope.loadPage = function () {
        $scope.getOnlinePayslipById();
    }

    $scope.getOnlinePayslipById = function () {
        $http.get($scope.getOnlinePayslipByIdUrl, {
            params: {
                "payslipId": $scope.PayslipId
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.Payslip = result.Data;
                log($scope.Payslip);

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Payslip ! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }

        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Payslip ! " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);

        });
    };


    //==============End Bkash Code==================

    $scope.printPayslip = function() {
        window.print();
    }

    //======Custom Variables goes hare=====
    $scope.Init = function (
        payslipId
        , getOnlinePaymentDataExtraUrl
        , getOnlinePayslipByIdUrl
    ) {
        $scope.PayslipId = payslipId;
        $scope.getOnlinePaymentDataExtraUrl = getOnlinePaymentDataExtraUrl;
        $scope.getOnlinePayslipByIdUrl = getOnlinePayslipByIdUrl;

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 



});



