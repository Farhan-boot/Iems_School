
//File: Aca_SemesterResult Anjular  Controller
emsApp.controller('PublicResultPublishPanelCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.SemesterResult = null;
    $scope.StudentUserName = "";
    $scope.TermId = 0;
    $scope.Student = null;
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;

    $scope.getSemesterResultByStudentUserName = function () {
        if ($scope.StudentUserName == null || $scope.StudentUserName === "") {
            return;
        }

        $http.get($scope.getSemesterResultByStudentUserNameUrl, {
            params: {
                "studentUserName": $scope.StudentUserName, 
                "semesterId": 201903,
                "termId": $scope.TermId
            }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;
                
                $scope.Student = result.DataExtra.Student;
                log(result);

                if (result.DataExtra.HasResult) {
                    $scope.SemesterResult = result.Data;
                    $scope.DueAmount = null;

                    // Final Term Result
                    $scope.TotalCredit = result.DataExtra.TotalCredit;
                    $scope.EarnedCredit = result.DataExtra.EarnedCredit;
                    $scope.SGPA = result.DataExtra.SGPA;

                    
                    $scope.ErrorMsg = "";

                } else {
                    $scope.SemesterResult = null;
                    $scope.HasError = true;
                    $scope.DueAmount = result.DataExtra.DueAmount;
                    $scope.ErrorMsg = result.Errors.toString();
                }
                updateUrlQuery('sid', $scope.StudentUserName);

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg =result.Errors.toString();
                $scope.Student = null;
                $scope.SemesterResult = null;
                
                //alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Please try again later!";

            log(JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString());
        });
    };


    //======Custom property and Functions Start=======================================================  
    //======Custom Variabales goes hare=====

    $scope.Init = function (
        StudentUserName
        ,TermId
        ,getSemesterResultByStudentUserNameUrl
    ) {
        $scope.StudentUserName = StudentUserName;
        $scope.TermId = TermId;
        $scope.getSemesterResultByStudentUserNameUrl = getSemesterResultByStudentUserNameUrl;

        
        $scope.getSemesterResultByStudentUserName();
    };

});

