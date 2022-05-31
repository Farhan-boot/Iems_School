
//File: Aca_ClassAttendanceSetting Anjular  Controller
emsApp.controller('AddEditAttendanceCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.AttendanceSetting = [];
    $scope.SettingsId = 0;
    $scope.ClassId = 0;
    
    $scope.ErrorMsg = "";
    $scope.HasError = false;

    $scope.loadPage = function () {

        $scope.getClassAttendanceSettingById();
    };
    $scope.takeNewAttendance = function () {
        $scope.SettingsId = '0';
        updateUrlQuery('settingId', $scope.SettingsId);
        $scope.getClassAttendanceSettingById();
    };
    $scope.getClassAttendanceSettingById = function () {
        //alertInfo($scope.ClassId + '-' + $scope.SettingsId)
        $http.get($scope.getAttendanceSettingByClassIdSettingIdUrl, {
            params: { "classId": $scope.ClassId, "settingsId": $scope.SettingsId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.AttendanceSetting = result.Data;
              
               
                if (result.DataExtra.ClassTypeEnumList != null)
                    $scope.ClassTypeEnumList = result.DataExtra.ClassTypeEnumList;
                if (result.DataExtra.ReasonEnumList != null)
                    $scope.ReasonEnumList = result.DataExtra.ReasonEnumList;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "" + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Class Attendance! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.saveClassAttendanceSetting = function () {
        //if (!$scope.validateClassAttendanceSetting()) {
        //    return;
        //}
        //alertInfo($scope.saveClassAttendanceSettingUrl)
        $http.post($scope.saveClassAttendanceSettingUrl + "/", $scope.AttendanceSetting).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.Data != null) {
                        $scope.AttendanceSetting = result.Data;
                        //$scope.AttendanceSetting.Id = result.Data.Id;
                        updateUrlQuery('settingId', result.Data.Id);
                        updateUrlQuery('classId', $scope.AttendanceSetting.ClassId);
                    }
                    alertSuccess("Class Attendance successfully saved!");
                    //$scope.onAfterSaveClassAttendanceSetting(result);
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "" + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to save Class Attendance! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };
    //IsAbsent
    $scope.selectAll = function ($event) {
        var checkbox = $event.target;
        for (var i = 0; i < $scope.AttendanceSetting.Aca_ClassAttendanceListJson.length; i++) {
            var entity = $scope.AttendanceSetting.Aca_ClassAttendanceListJson[i];
            entity.IsAbsent = checkbox.checked;
        }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variabales goes hare=====

    $scope.Init = function (classId, settingsId, getAttendanceSettingByClassIdSettingIdUrl, saveClassAttendanceSettingUrl) {
        $scope.ClassId = classId;
        $scope.SettingsId = settingsId;
        $scope.getAttendanceSettingByClassIdSettingIdUrl = getAttendanceSettingByClassIdSettingIdUrl;
        $scope.saveClassAttendanceSettingUrl = saveClassAttendanceSettingUrl;
        $scope.loadPage();
    };
    $scope.do = function (obj) {
        obj.setMinutes(obj.getMinutes() + 30);

        alertInfo(obj.getMinutes())
        
    }

});

