﻿
//File:Academic_GradingPolicyOption List Anjular  Controller
emsApp.controller('GradingPolicyOptionListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.GradingPolicyOptionList = [];
    $scope.SelectedGradingPolicyOption = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
     $scope.SelectedGradingPolicyId=0;      
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getGradingPolicyOptionListExtraData();
      $scope.getPagedGradingPolicyOptionList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedGradingPolicyOptionList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedGradingPolicyOptionList();
    };
    $scope.searchGradingPolicyOptionList = function () {
        $scope.getPagedGradingPolicyOptionList();
    };
    $scope.getPagedGradingPolicyOptionList = function () {
        $scope.getGradingPolicyOptionList();
    };
    /*For Search on data end*/
    $scope.getGradingPolicyOptionList = function () {
        $http.get($scope.getPagedGradingPolicyOptionUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
           ,"gradingPolicyId": $scope.SelectedGradingPolicyId      
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.GradingPolicyOptionList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Grading Policy Option list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Grading Policy Option list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getGradingPolicyOptionListExtraData= function()
    {
            $http.get($scope.getGradingPolicyOptionListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.LimitOperatorEnumList!=null)
                         $scope.LimitOperatorEnumList = result.DataExtra.LimitOperatorEnumList;
                  //DropDown Option Tables
                     if(result.DataExtra.GradingPolicyList!=null)
                       $scope.GradingPolicyList =  result.DataExtra.GradingPolicyList; 

                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Grading Policy Option! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Grading Policy Option! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllGradingPolicyOptionList = function () {
        $scope.IsLoading++;
        $http.get($scope.getGradingPolicyOptionListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.GradingPolicyOptionList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Grading Policy Option list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Grading Policy Option list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };

    $scope.deleteGradingPolicyOptionByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteGradingPolicyOptionByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.GradingPolicyOptionList.indexOf(obj);
                        $scope.GradingPolicyOptionList.splice(i, 1);
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
    };
    $scope.deleteGradingPolicyOptionById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteGradingPolicyOptionByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.GradingPolicyOptionList.indexOf(obj);
                        $scope.GradingPolicyOptionList.splice(i, 1);
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
    $scope.selectAll = function ($event) {
    var checkbox = $event.target;
    for (var i = 0; i < $scope.GradingPolicyOptionList.length; i++) {
        var entity = $scope.GradingPolicyOptionList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedGradingPolicyOptionUrl
        ,deleteGradingPolicyOptionByIdUrl
        ,getGradingPolicyOptionListDataExtraUrl
        ,saveGradingPolicyOptionListUrl
        ,getGradingPolicyOptionByIdUrl
        ,getGradingPolicyOptionDataExtraUrl
        ,saveGradingPolicyOptionUrl
        ) {
        $scope.getPagedGradingPolicyOptionUrl = getPagedGradingPolicyOptionUrl;
        $scope.deleteGradingPolicyOptionByIdUrl = deleteGradingPolicyOptionByIdUrl;
        /*bind extra url if need*/
        $scope.getGradingPolicyOptionListDataExtraUrl = getGradingPolicyOptionListDataExtraUrl;
        $scope.saveGradingPolicyOptionListUrl = saveGradingPolicyOptionListUrl;
        $scope.getGradingPolicyOptionByIdUrl = getGradingPolicyOptionByIdUrl;
        $scope.getGradingPolicyOptionDataExtraUrl = getGradingPolicyOptionDataExtraUrl;
        $scope.saveGradingPolicyOptionUrl = saveGradingPolicyOptionUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



