emsApp.controller('OnlineCourseRegistrationCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.ClassList = [];
    $scope.SelectedClass = [];
    $scope.SelectedClassList = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.IsRegistrationConfirm = false;
    $scope.SemesterId = 0;
    $scope.MaxCrAllowed = 0;
    $scope.RegHasError = false;
    $scope.RegErrorMsg = "";
    
    

    $scope.loadPage = function () {
        $scope.getPagedClassList();
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedClassList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedClassList();
    };
    $scope.searchClassList = function () {
        $scope.getPagedClassList();
    };
    $scope.getPagedClassList = function () {
        $scope.getClassList();
    };
    /*For Search on data end*/
    $scope.getClassList = function () {
        $http.get($scope.getPagedClassUrl, {
            params: {
                "userId": $scope.UserId
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.ClassList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
                $scope.IsRegistrationConfirm = result.DataExtra.IsRegistrationConfirm;
                $scope.RegistrationFor = result.DataExtra.UpcomingSemesterName;
                $scope.SemesterId = result.DataExtra.SemesterId;
                $scope.MaxCrAllowed = result.DataExtra.MaxCrAllowed;
                console.log($scope.MaxCrAllowed);
                $scope.RegistrationSummaryCalculate();
            } else {
                $scope.RegHasError = true;
                $scope.RegErrorMsg = result.Errors.toString();
                //alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Class list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.selectAll = function ($event) {
        var checkbox = $event.target;
        for (var i = 0; i < $scope.ClassList.length; i++) {
            var entity = $scope.ClassList[i];
            entity.IsSelected = checkbox.checked;
        }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====


    $scope.Init = function (
        userId
        ,getPagedClassUrl
        , courseAddRemoveUrl
        , confirmRegistrationUrl
        ) {
        $scope.UserId = userId;
        $scope.getPagedClassUrl = getPagedClassUrl;
        $scope.courseAddRemoveUrl = courseAddRemoveUrl;
        $scope.confirmRegistrationUrl = confirmRegistrationUrl;
        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    $scope.selectAllClasses = function () {
        $scope.SelectedClassList = [];
        for (var i = 0; i < $scope.ClassList.length; i++) {
            if ($scope.SelectAllClasses == true && $scope.ClassList[i].IsAlreadyAdded == false) {
                $scope.ClassList[i].IsSelected = true;
                $scope.SelectedClassList.push($scope.ClassList[i]);
            } else {
                $scope.ClassList[i].IsSelected = false;
                $scope.SelectedClassList.splice(i, 1);
            }
        }
    };
    $scope.selectClass = function (id) {
        var obj = $filter('filter')($scope.ClassList, { Id: id })[0];
        var index = $scope.ClassList.indexOf(obj);
        if ($scope.ClassList[index].IsSelected) {
            $scope.SelectedClassList.push($scope.ClassList[index]);
        } else {
            for (var i = 0; i < $scope.SelectedClassList.length; i++) {
                if ($scope.SelectedClassList[i].Id == $scope.ClassList[index].Id) {
                    $scope.SelectedClassList.splice(i, 1);
                }
            }
        }
    };

//    Old Code


/*    $scope.saveClasses = function () {
        var msg = "";
        if ($scope.SelectedClassList == null || $scope.SelectedClassList.length == 0) {
            msg += "Please select Course to save. ";
        }
        if (msg === "") {
            $http.post($scope.saveClassUrl + "/", $scope.SelectedClassList
            ).success(function (result, status) {
                if (result.HasError) {
                    $scope.error = true;
                    msg += result.Errors.toString();
                    toastr.error(msg, 'Oops. Something went wrong. ', { positionClass: 'toast-top-center', timeOut: 10000, closeButton: true });
                } else {
                    msg += "Successfully Saved!";
                    toastr.success(msg, 'Success', { positionClass: 'toast-top-center', timeOut: 10000, closeButton: true });
                    //$scope.SelectedClassList = [];
                    $scope.loadPage();
                }

            }).error(function (result, status) {
                $scope.error = true;
                msg += "Please try again later to save!!!" + result.Errors;
                toastr.error(msg, 'Oops. Something went wrong. ', { positionClass: 'toast-top-center', timeOut: 10000, closeButton: true });

            });
        } else {
            msg += "There is no new selected Course! So no need to save. ";
            toastr.warning(msg, 'Warning', { positionClass: 'toast-top-center', timeOut: 10000, closeButton: true });
        }
    };*/
    $scope.courseRemove =function(classObj) {
        classObj.IsAlreadyAdded = false;
        $scope.courseAddRemove(classObj);
    }

    $scope.courseAddRemove = function (classObj) {
        var msg = "";
        if (classObj == null) {
            return;
        }
        $http.get($scope.courseAddRemoveUrl, {
            params: {
                "classId":classObj.Id,
                "isAdded": classObj.IsAlreadyAdded
            }
        })
            .success(function (result, status) {
                if (result.HasError) {
                    $scope.error = true;
                    msg += result.Errors.toString();
                    alertError(msg);

                } else {
                    
                    msg += result.DataExtra.SuccessMess.toString();
                    alertSuccess(msg);
                }

                classObj.IsAlreadyAdded = result.Data;
                $scope.RegistrationSummaryCalculate();

            }).error(function (result, status) {
                $scope.error = true;
                msg += "Please try again later to save!!!" + result.Errors;
                alertError(msg);

            });
       
    };

    $scope.confirmRegistration = function () {
        $http.get($scope.confirmRegistrationUrl)
            .success(function (result, status) {
                if (result.HasError) {
                    $scope.error = true;
                    $scope.ErrorMsg = result.Errors;
                    alertError($scope.ErrorMsg);
                } else {
                    //alertSuccess("Successfully Registration Confirm");
                    window.open("/Student", "_self");
                }
            }).error(function (result, status) {
                $scope.error = true;
                $scope.ErrorMsg = "Please try again later to confirm Registration!!!" + result.Errors;
                alertError($scope.ErrorMsg);
            });

    };

    $scope.RegistrationSummaryCalculate = function () {
        $scope.TotalCredits = 0;
        $scope.TotalCourse = 0;
        $scope.TotalTheory = 0;
        $scope.TotalLab = 0;
        $scope.TotalCoreCredit = 0;
        $scope.TotalElectiveCredit = 0;
        
        var takenClass = $filter("filter")($scope.ClassList, { IsAlreadyAdded: true }, true);
        //console.log(takenClass);

        angular.forEach(takenClass, function (value, key) {
            $scope.TotalCredits += value.Aca_CurriculumCourseJson.CreditLoad;
            $scope.TotalCourse += 1;
            if (value.CourseCategoryEnumId == 0) {
                $scope.TotalTheory += 1;
            }
            if (value.CourseCategoryEnumId == 1) {
                $scope.TotalLab += 1;
            }
            if (value.CourseTypeEnumId == 0) {
                $scope.TotalCoreCredit += value.Aca_CurriculumCourseJson.CreditLoad;
            }
            if (value.CourseTypeEnumId == 2) {
                $scope.TotalElectiveCredit += value.Aca_CurriculumCourseJson.CreditLoad;
            }
            

        });
        //var component = $filter("filter")(takenClass, { Aca_CurriculumCourseJson: brakdown.ComponentId }, true);
        //for (var i = 0; i < takenClass.length; i++) {
            
            /*if ($scope.ClassList.IsAlreadyAdded === true) {
                //$scope.TotalCredits += $scope.ClassList.Aca_CurriculumCourseJson.CreditLoad;
            } else {
                //$scope.TotalCredits -= $scope.ClassList.Aca_CurriculumCourseJson.CreditLoad;
            }*/
       // }
    }

    //======Custom property and Functions End========================================================== 


});






