emsApp.controller('CurriculumManagerCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.CurriculumPagedList = [];
    $scope.Curriculum = [];
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 20;
    $scope.PageNo = 1;
    $scope.SearchByCurriculumName = "";
    $scope.selectedCurriculumIndex = 0;
    $scope.SearchByProgramId = 0;
    $scope.CurriculumStatusEnumList = [];

    $scope.CurriculumCourseList = [];
    $scope.CurriculumCourse = [];
    $scope.totalCurriculumCourseItems = 0;
    $scope.totalCurriculumCourseServerItems = 0;
    $scope.CurriculumCoursePageSize = 20;
    $scope.CurriculumCoursePageNo = 1;
    $scope.SearchByCurriculumCourseName = "";
    $scope.selectedCurriculumCourseIndex = 0;
    $scope.SearchByCurriculumId = 0;



    $scope.Init = function (
        getCurriculumByIdUrl,
        getCurriculumListUrl,
        getCurriculumDataExtraUrl,
        getCurriculumCourseByIdUrl,
        getCurriculumCourseListUrl,
        getCurriculumCourseDataExtraUrl,
        saveCurriculumUrl,
        saveCurriculumCourseUrl,
        deleteCurriculumByIdUrl,
        deleteCurriculumCourseByIdUrl) {

        $scope.getCurriculumByIdUrl = RootApiUrl + getCurriculumByIdUrl;
        $scope.getCurriculumListUrl = RootApiUrl + getCurriculumListUrl;
        $scope.getCurriculumDataExtraUrl = RootApiUrl + getCurriculumDataExtraUrl;
        $scope.getCurriculumCourseByIdUrl = RootApiUrl + getCurriculumCourseByIdUrl;
        $scope.getCurriculumCourseListUrl = RootApiUrl + getCurriculumCourseListUrl;
        $scope.getCurriculumCourseDataExtraUrl = RootApiUrl + getCurriculumCourseDataExtraUrl;
        $scope.saveCurriculumUrl = RootApiUrl + saveCurriculumUrl;
        $scope.saveCurriculumCourseUrl = RootApiUrl + saveCurriculumCourseUrl;
        $scope.deleteCurriculumByIdUrl = RootApiUrl + deleteCurriculumByIdUrl;
        $scope.deleteCurriculumCourseByIdUrl = RootApiUrl + deleteCurriculumCourseByIdUrl;

        $scope.getCurriculumDataExtra();
        $scope.getCurriculumCourseDataExtra();
        $scope.SearchByCurriculumId = $scope.CurriculumList[0].Id;
        $scope.getCurriculumList();
        $scope.getCurriculumCourseList();

    };

    /*Curriculum*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.searchCurriculum();
    };
    $scope.changePageNo = function () {
        $scope.searchCurriculum();
    };
    $scope.searchCurriculum = function () {
        $scope.getCurriculumList();
    };
    $scope.getNewCurriculum = function () {
        $scope.getCurriculumById(0);
    };
    $scope.getCurriculumById = function (CurriculumId) {
        if (CurriculumId != null && CurriculumId !== '')
            $scope.CurriculumId = CurriculumId;
        else return;

        $http.get($scope.getCurriculumByIdUrl, {
            params: { "id": $scope.CurriculumId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.Curriculum = result.Data;
                updateUrlQuery('id', $scope.Curriculum.Id);
                $scope.onAfterGetCurriculumById(result);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Curriculum! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Curriculum! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.refreshCurriculum = function () {
        $scope.getCurriculumList();
    };
    $scope.getCurriculumList = function () {

        if ($scope.SearchByProgramId <= 0 || $scope.SearchByProgramId == null) {
            $scope.SearchByProgramId = 0;
        }
        $http.get($scope.getCurriculumListUrl, {
            params: {
                "textkey": $scope.SearchByCurriculumName,
                "programId": $scope.SearchByProgramId,
                "pageSize": $scope.PageSize,
                "pageNo": $scope.PageNo
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.CurriculumPagedList = result.Data;
                $scope.editCurriculum($scope.selectedCurriculumIndex);
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Curriculum! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Curriculum! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getCurriculumDataExtra = function () {

        $http.get($scope.getCurriculumDataExtraUrl)
            .success(function (result) {
                if (!result.HasError) {
                    $scope.CurriculumTypeEnumList = result.DataExtra.CurriculumTypeEnumList;
                    $scope.DepartmentList = result.DataExtra.DepartmentList;
                    $scope.ProgramList = result.DataExtra.ProgramList;
                    $scope.GradingPolicyList = result.DataExtra.GradingPolicyList;

                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to get Curriculum Data Extra! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Curriculum Data Extra! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };
    $scope.editCurriculum = function ($index) {
        $scope.Curriculum = $scope.CurriculumPagedList[$index];
        $scope.changeDepartment();
        $scope.Curriculum = $scope.CurriculumPagedList[$index];
        $scope.selectedCurriculumIndex = $index;
    };
    $scope.saveCurriculum = function () {
        var msg = "";
        if ($scope.Curriculum.Name == null || $scope.Curriculum.Name === "") {
            msg += "Name can't be blank. ";
        }
        if (msg === "") {

            $http.post($scope.saveCurriculumUrl + "/", $scope.Curriculum).
                success(function (result, status) {
                    if (result.HasError) {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Unable to save Curriculum! " + result.Errors.toString();
                        alertError($scope.ErrorMsg);
                    } else {
                        $scope.HasError = false;
                        $scope.Curriculum = result.Data;
                        $scope.refreshCurriculum();
                        alertSuccess("Successfully Saved Curriculum!");
                    }
                }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to save Curriculum! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
        } else {
            msg += "Please provide all the required information! ";
            alertWarning(msg);
        }
        $scope.messages = msg;
    };
    $scope.deleteCurriculumById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteCurriculumByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.CurriculumPagedList.indexOf(obj);
                        $scope.CurriculumPagedList.splice(i, 1);
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
    $scope.onAfterSaveCurriculum = function (result) {
        //var data=result.Data;
    };
    $scope.onAfterGetCurriculumById = function (result) {
        //var data=result.Data;
        if ($scope.Curriculum.Id<=0) {
            $scope.Curriculum.DepartmentId = $scope.DepartmentList[0].Id;
            $scope.changeDepartment();
        }
    };
    $scope.changeDepartment = function () {
        if ($scope.Curriculum.DepartmentId != null || $scope.Curriculum.DepartmentId > 0) {
            $scope.FilterProgramList = $filter('filter')(
                $scope.ProgramList
                , { DepartmentId: $scope.Curriculum.DepartmentId }
            );
            $scope.Curriculum.ProgramId = $scope.FilterProgramList[0].Id;
        }
    };
    $scope.generateName = function () {
        if ($scope.Curriculum.ProgramId!=null || $scope.Curriculum.ProgramId>0){
            var program = $filter('filter')($scope.ProgramList, { Id: $scope.Curriculum.ProgramId })[0];
            $scope.Curriculum.Name = $scope.Curriculum.Session + " " + program.Name;
            $scope.Curriculum.ShortName = $scope.Curriculum.Session + " " + program.ShortTitle;
        }
    };

    /*Curriculum Course Map*/
    $scope.changeCurriculumCoursePageSize = function () {
        $scope.CurriculumCoursePageNo = 1;
        $scope.searchCurriculumCourse();
    };
    $scope.changeCurriculumCoursePageNo = function () {
        $scope.searchCurriculumCourse();
    };
    $scope.searchCurriculumCourse = function () {
        $scope.getCurriculumCourseList();
    };
    $scope.getNewCurriculumCourse = function () {
        $scope.getCurriculumCourseById(0);
    };
    $scope.getCurriculumCourseById = function (CurriculumCourseId) {
        if (CurriculumCourseId != null && CurriculumCourseId !== '')
            $scope.CurriculumCourseId = CurriculumCourseId;
        else return;

        $http.get($scope.getCurriculumCourseByIdUrl, {
            params: { "id": $scope.CurriculumCourseId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.CurriculumCourse = result.Data;
                updateUrlQuery('id', $scope.CurriculumCourse.Id);
                $scope.onAfterGetCurriculumCourseById(result);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Curriculum Course! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Curriculum Course! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.refreshCurriculumCourse = function () {
        $scope.getCurriculumCourseList();
    };
    $scope.getCurriculumCourseList = function () {

        if ($scope.SearchByCurriculumId <= 0 || $scope.SearchByCurriculumId == null) {
            $scope.SearchByCurriculumId = 0;
        }
        $http.get($scope.getCurriculumCourseListUrl, {
            params: {
                "textkey": $scope.SearchByCurriculumName,
                "curriculumId": $scope.SearchByCurriculumId,
                "pageSize": $scope.PageSize,
                "pageNo": $scope.PageNo
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.CurriculumCourseList = result.Data;
                $scope.editCurriculumCourse($scope.selectedCurriculumCourseIndex);
                $scope.totalCurriculumCourseItems = result.Count;
                $scope.totalCurriculumCourseServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Curriculum Course! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Curriculum Course! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getCurriculumCourseDataExtra = function () {
        $http.get($scope.getCurriculumCourseDataExtraUrl)
            .success(function (result) {
                if (!result.HasError) {
                    if (result.DataExtra.CourseTypeEnumList != null) {
                        $scope.CourseTypeEnumList = result.DataExtra.CourseTypeEnumList;
                        //$scope.SearchByCurriculumId = $scope.CurriculumCourseList[0].Id;
                    }
                    if (result.DataExtra.CreditTypeEnumList != null) {
                        $scope.CreditTypeEnumList = result.DataExtra.CreditTypeEnumList;
                    }
                    if (result.DataExtra.StatusEnumList != null) {
                        $scope.StatusEnumList = result.DataExtra.StatusEnumList;
                    }
                    if (result.DataExtra.CourseCategoryEnumList != null) {
                        $scope.CourseCategoryEnumList = result.DataExtra.CourseCategoryEnumList;
                    }
                    //Uncomment When Curriculum & CurriculumCourse page different
                    //if (result.DataExtra.DepartmentList != null) {
                    //    $scope.DepartmentList = result.DataExtra.DepartmentList;
                    //}
                    //if (result.DataExtra.ProgramList != null) {
                    //    $scope.ProgramList = result.DataExtra.ProgramList;
                    //}
                    if (result.DataExtra.StudyLevelTermList != null) {
                        $scope.StudyLevelTermList = result.DataExtra.StudyLevelTermList;
                    }
                    if (result.DataExtra.ElectiveGroupList != null) {
                        $scope.ElectiveGroupList = result.DataExtra.ElectiveGroupList;
                    }

                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to get Curriculum Course Data Extra! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to save Curriculum! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };
    $scope.editCurriculumCourse = function ($index) {

        $scope.CurriculumCourse = $scope.CurriculumCourseList[$index];
        $scope.selectedCurriculumCourseIndex = $index;

    };
    $scope.saveCurriculumCourse = function () {
        var msg = "";
        if ($scope.CurriculumCourse.Name == null || $scope.CurriculumCourse.Name === "") {
            msg += "Name can't be blank. ";
        }
        if (msg === "") {

            $http.post($scope.saveCurriculumCourseUrl + "/", $scope.CurriculumCourse).
                success(function (result, status) {
                    if (result.HasError) {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Unable to save Curriculum Course! " + result.Errors.toString();
                        alertError($scope.ErrorMsg);
                    } else {
                        $scope.HasError = false;
                        $scope.CurriculumCourse = result.Data;
                        $scope.refreshCurriculumCourse();
                        alertSuccess("Successfully Saved Curriculum Course!");
                    }
                }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to save Curriculum Course! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
        } else {
            msg += "Please provide all the required information! ";
            alertWarning(msg);
        }
        $scope.messages = msg;
    };
    $scope.deleteCurriculumCourseById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteCurriculumCourseByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.CurriculumCourseList.indexOf(obj);
                        $scope.CurriculumCourseList.splice(i, 1);
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
});