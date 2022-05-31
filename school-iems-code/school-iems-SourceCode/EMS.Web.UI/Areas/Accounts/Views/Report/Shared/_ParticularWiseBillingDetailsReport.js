
//File:Accounts_AdmissionSummaryReport List Anjular  Controller
emsApp.controller('ParticularWiseBillingDetailsReportCtrl', function ($scope, $http, $filter, cfpLoadingBar) {

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
        $scope.getParticularWiseBillingDetailsReportExtraDataExtra();
    };

     $scope.onChangeFilter = function () {
         $scope.getParticularWiseBillingDetailsReport();
     };

     $scope.getParticularWiseBillingDetailsReportExtraDataExtra = function () {
         $http.get($scope.getParticularWiseBillingDetailsReportExtraUrl, {
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
                $scope.ErrorMsg = "Unable to load option data for Particular Wise Billing Details! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Particular Wise Billing Details! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

     $scope.getParticularWiseBillingDetailsReport = function () {
        $scope.SelectedProgramId = 0;
        $scope.SelectedParticularNameId = 0;
        $scope.SelectedGenerateTypeEnumId = 0;

        if ($scope.SelectedProgram != null)
            $scope.SelectedProgramId = $scope.SelectedProgram.Id;

        if ($scope.SelectedParticularName != null)
            $scope.SelectedParticularNameId = $scope.SelectedParticularName.Id;

        if ($scope.SelectedGenerateType != null)
            $scope.SelectedGenerateTypeEnumId = $scope.SelectedGenerateType.Id;

        $http.get($scope.getParticularWiseBillingDetailsReportUrl, {
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
                $scope.PartWiseBillingDtlList = result.Data;
                $scope.GrandTotal = result.DataExtra.GrandTotal;
                log($scope.PartWiseBillingDtlList);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable Get Particular Wise Billing Details Report! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
                $scope.PartWiseBillingDtlList = [];
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.PartWiseBillingDtlList = [];
            $scope.ErrorMsg = "Unable Get Particular Wise Billing Details Report! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };

    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====


    $scope.Init = function (
        getParticularWiseBillingDetailsReportExtraUrl
        , getParticularWiseBillingDetailsReportUrl
        ) {
        $scope.getParticularWiseBillingDetailsReportExtraUrl = getParticularWiseBillingDetailsReportExtraUrl;
        $scope.getParticularWiseBillingDetailsReportUrl = getParticularWiseBillingDetailsReportUrl;
        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



