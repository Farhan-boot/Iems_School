
//File: Aca_BulkCourseOffering Anjular  Controller
emsApp.controller('BulkCourseOfferingAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.selectedSemesterId = 0;
    $scope.selectedProgramId = 0;
    $scope.selectedCurriculumId = 0;
    $scope.selectedLevelTermId = 0;
    $scope.SelectedBatchId = 0;
    $scope.SelectedSemesterDurationEnumId = 0;

    $scope.SelectedSemester = [];

    $scope.OfferAbleCourseList = [];
    $scope.DeptBatchList = [];

    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.IsShowMinusSign = true;
    $scope.IsOfferAble = false;

    $scope.getBulkCourseOfferingById = function () {
        
        $http.get($scope.getOfferAbleCourseListUrl, {
            params: {
                "semesterId": $scope.selectedSemesterId
                , "programId": $scope.selectedProgramId
                , "curriculumId": $scope.selectedCurriculumId
                , "levelTermId": $scope.selectedLevelTermId
                , "batchId": $scope.SelectedBatchId
            }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.OfferAbleCourseList = result.Data;

                if (result.DataExtra.DeptBatchList != null) {
                    $scope.DeptBatchList = result.DataExtra.DeptBatchList;
                }
                $scope.IsConfirmed = false;
                //log(result.DataExtra.DeptBatchList);

                //log(result.Data);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Bulk Courses for Offering! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Bulk Courses for Offering! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.loadBulkCourseOfferingExtraData = function () {
        
        $http.get($scope.getBulkCourseOfferingExtraDataUrl, {
            params: { "programId": $scope.selectedProgramId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;

                if (result.DataExtra.SemesterList != null)
                    $scope.SemesterList = result.DataExtra.SemesterList;

                if (result.DataExtra.ProgramList != null) {
                    $scope.ProgramList = result.DataExtra.ProgramList;
                    //log($scope.ProgramList);
                }

                if (result.DataExtra.CurriculumList != null && result.DataExtra.CurriculumList.length >0) {
                    $scope.CurriculumList = result.DataExtra.CurriculumList;
                    $scope.selectedCurriculumId = result.DataExtra.CurriculumList[0].Id;
                }

                if (result.DataExtra.StudyLevelTermList != null)
                    $scope.StudyLevelTermList = result.DataExtra.StudyLevelTermList;

                if (result.DataExtra.DeptBatchList != null)
                    $scope.DeptBatchList = result.DataExtra.DeptBatchList;

                $scope.semesterFilter();

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Bulk Course Offering! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Bulk Course Offering! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.onChangeProgramGetCurriculum = function() {
        $http.get($scope.onChangeProgramGetCurriculumUrl, {
            params: { "programId": $scope.selectedProgramId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;

                if (result.DataExtra.CurriculumList != null) {
                    $scope.CurriculumList = result.DataExtra.CurriculumList;
                    $scope.selectedCurriculumId = result.DataExtra.CurriculumList[0].Id;
                }

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load Curriculum data for Bulk Course Offering! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load Curriculum data for Bulk Course Offering! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    }

    $scope.saveBulkCourseOffering = function () {
        if (!$scope.IsCourseOfferAble()) {
            alertError("Please Select At least One Course");
            return;
        }

        bootbox.confirm("Selected Courses will be offered in the Semester "+$scope.SelectedSemester.Name +". Are you sure?",
            function(ok) {
                if (ok) {
                    $http.post($scope.saveBulkCourseOfferingUrl + "/", $scope.OfferAbleCourseList).
                        success(function (result, status) {
                            if (!result.HasError) {
                                $scope.HasError = false;
                                alertSuccess("All Course Offered Successfully");

                                window.location = $scope.offerCourseUrl +
                                    "?semesterId=" +
                                    $scope.selectedSemesterId +
                                    "&programId=" +
                                    $scope.selectedProgramId +
                                    "&levelTermId=" +
                                    $scope.selectedLevelTermId;
                            } else {
                                $scope.HasError = true;
                                $scope.ErrorMsg = "Unable to save Bulk Course Offering! " + result.Errors.toString();
                                alertError($scope.ErrorMsg);
                            }
                        }).
                        error(function (result, status) {
                            $scope.HasError = true;
                            $scope.ErrorMsg = "Unable to save Bulk Course Offering! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                            alertError($scope.ErrorMsg);
                        });
                }
            });
            
    };


    //======Custom property and Functions Start=======================================================  

    $scope.semesterFilter = function () {
        $scope.SelectedSemester = $filter('filter')($scope.SemesterList, { Id: $scope.selectedSemesterId }, true)[0];

        if ($scope.SelectedSemester != null) {
            $scope.SelectedSemesterDurationEnumId = $scope.SelectedSemester.SemesterDurationEnumId;
        }
    }

    $scope.selectAll = function ($event) {
        var checkbox = $event.target;
        for (var i = 0; i < $scope.OfferAbleCourseList.length; i++) {
            var entity = $scope.OfferAbleCourseList[i];
            entity.IsSelected = checkbox.checked;
        }
        $scope.IsCourseOfferAble();
    };

    $scope.IsCourseOfferAble = function () {
        var selectedCount = 0;
        for (var i = 0; i < $scope.OfferAbleCourseList.length; i++) {
            var entity = $scope.OfferAbleCourseList[i];
            if (entity.IsSelected) {
                selectedCount++;
                break;
            }
        }
        if (selectedCount > 0) {
            $scope.IsOfferAble = true;
            return true;
        } else {
            $scope.IsOfferAble = false;
            $scope.IsConfirmed = false;
            return false;
        }
        
    }

    $scope.addSectionCount = function () {
        var count = 0;
        for (var i = 0; i < $scope.OfferAbleCourseList.length; i++) {
            var entity = $scope.OfferAbleCourseList[i];
            entity.SectionCount += 1;
            if (entity.SectionCount > 1) {
                count++;
            }
        }

    }

    $scope.minusSectionCount = function () {
        var count = 0;
        for (var i = 0; i < $scope.OfferAbleCourseList.length; i++) {
            var entity = $scope.OfferAbleCourseList[i];
            if (entity.SectionCount > 1) {
                entity.SectionCount -= 1;
            }

            if (entity.SectionCount == 1) {
                count++;
            }
        }

    }
    //======Custom Variabales goes hare=====

    $scope.Init = function (
        semesterId
        , programId
        , levelTermId
        , batchId
        , getBulkCourseOfferingExtraDataUrl
        , onChangeProgramGetCurriculumUrl
        , getOfferAbleCourseListUrl
        , saveBulkCourseOfferingUrl
        , offerCourseUrl
    ) {
        $scope.selectedSemesterId = semesterId;
        $scope.selectedProgramId = parseInt(programId);
        $scope.selectedLevelTermId = parseInt(levelTermId);
        $scope.SelectedBatchId = parseInt(batchId);

        if ($scope.SelectedBatchId <= 0) {
            $scope.SelectedBatchId = null;
        }

        $scope.getBulkCourseOfferingExtraDataUrl = getBulkCourseOfferingExtraDataUrl;
        $scope.onChangeProgramGetCurriculumUrl = onChangeProgramGetCurriculumUrl;
        $scope.getOfferAbleCourseListUrl = getOfferAbleCourseListUrl;
        $scope.saveBulkCourseOfferingUrl = saveBulkCourseOfferingUrl;
        $scope.offerCourseUrl = offerCourseUrl;
        //$scope.loadPage(BulkCourseOfferingId);

        $scope.loadBulkCourseOfferingExtraData();
    };

    //======Other tabale's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 
    $scope.addNewClass = function (index, obj) {
        var copyObj = angular.copy(obj);
        //go the index,remove 0 elements and then add 'obj' to it
        $scope.OfferAbleCourseList.splice(index+1, 0, copyObj);
    }
    //======Custom property and Functions End========================================================== 
});

