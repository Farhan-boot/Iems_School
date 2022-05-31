
//File:Academic_TPE List Anjular  Controller
emsApp.controller('TPEListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.TPEList = [];
    $scope.SelectedTPE = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.SelectedClassId = 0;
    $scope.SelectedQuestionId = 0;
    $scope.SelectedAnswerId = 0;
    $scope.SelectedEmployeeId = 0;
    $scope.SelectedStudentId = 0;
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
        $scope.getTPEListExtraData();
        $scope.getPagedTPEList();

    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedTPEList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedTPEList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedTPEList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedTPEList();
    };
    $scope.searchTPEList = function () {
        $scope.getPagedTPEList();
    };
    $scope.getPagedTPEList = function () {
        $scope.getTPEList();
    };
    /*For Search on data end*/
    $scope.getTPEList = function () {
        $http.get($scope.getPagedTPEUrl, {
            params: {
                "textkey": $scope.SearchText
            , "pageSize": $scope.PageSize
            , "pageNo": $scope.PageNo
           , "classId": $scope.SelectedClassId
           , "questionId": $scope.SelectedQuestionId
           , "answerId": $scope.SelectedAnswerId
           , "employeeId": $scope.SelectedEmployeeId
           , "studentId": $scope.SelectedStudentId
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.TPEList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get T P E list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get T P E list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getTPEListExtraData = function () {
        $http.get($scope.getTPEListDataExtraUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                //DropDown Option Tables
                if (result.DataExtra.ClassList != null)
                    $scope.ClassList = result.DataExtra.ClassList;

                if (result.DataExtra.TPEQuestionList != null)
                    $scope.TPEQuestionList = result.DataExtra.TPEQuestionList;

                if (result.DataExtra.TPEQuestionAnswerList != null)
                    $scope.TPEQuestionAnswerList = result.DataExtra.TPEQuestionAnswerList;

                if (result.DataExtra.EmployeeList != null)
                    $scope.EmployeeList = result.DataExtra.EmployeeList;

                if (result.DataExtra.StudentList != null)
                    $scope.StudentList = result.DataExtra.StudentList;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for T P E! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for T P E! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllTPEList = function () {
        $scope.IsLoading++;
        $http.get($scope.getTPEListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.TPEList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get T P E list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get T P E list! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.deleteTPEById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteTPEByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.TPEList.indexOf(obj);
                        $scope.TPEList.splice(i, 1);
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
    /*$scope.deleteTPEByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteTPEByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.TPEList.indexOf(obj);
                        $scope.TPEList.splice(i, 1);
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
        for (var i = 0; i < $scope.TPEList.length; i++) {
            var entity = $scope.TPEList[i];
            entity.IsSelected = checkbox.checked;
        }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====


    $scope.Init = function (
         getPagedTPEUrl
        , deleteTPEByIdUrl
        , getTPEListDataExtraUrl
        , saveTPEListUrl
        , getTPEByIdUrl
        , getTPEDataExtraUrl
        , saveTPEUrl
        ) {
        $scope.getPagedTPEUrl = getPagedTPEUrl;
        $scope.deleteTPEByIdUrl = deleteTPEByIdUrl;
        /*bind extra url if need*/
        $scope.getTPEListDataExtraUrl = getTPEListDataExtraUrl;
        $scope.saveTPEListUrl = saveTPEListUrl;
        $scope.getTPEByIdUrl = getTPEByIdUrl;
        $scope.getTPEDataExtraUrl = getTPEDataExtraUrl;
        $scope.saveTPEUrl = saveTPEUrl;


        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



