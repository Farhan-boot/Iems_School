
//File:HR_SalarySettings List Anjular  Controller
emsApp.controller('SalarySettingsListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.SalarySettingsList = [];
    $scope.SelectedSalarySettings = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
     $scope.SelectedEmployeeId=0;      
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getSalarySettingsListExtraData();
      $scope.getPagedSalarySettingsList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedSalarySettingsList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedSalarySettingsList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedSalarySettingsList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedSalarySettingsList();
    };
    $scope.searchSalarySettingsList = function () {
        $scope.getPagedSalarySettingsList();
    };
    $scope.getPagedSalarySettingsList = function () {
        $scope.getSalarySettingsList();
    };
    /*For Search on data end*/
    $scope.getSalarySettingsList = function () {
        $http.get($scope.getPagedSalarySettingsUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
           ,"employeeId": $scope.SelectedEmployeeId      
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.SalarySettingsList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Salary Settings list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Salary Settings list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getSalarySettingsListExtraData= function()
    {
            $http.get($scope.getSalarySettingsListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.SalaryTypeEnumList!=null)
                         $scope.SalaryTypeEnumList = result.DataExtra.SalaryTypeEnumList;
                  //DropDown Option Tables
                     if(result.DataExtra.EmployeeList!=null)
                       $scope.EmployeeList =  result.DataExtra.EmployeeList; 

                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Salary Settings! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Salary Settings! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllSalarySettingsList = function () {
        $scope.IsLoading++;
        $http.get($scope.getSalarySettingsListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.SalarySettingsList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Salary Settings list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Salary Settings list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteSalarySettingsById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteSalarySettingsByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.SalarySettingsList.indexOf(obj);
                        $scope.SalarySettingsList.splice(i, 1);
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
    /*$scope.deleteSalarySettingsByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteSalarySettingsByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.SalarySettingsList.indexOf(obj);
                        $scope.SalarySettingsList.splice(i, 1);
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
    for (var i = 0; i < $scope.SalarySettingsList.length; i++) {
        var entity = $scope.SalarySettingsList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedSalarySettingsUrl
        ,deleteSalarySettingsByIdUrl
        ,getSalarySettingsListDataExtraUrl
        ,saveSalarySettingsListUrl
        ,getSalarySettingsByIdUrl
        ,getSalarySettingsDataExtraUrl
        ,saveSalarySettingsUrl
        ) {
        $scope.getPagedSalarySettingsUrl = getPagedSalarySettingsUrl;
        $scope.deleteSalarySettingsByIdUrl = deleteSalarySettingsByIdUrl;
        /*bind extra url if need*/
        $scope.getSalarySettingsListDataExtraUrl = getSalarySettingsListDataExtraUrl;
        $scope.saveSalarySettingsListUrl = saveSalarySettingsListUrl;
        $scope.getSalarySettingsByIdUrl = getSalarySettingsByIdUrl;
        $scope.getSalarySettingsDataExtraUrl = getSalarySettingsDataExtraUrl;
        $scope.saveSalarySettingsUrl = saveSalarySettingsUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



