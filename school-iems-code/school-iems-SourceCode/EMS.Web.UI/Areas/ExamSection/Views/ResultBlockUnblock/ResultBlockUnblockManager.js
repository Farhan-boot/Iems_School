
//File:Admin_ResultBlockUnblock List Anjular  Controller
emsApp.controller('ResultBlockUnblockListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.ResultBlockUnblockList = [];
    $scope.SelectedResultBlockUnblock = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
    
    //search items
    $scope.StudentUserNameForSearch = null;
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 10;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
        $scope.getResultBlockUnblockListExtraData();
        $scope.getPagedResultBlockUnblockList();

    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedResultBlockUnblockList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedResultBlockUnblockList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedResultBlockUnblockList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedResultBlockUnblockList();
    };
    $scope.searchResultBlockUnblockList = function () {
        $scope.getPagedResultBlockUnblockList();
    };
    $scope.getPagedResultBlockUnblockList = function () {
        //$scope.getResultBlockUnblockList();
    };
    /*For Search on data end*/
    $scope.getResultBlockUnblockList = function () {
        var examId = $scope.ResultBlockUnblock.ExamId;
        var typeEnumId = $scope.ResultBlockUnblock.TypeEnumId;
        var reasonTypeEnumId = $scope.ResultBlockUnblock.ReasonTypeEnumId;

        if ($scope.StudentUserNameForSearch!=null) {
             examId = -1;
             typeEnumId = -1;
             reasonTypeEnumId = -1;
        }
        $http.get($scope.getPagedResultBlockUnblockUrl, {
            params: {
                "examId":examId
                , "typeEnumId": typeEnumId
                , "reasonTypeEnumId": reasonTypeEnumId
                , "userName": $scope.StudentUserNameForSearch
                , "pageSize": $scope.PageSize
            , "pageNo": $scope.PageNo
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.ResultBlockUnblockList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Result Block list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Result Block list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getResultBlockUnblockListExtraData = function () {
        $http.get($scope.getResultBlockUnblockListDataExtraUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.DataExtra.ReasonTypeEnumList != null) {
                    $scope.ReasonTypeEnumList = result.DataExtra.ReasonTypeEnumList;
                }
                if (result.DataExtra.TypeEnumList != null) {
                    $scope.TypeEnumList = result.DataExtra.TypeEnumList;
                }


                //DropDown Option Tables
                if (result.DataExtra.ExamList!=null) {
                    $scope.ExamList = result.DataExtra.ExamList;
                }

                if (result.DataExtra.ResultBlockUnblock) {
                    $scope.ResultBlockUnblock = result.DataExtra.ResultBlockUnblock;
                }

                $scope.getResultBlockUnblockList();

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Result Block! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Result Block! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.resultBlockUnblockEdit=function(row) {
        $scope.ResultBlockUnblock = row;
        log($scope.ResultBlockUnblock);
    }
    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllResultBlockUnblockList = function () {
        $scope.IsLoading++;
        $http.get($scope.getResultBlockUnblockListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.ResultBlockUnblockList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Result Block list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Result Block list! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.deleteResultBlockUnblockById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteResultBlockUnblockByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.ResultBlockUnblockList.indexOf(obj);
                        $scope.ResultBlockUnblockList.splice(i, 1);
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



    $scope.saveResultBlockUnblock = function () {
        $http.post($scope.saveResultBlockUnblockUrl + "/", $scope.ResultBlockUnblock).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.Data != null) {
                        $scope.ResultBlockUnblock = result.Data;
                        $scope.getResultBlockUnblockList();

                        //updateUrlQuery('id', $scope.ResultBlock.Id);
                    }
                    alertSuccess("Successfully saved Result Block Unblock data!");
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to save Result Block Unblock! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to save Result Block! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };

    $scope.addNew = function () {
        $scope.ResultBlockUnblock = angular.copy($scope.ResultBlockUnblock);
        $scope.ResultBlockUnblock.Id = 0;
        $scope.ResultBlockUnblock.StudentUserName = "";
        $scope.ResultBlockUnblock.Remark = "";
    }


    /*$scope.deleteResultBlockUnblockByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteResultBlockUnblockByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.ResultBlockUnblockList.indexOf(obj);
                        $scope.ResultBlockUnblockList.splice(i, 1);
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
        for (var i = 0; i < $scope.ResultBlockUnblockList.length; i++) {
            var entity = $scope.ResultBlockUnblockList[i];
            entity.IsSelected = checkbox.checked;
        }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====


    $scope.Init = function (
         getPagedResultBlockUnblockUrl
        , deleteResultBlockUnblockByIdUrl
        , getResultBlockUnblockListDataExtraUrl
        , saveResultBlockUnblockListUrl
        , getResultBlockUnblockByIdUrl
        , getResultBlockUnblockDataExtraUrl
        , saveResultBlockUnblockUrl
        ) {
        $scope.getPagedResultBlockUnblockUrl = getPagedResultBlockUnblockUrl;
        $scope.deleteResultBlockUnblockByIdUrl = deleteResultBlockUnblockByIdUrl;
        /*bind extra url if need*/
        $scope.getResultBlockUnblockListDataExtraUrl = getResultBlockUnblockListDataExtraUrl;
        $scope.saveResultBlockUnblockListUrl = saveResultBlockUnblockListUrl;
        $scope.getResultBlockUnblockByIdUrl = getResultBlockUnblockByIdUrl;
        $scope.getResultBlockUnblockDataExtraUrl = getResultBlockUnblockDataExtraUrl;
        $scope.saveResultBlockUnblockUrl = saveResultBlockUnblockUrl;


        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



