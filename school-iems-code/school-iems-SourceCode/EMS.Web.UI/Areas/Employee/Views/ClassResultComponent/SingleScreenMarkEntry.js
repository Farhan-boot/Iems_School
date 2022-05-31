
emsApp.controller('SingleScreenMarkEntryCtrl', function ($scope, $http, $filter) {

    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.IsBusyGenerateClassResult = false;

    $scope.IsShowStudentDue = false;
    $scope.IsDueAlreadyLoaded = false;

    $scope.loadWithStudentDue = function () {
        if ($scope.IsShowStudentDue && !$scope.IsDueAlreadyLoaded) {
            $scope.getSingleScreenMarkEntryResultByClassId();
            $scope.IsDueAlreadyLoaded = true;
        }
    }

    $scope.loadPage = function () {
        $scope.getSingleScreenMarkEntryResultByClassId();
    }

    $scope.getSingleScreenMarkEntryResultByClassId = function () {

        $http.get($scope.getSingleScreenMarkEntryResultByClassIdUrl, {
            params: {
                "classId": $scope.ClassId,
                "isShowStudentDue": $scope.IsShowStudentDue
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.SingleScreenMarkEntryJson = result.Data;
                log($scope.SingleScreenMarkEntryJson);


            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get student marks! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }

        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get student marks! " + "Status: " + status.toString() + ". " + result.Message.toString();
            alertError($scope.ErrorMsg);

        });
    };

    $scope.saveAndLockSingleScreenMarkEntryResultComponentList = function () {

        if ($scope.SingleScreenMarkEntryJson.ClassStudentListJson == null && $scope.SingleScreenMarkEntryJson.ClassStudentListJson.length <= 0)
        { return; }

        if ($scope.IsBusySaveResultComponent) {
            alertWarning("Please Wait! Another process running.");
            return;
        }
        $scope.IsBusySaveResultComponent = true;
        $http.post($scope.saveAndLockSingleScreenMarkEntryResultComponentListUrl + "/", $scope.SingleScreenMarkEntryJson).
            success(function (result) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    $scope.getSingleScreenMarkEntryResultByClassId();
                    alertSuccess("Data saved successfully!");

                } else {
                    $scope.HasError = true;
                    $scope.SingleScreenMarkEntryJson.LockRequestComponentSettingId = "";
                    $scope.ErrorMsg = "" + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
                $scope.IsBusySaveResultComponent = false;
            }).error(function (result, status) {
                $scope.HasError = true;
                $scope.SingleScreenMarkEntryJson.LockRequestComponentSettingId = "";
                $scope.ErrorMsg = "Unable to save Data! " + "Status: " + status.toString() + ". " + result.toString();
                alertError($scope.ErrorMsg);
                $scope.IsBusySaveResultComponent = false;
            });
    };


    $scope.getGenerateClassResultFromSingleScreenMarkEntryByClassId = function () {
        if ($scope.IsBusyGenerateClassResult) {
            alertWarning("Please Wait! Another process running.");
            return;
        }
        $scope.IsBusyGenerateClassResult = true;
        $http.get($scope.getGenerateClassResultFromSingleScreenMarkEntryByClassIdUrl, {
            params: { "classId": $scope.ClassId }
        }).success(function (result) {
            $scope.IsBusyGenerateClassResult = false;
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.getSingleScreenMarkEntryResultByClassId();

                alertSuccess("Final Grade Sheet generated successfully!");
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Result Sheet! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }

        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Result Sheet! " + "Status: " + status.toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);

        });

    };


    $scope.saveComponentListAndLockByComponentSettingId = function (row) {

        if ($scope.SingleScreenMarkEntryJson == null || row ==null)
        { return; }

        bootbox.confirm("Are you sure you want to confirm & lock this ("+row.Name+") Mark?", function (yes) {
            if (yes) {

                $scope.SingleScreenMarkEntryJson.LockRequestComponentSettingId = row.Id;
                $scope.saveAndLockSingleScreenMarkEntryResultComponentList();
            }
        });

        
    };

    $scope.getSingleScreenMarkEntryLockClassResultByClassId = function () {
        bootbox.confirm("Are you sure you want to Submit & Lock Final Grade Sheet?", function (yes) {
            if (yes) {
                $http.get($scope.getSingleScreenMarkEntryLockClassResultByClassIdUrl, {
                    params: { "classId": $scope.ClassId }
                }).success(function (result) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        $scope.getSingleScreenMarkEntryResultByClassId();

                        alertSuccess("Final Grade Sheet Locked Successfully.");
                    } else {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Unable to lock Final Grade Sheet! " + result.Errors.toString();
                        alertError($scope.ErrorMsg);
                    }

                }).error(function (result, status) {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to lock Final Grade Sheet! " + "Status: " + status.toString() + ". " + result.Message.toString();
                    alertError($scope.ErrorMsg);

                });
            }
        });
    };

    $scope.getSingleScreenMarkEntryDeleteAllClassResultByResultClassSettingId = function () {
        bootbox.confirm("Are you sure you want to Delete Final Grades?", function (yes) {
            if (yes) {
                $http.get($scope.getSingleScreenMarkEntryDeleteAllClassResultByResultClassSettingIdUrl, {
                    params: {
                        "id": $scope.SingleScreenMarkEntryJson.ResultClassSettingJson.Id
                        , "classId": $scope.ClassId
                    }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        $scope.getSingleScreenMarkEntryResultByClassId();
                        alertSuccess("Successfully Deleted Grade Sheet of this Class!");
                    } else {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Sorry unable to Deleted Grade Sheet of this Class!" + result.Errors.toString();
                        alertError($scope.ErrorMsg);
                    }
                }).error(function (result, status) {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Sorry unable to Deleted Grade Sheet of this Class! Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
                    alertError($scope.ErrorMsg);
                });
            }
        });
    };

    $scope.changeState = function (row) {
        if (isNaN(row.TmpMark) || row.TmpMark === '') {
            ///log(row.TmpMark)
            row.AttendanceTypeEnumId = 1;
        }
        else {
            //log(row.TmpMark)
            row.AttendanceTypeEnumId = 0;
        }
    };


    $scope.Init = function (
        classId
        , getSingleScreenMarkEntryResultByClassIdUrl
    , saveAndLockSingleScreenMarkEntryResultComponentListUrl
    , getGenerateClassResultFromSingleScreenMarkEntryByClassIdUrl
    , getSingleScreenMarkEntryLockClassResultByClassIdUrl
    , getSingleScreenMarkEntryDeleteAllClassResultByResultClassSettingIdUrl
    ) {
        $scope.ClassId = classId;
        $scope.getSingleScreenMarkEntryResultByClassIdUrl = getSingleScreenMarkEntryResultByClassIdUrl;
        $scope.saveAndLockSingleScreenMarkEntryResultComponentListUrl = saveAndLockSingleScreenMarkEntryResultComponentListUrl;
        $scope.getGenerateClassResultFromSingleScreenMarkEntryByClassIdUrl =
            getGenerateClassResultFromSingleScreenMarkEntryByClassIdUrl;
        $scope.getSingleScreenMarkEntryLockClassResultByClassIdUrl =
            getSingleScreenMarkEntryLockClassResultByClassIdUrl;

        $scope.getSingleScreenMarkEntryDeleteAllClassResultByResultClassSettingIdUrl =
            getSingleScreenMarkEntryDeleteAllClassResultByResultClassSettingIdUrl;

        $scope.loadPage();
    };



});



