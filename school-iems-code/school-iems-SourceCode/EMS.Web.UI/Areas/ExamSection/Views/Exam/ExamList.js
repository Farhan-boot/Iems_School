
//File:Academic_Exam List Anjular  Controller
emsApp.controller('ExamListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.ExamList = [];
    $scope.SelectedExam = [];
    $scope.SemesterList = [];
    $scope.SelectedSemesterId = new Object();
    $scope.ErrorMsg = "";
    $scope.HasError = false;

    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 200;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
        $scope.getPagedExamList();
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedExamList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedExamList();
    };
    $scope.searchExamList = function () {
        $scope.getPagedExamList();
    };
    $scope.getPagedExamList = function () {
        $scope.getExamList();
    };
    /*For Search on data end*/
    $scope.getExamList = function () {
        $http.get($scope.getPagedExamUrl, {
            params: {
                "textkey": $scope.SearchText,
                "semesterId": $scope.SelectedSemesterId,
            "pageSize": $scope.PageSize, 
            "pageNo": $scope.PageNo }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.ExamList = result.Data;
                $scope.SemesterList = result.DataExtra.SemesterList;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Exam list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Exam list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllExamList = function () {
        $scope.IsLoading++;
        $http.get($scope.getExamListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.ExamList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Exam list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Exam list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteExamByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteExamByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.ExamList.indexOf(obj);
                        $scope.ExamList.splice(i, 1);
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
    $scope.deleteExamById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteExamByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.ExamList.indexOf(obj);
                        $scope.ExamList.splice(i, 1);
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
    for (var i = 0; i < $scope.ExamList.length; i++) {
        var entity = $scope.ExamList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====
    $scope.Init = function (
         getPagedExamUrl
        , deleteExamByIdUrl
        ) {
        $scope.getPagedExamUrl = getPagedExamUrl;
        $scope.deleteExamByIdUrl = deleteExamByIdUrl;
        /*bind extra url if need
        $scope.getExamByIdUrl = getExamByIdUrl;
        $scope.getExamDataExtraUrl = getExamDataExtraUrl;
        $scope.getExamListDataExtraUrl = getExamListDataExtraUrl;
        $scope.saveExamUrl = saveExamUrl;
        $scope.getExamListDataExtraUrl = getExamListDataExtraUrl;
        $scope.saveExamListUrl = saveExamListUrl;*/

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 


});



