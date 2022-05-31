
//File:BillAndPaymen Anjular  Controller
emsApp.controller('BillAndPaymentCtrl', function ($scope, $http, $filter) {
    $scope.ErrorMsg = "";
    $scope.HasError = false;

    $scope.loadPage = function () {
        $scope.getSemesterBill();
    }

    $scope.getSemesterBill = function () {
        $http.get($scope.getSemesterBillUrl, {
            params: {}
        }).success(function (result) {
            if (!result.HasError) {
                $scope.SemesterBill = result.Data;
                log($scope.SemesterBill);

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Semester Bill! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }

        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Semester Bill! " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);

        });
    };





    //======Custom property and Functions Start=======================================================  
    $scope.Amount = 0;
    $scope.bKashPaymentModalShow = function (amount) {
        
        $scope.Amount = amount;
        $('#bKashPaymentModal').modal('show');
    };

    $scope.verificationCodeModalShow = function () {
        $('#bKashPaymentModal').modal('hide');
        $('#verificationCodeModal').modal('show');
    };

    $scope.pinModalShow = function () {
        $('#verificationCodeModal').modal('hide');
        $('#pinModal').modal('show');
    };

    $scope.paymentSuccess = function () {
        $('#pinModal').modal('hide');
        alertSuccess("Payment Successful.");

    };



    //======Custom Variables goes hare=====
    $scope.Init = function (
        getSemesterBillUrl
    ) {
        $scope.getSemesterBillUrl = getSemesterBillUrl;
        //$scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 



});



