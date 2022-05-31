emsApp.controller('OnlinePaymentHistoryCtrl', function ($scope, $http, $filter, cfpLoadingBar) {

    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
    $scope.LoadOnlinePaymentHisotry = false;

    $scope.loadOnlinePaymentHistory = function () {
        $scope.LoadOnlinePaymentHisotry = true;
        $scope.getOnlinePaymentHistory();
    }

    $scope.getOnlinePaymentHistory = function () {
        $http.get($scope.getOnlinePaymentHistoryUrl, {
            params: {}
        }).success(function (result) {
            if (!result.HasError) {
                $scope.StdOnlinePaySlipList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Online Payment History data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }

        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Semester list! " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);

        });
    };

    //======Custom property and Functions Start=======================================================  

    $scope.recheckPaymentStatus = function (payslip) {
        $scope.selectedPayslipId = payslip.Id;
        $http.get($scope.getRecheckPaymentStatusByIdUrl, {
            params: {
                "payslipId": $scope.selectedPayslipId,
                "gateway": null
            }
        }).success(function (result, status) {
            $scope.trnxId = "";
            //$("#PaymentRecheckModal").modal('hide');
            if (!result.HasError) {
                $scope.GatewayResponse = result.Data;
                alertSuccess("Payment Status Updated Successfully.");
                //$("#PaymentConfirmationModal").modal('show');
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
            if (result.DataExtra.Payslip != null) {
                var payment = $scope.StdOnlinePaySlipList.filter(x => x.Id == result.DataExtra.Payslip.Id);
                if (payment != null) {
                    payment[0].TransStatus = result.DataExtra.Payslip.TransStatus;
                    payment[0].TransStatusEnumId = result.DataExtra.Payslip.TransStatusEnumId;
                    payment[0].IsPaid = result.DataExtra.Payslip.IsPaid;
                    payment[0].GatewayResponseTime = result.DataExtra.Payslip.GatewayResponseTime;
                }
            }
        }).error(function (result, status) {
            $("#PaymentRecheckModal").modal('hide');
            $scope.trnxId = "";
            $scope.HasError = true;
            $scope.ErrorMsg = JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    }


    //======Custom Variabales goes hare=====

    $scope.Init = function (
        getOnlinePaymentHistoryUrl
        , getRecheckPaymentStatusByIdUrl
    ) {
        $scope.getOnlinePaymentHistoryUrl = getOnlinePaymentHistoryUrl;
        $scope.getRecheckPaymentStatusByIdUrl = getRecheckPaymentStatusByIdUrl;
    };

});