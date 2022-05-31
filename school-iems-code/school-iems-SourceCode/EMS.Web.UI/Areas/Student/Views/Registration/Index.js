
//File:Academic_Semester List Anjular  Controller
emsApp.controller('StudentRegistrationCtrl', function ($scope, $http, $filter) {
    $scope.SemesterList = [];
    $scope.SelectedSemester = [];
    $scope.RegStatus = "";
    $scope.SemesterId = 0;

    $scope.ErrorMsg = "";
    $scope.HasError = false;

    $scope.loadPage = function () {
        $scope.getSemesterListByStudent();
    }

    $scope.getSemesterListByStudent = function () {

        $http.get($scope.getSemesterListByStudentUrl, {
            params: {}
        }).success(function (result) {
            if (!result.HasError) {
                $scope.SemesterList = result.Data;
                //console.log(result.DataExtra.Date);
                //console.log($scope.date);
                if ($scope.SemesterList != null && $scope.SemesterList.length > 0) {
                    $scope.SelectedSemester = $scope.SemesterList[0];
                    $scope.getClassListBySemesterId($scope.SelectedSemester);
                    LoadPaymentDetails( $scope.SelectedSemester.Id);
                }

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Semester list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }

        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Semester list! " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);

        });
    };

    $scope.onChangeSemester = function () {
        if ($scope.SelectedSemester != null) {
            $scope.getClassListBySemesterId($scope.SelectedSemester);
            LoadPaymentDetails($scope.SelectedSemester.Id);
            
        }
    };

    $scope.getClassListBySemesterId = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }

        $http.get($scope.getClassListBySemesterIdUrl, {
            params: {
                //"id": obj.Id,
                "semesterId": obj.Id

            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.ClassList = result.Data;
                $scope.RegStatus = result.DataExtra.RegStatus;
                
                $scope.SemesterId = obj.Id;
                $scope.RegistrationSummaryCalculate();
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Class list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }

        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Class list! " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);

        });
    };

    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====
    $scope.Init = function (
         getSemesterListByStudentUrl
        , getClassListBySemesterIdUrl
        , viewPaymentDetailsBySemesterIdUrl

    ) {
        $scope.getSemesterListByStudentUrl = getSemesterListByStudentUrl;
        $scope.getClassListBySemesterIdUrl = getClassListBySemesterIdUrl;
        $scope.viewPaymentDetailsBySemesterIdUrl = viewPaymentDetailsBySemesterIdUrl;
        //$scope.testurl = testurl;
        $scope.loadPage();
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

    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

    var LoadPaymentDetails = function (studentId, semesterId) {
        $.get($scope.viewPaymentDetailsBySemesterIdUrl + "/?studentId=" + studentId + "&semesterId=201701", function (data) {
            $(".payment-per-semester").html(data);
            $(".assessment").addClass("table table-hover table-responsive");
            //$('#showAssesmentModal').modal('show');
        }).done(function (result) {

        }).fail(function (result) {
            $(".payment-details").html("Data loading failed!");
        });
    }


});



