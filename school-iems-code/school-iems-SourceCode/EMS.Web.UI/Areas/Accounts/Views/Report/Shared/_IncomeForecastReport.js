
//File:Academic_Semester List Anjular  Controller
emsApp.controller('IncomeForecastReportCtrl', function ($scope, $http, $filter) {
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.StartMonth = "";
    $scope.EndMonth = "";

    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====
    $scope.Init = function (
         getIncomeForecastReportDataExtraUrl
         , getPagedIncomeForecastReportUrl
        , getBatchListForIncomeForecastReportUrl
        ) {
        $scope.getIncomeForecastReportDataExtraUrl = getIncomeForecastReportDataExtraUrl;
        $scope.getPagedIncomeForecastReportUrl = getPagedIncomeForecastReportUrl;
        $scope.getBatchListForIncomeForecastReportUrl = getBatchListForIncomeForecastReportUrl;
        $scope.getIncomeForecastReportDataExtra();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

    $scope.SelectedSemesterId = 0;
    $scope.SelectedProgramId = 0;
    $scope.SelectedContinuingBatchId = 0;
    $scope.SelectedRegistrationStatusEnumId = 0;

    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;


    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedClassList();
    };

    $scope.changePageNo = function () {
        $scope.getPagedClassList();
    };

    $scope.searchClassList = function () {
        $scope.getPagedClassList();
    };

    $scope.getPagedClassList = function () {
        $scope.getStdTransactionList();
    };

    $scope.getStdTransactionList = function () {
        $http.get($scope.getPagedIncomeForecastReportUrl, {
            params: {
                "pageSize": $scope.PageSize
           , "pageNo": $scope.PageNo
           , "programId": $scope.SelectedProgramId
           , "semesterId": $scope.SelectedSemesterId
           , "ContinuingBatchId": $scope.SelectedContinuingBatchId
             , "registrationStatusEnumId": $scope.SelectedRegistrationStatusEnumId
             , "startMonth": $scope.StartMonth
             , "endMonth": $scope.EndMonth
            }
        }).success(function (result) {
            if (!result.HasError) {

                $scope.StudentList = result.Data.StudentList;
                $scope.totalItems = result.DataExtra.Count;
                $scope.totalMonth = result.Data.MaxMonthCount;
                $scope.totalServerItems = $scope.StudentList.length;

                if (result.Data.MonthList != null) {
                    $scope.MonthList = result.Data.MonthList;
                }

                if (result.Data.TotalList != null) {
                        $scope.TotalList = result.Data.TotalList;
                    }
                    //$scope.SelectedSemesterId = $scope.SemesterList[0].Id;

                    if (result.Data.length <= 0) {
                    $scope.ErrorMsg = "No Data Found using your search! ";
                    $scope.StudentList = [];
                    $scope.totalItems = 0;
                    alertError($scope.ErrorMsg);
                }
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Income Forecast Report! " + result.Errors.toString();
                $scope.StudentList = [];
                $scope.totalItems = 0;
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Income Forecast Report! " + "Status: " + JSON.stringify(status) + ". " + JSON.stringify(result);
            alertError($scope.ErrorMsg);
        });
    };

    $scope.getBatchListForIncomeForecastReport = function () {
        $scope.cleanUi();
        if ( $scope.SelectedProgramId <= 0 || $scope.SelectedRegistrationStatusEnumId <= 0) {
            return;
        }
        $http.get($scope.getBatchListForIncomeForecastReportUrl, {
            params: {
                "semesterId": $scope.SelectedSemesterId
                ,"programId": $scope.SelectedProgramId
                , "registrationStatusEnumId": $scope.SelectedRegistrationStatusEnumId
            }
        }).success(function (result) {
            if (!result.HasError) {

                if (result.DataExtra.DeptBatchList != null) {
                    $scope.DeptBatchList = result.DataExtra.DeptBatchList;
                    $scope.SelectedContinuingBatchId = 0;
                }
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Batch List! " + result.Errors.toString();
                $scope.StudentList = [];
                $scope.DeptBatchList = [];
                $scope.totalItems = 0;
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Batch List! " + "Status: " + JSON.stringify(status) + ". " + JSON.stringify(result);
            alertError($scope.ErrorMsg);
        });
    };

    $scope.getIncomeForecastReportDataExtra = function () {
        $http.get($scope.getIncomeForecastReportDataExtraUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.DataExtra.ProgramList != null) {
                    $scope.ProgramList = result.DataExtra.ProgramList;
                    //$scope.SelectedProgramId = $scope.ProgramList[0].Id;
                }
                if (result.DataExtra.SemesterList != null) {
                    $scope.SemesterList = result.DataExtra.SemesterList;

                    if (result.DataExtra.SemesterList != null) {
                        $scope.SelectedSemesterId = result.DataExtra.CurrentSemesterId;
                    } else {
                        $scope.SelectedSemesterId = $scope.SemesterList[0].Id;
                    }
                }

                if (result.DataExtra.RegistrationStatusEnumList != null) {
                    $scope.RegistrationStatusEnumList = result.DataExtra.RegistrationStatusEnumList;
                }

                if (result.DataExtra.StartMonth != null) {
                    $scope.StartMonth = new Date(result.DataExtra.StartMonth);
                }

                if (result.DataExtra.EndMonth != null) {
                    $scope.EndMonth = new Date(result.DataExtra.EndMonth);
                }



            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Income Forecast Report! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Income Forecast Report! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.cleanUi = function () {
        $scope.StudentList = [];
    }

});



