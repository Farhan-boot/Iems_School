
//File:Academic_Semester List Anjular  Controller
emsApp.controller('ClassResultCtrl', function ($scope, $http, $filter) {
    $scope.StudentList = [];
   

    $scope.ErrorMsg = "";
    $scope.HasError = false;

    $scope.loadPage = function () {
        $scope.getClassResultByClassId();
    }

    $scope.getClassResultByClassId = function () {

        $http.get($scope.getClassResultByClassIdUrl, {
            params: { "classId": $scope.ClassId, "examType": $scope.ExamType }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.ClassResult = result.Data;
                log(result.Data);
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
    $scope.IsBusyGenerateClassResult = false;
    $scope.getGenerateClassResultByClassId = function () {
        // console.log($scope.SelectedClass);
        //for Faculty
        if ($scope.IsBusyGenerateClassResult) {
            alertWarning("Please Wait! Another process running.");
            return;
        }
        $scope.IsBusyGenerateClassResult = true;
        $http.get($scope.getGenerateClassResultByClassIdUrl, {
            params: { "id": $scope.ClassId, "examType": $scope.ExamType }
        }).success(function (result) {
            $scope.IsBusyGenerateClassResult = false;
            if (!result.HasError) {
                $scope.HasError = false;
               
                //$scope.ResultComponentSettingsList = result.Data;
                //$scope.ResultComponentSettingSectionAB = result.DataExtra.SectionAB;
                // console.log($scope.ResultSettingsList);
                //$scope.ClassResult = result.Data;
                $scope.getClassResultByClassId();
                //log($scope.ClassResult);
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
    $scope.getLockClassResultByClassId = function () {
        bootbox.confirm("Are you sure you want to confirm & lock Final Grade Sheet?", function (yes) {
            if (yes) {
                $http.get($scope.getLockClassResultByClassIdUrl, {
                    params: { "classId": $scope.ClassId, "examType": $scope.ExamType }
                }).success(function (result) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        $scope.ClassResult.IsLocked = true;
                        $scope.getClassResultByClassId();
                        // $scope.ClassResult = result.Data;
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


    $scope.selectAll = function ($event) {
        var checkbox = $event.target;
        for (var i = 0; i < $scope.ClassResult.DataBag.StudentList.length; i++) {
            var entity = $scope.ClassResult.DataBag.StudentList[i];
            entity.IsSelected = checkbox.checked;
        }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====
    $scope.Init = function (
         classId
        , examType
        , getClassResultByClassIdUrl
        , getGenerateClassResultByClassId
        , getLockClassResultByClassIdUrl
        ) {
        $scope.ExamType = examType;
        $scope.ClassId = classId;

        $scope.getClassResultByClassIdUrl = getClassResultByClassIdUrl;
        $scope.getGenerateClassResultByClassIdUrl = getGenerateClassResultByClassId;
        $scope.getLockClassResultByClassIdUrl = getLockClassResultByClassIdUrl;

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



