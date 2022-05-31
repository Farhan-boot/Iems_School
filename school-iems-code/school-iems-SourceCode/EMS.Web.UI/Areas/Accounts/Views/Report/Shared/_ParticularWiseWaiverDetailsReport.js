
//File:Accounts_AdmissionSummaryReport List Anjular  Controller
emsApp.controller('ParticularWiseWaiverDetailsReportCtrl', function ($scope, $http, $filter, cfpLoadingBar) {

    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;

    $scope.StartDate = "";
    $scope.EndDate = "";

    $scope.SelectedProgramId = 0;
    $scope.SelectedParticularNameId = 0;
    $scope.SelectedGenerateTypeEnumId = 0;

    $scope.SelectedProgram = null;
    $scope.SelectedParticularName = null;
    $scope.SelectedGenerateType = null;

    $scope.Report = [];


    $scope.loadPage = function () {
        $scope.getParticularWiseWaiverDetailsReportExtraDataExtra();
    };

     $scope.onChangeFilter = function () {
         $scope.getParticularWiseWaiverDetailsReport();
     };

     $scope.getParticularWiseWaiverDetailsReportExtraDataExtra = function () {
         $http.get($scope.getParticularWiseWaiverDetailsReportExtraUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;

                //DropDown Option Tables
                if (result.DataExtra.ProgramList != null)
                    $scope.ProgramList = result.DataExtra.ProgramList;

                if (result.DataExtra.ParticularNameList != null)
                    $scope.ParticularNameList = result.DataExtra.ParticularNameList;

                if (result.DataExtra.SemesterList != null)
                    $scope.SemesterList = result.DataExtra.SemesterList;

                if (result.DataExtra.GenerateTypeEnumList != null)
                    $scope.GenerateTypeEnumList = result.DataExtra.GenerateTypeEnumList;

                $scope.StartDate = result.DataExtra.StartDate;
                $scope.EndDate = result.DataExtra.EndDate;


            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Particular Wise Waiver Details! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Particular Wise Waiver Details! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

     $scope.getParticularWiseWaiverDetailsReport = function () {
        $scope.SelectedProgramId = 0;
        $scope.SelectedParticularNameId = 0;
        $scope.SelectedGenerateTypeEnumId = 0;

        if ($scope.SelectedProgram != null)
            $scope.SelectedProgramId = $scope.SelectedProgram.Id;

        if ($scope.SelectedParticularName != null)
            $scope.SelectedParticularNameId = $scope.SelectedParticularName.Id;

        if ($scope.SelectedGenerateType != null)
            $scope.SelectedGenerateTypeEnumId = $scope.SelectedGenerateType.Id;

        $http.get($scope.getParticularWiseWaiverDetailsReportUrl, {
            params: {
                "programId": $scope.SelectedProgramId,
                "particularId": $scope.SelectedParticularNameId,
                "generateTypeEnumId": $scope.SelectedGenerateTypeEnumId,
                "startDate": $scope.StartDate,
                "endDate": $scope.EndDate
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.PartWiseWaiverDtlList = result.Data;
                $scope.GrandTotal = result.DataExtra.GrandTotal;
                log($scope.PartWiseWaiverDtlList);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable Get Particular Wise Waiver Details Report! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
                $scope.PartWiseWaiverDtlList = [];
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.PartWiseWaiverDtlList = [];
            $scope.ErrorMsg = "Unable Get Particular Wise Waiver Details Report! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };

    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====


    $scope.Init = function (
        getParticularWiseWaiverDetailsReportExtraUrl
        , getParticularWiseWaiverDetailsReportUrl
        ) {
        $scope.getParticularWiseWaiverDetailsReportExtraUrl = getParticularWiseWaiverDetailsReportExtraUrl;
        $scope.getParticularWiseWaiverDetailsReportUrl = getParticularWiseWaiverDetailsReportUrl;
        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



