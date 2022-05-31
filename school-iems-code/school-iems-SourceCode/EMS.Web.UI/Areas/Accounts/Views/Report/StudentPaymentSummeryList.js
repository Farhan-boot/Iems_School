
//File:Accounts_StdTransaction List Anjular  Controller
emsApp.controller('StdTransactionListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.StdTransactionList = [];
    $scope.SelectedStdTransaction = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
     $scope.SelectedSemesterId=0;      
     $scope.SelectedStudentId=0;      
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getStdTransactionListExtraData();
      $scope.getPagedStdTransactionList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedStdTransactionList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedStdTransactionList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedStdTransactionList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedStdTransactionList();
    };
    $scope.searchStdTransactionList = function () {
        $scope.getPagedStdTransactionList();
    };
    $scope.getPagedStdTransactionList = function () {
        $scope.getStdTransactionList();
    };
    /*For Search on data end*/
    $scope.getStdTransactionList = function () {
        $http.get($scope.getPagedStdTransactionUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
           ,"semesterId": $scope.SelectedSemesterId      
           ,"studentId": $scope.SelectedStudentId      
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.StdTransactionList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
               
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Std Transaction list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Std Transaction list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getStdTransactionListExtraData= function()
    {
            $http.get($scope.getStdTransactionListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.PaymentStatusEnumList!=null)
                         $scope.PaymentStatusEnumList = result.DataExtra.PaymentStatusEnumList;
                      if(result.DataExtra.TransactionTypeEnumList!=null)
                         $scope.TransactionTypeEnumList = result.DataExtra.TransactionTypeEnumList;
                  //DropDown Option Tables
                     if(result.DataExtra.SemesterList!=null)
                       $scope.SemesterList =  result.DataExtra.SemesterList; 

                     if(result.DataExtra.StudentList!=null)
                       $scope.StudentList =  result.DataExtra.StudentList; 

                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Std Transaction! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Std Transaction! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllStdTransactionList = function () {
        $scope.IsLoading++;
        $http.get($scope.getStdTransactionListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.StdTransactionList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Std Transaction list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Std Transaction list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteStdTransactionById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteStdTransactionByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.StdTransactionList.indexOf(obj);
                        $scope.StdTransactionList.splice(i, 1);
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
    /*$scope.deleteStdTransactionByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteStdTransactionByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.StdTransactionList.indexOf(obj);
                        $scope.StdTransactionList.splice(i, 1);
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
    for (var i = 0; i < $scope.StdTransactionList.length; i++) {
        var entity = $scope.StdTransactionList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedStdTransactionUrl
        ,deleteStdTransactionByIdUrl
        ,getStdTransactionListDataExtraUrl
        ,saveStdTransactionListUrl
        ,getStdTransactionByIdUrl
        ,getStdTransactionDataExtraUrl
        ,saveStdTransactionUrl
        ) {
        $scope.getPagedStdTransactionUrl = getPagedStdTransactionUrl;
        $scope.deleteStdTransactionByIdUrl = deleteStdTransactionByIdUrl;
        /*bind extra url if need*/
        $scope.getStdTransactionListDataExtraUrl = getStdTransactionListDataExtraUrl;
        $scope.saveStdTransactionListUrl = saveStdTransactionListUrl;
        $scope.getStdTransactionByIdUrl = getStdTransactionByIdUrl;
        $scope.getStdTransactionDataExtraUrl = getStdTransactionDataExtraUrl;
        $scope.saveStdTransactionUrl = saveStdTransactionUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



