
//File:Accounts_AdmissionSummaryReport List Anjular  Controller
emsApp.controller('AdmissionSummaryReportCtrl', function ($scope, $http, $filter, cfpLoadingBar) {

    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;

    $scope.SelectedAdmExamId = 0;
    $scope.SelectedfacultyId = 0;
    $scope.AdmissionExamList = null;
    $scope.AdmissionDate = "";
    $scope.AdmDateInReport = "";
    $scope.TotalIncompleteProfile = 0;

    $scope.loadPage = function () {
        $scope.getAdmissionSummaryReport();

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

    $scope.Count = 0;

    $scope.getAdmissionSummaryReport = function () {
        $http.get($scope.getAdmissionSummaryReportUrl, {
            params: {
             "admExamId": $scope.SelectedAdmExamId
           , "facultyId": $scope.SelectedfacultyId
           ,"admDate": $scope.AdmissionDate
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.AdmissionExamList = result.DataExtra.AdmissionExamList;
                $scope.ProgramList = result.DataExtra.ProgramList;
                $scope.FacultyList = result.DataExtra.FacultyList;
                $scope.SelectedAdmExamId = result.DataExtra.SelectedExamId;

                $scope.AdmDateInReport = result.DataExtra.AdmissionDate;
                $scope.TotalIncompleteProfile = result.DataExtra.TotalIncompleteProfile;

                $scope.HasViewPermission = result.HasViewPermission;
                $scope.Report = result.Data; 
                $scope.Count = result.Count;
                $scope.GrandTotalMale= result.DataExtra.GrandTotalMale ;
                $scope.GrandTotalFemale = result.DataExtra.GrandTotalFemale;
                $scope.Paid = result.DataExtra.Paid ;
                $scope.Unpaid = result.DataExtra.Unpaid;
                $scope.TodayGrandTotal = result.DataExtra.TodayGrandTotal;

               
                log(result);
                $scope.filterSelectedAdmissionExam();
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable Get Admission Summary Report! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable Get Admission Summary Report! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.AdmissionExamName = "";

    $scope.filterSelectedAdmissionExam = function () {
        if ($scope.AdmissionExamList != null && $scope.SelectedAdmExamId!=null) {
            var admissionExam = $filter('filter')($scope.AdmissionExamList, { Id: $scope.SelectedAdmExamId})[0];
            $scope.AdmissionExamName = admissionExam.Name;
        } else {
            $scope.AdmissionExamName = "";
        }
    };

   $scope.getAdmissionSummaryReportExtraData = function () {
        $http.get($scope.getAdmissionSummaryReportDataExtraUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;

                $scope.AdmissionDate = result.DataExtra.AdmissionDate;
                $scope.AdmDateInReport = result.DataExtra.AdmissionDate;

                // First time Get Summary Report using AdmissionDate
                $scope.getAdmissionSummaryReport();

                /*if (result.DataExtra.StatusEnumList != null)
                    $scope.StatusEnumList = result.DataExtra.StatusEnumList;*/
                //DropDown Option Tables
                /*if (result.DataExtra.ProgramList != null)
                    $scope.ProgramList = result.DataExtra.ProgramList;*/

                /*if (result.DataExtra.SemesterList != null)
                    $scope.SemesterList = result.DataExtra.SemesterList;*/

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
    };

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
            getAdmissionSummaryReportDataExtraUrl,
            getAdmissionSummaryReportUrl
        ) {
        $scope.getAdmissionSummaryReportDataExtraUrl = getAdmissionSummaryReportDataExtraUrl;
        $scope.getAdmissionSummaryReportUrl = getAdmissionSummaryReportUrl;

        $scope.getAdmissionSummaryReportExtraData();
        //$scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



