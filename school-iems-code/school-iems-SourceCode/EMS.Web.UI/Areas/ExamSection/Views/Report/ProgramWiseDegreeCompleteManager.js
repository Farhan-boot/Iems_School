
//File:Academic_Semester List Anjular  Controller
emsApp.controller('ProgramWiseDegreeCompleteManagerCtrl', function ($scope, $http, $filter) {
    $scope.ErrorMsg = "";
    $scope.HasError = false;

    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====
    $scope.Init = function (
        getProgramWiseDegreeCompletePrintDataExtraUrl
    ) {
        $scope.getProgramWiseDegreeCompletePrintDataExtraUrl = getProgramWiseDegreeCompletePrintDataExtraUrl;
        $scope.getGeneralDataExtra();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

    $scope.SelectedSemesterId = 0;
    $scope.SelectedProgramId = 0;

    $scope.getGeneralDataExtra = function () {
        $http.get($scope.getProgramWiseDegreeCompletePrintDataExtraUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {

                $scope.HasError = false;
                if (result.DataExtra.SemesterList != null) {
                    $scope.SemesterList = result.DataExtra.SemesterList;
                    //$scope.SelectedSemesterId = $scope.SemesterList[0].Id;
                }
                if (result.DataExtra.ProgramList != null) {
                    $scope.ProgramList = result.DataExtra.ProgramList;
                    //$scope.SelectedProgramId = $scope.ProgramList[0].Id;
                }


            } else {
                $scope.HasError = true;
                $scope.ErrorMsg =  result.Errors.toString()+" Unable to load Data!";
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Program Wise Degree Complete Report! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

});



