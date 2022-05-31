
//File:Accounts_AdmissionSummaryReport List Anjular  Controller
emsApp.controller('FullPackageWaiverCtrl', function ($scope, $http, $filter, cfpLoadingBar) {

    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;

    $scope.StartDate = "";
    $scope.EndDate = "";

    $scope.SelectedProgramId = 0;
    $scope.SelectedSemesterId = 0;
    $scope.SelectedParticularNameId = 0;
    $scope.SelectedGenerateTypeEnumId = 0;

    $scope.SelectedProgram = null;
    $scope.SelectedSemester = null;
    $scope.SelectedParticularName = null;
    $scope.SelectedGenerateType = null;

    $scope.VoucherNo = "";
    $scope.Report = [];

    $scope.HasReportError = false;
    $scope.ReportErrorMessage = "";


    $scope.loadPage = function () {
        $scope.getFullPackageWaiverDataExtra();
        //$scope.getFullPackageWaiver();

    };

     $scope.onChangeFilter = function () {
         $scope.getFullPackageWaiver();
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

    $scope.getFullPackageWaiverDataExtra = function () {
        $http.get($scope.getFullPackageWaiverDataExtraUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;

                //DropDown Option Tables
                if (result.DataExtra.ProgramList != null)
                    $scope.ProgramList = result.DataExtra.ProgramList;
                //log($scope.ProgramList);
                if (result.DataExtra.SemesterList != null)
                    $scope.SemesterList = result.DataExtra.SemesterList;

                if (result.DataExtra.ValidPeriodEnumList != null)
                    $scope.ValidPeriodEnumList = result.DataExtra.ValidPeriodEnumList;

                if (result.DataExtra.AmountTypeEnumList != null)
                    $scope.AmountTypeEnumList = result.DataExtra.AmountTypeEnumList;

                if (result.DataExtra.ScholarshipTypeList != null)
                    $scope.ScholarshipTypeList = result.DataExtra.ScholarshipTypeList;

                $scope.StartDate = result.DataExtra.StartDate;
                $scope.EndDate = result.DataExtra.EndDate;


            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Particular Wise Billing Summary! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Particular Wise Billing Summary! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.getFullPackageWaiver = function () {
        $scope.SelectedProgramId = 0;
        $scope.SelectedAmountTypeId = 0;
        $scope.SelectedSemesterId = 0;
        $scope.SelectedValidPeriodEnumId = 0;
        $scope.SelectedAmountTypeEnumId = 0;

        if ($scope.SelectedProgram != null)
            $scope.SelectedProgramId = $scope.SelectedProgram.Id;

        if ($scope.SelectedScholarshipType != null)
            $scope.SelectedAmountTypeId = $scope.SelectedScholarshipType.Id;

        if ($scope.SelectedSemester != null)
            $scope.SelectedSemesterId = $scope.SelectedSemester.Id;

        if ($scope.SelectedValidPeriod != null)
            $scope.SelectedValidPeriodEnumId = $scope.SelectedValidPeriod.Id;

        if ($scope.SelectedAmountType != null)
            $scope.SelectedAmountTypeEnumId = $scope.SelectedAmountType.Id;

        $http.get($scope.getFullPackageWaiverUrl, {
            params: {
                "programId": $scope.SelectedProgramId,
                "selectedSemesterId": $scope.SelectedSemesterId,
                "scholarshipTypeId": $scope.SelectedAmountTypeId,
                "validPeriodEnumId": $scope.SelectedValidPeriodEnumId,
                "amountTypeEnumId": $scope.SelectedAmountTypeEnumId,
                "startDate": $scope.StartDate,
                "endDate": $scope.EndDate
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;

                $scope.Report = result.Data;
                $scope.GrandTotal = result.DataExtra.GrandTotal;

                $scope.VoucherDate = result.DataExtra.VoucherDate;
                $scope.HasReportError = result.DataExtra.HasReportError;
                $scope.ReportErrorMessage = result.DataExtra.ReportErrorMessage;

                //log(result);
                
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable Get Particular Wise Billing Summary Report! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
                $scope.Report = [];
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable Get Particular Wise Billing Summary Report! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
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
        getFullPackageWaiverDataExtraUrl
        , getFullPackageWaiverUrl
        ) {
        $scope.getFullPackageWaiverDataExtraUrl = getFullPackageWaiverDataExtraUrl;
        $scope.getFullPackageWaiverUrl = getFullPackageWaiverUrl;
        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



