
//File: Aca_SemesterResult Anjular  Controller
emsApp.controller('PublicResultPublishPanelCtrl', function ($scope, $http, $filter, cfpLoadingBar) {

    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;

    $scope.HasTransferredCourseResultError = false;
    $scope.TransferredCourseResultError = "";



    $scope.loadPage = function () {
        $scope.getTransferredCourseResultDataExtra();
    }

    $scope.refreshResult = function () {
        $scope.getTransferredCourseResultByCreditTransferId();
    }

    $scope.getTransferredCourseResultDataExtra = function () {
        $http.get($scope.getTransferredCourseResultDataExtraUrl, {
            params: {}
        }).success(function (result) {
            if (!result.HasError) {
                if (result.DataExtra.CreditTransferList != null) {
                    $scope.CreditTransferList = result.DataExtra.CreditTransferList;
                    $scope.SelectedCreditTransferId = result.DataExtra.CreditTransferList[0].Id;
                    $scope.refreshResult();
                }
                
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Semester list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }

        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Semester list! " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);

        });
    };

    $scope.getTransferredCourseResultByCreditTransferId = function () {
        $http.get($scope.getTransferredCourseResultByCreditTransferIdUrl, {
            params: {
                "creditTransferId": $scope.SelectedCreditTransferId
            }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;

                if (!result.DataExtra.HasTransferredCourseResultError) {

                    $scope.TransferredCourseResult = result.Data;
                    if (result.DataExtra.CurriculumCourseList != null) {
                        $scope.CurriculumCourseList = result.DataExtra.CurriculumCourseList;
                    }
                    
                    // Final Term Result
                    $scope.TotalCredit = result.DataExtra.TotalCredit;
                    $scope.EarnedCredit = result.DataExtra.EarnedCredit;

                    $scope.ErrorMsg = "";

                    $scope.HasTransferredCourseResultError = false;
                    $scope.TransferredCourseResultError = "";


                } else {
                    $scope.TransferredCourseResult = null;
                    $scope.HasTransferredCourseResultError = true;
                    $scope.TransferredCourseResultError = result.DataExtra.TransferredCourseResultError.toString();
                }

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = result.Errors.toString();
                $scope.TransferredCourseResult = null;

            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Please try again later!";

            log(JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString());
        });
    };


    //======Custom property and Functions Start=======================================================  
    $scope.onChangeInstitute = function () {
        if ($scope.SelectedCreditTransferId != null) {
            $scope.refreshResult();
        }
    };

    //======Custom Variabales goes hare=====

    $scope.Init = function (
            getTransferredCourseResultDataExtraUrl
            , getTransferredCourseResultByCreditTransferIdUrl
    ) {
        $scope.getTransferredCourseResultDataExtraUrl = getTransferredCourseResultDataExtraUrl;
        $scope.getTransferredCourseResultByCreditTransferIdUrl = getTransferredCourseResultByCreditTransferIdUrl;

        $scope.loadPage();
        
    };

});

