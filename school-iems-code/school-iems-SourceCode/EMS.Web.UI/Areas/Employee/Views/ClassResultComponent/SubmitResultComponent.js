
//File:Academic_Semester List Anjular  Controller
emsApp.controller('ClassResultComponentCtrl', function ($scope, $http, $filter) {
    $scope.ResultComponentList = [];

    $scope.ErrorMsg = "";
    $scope.HasError = false;

    $scope.loadPage = function () {
        $scope.getClassComponentResultSubmitByClassId();
    }

    $scope.getClassComponentResultSubmitByClassId = function () {

        $http.get($scope.getClassComponentResultSubmitByClassIdUrl, {
            params: { "classId": $scope.ClassId, "componentSettingsId": $scope.componentSettingsId }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.ResultComponentList = result.Data;
                $scope.ComponentSetting = result.DataExtra.ComponentSetting;
                $scope.SemesterLevelTerm = result.DataExtra.SemesterLevelTerm;
                $scope.AttendanceTypeEnumList = result.DataExtra.AttendanceTypeEnumList;
                $scope.HasBreakDown = result.DataExtra.HasBreakDown;
                //alertSuccess("All Marks reloaded from server.");
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
    $scope.IsBusySaveResultComponent = false;
    $scope.saveResultComponentListByClassId = function () {
        console.log($scope.saveResultComponentListByClassIdUrl);
        if ($scope.ResultComponentList == null && $scope.ResultComponentList.length <= 0)
        { return; }

        if ($scope.IsBusySaveResultComponent) {
            alertWarning("Please Wait! Another process running.");
            return;
        }
        $scope.IsBusySaveResultComponent = true;


        $http.post($scope.saveResultComponentListByClassIdUrl + "/", $scope.ResultComponentList).
            success(function (result) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    //$scope.ResultComponentList = result.Data;
                    //console.log($scope.ResultSettingsList);
                    alertSuccess("Mark saved successfully!");

                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "" + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
                $scope.IsBusySaveResultComponent = false;
            }).error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to save marks! " + "Status: " + status.toString() + ". " + result.toString();
                alertError($scope.ErrorMsg);
                $scope.IsBusySaveResultComponent = false;
            });
    };
    $scope.saveLockResultComponentListByClassId = function () {

        if ($scope.ResultComponentList == null && $scope.ResultComponentList.length <= 0)
        { return; }

        bootbox.confirm("Are you sure you want to confirm & lock this Mark Sheet?", function (yes) {
            if (yes) {

                $http.post($scope.saveLockResultComponentListByClassIdUrl + "/", $scope.ResultComponentList).
                    success(function (result) {
                        if (!result.HasError) {
                            $scope.HasError = false;
                            // $scope.ResultComponentList = result.Data;
                            //console.log($scope.ResultSettingsList);
                            $scope.ComponentSetting.IsLockedExaminer = true;
                            alertSuccess("Mark saved and locked successfully!");

                        } else {
                            $scope.HasError = true;
                            $scope.ErrorMsg = "Unable to Lock Marks! " + result.Errors.toString();
                            alertError($scope.ErrorMsg);
                        }

                    }).error(function (result, status) {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Unable to Lock result data! " + "Status: " + status.toString() + ". " + result.toString();
                        alertError($scope.ErrorMsg);

                    });
            }
        });
    };
    $scope.changeState = function (row) {
        if (isNaN(row.TmpMark) || row.TmpMark==='') {
            ///log(row.TmpMark)
            row.AttendanceTypeEnumId = 1;
        }
        else {
            //log(row.TmpMark)
            row.AttendanceTypeEnumId = 0;
        }
    };
    $scope.selectAll = function ($event) {
        var checkbox = $event.target;
        for (var i = 0; i < $scope.ResultComponentList.length; i++) {
            var entity = $scope.ResultComponentList[i];
            entity.User_StudentJson.IsSelected = checkbox.checked;
        }
    };
    $scope.getGenerateResultFromBreakdownByClassId = function () {
        if ($scope.IsBusyGenerateResultFromBreakdown) {
            alertWarning("Please Wait! Another process running.");
            return;
        }
        $scope.IsBusyGenerateResultFromBreakdown = true;

        $http.get($scope.getGenerateResultFromBreakdownByClassIdUrl, {
            params: { "classId": $scope.ClassId, "componentSettingsId": $scope.componentSettingsId }
        }).success(function (result) {
            $scope.IsBusyGenerateResultFromBreakdown = false;
            if (!result.HasError) {
                $scope.ResultComponentList = result.Data;
                $scope.ComponentSetting = result.DataExtra.ComponentSetting;
                alertSuccess("Auto Generate Result From Breakdown Success.");
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to generate result from breakdown! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }

        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to generate result from breakdown! " + "Status: " + status.toString() + ". " + result.Message.toString();
            alertError($scope.ErrorMsg);
            $scope.IsBusyGenerateResultFromBreakdown = false;

        });
    };
    $scope.getAttendanceMarkFromAttendanceSystemByComponentId = function () {
        bootbox.confirm("Are you Sure, this will auto generate Marks from Attendance System and replace current Mark Sheet with generated Attendance Marks (according to Attendance Marking Policy)?", function (yes) {
            if (yes) {
                $http.get($scope.getGenerateAttendanceMarkByComponentSettingIdUrl, {
                    params: { "classId": $scope.ClassId, "componentSettingsId": $scope.componentSettingsId }
                }).success(function (result) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        $scope.ResultComponentList = result.Data;
                        $scope.ComponentSetting = result.DataExtra.ComponentSetting;
                        //alertSuccess("All Marks reloaded from server.");
                    } else {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "" + result.Errors.toString();
                        alertError($scope.ErrorMsg);
                    }
                }).error(function (result, status) {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to generate Attendance Mark from Attendance System! " + "Status: " + status.toString() + ". " + result.Message.toString();
                    alertError($scope.ErrorMsg);

                });
            }
        });
    };

    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====
    $scope.Init = function (
        classId
        , componentSettingsId
        , getClassComponentResultSubmitByClassIdUrl
        , saveResultComponentListByClassIdUrl
        , saveLockResultComponentListByClassIdUrl
        , getGenerateAttendanceMarkByComponentSettingIdUrl
        , getGenerateResultFromBreakdownByClassIdUrl
        ) {
        $scope.ClassId = classId;
        $scope.componentSettingsId = componentSettingsId;
        $scope.getClassComponentResultSubmitByClassIdUrl = getClassComponentResultSubmitByClassIdUrl;
        $scope.saveResultComponentListByClassIdUrl = saveResultComponentListByClassIdUrl;
        $scope.saveLockResultComponentListByClassIdUrl = saveLockResultComponentListByClassIdUrl;
        $scope.getGenerateAttendanceMarkByComponentSettingIdUrl = getGenerateAttendanceMarkByComponentSettingIdUrl;
        $scope.getGenerateResultFromBreakdownByClassIdUrl = getGenerateResultFromBreakdownByClassIdUrl;

        $scope.loadPage();
    };
    $scope.validateInput = function (obj, maxnumber) {

        //console.log(maxnumber);
        //if (!obj.TotalMark ) {
        //    return;
        //}
        //if (!obj.TotalMark && obj.TotalMark<0) {
        //    obj.TotalMark = 0;;
        //    return;
        //}

        //if (obj.TotalMark > maxnumber) {
        //    obj.TotalMark = maxnumber;
        //}

    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



