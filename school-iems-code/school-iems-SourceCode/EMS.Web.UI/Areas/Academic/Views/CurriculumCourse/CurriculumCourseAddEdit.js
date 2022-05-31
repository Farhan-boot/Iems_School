
//File: Aca_CurriculumCourse Anjular  Controller
emsApp.controller('CurriculumCourseAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.CurriculumCourse = [];
    $scope.PreRequisiteCourseList = [];
    $scope.CurriculumCourseList = [];

    $scope.CurriculumCourseId = 0;
    $scope.SelectedPreRequisiteCourseId = 0;
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.SelectedStudyLevelTermId = 0;
    $scope.SelectedDepartmentId = 0;
    $scope.SearchByTypeId = 0;
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 300;
    $scope.PageNo = 1;

    $scope.loadPage = function (CurriculumCourseId) {
        if (CurriculumCourseId != null)
            $scope.CurriculumCourseId = CurriculumCourseId;

        $scope.loadCurriculumCourseExtraData($scope.CurriculumCourseId);
    };
    $scope.getNewCurriculumCourse = function () {
        $scope.getCurriculumCourseById(0);
        $scope.SelectedCourse = null;
        $scope.DepartmentId = scope.DepartmentList[0].Id;
        $scope.changeDepartment();
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

                if (result.DataExtra.PreRequisiteCourseList != null) {
                    $scope.PreRequisiteCourseList = result.DataExtra.PreRequisiteCourseList;
                }

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
    $scope.loadCurriculumCourseExtraData = function (CurriculumCourseId) {
        if (CurriculumCourseId != null)
            $scope.CurriculumCourseId = CurriculumCourseId;

        $http.get($scope.getCurriculumCourseExtraDataUrl, {
            params: { "id": $scope.CurriculumCourseId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.DataExtra.CourseCategoryEnumList != null)
                    $scope.CourseCategoryEnumList = result.DataExtra.CourseCategoryEnumList;
                if (result.DataExtra.CreditTypeEnumList != null)
                    $scope.CreditTypeEnumList = result.DataExtra.CreditTypeEnumList;
                if (result.DataExtra.CourseTypeEnumList != null)
                    $scope.CourseTypeEnumList = result.DataExtra.CourseTypeEnumList;
                if (result.DataExtra.StatusEnumList != null)
                    $scope.StatusEnumList = result.DataExtra.StatusEnumList;
                $scope.onAfterLoadCurriculumCourseExtraData(result);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Curriculum Course! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Curriculum Course! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.saveCurriculumCourse = function () {
        if (!$scope.validateCurriculumCourse()) {
            return;
        }
        $http.post($scope.saveCurriculumCourseUrl + "/", $scope.CurriculumCourse).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.Data != null) {
                        $scope.CurriculumCourse = result.Data;
                        updateUrlQuery('id', $scope.CurriculumCourse.Id);
                    }
                    alertSuccess("Successfully saved Curriculum Course data!");
                    $scope.onAfterSaveCurriculumCourse(result);
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to save Curriculum Course! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to save Curriculum Course! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };

    $scope.deleteCurriculumCourseById = function () {

    };
    $scope.validateCurriculumCourse = function () {
        return true;
    };

    $scope.refreshCurriculumCourseList = function() {
        $http.get($scope.getAddableCurriculumListUrl, {
            params: {
                "curriculumId": $scope.CurriculumCourse.CurriculumId
                , "curriculumCourseId": $scope.CurriculumCourseId
            }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;

                $scope.CurriculumCourseList = result.Data;
                $("#addPreRequisiteCourseModal").modal('show');
                //log($scope.CurriculumCourseList);
                $scope.onAfterLoadCurriculumCourseExtraData(result);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load Pre requisite data for Curriculum Course! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load Pre requisite for Curriculum Course! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    }

    $scope.openPreRequisiteAddModal = function() {
        if ($scope.CurriculumCourseList.length > 0) {
            $("#addPreRequisiteCourseModal").modal('show');
        } else {
            $http.get($scope.getAddableCurriculumListUrl, {
                params: {
                    "curriculumId": $scope.CurriculumCourse.CurriculumId
                    , "curriculumCourseId": $scope.CurriculumCourseId
                }
            }).success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    
                    $scope.CurriculumCourseList = result.Data;
                    $("#addPreRequisiteCourseModal").modal('show');
                    //log($scope.CurriculumCourseList);
                    $scope.onAfterLoadCurriculumCourseExtraData(result);
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to load Pre requisite data for Curriculum Course! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load Pre requisite for Curriculum Course! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
        }
        
    }

    $scope.addNewPreRequisiteCourse = function() {
        $http.get($scope.savePreRequisiteCourseUrl, {
            params: {
                "courseId": $scope.CurriculumCourseId
                , "preRequisiteCourseId": $scope.SelectedPreRequisiteCourseId
            }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                log(result.Data);

                var id = result.Data;
                var obj = $scope.CurriculumCourseList.find(x=>x.Id == id);
                var i = $scope.CurriculumCourseList.indexOf(obj);
                $scope.CurriculumCourseList.splice(i, 1);

                $scope.PreRequisiteCourseList.push(result.DataExtra.PreRequisiteCourse);


                $("#addPreRequisiteCourseModal").modal('hide');
                $scope.SelectedPreRequisiteCourseId = 0;

                alertSuccess("Pre Requisite Course Added Successfully");
                $scope.onAfterLoadCurriculumCourseExtraData(result);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to add Pre requisite for Curriculum Course! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to add Pre requisite for Curriculum Course! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    }

    $scope.deletePreRequisiteCourseById = function(obj) {
        log(obj);
        bootbox.confirm("Are you sure you want to delete this data?",
            function(yes) {
                if (yes) {
                    $http.get($scope.deletePreRequisiteCourseByIdUrl, {
                        params: {
                            "id": obj.Id
                        }
                    }).success(function (result, status) {
                        if (!result.HasError) {
                            $scope.HasError = false;

                            var i = $scope.PreRequisiteCourseList.indexOf(obj);
                            $scope.PreRequisiteCourseList.splice(i, 1);

                            alertSuccess("Pre Requisite Course Deleted Successfully");
                            $scope.onAfterLoadCurriculumCourseExtraData(result);
                        } else {
                            $scope.HasError = true;
                            $scope.ErrorMsg = "Unable to Delete data for Curriculum Course! " + result.Errors.toString();
                            alertError($scope.ErrorMsg);
                        }
                    }).error(function (result, status) {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Unable to Delete for Curriculum Course! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                        alertError($scope.ErrorMsg);
                    });
                    
                }
            });

    }

    //======Custom property and Functions Start=======================================================  
    //======Custom Variabales goes hare=====

    $scope.Init = function (
        CurriculumCourseId
        , getCurriculumCourseByIdUrl
        , getCurriculumCourseExtraDataUrl
        , saveCurriculumCourseUrl
        , deleteCurriculumCourseByIdUrl
        , getPagedCourseUrl
        , getCourseByIdUrl
        , getAddableCurriculumListUrl
        , savePreRequisiteCourseUrl
        , deletePreRequisiteCourseByIdUrl
        ) {
        $scope.CurriculumCourseId = CurriculumCourseId;
        $scope.getCurriculumCourseByIdUrl = getCurriculumCourseByIdUrl;
        $scope.saveCurriculumCourseUrl = saveCurriculumCourseUrl;
        $scope.getCurriculumCourseExtraDataUrl = getCurriculumCourseExtraDataUrl;
        $scope.deleteCurriculumCourseByIdUrl = deleteCurriculumCourseByIdUrl;
        $scope.getPagedCourseUrl = getPagedCourseUrl;
        $scope.getCourseByIdUrl = getCourseByIdUrl;
        $scope.getAddableCurriculumListUrl = getAddableCurriculumListUrl;
        $scope.savePreRequisiteCourseUrl = savePreRequisiteCourseUrl;
        $scope.deletePreRequisiteCourseByIdUrl = deletePreRequisiteCourseByIdUrl;

        $scope.loadPage(CurriculumCourseId);
    };

    $scope.onAfterSaveCurriculumCourse = function (result) {
        //var data=result.Data;
    };
    $scope.onAfterGetCurriculumCourseById = function (result) {
        //var data=result.Data;
        if ($scope.CurriculumCourse != null && $scope.CurriculumCourse.Id > 0) {
            var curriculum = $filter('filter')($scope.CurriculumList, { Id: $scope.CurriculumCourse.CurriculumId },true)[0];
            $scope.FilterProgramList = $filter('filter')(
                $scope.ProgramList
                , { DepartmentId: curriculum.DepartmentId }, true
            );
            $scope.FilterCurriculumList = $filter('filter')(
                $scope.CurriculumList
                , { ProgramId: curriculum.ProgramId }, true
            );
            $scope.FilterCurriculumElectiveGroupList = $filter('filter')(
                $scope.CurriculumElectiveGroupList
                , { ProgramId: curriculum.ProgramId }, true
            );
            
            var dept = $filter('filter')($scope.DepartmentList, { Id: curriculum.DepartmentId }, true)[0];
            var i = $scope.DepartmentList.indexOf(dept);
            $scope.DepartmentId = $scope.DepartmentList[i].Id;
            $scope.SelectedDepartmentId = $scope.DepartmentList[i].Id;

            var program = $filter('filter')($scope.FilterProgramList, { Id: curriculum.ProgramId }, true)[0];
            var j = $scope.FilterProgramList.indexOf(program);
            $scope.ProgramId = $scope.FilterProgramList[j].Id;

            $scope.getCourseById($scope.CurriculumCourse.CourseId);
        }
    };
    $scope.onAfterLoadCurriculumCourseExtraData = function (result) {
        //var DataExtra=result.DataExtra;
        //DropDown Option Tables
        if (result.DataExtra.CurriculumList != null)
            $scope.CurriculumList = result.DataExtra.CurriculumList;

        if (result.DataExtra.CurriculumElectiveGroupList != null)
            $scope.CurriculumElectiveGroupList = result.DataExtra.CurriculumElectiveGroupList;

        if (result.DataExtra.StudyLevelTermList != null) {
            $scope.StudyLevelTermList = result.DataExtra.StudyLevelTermList;
            $scope.SelectedStudyLevelTermId = $scope.StudyLevelTermList[0].Id;
        }

        if (result.DataExtra.DepartmentList != null) {
            $scope.DepartmentList = result.DataExtra.DepartmentList;
            $scope.SelectedDepartmentId = $scope.DepartmentList[0].Id;
        }
        if (result.DataExtra.ProgramList != null)
            $scope.ProgramList = result.DataExtra.ProgramList;
        //get Curriculum Course
        $scope.getCurriculumCourseById($scope.CurriculumCourseId);
    };
    //======Other tabale's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 
    $scope.SearchByTypeId = 0;
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 300;
    $scope.PageNo = 1;
    $scope.searchCourseList = function () {
        $http.get($scope.getPagedCourseUrl, {
            params: {
                "textkey": $scope.SearchText
                , "pageSize": $scope.PageSize
                , "pageNo": $scope.PageNo
                , "studyLevelTermId": $scope.SelectedStudyLevelTermId
                , "departmentId": $scope.DepartmentId
                , "typeId": $scope.SearchByTypeId
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.CourseList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Base/Master Course list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Base/MasterCourse list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getCourseById = function (courseId) {
        if (courseId != null && courseId !== '')
            $scope.CourseId = courseId;
        else return;

        $http.get($scope.getCourseByIdUrl, {
            params: { "id": $scope.CourseId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.SelectedCourse = result.Data;

                var levelterm = $filter('filter')(
                    $scope.StudyLevelTermList,
                    { Id: $scope.SelectedCourse.StudyLevelTermId }, true)[0];
                var lv = $scope.StudyLevelTermList.indexOf(levelterm);
                $scope.SelectedStudyLevelTermId = $scope.StudyLevelTermList[lv].Id;
                if ($scope.SelectedCourse.IsBachelorCourse) {
                    $scope.SearchByTypeId = 0;
                }
                else if ($scope.SelectedCourse.IsMastersCourse) {
                    $scope.SearchByTypeId = 1;
                }
                else if ($scope.SelectedCourse.IsPhdCourse) {
                    $scope.SearchByTypeId = 2;
                }
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Course! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Course! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.changeDepartment = function () {
        if ($scope.DepartmentId != null || $scope.DepartmentId > 0) {
            if ($scope.CurriculumCourse.IsDepartmental) {
                $scope.CurriculumCourse.OfferedByDepartmentId = $scope.DepartmentId;
            }
            $scope.FilterProgramList = $filter('filter')(
                $scope.ProgramList
                , { DepartmentId: $scope.DepartmentId }, true
            );
            if ($scope.CurriculumCourse.Id <= 0) {
                $scope.ProgramId = $scope.FilterProgramList[0].Id;
                $scope.changeProgram();
            }
        }
    };
    $scope.changeProgram = function () {
        if ($scope.ProgramId != null || $scope.ProgramId > 0) {
            $scope.FilterCurriculumList = $filter('filter')(
                $scope.CurriculumList
                , { ProgramId: $scope.ProgramId }, true
            );
            $scope.FilterCurriculumElectiveGroupList = $filter('filter')(
                $scope.CurriculumElectiveGroupList
                , { ProgramId: $scope.ProgramId }, true
            );
            if ($scope.CurriculumCourse.CurriculumId <= 0) {
                $scope.CurriculumCourse.CurriculumId = $scope.FilterCurriculumList[0].Id;
                $scope.changeCurriculum();
            }
        }
    };
    $scope.changeCurriculum = function () {
    };
    $scope.changeSelectedCourse = function (obj) {
        $scope.SelectedCourse = obj;
        $scope.CurriculumCourse.CourseId = obj.Id;
        $scope.CurriculumCourse.Code = obj.Code;
        $scope.CurriculumCourse.Name = obj.Name;
        $scope.CurriculumCourse.CourseCategoryEnumId = obj.CourseCategoryEnumId;
        $scope.CurriculumCourse.CreditLoad = obj.CreditLoad;
        $scope.CurriculumCourse.CreditHour = obj.CreditHour;
        $scope.CurriculumCourse.StudyLevelTermId = obj.StudyLevelTermId;
        $scope.CurriculumCourse.OfferedByDepartmentId = obj.DepartmentId;
        $scope.DepartmentId = obj.DepartmentId;
        $scope.changeDepartment();
        $("#selectCourseModal").modal('hide');
    };

    //======Custom property and Functions End========================================================== 
});

