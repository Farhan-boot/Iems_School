
//File: Acnt_StdTransaction Anjular  Controller
emsApp.controller('StdTransactionAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.StdTransactionList = [];
    $scope.StdTransaction = [];
    $scope.StdTransactionPay = [];
    $scope.SemesterList = [];
    $scope.ParticularTypeList = [];
    $scope.BankList = [];
    $scope.SelectParticularType = "";
    $scope.SelectedTransaction = [];
    $scope.StudentId = 0;
    $scope.StdTransactionId = 0;
    $scope.TotalDebitAmount = 0;
    $scope.TotalCreditAmount = 0;
    $scope.TotalBalance = 0;
    $scope.SelectedStdTransactionIndex = -1;
    $scope.IndexInModal = ""; // Uses inside the Payment modal
    $scope.ErrorMsg = "";
    $scope.HasError = true;
    $scope.IsPayDrForCr = false;

    $scope.getNewTransaction = function () {
        //$scope.StdTransaction = $scope.loadPaymentsByTransactionId(0);
        $scope.IndexInModal = "New"; //Uses inside the Payment modal
        $scope.getStdTransactionById(0);
        
        $('#addPaymentModal').modal('show');
    };

    $scope.selectParticularName = function (name) {
        $scope.StdTransaction.Name = name;
    };

    $scope.selectBankName = function (name) {
        $scope.StdTransaction.BankName = name;
    };

    $scope.getStdTransactionById = function (StdTransactionId) {

        if (StdTransactionId != null && StdTransactionId !== '')
            $scope.StdTransactionId = StdTransactionId;
        else return;

        $http.get($scope.getStdTransactionByIdUrl, {
            params: {
                "id": $scope.StdTransactionId,
                "studentId": $scope.StudentId
            }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.StdTransaction = result.Data;
                $('#addPaymentModal').modal('show');
                if ($scope.StdTransaction.Id!==0) {
                    $scope.IndexInModal = $scope.SelectedStdTransactionIndex+1;
                }
                
                //updateUrlQuery('id', $scope.StdTransaction.Id);
                //$scope.onAfterGetStdTransactionById(result);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Std Transaction! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Std Transaction! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.bindTransactionList = function (result) {

        $scope.StdTransactionList = result.Data;
        $scope.TotalDebitAmount = result.DataExtra.TotalDebitAmount;
        $scope.TotalCreditAmount = result.DataExtra.TotalCreditAmount;
        $scope.TotalBalance = result.DataExtra.TotalBalance;
    }

    $scope.getTransactionListByStudentId = function (studentId) {
        if (studentId != null && studentId !== '')
            $scope.StudentId = studentId;
        else return;

        $http.get($scope.getTransactionListByStudentIdUrl, {
            params: { "id": $scope.StudentId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasErrorGetList = false;
                $scope.bindTransactionList(result);
            } else {
                $scope.HasErrorGetList = true;
                $scope.ErrorMsg = "Unable to get Transaction List! " + result.Errors.toString();
                //alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Std Transaction! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.loadAddPaymentExtraData = function () {
        $http.get($scope.getAddPaymentExtraDataUrl)
            .success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    $scope.SemesterList = result.Data;
                    $scope.ParticularTypeList = result.DataExtra.particularTypeList;
                    $scope.BankList = result.DataExtra.bankList;
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

    $scope.saveStdTransaction = function () {
        if (!$scope.validateStdTransaction()) {
            return;
        }
        //call save pay a dr amount and insert credit amount
        if ($scope.IsPayDrForCr) {
            $scope.payDrAddCrTransaction();
            return;
        }
        //insert new transection
        $http.post($scope.saveStdTransactionUrl + "/", $scope.StdTransaction).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.Data != null) {
                        //$scope.StdTransaction = result.Data;
                        //Binding Transaction List By Student Id
                        $scope.bindTransactionList(result);
                    }
                    $('#addPaymentModal').modal('hide');
                    alertSuccess("Successfully saved Std Transaction data!");
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to save Std Transaction! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to save Std Transaction! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };

    $scope.balanceRecalculateByStudentId = function (studentId) {
        if (studentId != null && studentId !== '')
            $scope.StudentId = studentId;
        else return;

        $http.get($scope.balanceRecalculateByStudentIdUrl, {
            params: { "studentId": $scope.StudentId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasErrorGetList = false;
                $scope.bindTransactionList(result);

            } else {
                $scope.HasError = true;
                alertError("Unable to Balance Reclaculate! " + result.Errors.toString());
                
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to Balance Reclaculate! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    //save payment for a debit and add credit
    $scope.payDrAddCrTransaction = function () {
        if (!$scope.validateStdTransaction()) {
            return;
        }

        $http.post($scope.payDrAddCrTransactionUrl + "/", $scope.StdTransaction).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.Data != null) {
                    //$scope.StdTransaction = result.Data;
                    $scope.bindTransactionList(result);
                    //updateUrlQuery('id', $scope.StdTransaction.Id);

                }
                $('#addPaymentModal').modal('hide');
                alertSuccess("Successfully saved Transaction!");
                //$scope.onAfterSaveStdTransaction(result);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to save Transaction! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to save Transaction! " +
                JSON.stringify(result).toString() +
                ". " +
                "Status: " +
                JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.deleteStdTransactionById = function () {
        if ($scope.StdTransactionId == null || $scope.StdTransactionId <= 0) {
            alertError("Please select a row to delete!");
            return;
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteStdTransactionByIdUrl, {
                    params: {
                        "transactionId": $scope.StdTransactionId,
                        "studentId":$scope.StudentId
                    }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        //var i = $scope.StdTransactionList.indexOf(obj);
                        //$scope.StdTransactionList.splice(i, 1);
                        alertSuccess("Data successfully deleted!");
                        $scope.bindTransactionList(result);
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

    $scope.validateStdTransaction = function () {
        return true;
    };

    $scope.loadPage = function (studentId) {
        $scope.getTransactionListByStudentId($scope.StudentId);
        $scope.loadAddPaymentExtraData();
    }

    $scope.forIsDebited = function () {
        if ($scope.StdTransaction.IsDebited === true) {
            $scope.StdTransaction.ORNumber = "N/A";
        } else {
            $scope.StdTransaction.ORNumber = "";
        }

    };

    $scope.forTransactionType = function () {
        if ($scope.StdTransaction.TransactionTypeEnumId == 0) {
            $scope.StdTransaction.ChequeNumber = "N/A";
            $scope.StdTransaction.DateOfCheque = null;
        }
        if ($scope.StdTransaction.TransactionTypeEnumId == 1) {
            $scope.StdTransaction.ChequeNumber = "N/A";
            $scope.StdTransaction.DateOfCheque = null;
        }
        if ($scope.StdTransaction.TransactionTypeEnumId == 2) {
            $scope.StdTransaction.ChequeNumber = "";
            $scope.StdTransaction.DateOfCheque = "";
        }
    }
    //pay a debit amount
    $scope.openModalForAddCrAmountForDr = function ($index) {
        $scope.getCloneStdTransByRowIndex($index);
        $scope.StdTransaction.IsDebited = false;
        $('#addPaymentModal').modal('show');
        $scope.IsPayDrForCr = true;//is auto fill for pay cr from dr info

    };
    //edit transcetion modal
    $scope.onClickTransactionEdit = function ($index) {
        $scope.getCloneStdTransByRowIndex($index);
        $scope.getStdTransactionById($scope.StdTransactionId);
        $scope.IsPayDrForCr = false;//is auto fill for pay cr from dr info
    };
    //on row select
    $scope.selectStdTransaction = function ($index) {
        $scope.SelectedStdTransactionIndex = $index;
        var objOfRow = $scope.StdTransactionList[$index];
        $scope.StdTransactionId = objOfRow.Id;

    };
    //clone row object from StdTransactionList by index
    $scope.getCloneStdTransByRowIndex = function ($index) {
        if ($index < 0)
            return;
        $scope.SelectedStdTransactionIndex = $index;
        var objOfRow = $scope.StdTransactionList[$index];
        var objClone = new Object();
        angular.copy(objOfRow, objClone);
        $scope.StdTransactionId = objOfRow.Id;
        $scope.StdTransaction = objClone;
    }


    $scope.onAfterLoadStdTransactionExtraData = function (result) {
        //var DataExtra=result.DataExtra;
        //DropDown Option Tables
        if (result.DataExtra.SemesterList != null)
            $scope.SemesterList = result.DataExtra.SemesterList;
        if (result.DataExtra.StudentList != null)
            $scope.StudentList = result.DataExtra.StudentList;
        /*
        //Child Table Bindings, need to fix
                   $scope.TrasectionIdList =  result.DataExtra.TrasectionIdList; 
           */
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variabales goes hare=====

    $scope.Init = function (studentId
        , getTransactionListByStudentIdUrl
        , getAddPaymentExtraDataUrl
        , getStdTransactionByIdUrl
        , saveStdTransactionUrl
        , payDrAddCrTransactionUrl
        , deleteStdTransactionByIdUrl
        , viewPaymentDetailsByStudentIdSemesterIdUrl
        , balanceRecalculateByStudentIdUrl) {

        $scope.StudentId = studentId;
        $scope.getTransactionListByStudentIdUrl = getTransactionListByStudentIdUrl;
        $scope.getAddPaymentExtraDataUrl = getAddPaymentExtraDataUrl;
        $scope.getStdTransactionByIdUrl = getStdTransactionByIdUrl;
        $scope.saveStdTransactionUrl = saveStdTransactionUrl;
        $scope.payDrAddCrTransactionUrl = payDrAddCrTransactionUrl;
        $scope.deleteStdTransactionByIdUrl = deleteStdTransactionByIdUrl;
        $scope.viewPaymentDetailsByStudentIdSemesterIdUrl = viewPaymentDetailsByStudentIdSemesterIdUrl;
        $scope.balanceRecalculateByStudentIdUrl = balanceRecalculateByStudentIdUrl;

        $scope.loadPage(studentId);
        $scope.forTransactionType();
    };


    //======Other tabale's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

    //Searching for Particular Type
    $('#textDropdownParticularType').find('input').click(function (e) {
        e.stopPropagation();
    });


    //Searching for Bank Name
    $('#textDropdownBankName').find('input').click(function (e) {
        e.stopPropagation();
    });

    $scope.LoadPaymentDetails=function(semesterId) {
        LoadPaymentDetailsForAccount($scope.StudentId,semesterId);
    }
    var LoadPaymentDetailsForAccount = function (studentId, semesterId) {  // "&semesterId=201701"
        $.get($scope.viewPaymentDetailsByStudentIdSemesterIdUrl + "/?studentId=" + studentId + "&semesterId=" + semesterId, function (data) {
            $(".payment-details").html(data);
            $(".assessment").addClass("table table-hover table-responsive");
            $('#showAssesmentModal').modal('show');
        }).done(function (result) {

        }).fail(function (result) {
            $(".payment-details").html("Data loading failed!");
        });
    }

});

