
//File:Academic_CreditTransferInstitute List Anjular  Controller
emsApp.controller('CreditTransferInstituteListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.CreditTransferInstituteList = [];
    $scope.SelectedCreditTransferInstitute = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getCreditTransferInstituteListExtraData();
      $scope.getPagedCreditTransferInstituteList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedCreditTransferInstituteList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedCreditTransferInstituteList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedCreditTransferInstituteList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedCreditTransferInstituteList();
    };
    $scope.searchCreditTransferInstituteList = function () {
        $scope.getPagedCreditTransferInstituteList();
    };
    $scope.getPagedCreditTransferInstituteList = function () {
        $scope.getCreditTransferInstituteList();
    };
    /*For Search on data end*/
    $scope.getCreditTransferInstituteList = function () {
        $http.get($scope.getPagedCreditTransferInstituteUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.CreditTransferInstituteList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Credit Transfer Institute list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Credit Transfer Institute list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getCreditTransferInstituteListExtraData= function()
    {
            $http.get($scope.getCreditTransferInstituteListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                  //DropDown Option Tables
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Credit Transfer Institute! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Credit Transfer Institute! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllCreditTransferInstituteList = function () {
        $scope.IsLoading++;
        $http.get($scope.getCreditTransferInstituteListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.CreditTransferInstituteList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Credit Transfer Institute list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Credit Transfer Institute list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteCreditTransferInstituteById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteCreditTransferInstituteByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.CreditTransferInstituteList.indexOf(obj);
                        $scope.CreditTransferInstituteList.splice(i, 1);
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
    /*$scope.deleteCreditTransferInstituteByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteCreditTransferInstituteByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.CreditTransferInstituteList.indexOf(obj);
                        $scope.CreditTransferInstituteList.splice(i, 1);
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
    for (var i = 0; i < $scope.CreditTransferInstituteList.length; i++) {
        var entity = $scope.CreditTransferInstituteList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedCreditTransferInstituteUrl
        ,deleteCreditTransferInstituteByIdUrl
        ,getCreditTransferInstituteListDataExtraUrl
        ,saveCreditTransferInstituteListUrl
        ,getCreditTransferInstituteByIdUrl
        ,getCreditTransferInstituteDataExtraUrl
        ,saveCreditTransferInstituteUrl
        ) {
        $scope.getPagedCreditTransferInstituteUrl = getPagedCreditTransferInstituteUrl;
        $scope.deleteCreditTransferInstituteByIdUrl = deleteCreditTransferInstituteByIdUrl;
        /*bind extra url if need*/
        $scope.getCreditTransferInstituteListDataExtraUrl = getCreditTransferInstituteListDataExtraUrl;
        $scope.saveCreditTransferInstituteListUrl = saveCreditTransferInstituteListUrl;
        $scope.getCreditTransferInstituteByIdUrl = getCreditTransferInstituteByIdUrl;
        $scope.getCreditTransferInstituteDataExtraUrl = getCreditTransferInstituteDataExtraUrl;
        $scope.saveCreditTransferInstituteUrl = saveCreditTransferInstituteUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



