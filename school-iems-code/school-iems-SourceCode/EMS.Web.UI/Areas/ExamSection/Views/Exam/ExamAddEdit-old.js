
//File: Aca_Exam Anjular  Controller
emsApp.controller('ExamAddEditCtrl', function ($scope, $http, $filter, Upload, $timeout) {
    $scope.Exam = [];
    $scope.ExamId = 0;
    $scope.ErrorMsg = "";
    $scope.HasError = false;

    $scope.loadPage = function (ExamId) {
        if (ExamId != null)
            $scope.ExamId = ExamId;

        $scope.loadExamExtraData($scope.ExamId);
        $scope.getExamById($scope.ExamId);
    };
    $scope.getNewExam = function () {
        $scope.getExamById(0);

    };
    $scope.getExamById = function (ExamId) {
        if (ExamId != null && ExamId !== '')
            $scope.ExamId = ExamId;
        else return;
        $http.get($scope.getExamByIdUrl, {
            params: { "id": $scope.ExamId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.Exam = result.Data;
                updateUrlQuery('id', $scope.Exam.Id);
                $scope.onAfterGetExamById(result);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Exam! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Exam! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.loadExamExtraData = function (ExamId) {
        if (ExamId != null)
            $scope.ExamId = ExamId;

        $http.get($scope.getExamExtraDataUrl, {
            params: { "id": $scope.ExamId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.DataExtra.ProgramTypeEnumList != null)
                    $scope.ProgramTypeEnumList = result.DataExtra.ProgramTypeEnumList;
                if (result.DataExtra.ExamTypeEnumList != null)
                    $scope.ExamTypeEnumList = result.DataExtra.ExamTypeEnumList;
                $scope.onAfterLoadExamExtraData(result);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Exam! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Exam! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.saveExam = function () {
        if (!$scope.validateExam()) {
            return;
        }
        $http.post($scope.saveExamUrl + "/", $scope.Exam).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.Data != null) {
                        $scope.Exam = result.Data;
                        updateUrlQuery('id', $scope.Exam.Id);
                    }
                    alertSuccess("Successfully saved Exam data!");
                    $scope.onAfterSaveExam(result);
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to save Exam! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to save Exam! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };

    $scope.deleteExamById = function () {

    };
    $scope.validateExam = function () {
        return true;
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variabales goes hare=====

    $scope.Init = function (ExamId, getExamByIdUrl, getExamExtraDataUrl, saveExamUrl, deleteExamByIdUrl, uploadFileUrl) {
        $scope.ExamId = ExamId;
        $scope.getExamByIdUrl = getExamByIdUrl;
        $scope.saveExamUrl = saveExamUrl;
        $scope.getExamExtraDataUrl = getExamExtraDataUrl;
        $scope.deleteExamByIdUrl = deleteExamByIdUrl;
        $scope.uploadFileUrl = uploadFileUrl;

        $scope.loadPage(ExamId);
    };

    $scope.onAfterSaveExam = function (result) {
        //var data=result.Data;
    };
    $scope.onAfterGetExamById = function (result) {
        //var data=result.Data;
        /*//Child Table Bindings     */
    };
    $scope.onAfterLoadExamExtraData = function (result) {
        //var DataExtra=result.DataExtra;
        //DropDown Option Tables
        if (result.DataExtra.SemesterList != null)
            $scope.SemesterList = result.DataExtra.SemesterList;
    };
    //======Other tabale's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 
    //upload images
    $scope.uploadFile = function () {
        //alert($scope.ExamId);
        if ($scope.myFile == null) {
            alertError("Please select valid CSV file to upload!");
            return;
        }
        bootbox.confirm("Are you sure you want update Exam Roll for this examination?", function(yes) {
            if (yes) {
            Upload.upload({
                url: $scope.uploadFileUrl,
                data: {
                    file: $scope.myFile, examId: $scope.ExamId, isUpdateReg: $scope.IsUpdateReg, isReplaceExamRoll: $scope.IsReplaceExamRoll
                }
            }).then(function (response) {
                $timeout(function () {
                    $scope.result = response.data;
                    if (!$scope.result.HasError) {
                        //$scope.Obj.ApplicantPhotoUrl = $scope.result.DataExtra.UploadPath;
                        alertSuccess("Exam Roll upload successfully! " + $scope.result.DataExtra.Message.replace("\n","<br>"));
                    } else {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Exam Roll upload failed! " + $scope.result.Errors.toString();
                        alertError($scope.ErrorMsg);
                    }
                });
            }, function (response) {
                if (response.status > 0) {
                    $scope.errorMsg = response.status + ': ' + response.data;
                    $scope.ErrorMsg = "Exam Roll upload failed! " + JSON.stringify(response.data).toString() + ". " + "Status: " + JSON.stringify(response.status).toString();
                    alertError($scope.ErrorMsg);
                }
            }, function (evt) {
                $scope.progress = parseInt(100.0 * evt.loaded / evt.total);
            });
            }          
        });
    }
});

