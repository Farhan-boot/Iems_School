
//File:Inventory_RequisitionDetails List Anjular  Controller
emsApp.controller('RequisitionDetailsListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.RequisitionDetailsList = [];
    $scope.SelectedRequisitionDetails = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
     $scope.SelectedRequisitionId=0;      
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getRequisitionDetailsListExtraData();
      $scope.getPagedRequisitionDetailsList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedRequisitionDetailsList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedRequisitionDetailsList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedRequisitionDetailsList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedRequisitionDetailsList();
    };
    $scope.searchRequisitionDetailsList = function () {
        $scope.getPagedRequisitionDetailsList();
    };
    $scope.getPagedRequisitionDetailsList = function () {
        $scope.getRequisitionDetailsList();
    };
    /*For Search on data end*/
    $scope.getRequisitionDetailsList = function () {
        $http.get($scope.getPagedRequisitionDetailsUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
           ,"requisitionId": $scope.SelectedRequisitionId      
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.RequisitionDetailsList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Requisition Details list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Requisition Details list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getRequisitionDetailsListExtraData= function()
    {
            $http.get($scope.getRequisitionDetailsListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                  //DropDown Option Tables
                     if(result.DataExtra.RequisitionList!=null)
                       $scope.RequisitionList =  result.DataExtra.RequisitionList; 

                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Requisition Details! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Requisition Details! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllRequisitionDetailsList = function () {
        $scope.IsLoading++;
        $http.get($scope.getRequisitionDetailsListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.RequisitionDetailsList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Requisition Details list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Requisition Details list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteRequisitionDetailsById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteRequisitionDetailsByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.RequisitionDetailsList.indexOf(obj);
                        $scope.RequisitionDetailsList.splice(i, 1);
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
    /*$scope.deleteRequisitionDetailsByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteRequisitionDetailsByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.RequisitionDetailsList.indexOf(obj);
                        $scope.RequisitionDetailsList.splice(i, 1);
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
    for (var i = 0; i < $scope.RequisitionDetailsList.length; i++) {
        var entity = $scope.RequisitionDetailsList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedRequisitionDetailsUrl
        ,deleteRequisitionDetailsByIdUrl
        ,getRequisitionDetailsListDataExtraUrl
        ,saveRequisitionDetailsListUrl
        ,getRequisitionDetailsByIdUrl
        ,getRequisitionDetailsDataExtraUrl
        ,saveRequisitionDetailsUrl
        ) {
        $scope.getPagedRequisitionDetailsUrl = getPagedRequisitionDetailsUrl;
        $scope.deleteRequisitionDetailsByIdUrl = deleteRequisitionDetailsByIdUrl;
        /*bind extra url if need*/
        $scope.getRequisitionDetailsListDataExtraUrl = getRequisitionDetailsListDataExtraUrl;
        $scope.saveRequisitionDetailsListUrl = saveRequisitionDetailsListUrl;
        $scope.getRequisitionDetailsByIdUrl = getRequisitionDetailsByIdUrl;
        $scope.getRequisitionDetailsDataExtraUrl = getRequisitionDetailsDataExtraUrl;
        $scope.saveRequisitionDetailsUrl = saveRequisitionDetailsUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



