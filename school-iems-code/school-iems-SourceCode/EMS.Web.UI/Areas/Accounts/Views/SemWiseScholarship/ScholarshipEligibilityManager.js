
//File:Academic_Semester List Anjular  Controller
emsApp.controller('ScholarshipEligibilityManagerCtrl', function ($scope, $http, $filter) {
    $scope.ErrorMsg = "";
    $scope.HasError = false;

    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====
    $scope.Init = function (
        getScholarshipEligibilityManagerDataExtraUrl
        , getScholarshipListForEligibleStudentUrl
        , saveScholarshipListForEligibleStudentUrl
    ) {
        $scope.getScholarshipEligibilityManagerDataExtraUrl = getScholarshipEligibilityManagerDataExtraUrl;
        $scope.getScholarshipListForEligibleStudentUrl = getScholarshipListForEligibleStudentUrl;
        $scope.saveScholarshipListForEligibleStudentUrl = saveScholarshipListForEligibleStudentUrl;

        $scope.loadData();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

    $scope.SelectedSemesterId = 0;
    $scope.SelectedSemester = [];
    $scope.SelectedProgramId = 0;
    $scope.SelectedContinuingBatchId = 0;
    $scope.SelectedEligibilityStatusEnumId = 2;


    $scope.loadData = function () {
        $scope.getScholarshipEligibilityManagerDataExtra();
    }

    $scope.getScholarshipEligibilityManagerDataExtra = function () {
        $http.get($scope.getScholarshipEligibilityManagerDataExtraUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.DataExtra.SemesterList != null) {
                    $scope.SemesterList = result.DataExtra.SemesterList;
                    $scope.SelectedSemesterId = $scope.SemesterList[0].Id;
                    $scope.SelectedSemester = $scope.SemesterList[0];
                }

                if (result.DataExtra.ProgramList != null) {
                    $scope.ProgramList = result.DataExtra.ProgramList;
                }

                if (result.DataExtra.EligibilityStatusEnumList != null) {
                    $scope.EligibilityStatusEnumList = result.DataExtra.EligibilityStatusEnumList;
                }

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = result.Errors.toString() + " Unable to load Data!";
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Scholarship Eligibility Manager! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.onChangeSemester = function () {
        //$scope.getScholarshipEligibilityManagerProgramList();
    }

    $scope.getScholarshipListForEligibleStudent = function () {
        $http.get($scope.getScholarshipListForEligibleStudentUrl, {
            params: {
                "semesterId": $scope.SelectedSemester.Id,
                "programId": $scope.SelectedProgramId,
                "admSemesterId":$scope.SelectedAdmSemesterId,
                "eligibilityStatusEnumId": $scope.SelectedEligibilityStatusEnumId
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.ScholarshipList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = result.Errors.toString() + " Unable to load Data!";
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Scholarship Eligibility Manager! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.saveScholarshipListForEligibleStudent = function () {

        bootbox.confirm("Are you sure you want to add scholarship & apply to payment (Bill)?", function (yes) {
            if (yes) {
                $http.post($scope.saveScholarshipListForEligibleStudentUrl + "/", $scope.ScholarshipList).
                    success(function (result, status) {
                        if (!result.HasError) {
                            $scope.HasError = false;
                            alertSuccess("Successfully saved Sem Wise Scholarship data!");
                        } else {
                            $scope.HasError = true;
                            $scope.ErrorMsg = "Unable to save Sem Wise Scholarship! " + result.Errors.toString();
                            alertError($scope.ErrorMsg);
                        }
                    }).
                    error(function (result, status) {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Unable to save Sem Wise Scholarship! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                        alertError($scope.ErrorMsg);
                    });
            }
        });


        
    };

    $scope.IsSelectAll = false;
    $scope.selectAll = function () {
        for (var i = 0; i < $scope.ScholarshipList.length; i++) {
            var entity = $scope.ScholarshipList[i];
            entity.IsSelected = $scope.IsSelectAll;
        }
        $scope.IsScholarshipUpdateable();
    };

    $scope.IsScholarshipUpdateable = function () {
        var selectedCount = 0;
        for (var i = 0; i < $scope.ScholarshipList.length; i++) {
            var entity = $scope.ScholarshipList[i];
            if (entity.IsSelected) {
                selectedCount++;
                break;
            }
        }
        if (selectedCount > 0) {
            $scope.IsUpdateable = true;
            return true;
        } else {
            $scope.IsUpdateable = false;
            $scope.IsConfirmed = false;
            return false;
        }
    }



});



