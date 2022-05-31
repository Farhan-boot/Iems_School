
//File:Accounts_AdmissionSummaryReport List Anjular  Controller
emsApp.controller('ParticularWiseCollectionDetailsReportCtrl', function ($scope, $http, $filter, cfpLoadingBar) {

    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;

    $scope.StartDate = "";
    $scope.EndDate = "";

    $scope.SelectedProgramId = 0;
    $scope.SelectedBankId = 0;
    $scope.SelectedParticularNameId = 0;

    $scope.SelectedProgram = null;
    $scope.SelectedBank = null;
    $scope.SelectedParticularName = null;

    $scope.Report = [];


    $scope.loadPage = function () {
        $scope.getParticularWiseCollectionDetailsReportExtraDataExtra();
    };

     $scope.onChangeFilter = function () {
         $scope.getParticularWiseCollectionDetailsReport();
     };

     $scope.getParticularWiseCollectionDetailsReportExtraDataExtra = function () {
         $http.get($scope.getParticularWiseCollectionDetailsReportExtraUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;

                //DropDown Option Tables
                if (result.DataExtra.ProgramList != null)
                    $scope.ProgramList = result.DataExtra.ProgramList;
                
                if (result.DataExtra.OfficialBankList != null)
                    $scope.OfficialBankList = result.DataExtra.OfficialBankList;
                if (result.DataExtra.ParticularNameList != null)
                    $scope.ParticularNameList = result.DataExtra.ParticularNameList;

                if (result.DataExtra.SemesterList != null)
                    $scope.SemesterList = result.DataExtra.SemesterList;

                $scope.StartDate = result.DataExtra.StartDate;
                $scope.EndDate = result.DataExtra.EndDate;


            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Particular Wise Collection Details! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Particular Wise Collection Details! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

     $scope.getParticularWiseCollectionDetailsReport = function () {
        $scope.SelectedProgramId = 0;
        $scope.SelectedBankId = 0;
        $scope.SelectedParticularNameId = 0;

        if ($scope.SelectedProgram != null)
            $scope.SelectedProgramId = $scope.SelectedProgram.Id;

        if ($scope.SelectedBank != null)
            $scope.SelectedBankId = $scope.SelectedBank.Id;

        if ($scope.SelectedParticularName != null)
            $scope.SelectedParticularNameId = $scope.SelectedParticularName.Id;


        $http.get($scope.getParticularWiseCollectionDetailsReportUrl, {
            params: {
                "programId": $scope.SelectedProgramId,
                "bankId": $scope.SelectedBankId,
                "particularId": $scope.SelectedParticularNameId,
                "startDate": $scope.StartDate,
                "endDate": $scope.EndDate
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.PartWiseCollectionDtlList = result.Data;
                $scope.GrandTotal = result.DataExtra.GrandTotal;
                log($scope.PartWiseCollectionDtlList);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable Get Particular Wise Collection Details Report! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
                $scope.PartWiseCollectionDtlList = [];
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.PartWiseCollectionDtlList = [];
            $scope.ErrorMsg = "Unable Get Particular Wise Collection Details Report! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };

    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====


    $scope.Init = function (
        getParticularWiseCollectionDetailsReportExtraUrl
        , getParticularWiseCollectionDetailsReportUrl
        ) {
        $scope.getParticularWiseCollectionDetailsReportExtraUrl = getParticularWiseCollectionDetailsReportExtraUrl;
        $scope.getParticularWiseCollectionDetailsReportUrl = getParticularWiseCollectionDetailsReportUrl;
        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



