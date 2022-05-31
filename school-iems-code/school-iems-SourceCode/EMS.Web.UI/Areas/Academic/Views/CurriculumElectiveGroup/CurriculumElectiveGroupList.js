
//File:Academic_CurriculumElectiveGroup List Anjular  Controller
emsApp.controller('CurriculumElectiveGroupListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.CurriculumElectiveGroupList = [];
    $scope.SelectedCurriculumElectiveGroup = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
     $scope.SelectedProgramId=0;      
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
        $scope.getCurriculumElectiveGroupListExtraData();
        $scope.getPagedCurriculumElectiveGroupList();
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedCurriculumElectiveGroupList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedCurriculumElectiveGroupList();
    };
    $scope.searchCurriculumElectiveGroupList = function () {
        $scope.getPagedCurriculumElectiveGroupList();
    };
    $scope.getPagedCurriculumElectiveGroupList = function () {
        $scope.getCurriculumElectiveGroupList();
    };
    /*For Search on data end*/
    $scope.getCurriculumElectiveGroupList = function () {
        $http.get($scope.getPagedCurriculumElectiveGroupUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
           ,"programId": $scope.SelectedProgramId      
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.CurriculumElectiveGroupList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Curriculum Elective Group list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Curriculum Elective Group list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getCurriculumElectiveGroupListExtraData= function()
    {
        $http.get($scope.getCurriculumElectiveGroupListDataExtraUrl)
            .success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                    //DropDown Option Tables
                   if (result.DataExtra.ProgramList != null)
                       $scope.ProgramList = result.DataExtra.ProgramList;
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Curriculum Elective Group! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Curriculum Elective Group! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllCurriculumElectiveGroupList = function () {
        $scope.IsLoading++;
        $http.get($scope.getCurriculumElectiveGroupListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.CurriculumElectiveGroupList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Curriculum Elective Group list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Curriculum Elective Group list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };

    $scope.deleteCurriculumElectiveGroupByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteCurriculumElectiveGroupByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.CurriculumElectiveGroupList.indexOf(obj);
                        $scope.CurriculumElectiveGroupList.splice(i, 1);
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
    $scope.deleteCurriculumElectiveGroupById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteCurriculumElectiveGroupByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.CurriculumElectiveGroupList.indexOf(obj);
                        $scope.CurriculumElectiveGroupList.splice(i, 1);
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
    for (var i = 0; i < $scope.CurriculumElectiveGroupList.length; i++) {
        var entity = $scope.CurriculumElectiveGroupList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedCurriculumElectiveGroupUrl
        ,deleteCurriculumElectiveGroupByIdUrl
        ,getCurriculumElectiveGroupListDataExtraUrl
        ,saveCurriculumElectiveGroupListUrl
        ,getCurriculumElectiveGroupByIdUrl
        ,getCurriculumElectiveGroupDataExtraUrl
        ,saveCurriculumElectiveGroupUrl
        ) {
        $scope.getPagedCurriculumElectiveGroupUrl = getPagedCurriculumElectiveGroupUrl;
        $scope.deleteCurriculumElectiveGroupByIdUrl = deleteCurriculumElectiveGroupByIdUrl;
        $scope.getCurriculumElectiveGroupListDataExtraUrl = getCurriculumElectiveGroupListDataExtraUrl;
        /*bind extra url if need
        $scope.saveCurriculumElectiveGroupListUrl = saveCurriculumElectiveGroupListUrl;
        $scope.getCurriculumElectiveGroupByIdUrl = getCurriculumElectiveGroupByIdUrl;
        $scope.getCurriculumElectiveGroupDataExtraUrl = getCurriculumElectiveGroupDataExtraUrl;
        $scope.saveCurriculumElectiveGroupUrl = saveCurriculumElectiveGroupUrl;
        */

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



