
//File: BAcnt_Voucher Anjular  Controller
emsApp.controller('LedgerTransactionCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.VoucherList = [];
    $scope.LedgerId = 0;
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;

    $scope.TotalVoucherItem = 0;

    // Report Parameters
    $scope.IncludeNarration = false;
    $scope.IncludeDetails = false;
    $scope.IncludeCodeName  = false;
     




    $scope.loadPage = function (LedgerId) {
        if (LedgerId != null)
            $scope.LedgerId = LedgerId;

        $scope.loadLedgerTransactionDataExtra($scope.LedgerId);
        //$scope.getTransactionListByLedgerId($scope.LedgerId);
    };

    $scope.getTransactionListByLedgerId = function (LedgerId) {
        /*if (LedgerId != null && LedgerId !== '')
            $scope.LedgerId = LedgerId;
        else return;*/

        $http.get($scope.getTransactionListByLedgerIdUrl, {
            params: {
                "id": $scope.LedgerId,
                "branchId":$scope.BranchId,
                "startDate":$scope.StartDate,
                "endDate":$scope.EndDate
            }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.LedgerTrans = result.Data;

                $scope.TotalVoucherItem = result.DataExtra.TotalVoucherItem;
                
                log($scope.LedgerTrans);
                //updateUrlQuery('id', $scope.Voucher.Id);

                

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Ledger Transactions! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Ledger Transactions! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.loadLedgerTransactionDataExtra = function (LedgerId) {
        $http.get($scope.getLedgerTransactionDataExtraUrl)
            .success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;

                if (result.DataExtra.BranchList != null)
                    $scope.BranchList = result.DataExtra.BranchList;

                $scope.StartDate = result.DataExtra.StartDate;
                $scope.EndDate = result.DataExtra.EndDate;

                $scope.getTransactionListByLedgerId($scope.LedgerId);


            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Ledger Transactions! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Ledger Transactions! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.collapseIconToggle= function () {
        //Fixing jQuery Click Events for the iPad
        var ua = navigator.userAgent,
            event = (ua.match(/iPad/i)) ? "touchstart" : "click";
        if ($('.table').length > 0) {
            $('.table .header').on(event, function () {
                $(this).toggleClass("active", "").nextUntil('.header');
            });
        }
    }

    $scope.collapsedTable = function (index) {
        if ($scope.TotalVoucherItem === index) {
            setTimeout(function () {
                $scope.collapseIconToggle();
            }, 100);
        }
    }
    

    //======Custom property and Functions Start=======================================================  








    //======Custom Variabales goes hare=====

    $scope.Init = function (LedgerId,
        getTransactionListByLedgerIdUrl
    , getLedgerTransactionDataExtraUrl

    ) {
        $scope.LedgerId = LedgerId;
        $scope.getTransactionListByLedgerIdUrl = getTransactionListByLedgerIdUrl;
        $scope.getLedgerTransactionDataExtraUrl = getLedgerTransactionDataExtraUrl;
        

        $scope.loadPage(LedgerId);
    };




    //======Other tabale's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 
});

