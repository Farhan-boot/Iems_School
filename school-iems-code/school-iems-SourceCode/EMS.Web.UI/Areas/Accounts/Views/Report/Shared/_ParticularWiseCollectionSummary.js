
//File:Accounts_AdmissionSummaryReport List Anjular  Controller
emsApp.controller('ParticularWiseCollectionSummaryCtrl', function ($scope, $http, $filter, cfpLoadingBar) {

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

    $scope.VoucherNo = "";
    $scope.Report = [];

    $scope.HasReportError = false;
    $scope.ReportErrorMessage = "";


    $scope.loadPage = function () {
        $scope.getParticularWiseCollectionSummaryDataExtra();
        //$scope.getParticularWiseCollectionSummary();

    };

     $scope.onChangeFilter = function () {
         $scope.getParticularWiseCollectionSummary();
     };

    /*For Search on data start*/
    /*  $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedAdmissionSummaryReport();
    };*/

    /*    $scope.changePageNo = function () {
            $scope.getPagedAdmissionSummaryReport();
        };*/
    /*    $scope.prev = function () {
            $scope.PageNo = $scope.PageNo - 1;
            $scope.getPagedAdmissionSummaryReport();
        };
        $scope.next = function () {
            $scope.PageNo = $scope.PageNo + 1;
            $scope.getPagedAdmissionSummaryReport();
        };
    
    
        $scope.searchAdmissionSummaryReport = function () {
            $scope.getPagedAdmissionSummaryReport();
        };*/

    //$scope.Count = 0;

    $scope.getParticularWiseCollectionSummaryDataExtra = function () {
        $http.get($scope.getParticularWiseCollectionSummaryDataExtraUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;

                //DropDown Option Tables
                if (result.DataExtra.ProgramList != null)
                    $scope.ProgramList = result.DataExtra.ProgramList;
                log($scope.ProgramList);
                if (result.DataExtra.OfficialBankList != null)
                    $scope.OfficialBankList = result.DataExtra.OfficialBankList;
                if (result.DataExtra.ParticularNameList != null)
                    $scope.ParticularNameList = result.DataExtra.ParticularNameList;

                $scope.StartDate = result.DataExtra.StartDate;
                $scope.EndDate = result.DataExtra.EndDate;


            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Particular Wise Collection Summary! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Particular Wise Collection Summary! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.getParticularWiseCollectionSummary = function () {
        $scope.SelectedProgramId = 0;
        $scope.SelectedBankId = 0;
        $scope.SelectedParticularNameId = 0;

        if ($scope.SelectedProgram != null)
            $scope.SelectedProgramId = $scope.SelectedProgram.Id;

        if ($scope.SelectedBank != null)
            $scope.SelectedBankId = $scope.SelectedBank.Id;

        if ($scope.SelectedParticularName != null)
            $scope.SelectedParticularNameId = $scope.SelectedParticularName.Id;

        $http.get($scope.getParticularWiseCollectionSummaryUrl, {
            params: {
                "programId": $scope.SelectedProgramId,
                "bankId": $scope.SelectedBankId,
                "particularId": $scope.SelectedParticularNameId,
                "startDate": $scope.StartDate,
                "endDate": $scope.EndDate,
                "voucherName": $scope.VoucherNo
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;

                $scope.Report = result.Data;
                $scope.GrandTotal = result.DataExtra.GrandTotal;

                $scope.VoucherDate = result.DataExtra.VoucherDate;
                $scope.HasReportError = result.DataExtra.HasReportError;
                $scope.ReportErrorMessage = result.DataExtra.ReportErrorMessage;

                log(result);
                
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable Get Particular Wise Collection Summary Report! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
                $scope.Report = [];
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable Get Particular Wise Collection Summary Report! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };

    //$scope.AdmissionExamName = "";

    //$scope.filterSelectedAdmissionExam = function () {
    //    if ($scope.AdmissionExamList != null && $scope.SelectedAdmExamId != null) {
    //        var admissionExam = $filter('filter')($scope.AdmissionExamList, { Id: $scope.SelectedAdmExamId })[0];
    //        $scope.AdmissionExamName = admissionExam.Name;
    //    } else {
    //        $scope.AdmissionExamName = "";
    //    }
    //};

    /*    $scope.getAdmissionSummaryReportExtraData = function () {
            $http.get($scope.getAdmissionSummaryReportDataExtraUrl, {
                params: {}
            }).success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.DataExtra.StatusEnumList != null)
                        $scope.StatusEnumList = result.DataExtra.StatusEnumList;
                    //DropDown Option Tables
                    if (result.DataExtra.ProgramList != null)
                        $scope.ProgramList = result.DataExtra.ProgramList;
    
                    if (result.DataExtra.SemesterList != null)
                        $scope.SemesterList = result.DataExtra.SemesterList;
    
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to load option data for Fee Code! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Fee Code! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
        };*/

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    /*
        $scope.getAllAdmissionSummaryReport = function () {
            $scope.IsLoading++;
            $http.get($scope.getAdmissionSummaryReportUrl, {
                params: {}
            }).success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    $scope.AdmissionSummaryReport = result.Data;
    
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to Admission Summary Report Code list data! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to Admission Summary Report Code list! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
        };
    
    */


    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====


    $scope.Init = function (
        getParticularWiseCollectionSummaryDataExtraUrl
        , getParticularWiseCollectionSummaryUrl
        ) {
        $scope.getParticularWiseCollectionSummaryDataExtraUrl = getParticularWiseCollectionSummaryDataExtraUrl;
        $scope.getParticularWiseCollectionSummaryUrl = getParticularWiseCollectionSummaryUrl;
        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



