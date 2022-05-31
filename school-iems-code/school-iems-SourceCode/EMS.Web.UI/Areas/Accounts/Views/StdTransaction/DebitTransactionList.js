
//File:Accounts_StdTransaction List Anjular  Controller
emsApp.controller('DebitTransactionListCtrl', function ($scope, $http, $filter, cfpLoadingBar, $sce) {

    $scope.DebitTransactionList = [];
    $scope.SelectedStdTransaction = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
    $scope.SelectedSemesterId = 0;
    $scope.SelectedStudentId = "";
    $scope.SelectedDebitTransactionTypeEnumId = 0;
    $scope.SelectedGenerateTypeEnumId = 0;
    //search items
    $scope.SearchText = "";
    $scope.VoucherName = "";
    $scope.VoucherId = 0;
    //
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 20;
    $scope.PageNo = 1;
    $scope.HistoryMsg = "";
    $scope.loadPage = function () {
        $scope.getDebitTransactionListExtraData();
        $scope.getPagedDebitTransactionList();

    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedDebitTransactionList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedDebitTransactionList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedDebitTransactionList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedDebitTransactionList();
    };
    $scope.searchDebitTransactionList = function () {
        $scope.getPagedDebitTransactionList();
    };
    $scope.getPagedDebitTransactionList = function () {
        $scope.getDebitTransactionList();
    };
    /*For Search on data end*/
    $scope.getDebitTransactionList = function () {
        $http.get($scope.getPagedStdTransactionUrl, {
            params: {
            "textkey": $scope.SearchText
            , "pageSize": $scope.PageSize
            , "pageNo": $scope.PageNo
            , "semesterId": $scope.SelectedSemesterId
            , "studentId": $scope.StudentId
            , "createdById": $scope.CreatedById
            , "slipId": $scope.SlipId
            , "startDate": $scope.StartDate
            , "endDate": $scope.EndDate
            , "isShowTrashedItems": $scope.IsShowTrashedItems
            , "debitTransactionTypeEnumId": $scope.SelectedDebitTransactionTypeEnumId
            , "generateTypeEnumId": $scope.SelectedGenerateTypeEnumId
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.DebitTransactionList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;

                $scope.VoucherName = result.DataExtra.VoucherName;
                $scope.VoucherId = -1;
                updateUrlQuery('vid', $scope.VoucherId);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Std Transaction list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
                $scope.DebitTransactionList = [];
                $scope.totalItems = 0;
                $scope.totalServerItems = 0;
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Std Transaction list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
            $scope.DebitTransactionList = [];
            $scope.totalItems = 0;
            $scope.totalServerItems = 0;
        });
    };
    $scope.getDebitTransactionListExtraData = function () {
        $http.get($scope.getDebitTransactionListDataExtraUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.DataExtra.PaymentStatusEnumList != null)
                    $scope.PaymentStatusEnumList = result.DataExtra.PaymentStatusEnumList;
                if (result.DataExtra.TransactionTypeEnumList != null)
                    $scope.TransactionTypeEnumList = result.DataExtra.TransactionTypeEnumList;
                //DropDown Option Tables
                if (result.DataExtra.SemesterList != null)
                    $scope.SemesterList = result.DataExtra.SemesterList;

                if (result.DataExtra.OfficialBankList != null)
                    $scope.OfficialBankList = result.DataExtra.OfficialBankList;

                if (result.DataExtra.StudentList != null)
                    $scope.StudentList = result.DataExtra.StudentList;

                if (result.DataExtra.DebitTransactionTypeEnumList != null)
                    $scope.DebitTransactionTypeEnumList = result.DataExtra.DebitTransactionTypeEnumList;

                if (result.DataExtra.GenerateTypeEnumList != null)
                    $scope.GenerateTypeEnumList = result.DataExtra.GenerateTypeEnumList;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Std Transaction! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Std Transaction! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllDebitTransactionList = function () {
        $scope.IsLoading++;
        $http.get($scope.getDebitTransactionListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.DebitTransactionList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Std Transaction list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Std Transaction list! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.deleteStdTransactionById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteStdTransactionByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.DebitTransactionList.indexOf(obj);
                        $scope.DebitTransactionList.splice(i, 1);
                        alertSuccess("Data successfully deleted!");
                    } else {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Sorry unable to delete! " + result.Errors.toString();
                        alertError($scope.ErrorMsg);
                    }
                }).error(function (result, status) {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Sorry unable to delete! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
                    alertError($scope.ErrorMsg);
                });
            }
        });
    };


    $scope.selectAll = function ($event) {
        var checkbox = $event.target;
        for (var i = 0; i < $scope.DebitTransactionList.length; i++) {
            var entity = $scope.DebitTransactionList[i];
            entity.IsSelected = checkbox.checked;
        }
    };

    $scope.openHistoryModal = function (history) {
        if (history == null || history === '') {
            $scope.HistoryMsg = '<center style="color:red;font-weight: bold;">No History Available.</center>';
        } else {
            $scope.HistoryMsg = history;
        }
        
        $('#HistoryModal').modal('show');
    }
    $scope.renderHtml = function (html_code) {
        return $sce.trustAsHtml(html_code);
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====


    $scope.Init = function (
        getPagedStdTransactionUrl
        , deleteStdTransactionByIdUrl
        , getDebitTransactionListDataExtraUrl
        , getStdTransactionByIdUrl
        , getStdTransactionDataExtraUrl
        , saveStdTransactionUrl
        ) {
        $scope.getPagedStdTransactionUrl = getPagedStdTransactionUrl;
        $scope.deleteStdTransactionByIdUrl = deleteStdTransactionByIdUrl;
        /*bind extra url if need*/
        $scope.getDebitTransactionListDataExtraUrl = getDebitTransactionListDataExtraUrl;
        $scope.getStdTransactionByIdUrl = getStdTransactionByIdUrl;
        $scope.getStdTransactionDataExtraUrl = getStdTransactionDataExtraUrl;
        $scope.saveStdTransactionUrl = saveStdTransactionUrl;

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



