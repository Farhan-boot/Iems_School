
//File: Aca_Class Anjular  Controller
emsApp.controller('ClassAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.Class = [];
    $scope.ClassId = 0;
    $scope.ErrorMsg = "";
    $scope.HasError = false;

    $scope.loadPage = function (ClassId) {
        if (ClassId != null)
            $scope.ClassId = ClassId;

        $scope.loadClassExtraData($scope.ClassId);
        $scope.getClassById($scope.ClassId);
    };
    $scope.getNewClass = function () {
        $scope.getClassById(0);
    };
    $scope.getClassById = function (ClassId) {
        if (ClassId != null && ClassId !== '')
            $scope.ClassId = ClassId;
        else return;

        $http.get($scope.getClassByIdUrl, {
            params: { "id": $scope.ClassId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.Class = result.Data;
                updateUrlQuery('id', $scope.Class.Id);
                $scope.onAfterGetClassById(result);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Class! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Class! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.loadClassExtraData = function (ClassId) {
        if (ClassId != null)
            $scope.ClassId = ClassId;

        $http.get($scope.getClassExtraDataUrl, {
            params: { "id": $scope.ClassId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.DataExtra.ProgramTypeEnumList != null)
                    $scope.ProgramTypeEnumList = result.DataExtra.ProgramTypeEnumList;
                if (result.DataExtra.StatusEnumList != null)
                    $scope.StatusEnumList = result.DataExtra.StatusEnumList;
                $scope.onAfterLoadClassExtraData(result);
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
    $scope.saveClass = function () {
        if (!$scope.validateClass()) {
            return;
        }
        $http.post($scope.saveClassUrl + "/", $scope.Class).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.Data != null) {
                        $scope.Class = result.Data;
                        updateUrlQuery('id', $scope.Class.Id);
                    }
                    alertSuccess("Successfully saved Class data!");
                    $scope.onAfterSaveClass(result);
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to save Class! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to save Class! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };

    $scope.deleteClassById = function () {

    };
    $scope.validateClass = function () {
        return true;
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variabales goes hare=====

    $scope.Init = function (ClassId, getClassByIdUrl, getClassExtraDataUrl, saveClassUrl, deleteClassByIdUrl) {
        $scope.ClassId = ClassId;
        $scope.getClassByIdUrl = getClassByIdUrl;
        $scope.saveClassUrl = saveClassUrl;
        $scope.getClassExtraDataUrl = getClassExtraDataUrl;
        $scope.deleteClassByIdUrl = deleteClassByIdUrl;

        $scope.loadPage(ClassId);
    };

    $scope.onAfterSaveClass = function (result) {
        //var data=result.Data;
    };
    $scope.onAfterGetClassById = function (result) {
        //var data=result.Data;

    };
    $scope.onAfterLoadClassExtraData = function (result) {
        //var DataExtra=result.DataExtra;
        //DropDown Option Tables
        if (result.DataExtra.DepartmentList != null) {
            $scope.DepartmentList = result.DataExtra.DepartmentList;
            $scope.SelectedDepartmentId = $scope.DepartmentList[0].Id;
        }
        if (result.DataExtra.ProgramList != null) {
            $scope.ProgramList = result.DataExtra.ProgramList;
            $scope.SelectedProgramId = $scope.ProgramList[0].Id;
        }
        if (result.DataExtra.CurriculumList != null) {
            $scope.CurriculumList = result.DataExtra.CurriculumList;
            $scope.SelectedCurriculumId = $scope.CurriculumList[0].Id;
        }
        if (result.DataExtra.CurriculumCourseList != null)
            $scope.CurriculumCourseList = result.DataExtra.CurriculumCourseList;

        if (result.DataExtra.SemesterList != null)
            $scope.SemesterList = result.DataExtra.SemesterList;

        if (result.DataExtra.SemesterLevelTermList != null)
            $scope.SemesterLevelTermList = result.DataExtra.SemesterLevelTermList;

        if (result.DataExtra.StudyLevelTermList != null)
            $scope.StudyLevelTermList = result.DataExtra.StudyLevelTermList;

        if (result.DataExtra.CampusList != null)
            $scope.CampusList = result.DataExtra.CampusList;

        if (result.DataExtra.DepartmentList != null)
            $scope.DepartmentList = result.DataExtra.DepartmentList;
    };
    //======Other tabale's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 
});

