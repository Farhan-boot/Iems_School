
//File:HR_SalaryTemplateDetails List Anjular  Controller
emsApp.controller('SalaryTemplateDetailsListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.SalaryTemplateDetailsList = [];
    $scope.SelectedSalaryTemplateDetails = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
     $scope.SelectedParentId=0;      
     $scope.SelectedSalaryTemplateId=0;      
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getSalaryTemplateDetailsListExtraData();
      $scope.getPagedSalaryTemplateDetailsList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedSalaryTemplateDetailsList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedSalaryTemplateDetailsList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedSalaryTemplateDetailsList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedSalaryTemplateDetailsList();
    };
    $scope.searchSalaryTemplateDetailsList = function () {
        $scope.getPagedSalaryTemplateDetailsList();
    };
    $scope.getPagedSalaryTemplateDetailsList = function () {
        $scope.getSalaryTemplateDetailsList();
    };
    /*For Search on data end*/
    $scope.getSalaryTemplateDetailsList = function () {
        $http.get($scope.getPagedSalaryTemplateDetailsUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
           ,"parentId": $scope.SelectedParentId      
           ,"salaryTemplateId": $scope.SelectedSalaryTemplateId      
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.SalaryTemplateDetailsList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Salary Template Details list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Salary Template Details list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getSalaryTemplateDetailsListExtraData= function()
    {
            $http.get($scope.getSalaryTemplateDetailsListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.SalaryTypeEnumList!=null)
                         $scope.SalaryTypeEnumList = result.DataExtra.SalaryTypeEnumList;
                      if(result.DataExtra.CategoryTypeEnumList!=null)
                         $scope.CategoryTypeEnumList = result.DataExtra.CategoryTypeEnumList;
                  //DropDown Option Tables
                     if(result.DataExtra.SalaryTemplateDetailsList!=null)
                       $scope.SalaryTemplateDetailsList =  result.DataExtra.SalaryTemplateDetailsList; 

                     if(result.DataExtra.SalaryTemplateList!=null)
                       $scope.SalaryTemplateList =  result.DataExtra.SalaryTemplateList; 

                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Salary Template Details! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Salary Template Details! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllSalaryTemplateDetailsList = function () {
        $scope.IsLoading++;
        $http.get($scope.getSalaryTemplateDetailsListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.SalaryTemplateDetailsList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Salary Template Details list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Salary Template Details list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteSalaryTemplateDetailsById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteSalaryTemplateDetailsByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.SalaryTemplateDetailsList.indexOf(obj);
                        $scope.SalaryTemplateDetailsList.splice(i, 1);
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
    /*$scope.deleteSalaryTemplateDetailsByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteSalaryTemplateDetailsByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.SalaryTemplateDetailsList.indexOf(obj);
                        $scope.SalaryTemplateDetailsList.splice(i, 1);
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
    for (var i = 0; i < $scope.SalaryTemplateDetailsList.length; i++) {
        var entity = $scope.SalaryTemplateDetailsList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedSalaryTemplateDetailsUrl
        ,deleteSalaryTemplateDetailsByIdUrl
        ,getSalaryTemplateDetailsListDataExtraUrl
        ,saveSalaryTemplateDetailsListUrl
        ,getSalaryTemplateDetailsByIdUrl
        ,getSalaryTemplateDetailsDataExtraUrl
        ,saveSalaryTemplateDetailsUrl
        ) {
        $scope.getPagedSalaryTemplateDetailsUrl = getPagedSalaryTemplateDetailsUrl;
        $scope.deleteSalaryTemplateDetailsByIdUrl = deleteSalaryTemplateDetailsByIdUrl;
        /*bind extra url if need*/
        $scope.getSalaryTemplateDetailsListDataExtraUrl = getSalaryTemplateDetailsListDataExtraUrl;
        $scope.saveSalaryTemplateDetailsListUrl = saveSalaryTemplateDetailsListUrl;
        $scope.getSalaryTemplateDetailsByIdUrl = getSalaryTemplateDetailsByIdUrl;
        $scope.getSalaryTemplateDetailsDataExtraUrl = getSalaryTemplateDetailsDataExtraUrl;
        $scope.saveSalaryTemplateDetailsUrl = saveSalaryTemplateDetailsUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



