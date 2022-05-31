
//File:Accounts_PaymentGatewayProgramMap List Anjular  Controller
emsApp.controller('PaymentGatewayProgramMapListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.PaymentGatewayProgramMapList = [];
    $scope.SelectedPaymentGatewayProgramMap = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
     $scope.SelectedGatewayId=0;      
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getPaymentGatewayProgramMapListExtraData();
      $scope.getPagedPaymentGatewayProgramMapList();
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedPaymentGatewayProgramMapList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedPaymentGatewayProgramMapList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedPaymentGatewayProgramMapList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedPaymentGatewayProgramMapList();
    };
    $scope.searchPaymentGatewayProgramMapList = function () {
        $scope.getPagedPaymentGatewayProgramMapList();
    };
    $scope.getPagedPaymentGatewayProgramMapList = function () {
        $scope.getPaymentGatewayProgramMapList();
    };
    /*For Search on data end*/
    $scope.getPaymentGatewayProgramMapList = function () {
        $http.get($scope.getPagedPaymentGatewayProgramMapUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
           ,"gatewayId": $scope.SelectedGatewayId      
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.PaymentGatewayProgramMapList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Payment Gateway Program Map list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Payment Gateway Program Map list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getPaymentGatewayProgramMapListExtraData= function()
    {
            $http.get($scope.getPaymentGatewayProgramMapListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                  //DropDown Option Tables
                     if(result.DataExtra.PaymentGatewayList!=null)
                       $scope.PaymentGatewayList =  result.DataExtra.PaymentGatewayList; 

                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Payment Gateway Program Map! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Payment Gateway Program Map! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllPaymentGatewayProgramMapList = function () {
        $scope.IsLoading++;
        $http.get($scope.getPaymentGatewayProgramMapListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.PaymentGatewayProgramMapList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Payment Gateway Program Map list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Payment Gateway Program Map list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deletePaymentGatewayProgramMapById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deletePaymentGatewayProgramMapByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.PaymentGatewayProgramMapList.indexOf(obj);
                        $scope.PaymentGatewayProgramMapList.splice(i, 1);
                        alertSuccess("Data successfully deleted!");
                    } else {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Sorry unable to delete! " + result.Errors.toString();
                        alertError($scope.ErrorMsg);
                    }
                }).error(function (result, status) {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Sorry unable to delete! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
                    alertError($scope.ErrorMsg);
                });
            }
        });
    };
    /*$scope.deletePaymentGatewayProgramMapByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deletePaymentGatewayProgramMapByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.PaymentGatewayProgramMapList.indexOf(obj);
                        $scope.PaymentGatewayProgramMapList.splice(i, 1);
                       alertSuccess("Data successfully deleted!");
                    } else {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Sorry unable to delete! " + result.Errors.toString();
                        alertError($scope.ErrorMsg);
                    }
                }).error(function (result, status) {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Sorry unable to delete! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                    alertError($scope.ErrorMsg);
                });
            }
        });
    };*/

    $scope.selectAll = function ($event) {
    var checkbox = $event.target;
    for (var i = 0; i < $scope.PaymentGatewayProgramMapList.length; i++) {
        var entity = $scope.PaymentGatewayProgramMapList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedPaymentGatewayProgramMapUrl
        ,deletePaymentGatewayProgramMapByIdUrl
        ,getPaymentGatewayProgramMapListDataExtraUrl
        ,savePaymentGatewayProgramMapListUrl
        ,getPaymentGatewayProgramMapByIdUrl
        ,getPaymentGatewayProgramMapDataExtraUrl
        ,savePaymentGatewayProgramMapUrl
        ) {
        $scope.getPagedPaymentGatewayProgramMapUrl = getPagedPaymentGatewayProgramMapUrl;
        $scope.deletePaymentGatewayProgramMapByIdUrl = deletePaymentGatewayProgramMapByIdUrl;
        /*bind extra url if need*/
        $scope.getPaymentGatewayProgramMapListDataExtraUrl = getPaymentGatewayProgramMapListDataExtraUrl;
        $scope.savePaymentGatewayProgramMapListUrl = savePaymentGatewayProgramMapListUrl;
        $scope.getPaymentGatewayProgramMapByIdUrl = getPaymentGatewayProgramMapByIdUrl;
        $scope.getPaymentGatewayProgramMapDataExtraUrl = getPaymentGatewayProgramMapDataExtraUrl;
        $scope.savePaymentGatewayProgramMapUrl = savePaymentGatewayProgramMapUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



