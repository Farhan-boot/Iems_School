
//File:Accounts_StudentPaySlip List Anjular  Controller
emsApp.controller('StudentPaySlipListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.StudentPaySlipList = [];
    $scope.SelectedStudentPaySlip = [];
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
      $scope.getStudentPaySlipListExtraData();
      $scope.getPagedStudentPaySlipList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedStudentPaySlipList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedStudentPaySlipList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedStudentPaySlipList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedStudentPaySlipList();
    };
    $scope.searchStudentPaySlipList = function () {
        $scope.getPagedStudentPaySlipList();
    };
    $scope.getPagedStudentPaySlipList = function () {
        $scope.getStudentPaySlipList();
    };
    /*For Search on data end*/
    $scope.getStudentPaySlipList = function () {
        $http.get($scope.getPagedStudentPaySlipUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.StudentPaySlipList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Student Pay Slip list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Student Pay Slip list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getStudentPaySlipListExtraData= function()
    {
            $http.get($scope.getStudentPaySlipListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.PostStatusEnumList!=null)
                         $scope.PostStatusEnumList = result.DataExtra.PostStatusEnumList;
                  //DropDown Option Tables
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Student Pay Slip! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Student Pay Slip! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllStudentPaySlipList = function () {
        $scope.IsLoading++;
        $http.get($scope.getStudentPaySlipListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.StudentPaySlipList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Student Pay Slip list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Student Pay Slip list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteStudentPaySlipById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteStudentPaySlipByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.StudentPaySlipList.indexOf(obj);
                        $scope.StudentPaySlipList.splice(i, 1);
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
    /*$scope.deleteStudentPaySlipByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteStudentPaySlipByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.StudentPaySlipList.indexOf(obj);
                        $scope.StudentPaySlipList.splice(i, 1);
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
    for (var i = 0; i < $scope.StudentPaySlipList.length; i++) {
        var entity = $scope.StudentPaySlipList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedStudentPaySlipUrl
        ,deleteStudentPaySlipByIdUrl
        ,getStudentPaySlipListDataExtraUrl
        ,saveStudentPaySlipListUrl
        ,getStudentPaySlipByIdUrl
        ,getStudentPaySlipDataExtraUrl
        ,saveStudentPaySlipUrl
        ) {
        $scope.getPagedStudentPaySlipUrl = getPagedStudentPaySlipUrl;
        $scope.deleteStudentPaySlipByIdUrl = deleteStudentPaySlipByIdUrl;
        /*bind extra url if need*/
        $scope.getStudentPaySlipListDataExtraUrl = getStudentPaySlipListDataExtraUrl;
        $scope.saveStudentPaySlipListUrl = saveStudentPaySlipListUrl;
        $scope.getStudentPaySlipByIdUrl = getStudentPaySlipByIdUrl;
        $scope.getStudentPaySlipDataExtraUrl = getStudentPaySlipDataExtraUrl;
        $scope.saveStudentPaySlipUrl = saveStudentPaySlipUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



