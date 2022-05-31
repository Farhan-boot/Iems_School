
//File:HR_SalarySettingsDetails List Anjular  Controller
emsApp.controller('SalarySettingsDetailsListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.SalarySettingsDetailsList = [];
    $scope.SelectedSalarySettingsDetails = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
     $scope.SelectedSalarySettingsId=0;      
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getSalarySettingsDetailsListExtraData();
      $scope.getPagedSalarySettingsDetailsList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedSalarySettingsDetailsList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedSalarySettingsDetailsList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedSalarySettingsDetailsList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedSalarySettingsDetailsList();
    };
    $scope.searchSalarySettingsDetailsList = function () {
        $scope.getPagedSalarySettingsDetailsList();
    };
    $scope.getPagedSalarySettingsDetailsList = function () {
        $scope.getSalarySettingsDetailsList();
    };
    /*For Search on data end*/
    $scope.getSalarySettingsDetailsList = function () {
        $http.get($scope.getPagedSalarySettingsDetailsUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
           ,"salarySettingsId": $scope.SelectedSalarySettingsId      
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.SalarySettingsDetailsList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Salary Settings Details list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Salary Settings Details list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getSalarySettingsDetailsListExtraData= function()
    {
            $http.get($scope.getSalarySettingsDetailsListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.SalaryTypeEnumList!=null)
                         $scope.SalaryTypeEnumList = result.DataExtra.SalaryTypeEnumList;
                      if(result.DataExtra.CategoryTypeEnumList!=null)
                         $scope.CategoryTypeEnumList = result.DataExtra.CategoryTypeEnumList;
                  //DropDown Option Tables
                     if(result.DataExtra.SalarySettingsList!=null)
                       $scope.SalarySettingsList =  result.DataExtra.SalarySettingsList; 

                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Salary Settings Details! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Salary Settings Details! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllSalarySettingsDetailsList = function () {
        $scope.IsLoading++;
        $http.get($scope.getSalarySettingsDetailsListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.SalarySettingsDetailsList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Salary Settings Details list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Salary Settings Details list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteSalarySettingsDetailsById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteSalarySettingsDetailsByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.SalarySettingsDetailsList.indexOf(obj);
                        $scope.SalarySettingsDetailsList.splice(i, 1);
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
    /*$scope.deleteSalarySettingsDetailsByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteSalarySettingsDetailsByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.SalarySettingsDetailsList.indexOf(obj);
                        $scope.SalarySettingsDetailsList.splice(i, 1);
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
    for (var i = 0; i < $scope.SalarySettingsDetailsList.length; i++) {
        var entity = $scope.SalarySettingsDetailsList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedSalarySettingsDetailsUrl
        ,deleteSalarySettingsDetailsByIdUrl
        ,getSalarySettingsDetailsListDataExtraUrl
        ,saveSalarySettingsDetailsListUrl
        ,getSalarySettingsDetailsByIdUrl
        ,getSalarySettingsDetailsDataExtraUrl
        ,saveSalarySettingsDetailsUrl
        ) {
        $scope.getPagedSalarySettingsDetailsUrl = getPagedSalarySettingsDetailsUrl;
        $scope.deleteSalarySettingsDetailsByIdUrl = deleteSalarySettingsDetailsByIdUrl;
        /*bind extra url if need*/
        $scope.getSalarySettingsDetailsListDataExtraUrl = getSalarySettingsDetailsListDataExtraUrl;
        $scope.saveSalarySettingsDetailsListUrl = saveSalarySettingsDetailsListUrl;
        $scope.getSalarySettingsDetailsByIdUrl = getSalarySettingsDetailsByIdUrl;
        $scope.getSalarySettingsDetailsDataExtraUrl = getSalarySettingsDetailsDataExtraUrl;
        $scope.saveSalarySettingsDetailsUrl = saveSalarySettingsDetailsUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



