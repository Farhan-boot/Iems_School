
//File:Accounts_AdmissionSummaryReport List Anjular  Controller
emsApp.controller('ParticularWiseWaiverSummaryCtrl', function ($scope, $http, $filter, cfpLoadingBar) {

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
        $scope.getParticularWiseWaiverSummaryDataExtra();
        //$scope.getParticularWiseWaiverSummary();

    };

     $scope.onChangeFilter = function () {
         $scope.getParticularWiseWaiverSummary();
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

    $scope.getParticularWiseWaiverSummaryDataExtra = function () {
        $http.get($scope.getParticularWiseWaiverSummaryDataExtraUrl, {
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
                if (result.DataExtra.ParticularNameList != null)
                    $scope.ParticularNameList = result.DataExtra.ParticularNameList;

                if (result.DataExtra.GenerateTypeEnumList != null)
                    $scope.GenerateTypeEnumList = result.DataExtra.GenerateTypeEnumList;

                $scope.StartDate = result.DataExtra.StartDate;
                $scope.EndDate = result.DataExtra.EndDate;


            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Particular Wise Waiver Summary! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Particular Wise Waiver Summary! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.getParticularWiseWaiverSummary = function () {
        $scope.SelectedProgramId = 0;
        $scope.SelectedParticularNameId = 0;
        $scope.SelectedSemesterId = 0;
        $scope.SelectedGenerateTypeEnumId = 0;

        if ($scope.SelectedProgram != null)
            $scope.SelectedProgramId = $scope.SelectedProgram.Id;

        if ($scope.SelectedParticularName != null)
            $scope.SelectedParticularNameId = $scope.SelectedParticularName.Id;

        if ($scope.SelectedSemester != null)
            $scope.SelectedSemesterId = $scope.SelectedSemester.Id;
        
        if ($scope.SelectedGenerateType != null)
            $scope.SelectedGenerateTypeEnumId = $scope.SelectedGenerateType.Id;

        $http.get($scope.getParticularWiseWaiverSummaryUrl, {
            params: {
                "programId": $scope.SelectedProgramId,
                "selectedSemesterId": $scope.SelectedSemesterId,
                "particularId": $scope.SelectedParticularNameId,
                "generateTypeEnumId": $scope.SelectedGenerateTypeEnumId,
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
                $scope.ErrorMsg = "Unable Get Particular Wise Waiver Summary Report! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
                $scope.Report = [];
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable Get Particular Wise Waiver Summary Report! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
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
        getParticularWiseWaiverSummaryDataExtraUrl
        , getParticularWiseWaiverSummaryUrl
        ) {
        $scope.getParticularWiseWaiverSummaryDataExtraUrl = getParticularWiseWaiverSummaryDataExtraUrl;
        $scope.getParticularWiseWaiverSummaryUrl = getParticularWiseWaiverSummaryUrl;
        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



