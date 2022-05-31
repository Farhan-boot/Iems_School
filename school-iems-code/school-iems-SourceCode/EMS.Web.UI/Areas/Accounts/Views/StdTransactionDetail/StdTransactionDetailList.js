
//File:Accounts_StdTransactionDetail List Anjular  Controller
emsApp.controller('StdTransactionDetailListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.StdTransactionDetailList = [];
    $scope.SelectedStdTransactionDetail = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
     $scope.SelectedSemesterId=0;      
     $scope.SelectedTrasectionId=0;      
     $scope.SelectedStudentId=0;      
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getStdTransactionDetailListExtraData();
      $scope.getPagedStdTransactionDetailList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedStdTransactionDetailList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedStdTransactionDetailList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedStdTransactionDetailList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedStdTransactionDetailList();
    };
    $scope.searchStdTransactionDetailList = function () {
        $scope.getPagedStdTransactionDetailList();
    };
    $scope.getPagedStdTransactionDetailList = function () {
        $scope.getStdTransactionDetailList();
    };
    /*For Search on data end*/
    $scope.getStdTransactionDetailList = function () {
        $http.get($scope.getPagedStdTransactionDetailUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
           ,"semesterId": $scope.SelectedSemesterId      
           ,"trasectionId": $scope.SelectedTrasectionId      
           ,"studentId": $scope.SelectedStudentId      
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.StdTransactionDetailList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Std Transaction Detail list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Std Transaction Detail list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getStdTransactionDetailListExtraData= function()
    {
            $http.get($scope.getStdTransactionDetailListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.ParticularTypeEnumList!=null)
                         $scope.ParticularTypeEnumList = result.DataExtra.ParticularTypeEnumList;
                  //DropDown Option Tables
                     if(result.DataExtra.SemesterList!=null)
                       $scope.SemesterList =  result.DataExtra.SemesterList; 

                     if(result.DataExtra.StdTransactionList!=null)
                       $scope.StdTransactionList =  result.DataExtra.StdTransactionList; 

                     if(result.DataExtra.StudentList!=null)
                       $scope.StudentList =  result.DataExtra.StudentList; 

                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Std Transaction Detail! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Std Transaction Detail! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllStdTransactionDetailList = function () {
        $scope.IsLoading++;
        $http.get($scope.getStdTransactionDetailListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.StdTransactionDetailList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Std Transaction Detail list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Std Transaction Detail list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteStdTransactionDetailById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteStdTransactionDetailByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.StdTransactionDetailList.indexOf(obj);
                        $scope.StdTransactionDetailList.splice(i, 1);
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
    /*$scope.deleteStdTransactionDetailByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteStdTransactionDetailByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.StdTransactionDetailList.indexOf(obj);
                        $scope.StdTransactionDetailList.splice(i, 1);
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
    for (var i = 0; i < $scope.StdTransactionDetailList.length; i++) {
        var entity = $scope.StdTransactionDetailList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedStdTransactionDetailUrl
        ,deleteStdTransactionDetailByIdUrl
        ,getStdTransactionDetailListDataExtraUrl
        ,saveStdTransactionDetailListUrl
        ,getStdTransactionDetailByIdUrl
        ,getStdTransactionDetailDataExtraUrl
        ,saveStdTransactionDetailUrl
        ) {
        $scope.getPagedStdTransactionDetailUrl = getPagedStdTransactionDetailUrl;
        $scope.deleteStdTransactionDetailByIdUrl = deleteStdTransactionDetailByIdUrl;
        /*bind extra url if need*/
        $scope.getStdTransactionDetailListDataExtraUrl = getStdTransactionDetailListDataExtraUrl;
        $scope.saveStdTransactionDetailListUrl = saveStdTransactionDetailListUrl;
        $scope.getStdTransactionDetailByIdUrl = getStdTransactionDetailByIdUrl;
        $scope.getStdTransactionDetailDataExtraUrl = getStdTransactionDetailDataExtraUrl;
        $scope.saveStdTransactionDetailUrl = saveStdTransactionDetailUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



