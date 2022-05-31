emsApp.controller('SemesterManagerCtrl', function ($scope, $http, $filter) {
    $scope.SemesterList = [];
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;
    $scope.SearchBySemesterName = "";
    $scope.ErrorMsg = "";
    $scope.Semester = [];
    $scope.SemesterStatusEnumList = [];

    $scope.selectedSemesterIndex = 0;

    $scope.Init = function (getSemesterByIdUrl, getSemesterListUrl, getSemesterDataExtraUrl, saveSemesterUrl, deleteSemesterByIdUrl) {

        $scope.getSemesterByIdUrl = RootApiUrl + getSemesterByIdUrl;
        $scope.getSemesterListUrl = RootApiUrl + getSemesterListUrl;
        $scope.getSemesterDataExtraUrl = RootApiUrl + getSemesterDataExtraUrl;
        $scope.saveSemesterUrl = RootApiUrl + saveSemesterUrl;
        $scope.deleteSemesterByIdUrl = RootApiUrl + deleteSemesterByIdUrl;
        $scope.getSemesterList($scope.SearchBySemesterName, $scope.SearchBySemesterCode, $scope.PageSize, $scope.PageNo);
        $scope.getSemesterDataExtra();

    };
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.search();
    };
    $scope.changePageNo = function () {
        $scope.search();
    };
    $scope.search = function () {
        $scope.getSemesterList();
    };

    $scope.getNewSemester = function () {
        $scope.getSemesterById(0);
    };
    $scope.getSemesterById = function (SemesterId) {
        if (SemesterId != null && SemesterId !== '')
            $scope.SemesterId = SemesterId;
        else return;

        $http.get($scope.getSemesterByIdUrl, {
            params: { "id": $scope.SemesterId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.Semester = result.Data;
                updateUrlQuery('id', $scope.Semester.Id);
                //$scope.onAfterGetSemesterById(result);
                $scope.updateSemesterDuration();
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Semester! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Semester! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.refreshSemester = function () {
        $scope.getSemesterList();
    };
    $scope.searchBySemesterId = function () {
        if ($scope.SearchBySemesterId == null || $scope.SearchBySemesterId < 0) {
            toastr.error("Enter SemesterId! ", "Oops. Something went wrong. ", { positionClass: "toast-top-center", timeOut: 10000, closeButton: true });
        } else {
            $scope.getSemesterById($scope.SearchBySemesterId);
        }
    };
    /*Get Semester*/
    $scope.getSemesterList = function () {

        $http.get($scope.getSemesterListUrl, {
            params: {
                "textkey": $scope.SearchBySemesterName,
                "code": $scope.SearchBySemesterCode,
                "pageSize": $scope.PageSize,
                "pageNo": $scope.PageNo,
                "semesterDurationEnumId": $scope.SemesterDurationEnumId
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.SemesterList = result.Data;
                $scope.editSemester($scope.selectedSemesterIndex);
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;

            } else {

                toastr.error("Please try again later to get Data! " + result.Errors.toString(), "Oops. Something went wrong. ", { positionClass: "toast-top-center", timeOut: 10000, closeButton: true });
            }
        }).error(function (data, status) {

            toastr.error("Please try again later to get Data!", "Oops. Something went wrong. ", { positionClass: "toast-top-center", timeOut: 10000, closeButton: true });
        });
    };
    $scope.getSemesterDataExtra = function () {

        $http.get($scope.getSemesterDataExtraUrl)
            .success(function (result) {
                if (!result.HasError) {
                    if (result.DataExtra.StatusEnumList != null)
                        $scope.StatusEnumList = result.DataExtra.StatusEnumList;

                    $scope.ProgramTypeEnumList = result.DataExtra.ProgramTypeEnumList;

                    //$scope.SemesterTypeEnumList = result.DataExtra.SemesterTypeEnumList;
                    //$scope.DepartmentList = result.DataExtra.DepartmentList;
                    //$scope.StudyLevelList = result.DataExtra.StudyLevelList;
                    $scope.EnabledRegistrationTypeEnumList = result.DataExtra.EnabledRegistrationTypeEnumList;
                    if (result.DataExtra.StudyTermList != null)
                        $scope.StudyTermList = result.DataExtra.StudyTermList;

                    if (result.DataExtra.SemesterDurationList != null)
                        $scope.SemesterDurationList = result.DataExtra.SemesterDurationList;

                    if (result.DataExtra.MonthEnumList != null)
                        $scope.MonthEnumList = result.DataExtra.MonthEnumList;

                } else {

                    toastr.error("Please try again later to get Data! " + result.Errors.toString(), "Oops. Something went wrong. ", { positionClass: "toast-top-center", timeOut: 10000, closeButton: true });
                }
            })
            .error(function (data, status) {

                toastr.error("Please try again later to get Data!", "Oops. Something went wrong. ", { positionClass: "toast-top-center", timeOut: 10000, closeButton: true });
            });
    };

    /*Edit Semester*/
    $scope.editSemester = function ($index) {
        $scope.Semester = $scope.SemesterList[$index];
        $scope.selectedSemesterIndex = $index;
    };
    /*Save Semester*/
    $scope.saveSemester = function () {
        var msg = "";
        if ($scope.Semester.Name == null || $scope.Semester.Name === "") {
            msg += "Name can't be blank. ";
        }
        if (msg === "") {
            console.log($scope.Semester);
            $http.post($scope.saveSemesterUrl + "/", $scope.Semester).
                success(function (result, status) {
                    if (result.HasError) {
                        $scope.error = true;
                        msg += result.Errors.toString();
                        alertError(msg);
                        $scope.HasError = true;
                        $scope.ErrorMsg = msg;
                    } else {
                        msg += "Successfully Saved!";
                        toastr.success(msg, 'Success', { positionClass: 'toast-top-center', timeOut: 10000, closeButton: true });
                        $scope.Obj = result.Data;
                        $scope.refreshSemester();
                    }

                }).
                error(function (result, status) {
                    $scope.error = true;
                    msg += "Please try again later to save!!!" + result.Errors.toString();
                    toastr.error(msg, 'Error', { positionClass: 'toast-top-center', timeOut: 10000, closeButton: true });

                });
        } else {
            msg += "Please provide all the required information! ";
            toastr.warning(msg, 'Warning', { positionClass: 'toast-top-center', timeOut: 10000, closeButton: true });
        }
        $scope.messages = msg;
    };
    /*Delete Semester*/
    $scope.deleteSemesterById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteSemesterByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.SemesterList.indexOf(obj);
                        $scope.SemesterList.splice(i, 1);
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

    $scope.updateSemesterDuration = function () {
        var selectedStudyTerm = $scope.StudyTermList.find(x => x.Id === $scope.Semester.TermId);
        if (selectedStudyTerm != null) {
            $scope.Semester.SemesterDurationEnumId = selectedStudyTerm.SemesterDurationEnumId;
        } else {
            $scope.Semester.SemesterDurationEnumId = 0;
        }

        $scope.updateSemesterName();
    }

    $scope.updateSemesterName = function () {
        var selectedStudyTerm = $scope.StudyTermList.find(x => x.Id === $scope.Semester.TermId);

        var studyTermsName = selectedStudyTerm.Name.split(' ');

        $scope.Semester.Name = studyTermsName[0] + " ";

        if ($scope.Semester.Year != null && $scope.Semester.Year != '') {
            $scope.Semester.Name += $scope.Semester.Year + " ";
        }

        $scope.Semester.Name += "(" + $scope.Semester.SemesterDurationEnumId + "M)";

        $scope.updateAdmissionCircularNameAndFinalTermExam();
    }

    $scope.updateAdmissionCircularNameAndFinalTermExam = function () {
        $scope.Semester.AdmExamName = "Admission " + $scope.Semester.Name;
        $scope.Semester.FinalTermExamName = "Final Exam " + $scope.Semester.Name;
        $scope.Semester.FinalTermExamShortName = "Final Exam " + $scope.Semester.Name;
        $scope.Semester.OfficialExamName = "Exam";

        //if ($scope.Semester.AdmExamName == null || $scope.Semester.AdmExamName == '')
        //    $scope.Semester.AdmExamName = "Admission " + $scope.Semester.Name;

        //if ($scope.Semester.FinalTermExamName == null || $scope.Semester.FinalTermExamName == '')
        //    $scope.Semester.FinalTermExamName = "Final Exam " + $scope.Semester.Name;

        //if ($scope.Semester.FinalTermExamShortName == null || $scope.Semester.FinalTermExamShortName == '')
        //    $scope.Semester.FinalTermExamShortName = "Final Exam " + $scope.Semester.Name;

        //if ($scope.Semester.OfficialExamName == null || $scope.Semester.OfficialExamName == '')
        //    $scope.Semester.OfficialExamName = "Exam";
    }
});