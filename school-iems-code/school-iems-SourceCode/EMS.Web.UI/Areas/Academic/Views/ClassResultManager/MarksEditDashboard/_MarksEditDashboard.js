
//File:Academic_Semester List Anjular  Controller
emsApp.controller('MarksEditDashboardCtrl', function ($scope, $http, $filter, Upload, $timeout) {

    $scope.ClassId = '0';

    $scope.ResultComponentSettingsList = [];
    $scope.ResultSettingSectionAB = [];

    $scope.ErrorMsgMarksPane = "";
    $scope.HasErrorMarksPane = false;

    $scope.Init = function (
            classId
            , getResultSettingsByClassIdUrl
            , getDefaultMarkDistributionByClassIdUrl
            , getLockUnlockComponentSettingByIdUrl
            , getLockUnlockBreakdownSettingByIdUrl
        ,getLockUnlockComponentScrutinizergByIdUrl
        , getLockUnlockClassResultSettingByIdUrl
        , getDeleteAllClassResultByResultClassSettingIdUrl
         , getDeleteResultClassSettingByIdUrl
            , getDeleteAllMarksByComponentSettingIdUrl
            , getDeleteComponentSettingByIdUrl
            , getDeleteAllMarksByBreakdownSettingIdUrl
            , getDeleteBreakdownSettingByIdUrl
            , getDeleteAllGradeComponentBreakdownMarksByClassIdUrl
            , getDeleteAllMarkDistributionByClassIdUrl
        ) {
        $scope.ClassId = classId;
        $scope.getResultSettingsByClassIdUrl = getResultSettingsByClassIdUrl;
        $scope.getDefaultMarkDistributionByClassIdUrl = getDefaultMarkDistributionByClassIdUrl;
        $scope.getLockUnlockComponentSettingByIdUrl = getLockUnlockComponentSettingByIdUrl;
        $scope.getLockUnlockBreakdownSettingByIdUrl = getLockUnlockBreakdownSettingByIdUrl;
        $scope.getLockUnlockComponentScrutinizergByIdUrl = getLockUnlockComponentScrutinizergByIdUrl;
        $scope.getLockUnlockClassResultSettingByIdUrl = getLockUnlockClassResultSettingByIdUrl;
        $scope.getDeleteAllClassResultByResultClassSettingIdUrl = getDeleteAllClassResultByResultClassSettingIdUrl;
        $scope.getDeleteResultClassSettingByIdUrl = getDeleteResultClassSettingByIdUrl;
        $scope.getDeleteAllMarksByComponentSettingIdUrl = getDeleteAllMarksByComponentSettingIdUrl;
        $scope.getDeleteComponentSettingByIdUrl = getDeleteComponentSettingByIdUrl;
        $scope.getDeleteAllMarksByBreakdownSettingIdUrl = getDeleteAllMarksByBreakdownSettingIdUrl;
        $scope.getDeleteBreakdownSettingByIdUrl = getDeleteBreakdownSettingByIdUrl;
        $scope.getDeleteAllGradeComponentBreakdownMarksByClassIdUrl = getDeleteAllGradeComponentBreakdownMarksByClassIdUrl;
        $scope.getDeleteAllMarkDistributionByClassIdUrl = getDeleteAllMarkDistributionByClassIdUrl;

        $scope.loadPage();
    };

    $scope.loadPage = function () {
        $scope.getResultSettingsByClassId();
       };
    $scope.refresh = function () {
        if ($scope.ClassId === '0') {
            $scope.ClassId = getQueryString('id');
        }
        $scope.loadPage();

    };
    $scope.getResultSettingsByClassId = function () {
        // console.log($scope.SelectedClass);

        if ($scope.ClassId === '0')
        { return; }

        //for Faculty
        $http.get($scope.getResultSettingsByClassIdUrl, {
            params: { "id": $scope.ClassId }
        }).success(function (result) {
            if (!result.HasError) {

                $scope.IsUgTheory = result.DataExtra.IsUgTheory;
                $scope.Data = result;
               
                //$scope.ResultComponentSettingsList = result.DataExtra.RegularFacultyComponent;
                //$scope.ResultComponentSettingSectionAB = result.DataExtra.RegularSectionAB;
                $scope.RegularClassSetting = result.DataExtra.RegularClassSetting;
                log($scope.RegularClassSetting);
                $scope.RegularFacultyComponent = result.DataExtra.RegularFacultyComponent;
                $scope.RegularSectionAB = result.DataExtra.RegularSectionAB;

                $scope.ReferredClassSetting = result.DataExtra.ReferredClassSetting;
                $scope.ReferredFacultyComponent = result.DataExtra.ReferredFacultyComponent;
                $scope.ReferredSectionAB = result.DataExtra.ReferredSectionAB;

                $scope.BacklogClassSetting = result.DataExtra.BacklogClassSetting;
                $scope.BacklogFacultyComponent = result.DataExtra.BacklogFacultyComponent;
                $scope.BacklogSectionAB = result.DataExtra.BacklogSectionAB;
               
            } else {
                $scope.HasErrorMarksPane = true;
                $scope.ErrorMsgMarksPane = "Unable to get Class Marks list! " + result.Errors.toString();
                //alertError($scope.ErrorMsgMarksPane);
            }

        }).error(function (result, status) {
            $scope.HasErrorMarksPane = true;
            $scope.ErrorMsgMarksPane = "Unable to get Class Marks list! " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsgMarksPane);

        });

    };
    $scope.getDefaultMarkDistributionByClassId = function () {
        if ($scope.ClassId === '0')
        { return; }
        $http.get($scope.getDefaultMarkDistributionByClassIdUrl, {
            params: { "id": $scope.ClassId }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.getResultSettingsByClassId();
            } else {
                $scope.HasErrorMarksPane = true;
                $scope.ErrorMsgMarksPane = "" + result.Errors.toString();
                alertWarning($scope.ErrorMsgMarksPane);
            }

        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to Default Mark Distribution! " + JSON.stringify(result).toString();
            //console.log(result);
            alertError($scope.ErrorMsgMarksPane);
        });
    };

    $scope.getLockUnlockClassResultSettingById = function (obj, islock) {
        if ($scope.ClassId === '0' || obj == null)
        { return; }

        $http.get($scope.getLockUnlockClassResultSettingByIdUrl, {
            params: { "id": obj.Id, "isLock": islock }
        }).success(function (result) {
            if (!result.HasError) {
                obj.IsLockedTabulator = result.Data;
                var lock = obj.IsLockedTabulator ? "Locked" : "Unlocked";
                alertSuccess(obj.Name + " " + lock + ".");
            } else {
                $scope.HasErrorMarksPane = true;
                $scope.ErrorMsgMarksPane = "" + result.Errors.toString();
                alertError($scope.ErrorMsgMarksPane);
            }

        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable Lock/Unlock! " + JSON.stringify(result).toString();
            //console.log(result);
            alertError($scope.ErrorMsgMarksPane);
        });
    };

    $scope.getLockUnlockComponentSettingById = function (obj,islock) {
        if ($scope.ClassId === '0'|| obj== null)
        { return; }
        
        $http.get($scope.getLockUnlockComponentSettingByIdUrl, {
            params: { "id": obj.Id,"isLock":islock }
        }).success(function (result) {
            if (!result.HasError) {
                obj.IsLockedExaminer = result.Data;
                var lock = obj.IsLockedExaminer ? "Locked" : "Unlocked";
                alertSuccess(obj.Name + " " + lock + ".");
            } else {
                $scope.HasErrorMarksPane = true;
                $scope.ErrorMsgMarksPane = "" + result.Errors.toString();
                alertError($scope.ErrorMsgMarksPane);
            }

        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable Lock/Unlock! " + JSON.stringify(result).toString();
            //console.log(result);
            alertError($scope.ErrorMsgMarksPane);
        });
    };

    $scope.getLockUnlockComponentScrutinizergById = function (obj, islock) {
        if ($scope.ClassId === '0' || obj == null)
        { return; }

        $http.get($scope.getLockUnlockComponentScrutinizergByIdUrl, {
            params: { "id": obj.Id, "isLock": islock }
        }).success(function (result) {
            if (!result.HasError) {
                obj.IsLockedScrutinizer = result.Data;
                var lock = obj.IsLockedScrutinizer ? "Locked" : "Unlocked";
                alertSuccess(obj.Name + " " + lock + ".");
            } else {
                $scope.HasErrorMarksPane = true;
                $scope.ErrorMsgMarksPane = "" + result.Errors.toString();
                alertError($scope.ErrorMsgMarksPane);
            }

        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable Lock/Unlock! " + JSON.stringify(result).toString();
            //console.log(result);
            alertError($scope.ErrorMsgMarksPane);
        });
    };
 
    $scope.getLockUnlockBreakdownSettingById = function (obj, islock) {
        if ($scope.ClassId === '0' || obj == null)
        { return; }

        $http.get($scope.getLockUnlockBreakdownSettingByIdUrl, {
            params: { "id": obj.Id, "isLock": islock }
        }).success(function (result) {
            if (!result.HasError) {
                obj.IsLockedFaculty = result.Data;
                var lock = obj.IsLockedFaculty ? "Locked" : "Unlocked";
                alertSuccess(obj.Name + " " + lock + ".");
            } else {
                $scope.HasErrorMarksPane = true;
                $scope.ErrorMsgMarksPane = "" + result.Errors.toString();
                alertError($scope.ErrorMsgMarksPane);
            }

        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to Lock/Unlock! " + JSON.stringify(result).toString();
            //console.log(result);
            alertError($scope.ErrorMsgMarksPane);
        });
    };

    //=============================================

    $scope.getDeleteAllClassResultByResultClassSettingId = function () {
        
        bootbox.confirm("Delete all Final Grades of this Class, Are you sure?", function (yes) {
            if (yes) {
                $http.get($scope.getDeleteAllClassResultByResultClassSettingIdUrl, {
                    params: {
                        "id": $scope.RegularClassSetting.Id
                    }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        alertSuccess("Data successfully Deleted all Final Grades of this Class!");
                    } else {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Sorry unable to Deleted all Final Grades of this Class!" + result.Errors.toString();
                        alertError($scope.ErrorMsg);
                    }
                }).error(function (result, status) {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Sorry unable to Deleted all Final Grades of this Class! Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
                    alertError($scope.ErrorMsg);
                });
            }
        });
    };

    $scope.getDeleteResultClassSettingById = function () {

        bootbox.confirm("Delete this Final Grade Component of this Class, Are you sure?", function (yes) {
            if (yes) {
                $http.get($scope.getDeleteResultClassSettingByIdUrl, {
                    params: {
                        "id": $scope.RegularClassSetting.Id
                    }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        alertSuccess("Data successfully Deleted this component of this Class!");
                    } else {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Sorry unable to Deleted this component of this Class!" + result.Errors.toString();
                        alertError($scope.ErrorMsg);
                    }
                }).error(function (result, status) {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Sorry unable to Deleted this component of this Class! Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
                    alertError($scope.ErrorMsg);
                });
            }
        });
    };

    $scope.getDeleteAllMarksByComponentSettingId = function (id) {
        bootbox.confirm("Delete this component mark of this Class, Are you sure?", function (yes) {
            if (yes) {
                $http.get($scope.getDeleteAllMarksByComponentSettingIdUrl, {
                    params: {
                        "id": id
                    }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        alertSuccess("Data successfully Deleted this component mark!");
                    } else {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Sorry unable to Deleted this component mark!" + result.Errors.toString();
                        alertError($scope.ErrorMsg);
                    }
                }).error(function (result, status) {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Sorry unable to Deleted this component mark! Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
                    alertError($scope.ErrorMsg);
                });
            }
        });
    };

    $scope.getDeleteComponentSettingById = function (id,list,index) {
        bootbox.confirm("Delete this component of this Class, Are you sure?", function (yes) {
            if (yes) {
                $http.get($scope.getDeleteComponentSettingByIdUrl, {
                    params: {
                        "id": id
                    }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        list.splice(index, 1);
                        alertSuccess("Data successfully Deleted this component!");
                    } else {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Sorry unable to Deleted this component!" + result.Errors.toString();
                        alertError($scope.ErrorMsg);
                    }
                }).error(function (result, status) {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Sorry unable to Deleted this component! Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
                    alertError($scope.ErrorMsg);
                });
            }
        });
    };

    $scope.getDeleteAllMarksByBreakdownSettingId = function (id) {
        bootbox.confirm("Delete this breakdown mark of this Class, Are you sure?", function (yes) {
            if (yes) {
                $http.get($scope.getDeleteAllMarksByBreakdownSettingIdUrl, {
                    params: {
                        "id": id
                    }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        alertSuccess("Data successfully Deleted this breakdown mark!");
                    } else {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Sorry unable to Deleted this breakdown mark!" + result.Errors.toString();
                        alertError($scope.ErrorMsg);
                    }
                }).error(function (result, status) {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Sorry unable to Deleted this breakdown mark! Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
                    alertError($scope.ErrorMsg);
                });
            }
        });
    };

    $scope.getDeleteBreakdownSettingById = function (id,list,index) {
        bootbox.confirm("Delete this breakdown of this Class, Are you sure?", function (yes) {
            if (yes) {
                $http.get($scope.getDeleteBreakdownSettingByIdUrl, {
                    params: {
                        "id": id
                    }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        list.splice(index, 1);
                        alertSuccess("Data successfully Deleted this breakdown!");
                    } else {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Sorry unable to Deleted this breakdown!" + result.Errors.toString();
                        alertError($scope.ErrorMsg);
                    }
                }).error(function (result, status) {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Sorry unable to Deleted this breakdown! Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
                    alertError($scope.ErrorMsg);
                });
            }
        });
    };

    $scope.getDeleteAllGradeComponentBreakdownMarksByClassId = function (examTypeEnumId) {

        bootbox.confirm("Delete all Final Grades, Components and Breakdown Marks of this Class, Are you sure?", function (yes) {
            if (yes) {
                $http.get($scope.getDeleteAllGradeComponentBreakdownMarksByClassIdUrl, {
                    params: {
                        "classId": $scope.ClassId,
                        "examTypeEnumId": examTypeEnumId
                    }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        alertSuccess("Data successfully Deleted all Final Grades, Components and Breakdown Marks of this Class!");
                    } else {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Sorry unable to Deleted all Final Grades, Components and Breakdown Marks of this Class!" + result.Errors.toString();
                        alertError($scope.ErrorMsg);
                    }
                }).error(function (result, status) {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Sorry unable to Deleted all Final Grades, Components and Breakdown Marks of this Class! Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
                    alertError($scope.ErrorMsg);
                });
            }
        });
    };
    
    $scope.getDeleteAllMarkDistributionByClassId = function (examTypeEnumId) {

        bootbox.confirm("Delete all mark distribution of this Class, Are you sure?", function (yes) {
            if (yes) {
                $http.get($scope.getDeleteAllMarkDistributionByClassIdUrl, {
                    params: {
                        "classId": $scope.ClassId,
                        "examTypeEnumId": examTypeEnumId
                    }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        alertSuccess("Data successfully Deleted all mark distribution of this Class!");
                        $scope.loadPage();
                    } else {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Sorry unable to Deleted all mark distribution of this Class!" + result.Errors.toString();
                        alertError($scope.ErrorMsg);
                    }
                }).error(function (result, status) {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Sorry unable to Deleted all mark distribution of this Class! Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
                    alertError($scope.ErrorMsg);
                });
            }
        });
    };
});
