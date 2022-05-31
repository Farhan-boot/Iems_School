
//File:HR_SalaryTemplate List Anjular  Controller
emsApp.controller('SalaryTemplateListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.SalaryTemplateList = [];
    $scope.SelectedSalaryTemplate = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;

    $scope.IsShowTrashedItems = false;    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      //$scope.getSalaryTemplateListExtraData();
      $scope.getPagedSalaryTemplateList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedSalaryTemplateList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedSalaryTemplateList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedSalaryTemplateList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedSalaryTemplateList();
    };
    $scope.searchSalaryTemplateList = function () {
        $scope.getPagedSalaryTemplateList();
    };
    $scope.getPagedSalaryTemplateList = function () {
        $scope.getSalaryTemplateList();
    };
    /*For Search on data end*/
    $scope.getSalaryTemplateList = function () {
        $http.get($scope.getPagedSalaryTemplateUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
                , "isShowTrashedItems": $scope.IsShowTrashedItems
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.SalaryTemplateList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Salary Template list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Salary Template list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getSalaryTemplateListExtraData= function()
    {
            $http.get($scope.getSalaryTemplateListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                  //DropDown Option Tables
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Salary Template! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Salary Template! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllSalaryTemplateList = function () {
        $scope.IsLoading++;
        $http.get($scope.getSalaryTemplateListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.SalaryTemplateList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Salary Template list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Salary Template list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteSalaryTemplateById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteSalaryTemplateByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.SalaryTemplateList.indexOf(obj);
                        $scope.SalaryTemplateList.splice(i, 1);
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

    $scope.trashUnTrashById = function (obj, isDelete) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        var deleteUnDeleteMsg = isDelete ? "Delete" : "Un-Delete";
        bootbox.confirm("Are you sure you want to " + deleteUnDeleteMsg + " this data?", function (yes) {
            if (yes) {
                $http.get($scope.trashUnTrashByIdUrl, {
                    params: {
                        "id": obj.Id,
                        "isDelete": isDelete
                    }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.SalaryTemplateList.indexOf(obj);

                        $scope.SalaryTemplateList.splice(i, 1);
                        alertSuccess("Data successfully " + deleteUnDeleteMsg + "!");
                    } else {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Sorry unable to " + deleteUnDeleteMsg + "! " + result.Errors.toString();
                        alertError($scope.ErrorMsg);
                    }
                }).error(function (result, status) {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Sorry unable to " + deleteUnDeleteMsg + "! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
                    alertError($scope.ErrorMsg);
                });
            }
        });
    };

    /*$scope.deleteSalaryTemplateByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteSalaryTemplateByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.SalaryTemplateList.indexOf(obj);
                        $scope.SalaryTemplateList.splice(i, 1);
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
    for (var i = 0; i < $scope.SalaryTemplateList.length; i++) {
        var entity = $scope.SalaryTemplateList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedSalaryTemplateUrl
        ,deleteSalaryTemplateByIdUrl
        ,getSalaryTemplateListDataExtraUrl
        ,saveSalaryTemplateListUrl
        ,getSalaryTemplateByIdUrl
        ,getSalaryTemplateDataExtraUrl
        ,saveSalaryTemplateUrl
        , trashUnTrashByIdUrl
        ) {
        $scope.getPagedSalaryTemplateUrl = getPagedSalaryTemplateUrl;
        $scope.deleteSalaryTemplateByIdUrl = deleteSalaryTemplateByIdUrl;
        /*bind extra url if need*/
        $scope.getSalaryTemplateListDataExtraUrl = getSalaryTemplateListDataExtraUrl;
        $scope.saveSalaryTemplateListUrl = saveSalaryTemplateListUrl;
        $scope.getSalaryTemplateByIdUrl = getSalaryTemplateByIdUrl;
        $scope.getSalaryTemplateDataExtraUrl = getSalaryTemplateDataExtraUrl;
        $scope.saveSalaryTemplateUrl = saveSalaryTemplateUrl;
        $scope.trashUnTrashByIdUrl = trashUnTrashByIdUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



