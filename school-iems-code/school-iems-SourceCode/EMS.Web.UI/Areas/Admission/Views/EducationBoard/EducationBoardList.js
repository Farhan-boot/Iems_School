
//File:Admission_EducationBoard List Anjular  Controller
emsApp.controller('EducationBoardListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.EducationBoardList = [];
    $scope.SelectedEducationBoard = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getEducationBoardListExtraData();
      $scope.getPagedEducationBoardList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedEducationBoardList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedEducationBoardList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedEducationBoardList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedEducationBoardList();
    };
    $scope.searchEducationBoardList = function () {
        $scope.getPagedEducationBoardList();
    };
    $scope.getPagedEducationBoardList = function () {
        $scope.getEducationBoardList();
    };
    /*For Search on data end*/
    $scope.getEducationBoardList = function () {
        $http.get($scope.getPagedEducationBoardUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.EducationBoardList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Education Board list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Education Board list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getEducationBoardListExtraData= function()
    {
            $http.get($scope.getEducationBoardListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.BoardTypeEnumList!=null)
                         $scope.BoardTypeEnumList = result.DataExtra.BoardTypeEnumList;
                  //DropDown Option Tables
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Education Board! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Education Board! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllEducationBoardList = function () {
        $scope.IsLoading++;
        $http.get($scope.getEducationBoardListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.EducationBoardList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Education Board list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Education Board list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteEducationBoardById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteEducationBoardByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.EducationBoardList.indexOf(obj);
                        $scope.EducationBoardList.splice(i, 1);
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
    /*$scope.deleteEducationBoardByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteEducationBoardByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.EducationBoardList.indexOf(obj);
                        $scope.EducationBoardList.splice(i, 1);
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
    for (var i = 0; i < $scope.EducationBoardList.length; i++) {
        var entity = $scope.EducationBoardList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedEducationBoardUrl
        ,deleteEducationBoardByIdUrl
        ,getEducationBoardListDataExtraUrl
        ,saveEducationBoardListUrl
        ,getEducationBoardByIdUrl
        ,getEducationBoardDataExtraUrl
        ,saveEducationBoardUrl
        ) {
        $scope.getPagedEducationBoardUrl = getPagedEducationBoardUrl;
        $scope.deleteEducationBoardByIdUrl = deleteEducationBoardByIdUrl;
        /*bind extra url if need*/
        $scope.getEducationBoardListDataExtraUrl = getEducationBoardListDataExtraUrl;
        $scope.saveEducationBoardListUrl = saveEducationBoardListUrl;
        $scope.getEducationBoardByIdUrl = getEducationBoardByIdUrl;
        $scope.getEducationBoardDataExtraUrl = getEducationBoardDataExtraUrl;
        $scope.saveEducationBoardUrl = saveEducationBoardUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



