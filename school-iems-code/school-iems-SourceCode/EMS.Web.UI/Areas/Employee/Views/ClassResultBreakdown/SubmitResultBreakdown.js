
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
                $scope.ResultComponentList = result.Data;
                $scope.BreakdownSetting = result.DataExtra.BreakdownSetting;
                //$scope.Marks1stSavedByName = result.DataExtra.Marks1stSavedByName;
                $scope.SemesterLevelTerm = result.DataExtra.SemesterLevelTerm;
                $scope.AttendanceTypeEnumList = result.DataExtra.AttendanceTypeEnumList;
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

    $scope.saveResultComponentListByClassId = function () {
        console.log($scope.saveResultComponentListByClassIdUrl);
        if ($scope.ResultComponentList == null && $scope.ResultComponentList.length<=0)
        { return; }
        $http.post($scope.saveResultComponentListByClassIdUrl + "/", $scope.ResultComponentList).
            success(function (result) {
                if (!result.HasError) {
                    $scope.ResultComponentList = result.Data;
                    alertSuccess("Mark saved successfully!");
                    
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "" + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
                
            }).error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to save marks! " + "Status: " + status.toString() + ". " + result.toString();
                alertError($scope.ErrorMsg);
                
            });
    };
    $scope.saveLockResultComponentListByClassId = function () {
      
        if ($scope.ResultComponentList == null && $scope.ResultComponentList.length <= 0)
        { return; }

        bootbox.confirm("Are you sure you want to confirm & lock this Mark Sheet?", function (yes) {
            if (yes) {
                
                $http.post($scope.saveLockResultComponentListByClassIdUrl + "/", $scope.ResultComponentList).
                    success(function(result) {
                        if (!result.HasError) {
                            $scope.ResultComponentList = result.Data;
                            $scope.BreakdownSetting.IsLockedFaculty = true;
                            alertSuccess("Mark saved and locked successfully!");
                            //console.log($scope.ResultSettingsList);
                        } else {
                            $scope.HasError = true;
                            $scope.ErrorMsg = "Unable to save Marks! " + result.Errors.toString();
                            alertError($scope.ErrorMsg);
                        }
                        
                    }).error(function(result, status) {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Unable to save result data! " + "Status: " + status.toString() + ". " + result.toString();
                        alertError($scope.ErrorMsg);
                        
                    });
            }
        });
    };
    $scope.selectAll = function ($event) {
        var checkbox = $event.target;
        for (var i = 0; i < $scope.ResultComponentList.length; i++) {
            var entity = $scope.ResultComponentList[i];
            entity.User_StudentJson.IsSelected = checkbox.checked;
        }
    };
    

    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====
    $scope.Init = function (
        classId
        ,componentSettingsId
        ,getClassComponentResultSubmitByClassIdUrl
        , saveResultComponentListByClassIdUrl
        , saveLockResultComponentListByClassIdUrl
        ) {
        $scope.ClassId = classId;
        $scope.componentSettingsId = componentSettingsId;
        $scope.getClassComponentResultSubmitByClassIdUrl = getClassComponentResultSubmitByClassIdUrl;
        $scope.saveResultComponentListByClassIdUrl = saveResultComponentListByClassIdUrl;
        $scope.saveLockResultComponentListByClassIdUrl = saveLockResultComponentListByClassIdUrl;

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



