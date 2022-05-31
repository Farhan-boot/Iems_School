
//File:Admission_StudentQuotaName List Anjular  Controller
emsApp.controller('StudentQuotaNameListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.StudentQuotaNameList = [];
    $scope.SelectedStudentQuotaName = [];
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
      $scope.getStudentQuotaNameListExtraData();
      $scope.getPagedStudentQuotaNameList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedStudentQuotaNameList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedStudentQuotaNameList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedStudentQuotaNameList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedStudentQuotaNameList();
    };
    $scope.searchStudentQuotaNameList = function () {
        $scope.getPagedStudentQuotaNameList();
    };
    $scope.getPagedStudentQuotaNameList = function () {
        $scope.getStudentQuotaNameList();
    };
    /*For Search on data end*/
    $scope.getStudentQuotaNameList = function () {
        $http.get($scope.getPagedStudentQuotaNameUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.StudentQuotaNameList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Student Quota Name list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Student Quota Name list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getStudentQuotaNameListExtraData= function()
    {
            $http.get($scope.getStudentQuotaNameListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                  //DropDown Option Tables
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Student Quota Name! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Student Quota Name! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllStudentQuotaNameList = function () {
        $scope.IsLoading++;
        $http.get($scope.getStudentQuotaNameListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.StudentQuotaNameList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Student Quota Name list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Student Quota Name list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteStudentQuotaNameById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteStudentQuotaNameByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.StudentQuotaNameList.indexOf(obj);
                        $scope.StudentQuotaNameList.splice(i, 1);
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
    /*$scope.deleteStudentQuotaNameByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteStudentQuotaNameByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.StudentQuotaNameList.indexOf(obj);
                        $scope.StudentQuotaNameList.splice(i, 1);
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
    for (var i = 0; i < $scope.StudentQuotaNameList.length; i++) {
        var entity = $scope.StudentQuotaNameList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedStudentQuotaNameUrl
        ,deleteStudentQuotaNameByIdUrl
        ,getStudentQuotaNameListDataExtraUrl
        ,saveStudentQuotaNameListUrl
        ,getStudentQuotaNameByIdUrl
        ,getStudentQuotaNameDataExtraUrl
        ,saveStudentQuotaNameUrl
        ) {
        $scope.getPagedStudentQuotaNameUrl = getPagedStudentQuotaNameUrl;
        $scope.deleteStudentQuotaNameByIdUrl = deleteStudentQuotaNameByIdUrl;
        /*bind extra url if need*/
        $scope.getStudentQuotaNameListDataExtraUrl = getStudentQuotaNameListDataExtraUrl;
        $scope.saveStudentQuotaNameListUrl = saveStudentQuotaNameListUrl;
        $scope.getStudentQuotaNameByIdUrl = getStudentQuotaNameByIdUrl;
        $scope.getStudentQuotaNameDataExtraUrl = getStudentQuotaNameDataExtraUrl;
        $scope.saveStudentQuotaNameUrl = saveStudentQuotaNameUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



