
//File:Admin_Country List Anjular  Controller
emsApp.controller('RepeaterCourseRegistrationCtrl', function ($scope, $http, $filter, cfpLoadingBar, $sce) {
    $scope.SelectedSemester = [];
    $scope.LevelTermWiseStudentList = [];

    $scope.IsRepeaterRegistrationSuccessfull = false;
    $scope.IsRegistraAble = false;

    $scope.IsAllUnselected = false;

    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;

    $scope.SelectedSemesterId = 0;
    $scope.SelectedProgramId = null;
    $scope.SelectedLevelTermId = 0;

    //===================================

    $scope.loadPage = function () {
        $scope.getRepeaterRegExtraData();
        //$scope.getPagedClassList();
    };
    $scope.getRepeaterRegExtraData = function () {
        $http.get($scope.getRepeaterRegClassDataExtraUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;

                if (result.DataExtra.SemesterList != null) {
                    $scope.SemesterList = result.DataExtra.SemesterList;
                    $scope.SelectedSemesterId = $scope.SemesterList[0].Id;
                }

                $scope.IsRepeaterRegistrationSuccessfull = false;

                if (result.DataExtra.ProgramList != null)
                    $scope.ProgramList = result.DataExtra.ProgramList;

                if (result.DataExtra.StudyLevelTermList != null)
                    $scope.StudyLevelTermList = result.DataExtra.StudyLevelTermList;

                $scope.semesterFilter();
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Class! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Class! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getRepeaterRegLevelTermWiseStudentListById = function () {
        $scope.LevelTermWiseStudentList = [];
        $scope.IsRepeaterRegistrationSuccessfull = false;
        $http.get($scope.getRepeaterRegStudentListByIdUrl, {
                params: {
                    "semesterId": $scope.SelectedSemesterId
                    , "programId": $scope.SelectedProgramId
                    , "levelTermId": $scope.SelectedLevelTermId
                }
            })
            .success(function (result) {
                if (!result.HasError) {
                    $scope.LevelTermWiseStudentList = result.Data;
                    $scope.HasError = false;
                    $scope.onChangeSelection();
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to get Repeater Student list data! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Repeater Student list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
                alertError($scope.ErrorMsg);
            });
    };

    $scope.registerRepeaterStudentList = function () {
        bootbox.confirm("Are you sure you want to Register All This Course to these Students?",
            function(yes) {
                if (yes) {
                    $http.post($scope.saveRegisterRepeaterStudentListUrl + "/", $scope.LevelTermWiseStudentList)
                        .success(function (result) {
                            $scope.LevelTermWiseStudentList = result.Data;
                            if (!result.HasError) {
                                alertSuccess("All Students Registration is Successful");
                                $scope.IsRepeaterRegistrationSuccessfull = true;
                                $scope.onChangeSelection();
                            } else {
                                $scope.HasError = true;
                                $scope.ErrorMsg = "Unable to Save Repeater Student list data! " + result.Errors.toString();
                                alertError($scope.ErrorMsg);
                            }
                        }).error(function (result, status) {
                            $scope.RegistrationJson = result.Data;
                            $scope.StudentList = $scope.RegistrationJson.StudentList;
                            log($scope.RegistrationJson)
                            $scope.RegisterStdHasError = true;
                            $scope.RegisterStdErrorMsg = "Unable to Save Repeater Student list data! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
                            alertError($scope.RegisterStdErrorMsg);
                        });
                }
            });
    };

    $scope.renderHtml = function (html_code) {
        return $sce.trustAsHtml(html_code);
    };



    //======Custom Variables goes hare=====


    $scope.Init = function (
        getRepeaterRegClassDataExtraUrl
, getRepeaterRegStudentListByIdUrl
, saveRegisterRepeaterStudentListUrl
       ) {
        $scope.getRepeaterRegClassDataExtraUrl = getRepeaterRegClassDataExtraUrl;
        /*bind extra url if need*/
        $scope.getRepeaterRegStudentListByIdUrl = getRepeaterRegStudentListByIdUrl;
        $scope.saveRegisterRepeaterStudentListUrl = saveRegisterRepeaterStudentListUrl;

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    $scope.semesterFilter = function () {
        var semObj = $filter('filter')($scope.SemesterList, { Id: $scope.SelectedSemesterId }, true)[0];
        $scope.SelectedSemester = semObj;
    }
    $scope.clearRegistration = function () {
        $scope.LevelTermWiseStudentList = [];
    }
    $scope.removeStudent = function (stdObj, levelTermObj) {
        if (stdObj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to remove this Student from Repeater registration?", function (yes) {
            if (yes) {
                var i = levelTermObj.RegistrationStudentListJson.indexOf(stdObj);
                $scope.$apply(function () {
                    levelTermObj.RegistrationStudentListJson.splice(i, 1);
                });
            }
        });
    };

    $scope.removeLevelTerm = function (levelTermObj) {
        if (levelTermObj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to remove this LevelTerm from Repeater registration?", function (yes) {
            if (yes) {
                var i = $scope.LevelTermWiseStudentList.indexOf(levelTermObj);
                $scope.$apply(function () {
                    $scope.LevelTermWiseStudentList.splice(i, 1);
                });
            }
        });
    };

    $scope.onChangeSelection = function () {
        var length = $scope.LevelTermWiseStudentList.length;
        for (var i = 0; i < length; i++) {
            for (var j = 0; j < $scope.LevelTermWiseStudentList[i].RegistrationStudentListJson.length; j++) {
                for (var k = 0; k < $scope.LevelTermWiseStudentList[i].RegistrationStudentListJson[j].RegistrationCourseListJson.length; k++) {
                    if ($scope.LevelTermWiseStudentList[i].RegistrationStudentListJson[j].RegistrationCourseListJson[k]
                        .IsSelected) {
                        $scope.IsRegistraAble = true;
                        return;
                    }
                }
            }
        }
        $scope.IsRegistraAble = false;
    };

    $scope.changeAllSelection = function () {
        var length = $scope.LevelTermWiseStudentList.length;
        for (var i = 0; i < length; i++) {
            for (var j = 0; j < $scope.LevelTermWiseStudentList[i].RegistrationStudentListJson.length; j++) {
                for (var k = 0; k < $scope.LevelTermWiseStudentList[i].RegistrationStudentListJson[j].RegistrationCourseListJson.length; k++) {
                    $scope.LevelTermWiseStudentList[i].RegistrationStudentListJson[j].RegistrationCourseListJson[k]
                        .IsSelected = $scope.IsAllUnselected;
                }
            }
        }
        $scope.IsRegistraAble = $scope.IsAllUnselected;
        $scope.IsAllUnselected = !$scope.IsAllUnselected;
    };

    $scope.clearSelectedStudent = function () {
        var selectedStudent = $filter('filter')($scope.LevelTermWiseStudentList, { IsSelected: true }, true);
        for (var i = 0; i < selectedStudent.length; i++) {
            var index = $scope.LevelTermWiseStudentList.indexOf(selectedStudent[i]);
            $scope.LevelTermWiseStudentList.splice(index, 1);
        }
    }
    //======Custom property and Functions End========================================================== 

});



