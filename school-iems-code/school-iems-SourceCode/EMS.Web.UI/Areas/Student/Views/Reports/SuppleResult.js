
//File: Aca_SemesterResult Anjular  Controller
emsApp.controller('PublicResultPublishPanelCtrl', function ($scope, $http, $filter, cfpLoadingBar) {

    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;

    $scope.HasSuppleResultError = false;
    $scope.SuppleResultError = "";

    $scope.loadPage = function () {
        $scope.getSuppleResultDataExtra();
    }

    $scope.refreshResult = function () {
        $scope.getSuppleResultByExamId();
    }

    $scope.getSuppleResultDataExtra = function () {
        $http.get($scope.getSuppleResultDataExtraUrl, {
            params: {}
        }).success(function (result) {
            if (!result.HasError) {

                $scope.ExamListForSuppleList = result.DataExtra.ExamListForSuppleList;
                if ($scope.ExamListForSuppleList != null && $scope.ExamListForSuppleList.length > 0) {
                    $scope.SelectedExam = $scope.ExamListForSuppleList[0];
                    $scope.refreshResult();
                }

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Exam list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }

        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Exam list! " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);

        });
    };


    $scope.getSuppleResultByExamId = function () {
        $http.get($scope.getSuppleResultByExamIdUrl, {
            params: {
                "examId": $scope.SelectedExam.Id
            }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;
                log("Final");
                log(result);

                if (!result.DataExtra.HasSuppleResultError) {

                    $scope.FinalTermResult = result.Data;
                    // Final Term Result
                    $scope.TotalCredit = result.DataExtra.TotalCredit;
                    $scope.EarnedCredit = result.DataExtra.EarnedCredit;
                    $scope.SGPA = result.DataExtra.SGPA;

                    $scope.ErrorMsg = "";

                    $scope.HasSuppleResultError = false;
                    $scope.SuppleResultError = "";


                } else {
                    $scope.FinalTermResult = null;
                    $scope.HasSuppleResultError = true;
                    $scope.SuppleResultError = result.DataExtra.SuppleResultError.toString();
                }

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = result.Errors.toString();
                $scope.FinalTermResult = null;

            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Please try again later!";

            log(JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString());
        });
    };


    //======Custom property and Functions Start=======================================================  
    $scope.onChangeExam = function () {
        if ($scope.SelectedExam != null) {
            $scope.refreshResult();
        }
    };

    //======Custom Variabales goes hare=====

    $scope.Init = function (
        getSuppleResultDataExtraUrl
        , getSuppleResultByExamIdUrl
    ) {
        $scope.getSuppleResultDataExtraUrl = getSuppleResultDataExtraUrl;
        $scope.getSuppleResultByExamIdUrl = getSuppleResultByExamIdUrl;

        $scope.loadPage();
        
    };

});

