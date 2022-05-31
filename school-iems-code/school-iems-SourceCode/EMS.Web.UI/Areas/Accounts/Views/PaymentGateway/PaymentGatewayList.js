
//File:Accounts_PaymentGateway List Anjular  Controller
emsApp.controller('PaymentGatewayListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.PaymentGatewayList = [];
    $scope.SelectedPaymentGateway = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
     $scope.SelectedBankId=0;      
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getPaymentGatewayListExtraData();
      $scope.getPagedPaymentGatewayList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedPaymentGatewayList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedPaymentGatewayList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedPaymentGatewayList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedPaymentGatewayList();
    };
    $scope.searchPaymentGatewayList = function () {
        $scope.getPagedPaymentGatewayList();
    };
    $scope.getPagedPaymentGatewayList = function () {
        $scope.getPaymentGatewayList();
    };
    /*For Search on data end*/
    $scope.getPaymentGatewayList = function () {
        $http.get($scope.getPagedPaymentGatewayUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
           ,"bankId": $scope.SelectedBankId      
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.PaymentGatewayList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Payment Gateway list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Payment Gateway list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getPaymentGatewayListExtraData= function()
    {
            $http.get($scope.getPaymentGatewayListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.ActiveStatusEnumList!=null)
                         $scope.ActiveStatusEnumList = result.DataExtra.ActiveStatusEnumList;
                      if(result.DataExtra.TypeEnumList!=null)
                         $scope.TypeEnumList = result.DataExtra.TypeEnumList;
                  //DropDown Option Tables
                     if(result.DataExtra.OfficialBankList!=null)
                       $scope.OfficialBankList =  result.DataExtra.OfficialBankList; 

                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Payment Gateway! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Payment Gateway! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllPaymentGatewayList = function () {
        $scope.IsLoading++;
        $http.get($scope.getPaymentGatewayListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.PaymentGatewayList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Payment Gateway list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Payment Gateway list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deletePaymentGatewayById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deletePaymentGatewayByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.PaymentGatewayList.indexOf(obj);
                        $scope.PaymentGatewayList.splice(i, 1);
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
    /*$scope.deletePaymentGatewayByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deletePaymentGatewayByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.PaymentGatewayList.indexOf(obj);
                        $scope.PaymentGatewayList.splice(i, 1);
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
    for (var i = 0; i < $scope.PaymentGatewayList.length; i++) {
        var entity = $scope.PaymentGatewayList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedPaymentGatewayUrl
        ,deletePaymentGatewayByIdUrl
        ,getPaymentGatewayListDataExtraUrl
        ,savePaymentGatewayListUrl
        ,getPaymentGatewayByIdUrl
        ,getPaymentGatewayDataExtraUrl
        ,savePaymentGatewayUrl
        ) {
        $scope.getPagedPaymentGatewayUrl = getPagedPaymentGatewayUrl;
        $scope.deletePaymentGatewayByIdUrl = deletePaymentGatewayByIdUrl;
        /*bind extra url if need*/
        $scope.getPaymentGatewayListDataExtraUrl = getPaymentGatewayListDataExtraUrl;
        $scope.savePaymentGatewayListUrl = savePaymentGatewayListUrl;
        $scope.getPaymentGatewayByIdUrl = getPaymentGatewayByIdUrl;
        $scope.getPaymentGatewayDataExtraUrl = getPaymentGatewayDataExtraUrl;
        $scope.savePaymentGatewayUrl = savePaymentGatewayUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



