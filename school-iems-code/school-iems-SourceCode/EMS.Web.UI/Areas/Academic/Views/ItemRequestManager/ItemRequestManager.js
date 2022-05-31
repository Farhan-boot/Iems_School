
//File: Aca_Curriculum Anjular  Controller
emsApp.controller('ItemRequestAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.Curriculum = [];
    $scope.CurriculumId = 0;
    $scope.ErrorMsg = "";
    $scope.HasError = false;

    //alert("test");
    //$scope.loadPage = function (CurriculumId) {
    //    if (CurriculumId != null)
    //        $scope.CurriculumId = CurriculumId;

    //    $scope.loadCurriculumExtraData($scope.CurriculumId);


    //};
    //$scope.getNewCurriculum = function () {
    //    $scope.getCurriculumById(0);
    //};
    //$scope.getCurriculumById = function (CurriculumId) {
    //    if (CurriculumId != null && CurriculumId !== '')
    //        $scope.CurriculumId = CurriculumId;
    //    else return;

    //    $http.get($scope.getCurriculumByIdUrl, {
    //        params: { "id": $scope.CurriculumId }
    //    }).success(function (result, status) {
    //        if (!result.HasError) {
    //            $scope.HasError = false;
    //            $scope.Curriculum = result.Data;
    //            updateUrlQuery('id', $scope.Curriculum.Id);
    //            $scope.onAfterGetCurriculumById(result);
    //        } else {
    //            $scope.HasError = true;
    //            $scope.ErrorMsg = "Unable to get Curriculum! " + result.Errors.toString();
    //            alertError($scope.ErrorMsg);
    //        }
    //    }).error(function (result, status) {
    //        $scope.HasError = true;
    //        $scope.ErrorMsg = "Unable to get Curriculum! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
    //        alertError($scope.ErrorMsg);
    //    });
    //};
    //$scope.loadCurriculumExtraData = function (CurriculumId) {
    //    if (CurriculumId != null)
    //        $scope.CurriculumId = CurriculumId;

    //    $http.get($scope.getCurriculumExtraDataUrl, {
    //        params: { "id": $scope.CurriculumId }
    //    }).success(function (result, status) {
    //        if (!result.HasError) {
    //            $scope.HasError = false;
    //            $scope.onAfterLoadCurriculumExtraData(result);
    //        } else {
    //            $scope.HasError = true;
    //            $scope.ErrorMsg = "Unable to load option data for Curriculum! " + result.Errors.toString();
    //            alertError($scope.ErrorMsg);
    //        }
    //    }).error(function (result, status) {
    //        $scope.HasError = true;
    //        $scope.ErrorMsg = "Unable to load option data for Curriculum! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
    //        alertError($scope.ErrorMsg);
    //    });
    //};
    //$scope.saveCurriculum = function () {
    //    if (!$scope.validateCurriculum()) {
    //        return;
    //    }
    //    $http.post($scope.saveCurriculumUrl + "/", $scope.Curriculum).
    //        success(function (result, status) {
    //            if (!result.HasError) {
    //                $scope.HasError = false;
    //                if (result.Data != null) {
    //                    $scope.Curriculum = result.Data;
    //                    updateUrlQuery('id', $scope.Curriculum.Id);
    //                }
    //                alertSuccess("Successfully saved Curriculum data!");
    //                $scope.onAfterSaveCurriculum(result);
    //            } else {
    //                $scope.HasError = true;
    //                $scope.ErrorMsg = "Unable to save Curriculum! " + result.Errors.toString();
    //                alertError($scope.ErrorMsg);
    //            }
    //        }).
    //        error(function (result, status) {
    //            $scope.HasError = true;
    //            $scope.ErrorMsg = "Unable to save Curriculum! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
    //            alertError($scope.ErrorMsg);
    //        });
    //};

    //$scope.deleteCurriculumById = function () {

    //};
    //$scope.validateCurriculum = function () {
    //    return true;
    //};
    ////======Custom property and Functions Start=======================================================  
    ////======Custom Variabales goes hare=====

    //$scope.Init = function (CurriculumId, getCurriculumByIdUrl, getCurriculumExtraDataUrl, saveCurriculumUrl, deleteCurriculumByIdUrl) {
    //    $scope.CurriculumId = CurriculumId;
    //    $scope.getCurriculumByIdUrl = getCurriculumByIdUrl;
    //    $scope.saveCurriculumUrl = saveCurriculumUrl;
    //    $scope.getCurriculumExtraDataUrl = getCurriculumExtraDataUrl;
    //    $scope.deleteCurriculumByIdUrl = deleteCurriculumByIdUrl;

    //    $scope.loadPage(CurriculumId);
    //};

    //$scope.onAfterSaveCurriculum = function (result) {
    //    //var data=result.Data;
    //};
    //$scope.onAfterGetCurriculumById = function (result) {
    //    //var data=result.Data;
    //    if ($scope.Curriculum.Id <= 0) {
    //        if ($scope.ProgramList != null) {
    //            $scope.Curriculum.ProgramId = $scope.ProgramList[0].Id;
    //        }
    //        if ($scope.GradingPolicyList != null) {
    //            $scope.Curriculum.GradingPolicyId = $scope.GradingPolicyList[0].Id;
    //        }
    //        $scope.generateName();
    //    }
    //};
    //$scope.onAfterLoadCurriculumExtraData = function (result) {
    //    //var DataExtra=result.DataExtra;
    //    //DropDown Option Tables
    //    if (result.DataExtra.GradingPolicyList != null)
    //        $scope.GradingPolicyList = result.DataExtra.GradingPolicyList;

    //    if (result.DataExtra.ProgramList != null)
    //        $scope.ProgramList = result.DataExtra.ProgramList;

    //    $scope.getCurriculumById($scope.CurriculumId);
    //};
    ////======Other tabale's get save Functions start============================================================== 

    ////======Supporting Functions start================================================================ 
    //$scope.generateName = function () {
    //    if ($scope.Curriculum.ProgramId != null || $scope.Curriculum.ProgramId > 0) {
    //        var program = $filter('filter')($scope.ProgramList, { Id: $scope.Curriculum.ProgramId })[0];
    //        $scope.Curriculum.Name = $scope.Curriculum.Year + " " + program.Name + " Syllabus";
    //        $scope.Curriculum.ShortName = $scope.Curriculum.Year + " " + program.ShortTitle + " Syllabus";
    //        $scope.Curriculum.TotalSemester = program.Semester;
    //    }
    //};
    //======Custom property and Functions End========================================================== 
});

