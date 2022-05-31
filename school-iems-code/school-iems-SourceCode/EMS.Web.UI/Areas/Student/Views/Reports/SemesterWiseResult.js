
//File: Aca_SemesterResult Anjular  Controller
emsApp.controller('PublicResultPublishPanelCtrl', function ($scope, $http, $filter, cfpLoadingBar) {

    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;

    $scope.HasMidtermResultError = false;
    $scope.MidtermResultError = "";

    $scope.HasFinalTermResultError = false;
    $scope.IsPrintOnlyFinalTerm = false;
    $scope.IsPrintOnlyFinalTerm = false;
    $scope.FinalTermResultError = "";
    $scope.StudentProgramName = "";

    $scope.loadPage = function () {
        $scope.getSemesterWiseResultDataExtra();
    }

    $scope.refreshResult = function () {
        $scope.getMidtermResultBySemesterId();
        $scope.getFinalTermResultBySemesterId();
    }

    $scope.getSemesterWiseResultDataExtra = function () {
        $http.get($scope.getSemesterWiseResultDataExtraUrl, {
            params: {}
        }).success(function (result) {
            if (!result.HasError) {

                $scope.SemesterList = result.Data;
                if ($scope.SemesterList != null && $scope.SemesterList.length > 0) {
                    $scope.SelectedSemester = $scope.SemesterList[0];
                    $scope.refreshResult();
                }

                if (result.DataExtra.StudentProgramName != null) {
                    $scope.StudentProgramName = result.DataExtra.StudentProgramName;
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

    $scope.getMidtermResultBySemesterId = function () {
        $http.get($scope.getMidtermResultBySemesterIdUrl, {
            params: {
                "semesterId": $scope.SelectedSemester.Id
            }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;
                if (!result.DataExtra.HasMidtermResultError) {
                    $scope.MidtermResult = result.Data;
                    $scope.ErrorMsg = "";

                    $scope.HasMidtermResultError = false;
                    $scope.MidtermResultError = "";


                } else {
                    $scope.MidtermResult = null;
                    $scope.HasMidtermResultError = true;
                    $scope.MidtermResultError = result.DataExtra.MidtermResultError.toString();
                }
                

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg =result.Errors.toString();
                $scope.MidtermResult = null;
                
                //alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Please try again later!";

            log(JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString());
        });
    };

    $scope.getFinalTermResultBySemesterId = function () {
        $http.get($scope.getFinalTermResultBySemesterIdUrl, {
            params: {
                "semesterId": $scope.SelectedSemester.Id
            }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;
                if (!result.DataExtra.HasFinalTermResultError) {

                    $scope.FinalTermResult = result.Data;
                    // Final Term Result
                    $scope.TotalCredit = result.DataExtra.TotalCredit;
                    $scope.EarnedCredit = result.DataExtra.EarnedCredit;
                    $scope.SGPA = result.DataExtra.SGPA;

                    $scope.ErrorMsg = "";

                    $scope.HasFinalTermResultError = false;
                    $scope.FinalTermResultError = "";


                } else {
                    $scope.FinalTermResult = null;
                    $scope.HasFinalTermResultError = true;
                    $scope.FinalTermResultError = result.DataExtra.FinalTermResultError.toString();
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
    $scope.onChangeSemester = function () {
        if ($scope.SelectedSemester != null) {
            $scope.refreshResult();
        }
    };

    $scope.isPrintOnlyMidTerm = function() {
        $('#midTermResult').removeClass('no-print');
        $('#finalTermResult').addClass('no-print');

        window.print();
        return;
    }

    $scope.isPrintOnlyFinalTerm = function () {
        $('#midTermResult').addClass('no-print');
        $('#finalTermResult').removeClass('no-print');

        window.print();
        return;
    }

    //======Custom Variabales goes hare=====

    $scope.Init = function (
            getSemesterWiseResultDataExtraUrl
            , getMidtermResultBySemesterIdUrl
            , getFinalTermResultBySemesterIdUrl
    ) {
        $scope.getSemesterWiseResultDataExtraUrl = getSemesterWiseResultDataExtraUrl;
        $scope.getMidtermResultBySemesterIdUrl = getMidtermResultBySemesterIdUrl;
        $scope.getFinalTermResultBySemesterIdUrl = getFinalTermResultBySemesterIdUrl;

        $scope.loadPage();
        
    };

});

