
//File:Student Payment Collection Anjular  Controller
emsApp.controller('FastPaymentCollection', function ($scope, $http, $filter, $document, cfpLoadingBar, $sce) {

    $scope.Student = null;
    $scope.ErrorMsg = "";
    $scope.HasError = false;

    $scope.SearchUserName = "";
    $scope.StudentId = "";
    $scope.SelectedSemesterId = "";
    $scope.SelectedSemesterName = "";
    $scope.PreviousBalanceTransaction = [];
    $scope.ThisSemesterBalanceTransaction = [];
    $scope.NetBalanceTransaction = [];
    $scope.CollectionTransaction = null;
    $scope.CollectionTransactionDetail = null;
    $scope.SelectedBankId = 0;
    $scope.SelectedBankName = "";
    $scope.StudentTransaction = [];
    $scope.isCheckedAllSemester = false;
    $scope.AllSemesterList = null;

    $scope.ThisSemesterPayable = "";
    $scope.PreviousBalance = "";
    $scope.CurrentBalance = "";
    $scope.ThisSemesterTotalPaid = "";
    $scope.Balance = "";
    $scope.CurrentPay = 0.00;
    $scope.CurrentPayable = 0;
    $scope.AddableCreditList = true;
    $scope.CollectionItem = [];
    $scope.CanEditCollection = false;
    $scope.CurrentSemesterId = "";

    $scope.WaitForCreditSaving = false;//detect when saving a credit item start
    $scope.WaitForDebitSaving = false;//detect when saving a credit item start

    // Semester Exam Term Bill And Due
    $scope.MidtermBill = 0;
    $scope.MidtermDue = 0;
    $scope.FinalTermBill = 0;
    $scope.FinalTermDue = 0;
    $scope.AdmissionFeeDue = 0;

    $scope.RefreshUi = function () {

        $scope.MidtermBill = 0;
        $scope.MidtermDue = 0;
        $scope.FinalTermBill = 0;
        $scope.FinalTermDue = 0;
        $scope.AdmissionFeeDue = 0;
    };



    $scope.KeyEvent = null;
    $scope.loadPage = function () {
        $scope.getFastPaymentCollectionDataExtra();

        if ($scope.SearchUserName.length > 3) {
            $scope.getStudentByUserName();
        }
        //test key code https://keycode.info/
        $document.bind('keydown', function (e) {//keyup angular
            if ($scope.Student == null) {
                return;
            }
            // $(document).keydown(function (e) {//jquery
           
            //if (e.which === 89 && e.ctrlKey) {
            //    alert('control + y');
            //}
            //else if (e.which === 90 && e.ctrlKey) {
            //    alert('control + z');
            //}

            $scope.$apply(function () {
                $scope.KeyEvent = e;
                if (e.which === 89 && e.ctrlKey) {
                    log('control + y');
                }
               // log(e)
               
              
                //show credit tab
                if (e.which === 68 && e.shiftKey) {
                    log('shiftKey + d');
                    $('.nav-tabs a[href="#debit-payable-items"]').tab('show');
                    return;
                }
                //show credit tab
                if (e.which === 67 && e.shiftKey) {
                    log('shiftKey + c');
                    $('.nav-tabs a[href="#credit-paid-items"]').tab('show');
                    return;
                }
                //show trash tab
                if (e.which === 84 && e.shiftKey) {
                    log('shiftKey + t');
                    $('.nav-tabs a[href="#trash"]').tab('show');
                    return;
                }
                //show PackageDiscount tab
                if (e.which === 80 && e.shiftKey) {
                    log('shiftKey + p');
                    $('.nav-tabs a[href="#PackageDiscount"]').tab('show');
                    return;

                }   //show SemesterBills tab
                if (e.which === 83 && e.shiftKey) {
                    log('shiftKey + s');
                    $('.nav-tabs a[href="#SemesterBills"]').tab('show');
                    return;
                }
                //show AllCollection tab
                if (e.which === 65 && e.shiftKey) {
                    log('shiftKey + a');
                    $('.nav-tabs a[href="#AllCollection"]').tab('show');
                    return;
                }
                //search student Focus
                if (e.altKey && e.ctrlKey && e.which === 96) {
                    log('ctrl+altKey + 0');
                    $('#SearchByStdId').focus();
                    //$('#SearchByStdId').val('');
                    $scope.SearchUserName = "";
                    return;
                }
                //add new debit modal e.which === 78
                if (e.which === 99 && e.ctrlKey && e.altKey) {
                    log('ctrl+altKey + 3');
                    //$('#editDebitTransactionModal').modal('show');
                    //$('#addDebitTransactionModal').modal('show');
                    $scope.addDebitTransactionModelShow();
                    return;
                }
                //add semester payment cedit directly
                if (e.which === 98 && e.ctrlKey && e.altKey) {
                    log('ctrl+altKey + 2');
                    //$('#editDebitTransactionModal').modal('show');
                    //$('#addDebitTransactionModal').modal('show');
                    $scope.addBlankSemesterPaymentCreditItem();
                    return;
                }
                // create collection credit saveStdCreditTransaction()
                if (e.which === 13 && e.altKey && e.ctrlKey) {
                    //alertWarning("ctrl+alt enter pressed");
                    $scope.saveStdCreditTransaction();
                    return;
                }
                // add new debit $scope.saveStdDebitTransaction
                if (e.which === 13 && e.ctrlKey) {
                    //alertWarning("ctrl enter pressed");
                    if ($("#addDebitTransactionModal").css('display').toLowerCase() === 'block') {
                        $scope.saveStdDebitTransaction(true);
                    }
                    return;
                }

                //todo key test
                if (e.which === 13 && e.ctrlKey && e.shiftKey && e.altKey) {
                    //alertWarning("ctrl+alt+shift enter pressed");
                    return;
                }
                if (e.which === 13 && e.ctrlKey && e.shiftKey) {
                    //alertWarning("ctrl+shift enter pressed");
                    return;
                }

                log(e.which);
            });//scope apply
        });
    };

    $scope.putStudent = function () {
        if ($scope.StudentList === null) {
            $scope.Student.FullName = "Not Found";
        }
        $scope.Student = $scope.StudentList[0];
    }
    /*For Search on data start*/

    /*For Search on data end*/

    // Api Calling Start
    $scope.getStudentByUserName = function () {

        if ($scope.SearchUserName == null || $scope.SearchUserName.length <= 3) {
            alertWarning("Invalid Student Id to search!");
            return;
        }
        //show PackageDiscount tab
        //if ($scope.KeyEvent!=null && $scope.KeyEvent.which === 90 && $scope.KeyEvent.which === 13) {
        //    log('shiftKey + p');
        //    // $('.nav-tabs a[href="#PackageDiscount"]').tab('show');

        //    alertWarning("test");
        //    return;

        //}
        $http.get($scope.getStudentByUserNameUrl, {
            params: {
                "stdUserName": $scope.SearchUserName
            }

        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.Student = result.Data;
                $scope.StudentId = $scope.Student.Id;
                //$scope.PackageDetails = result.DataExtra.PackageDetails;
                $scope.SelectedBankId = result.DataExtra.SelectedBankId;
                $scope.SelectedBankName = result.DataExtra.SelectedBankName;

                $scope.DateTimeNow = result.DataExtra.DateTimeNow;
                //set black collection trans
                $scope.CollectionTransaction = $scope.getBlankCollectionTransaction();
                $scope.CollectionTransactionDetail = $scope.getBlankCollectionTransactionDetail();

                $('#VoucherNoText').focus();
                updateUrlQuery('sid', $scope.SearchUserName);
                $scope.getTransactionListByStudentId();

            } else {
                $scope.HasError = true;
                $scope.Student =null;
                updateUrlQuery('sid', $scope.SearchUserName);
                $scope.ErrorMsg = result.Errors.toString() + " Unable to get Student data!";
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.Student = null;
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Student ! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getAllSemesterList = function () {
        $http.get($scope.getAllSemesterListUrl)
            .success(function (result) {
                if (!result.HasError) {

                    $scope.AllSemesterList = result.DataExtra.AllSemesterList; // $scope.AllSemesterList store next time uses
                    $scope.SemesterList = $scope.AllSemesterList; // $scope.SemesterList set in all Dropdown

                    if ($scope.SelectedSemesterId == null || $scope.SelectedSemesterId === "") {
                        $scope.SelectedSemesterId = result.DataExtra.SelectedSemesterId;
                        $scope.SelectedSemesterName = result.DataExtra.SelectedSemesterName;
                    }


                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to get Student data! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
            
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Student ! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
                alertError($scope.ErrorMsg);
            });
    };
    $scope.getTransactionListByStudentId = function () {
        $http.get($scope.getTransactionListByStudentIdUrl, {
            params: {
                "id": $scope.StudentId,
                "isCheckedAllSemester": $scope.isCheckedAllSemester,
                "feeCodeId": $scope.Student.FeeCodeId
            }

        }).success(function (result) {
            if (!result.HasError) {

                $scope.StudentTransactionList = result.Data;
                $scope.TransactionSemesterList = result.DataExtra.TransactionSemesterList; // $scope.TransactionSemesterList store next time uses
                $scope.SemesterList = result.DataExtra.TransactionSemesterList; // $scope.SemesterList set in all Dropdown

                if ($scope.SelectedSemesterId === null || $scope.SelectedSemesterId === "") {
                    $scope.SelectedSemesterId = result.DataExtra.SelectedSemesterId;
                    $scope.SelectedSemesterName = result.DataExtra.SelectedSemesterName;
                }
                $scope.CurrentSemesterId = angular.copy(result.DataExtra.SelectedSemesterId);
                $scope.CollectionTransaction.SemesterId = result.DataExtra.SelectedSemesterId;
                $scope.SemesterDebitList = result.DataExtra.SemesterDebitList;
                $scope.SemesterCreditList = result.DataExtra.SemesterCreditList;
                $scope.TotalPackageAmount = result.DataExtra.TotalPackageAmount;
                //AdmissionFeeDue
                $scope.AdmissionFeeDue = result.DataExtra.AdmissionFeeDue;

                //$scope.MidtermBill = result.DataExtra.MidtermBill;
                $scope.MidtermDue = result.DataExtra.MidtermDue;


                

                //new
                $scope.PackageDetails = result.DataExtra.PackageDetails;

                $scope.PreviousBalanceTransaction = result.DataExtra.PreviousBalanceTransaction;
                $scope.ThisSemesterBalanceTransaction = result.DataExtra.ThisSemesterBalanceTransaction;
                $scope.NetBalanceTransaction = result.DataExtra.NetBalanceTransaction;

                // Semester Exam Term Bill And Due
                $scope.semesterBillAndDue();

                if ($scope.SelectedSemesterId !== "") {
                    $scope.perSemesterBalanceCalculation();
                }

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Transaction List! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
    
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Transaction List! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getDeleteUndeleteDebitTransactionByIdStdId = function (row) {

        var msg = "Are you sure you want to delete this data?";
        if (row.IsDeleted) {
            msg = "Are you sure you want to Restore this data?";
        }
        bootbox.confirm(msg, function (yes) {
            if (yes) {
                $http.get($scope.getDeleteUndeleteDebitTransactionByIdStdIdUrl, {
                    params: {
                        "id": row.Id,
                        "stdId": $scope.StudentId
                    }

                }).success(function (result) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        $scope.getTransactionListByStudentId();

                    } else {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Unable to Delete Transaction! " + result.Errors.toString();
                        alertError($scope.ErrorMsg);
                    }
                }).error(function (result, status) {
      
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to Delete Transaction! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
                    alertError($scope.ErrorMsg);
                });
            }
        });
    };
    $scope.getDeleteUndeleteCreditTransactionByIdStdId = function (row) {
        var msg = "Are you sure you want to delete this data?";
        if (row.IsDeleted) {
            msg = "Are you sure you want to Restore this data?";
        }
        bootbox.confirm(msg, function (yes) {
            if (yes) {
                $http.get($scope.getDeleteUndeleteCreditTransactionByIdStdIdUrl, {
                    params: {
                        "id": row.Id,
                        "stdId": $scope.StudentId
                    }

                }).success(function (result) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        $scope.getTransactionListByStudentId();

                    } else {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Unable to Delete Transaction! " + result.Errors.toString();
                        alertError($scope.ErrorMsg);
                    }
                }).error(function (result, status) {
          
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to Delete Transaction! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
                    alertError($scope.ErrorMsg);
                });
            }
        });
    };

  

    $scope.saveStdCreditTransaction = function () {
        if ($scope.CanEditCollection === false && $scope.CollectionTransaction.Id!==0) {
            alertError("To Update this Collection First Enable Edit Mode");
            return;
        }

        //if ($scope.CollectionTransaction.CreditAmount <= 0) {
        //    alertError("Payment Saving Failed! Current Pay can't be zero or less then zero.");
        //    return;
        //}
        if (!$scope.validateStdTransaction()) {
            return;
        }
        if ($scope.WaitForCreditSaving) {
            alertWarning("Please Wait For Saving The Credit Transaction.");
            return;
        }
        $scope.WaitForCreditSaving = true;
        $http.post($scope.saveStdCreditTransactionUrl + "/", $scope.CollectionTransaction).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.Data != null) {

                        $scope.getTransactionListByStudentId();
                        $scope.CollectionTransaction = [];
                        $scope.CollectionTransaction = result.Data;
                    }
                    $scope.CanEditCollection = false;
                    alertSuccess("Successfully Saved Student Payment!");
                    $scope.WaitForCreditSaving = false;
                    window.open($scope.studentMoneyReceiptUrl + "?transId=" + $scope.CollectionTransaction.Id + "&" + "stdId=" + $scope.StudentId);


                    $('.nav-tabs a[href="#credit-paid-items"]').tab('show');
                    if (result.DataExtra.IsCompleate) {
                        alert("25 Collection Compleated Using This Voucher!");
                    }

                    //$scope.onAfterSaveStdTransaction(result);
                } else {
                    $scope.HasError = true;
                    $scope.WaitForCreditSaving = false;
                    $scope.ErrorMsg = result.Errors.toString() + " Payment Saving Failed!";
                    alertError($scope.ErrorMsg);
                }
               
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.WaitForCreditSaving = false;
                $scope.ErrorMsg = "Unable to save Payment! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };
    $scope.saveStdDebitTransaction = function (isNewDebit) {
        if (isNewDebit) {
            $scope.StudentTransaction.StdTransactionDetailListJson[0].Amount = $scope.StudentTransaction.DebitAmount;
            $scope.StudentTransaction.StdTransactionDetailListJson[0].IsPackagePayment = $scope.StudentTransaction.IsPackagePayment;
            $scope.StudentTransaction.StdTransactionDetailListJson[0].Remark = $scope.StudentTransaction.Remark;
        }

        if ($scope.StudentTransaction == null || $scope.StudentTransaction===[]) {
            alertError("Don't have any Transaction for Save!");
            return;
        }

        if (!$scope.validateStdTransaction()) {
            return;
        }

        if ($scope.WaitForDebitSaving) {
            alertWarning("Please Wait For Saving The Debit Transaction!");
            return;
        }

        $scope.WaitForDebitSaving = true;
        $http.post($scope.saveStdDebitTransactionUrl + "/", $scope.StudentTransaction).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    $scope.WaitForDebitSaving = false;
                    if (result.Data != null) {

                        $scope.getTransactionListByStudentId();

                       
                            if (isNewDebit) {
                                $scope.NewDebit = result.Data;
                                $scope.NewDebit.IsSelected = true;
                                $scope.StudentTransaction = null;

                                if($scope.NewDebit.DebitAmount>0)
                                $scope.addRemoveTransactionInPaymentCollectionList($scope.NewDebit);
                            }


                    }
                    $('#addDebitTransactionModal').modal('hide');
                    $('.nav-tabs a[href="#debit-payable-items"]').tab('show');
                    alertSuccess("Debit Transaction Added Successfully!");
                    //$scope.onAfterSaveStdTransaction(result);
                } else {
                    $scope.HasError = true;
                    $scope.WaitForDebitSaving = false;
                    $scope.ErrorMsg = result.Errors.toString() + " Unable to save Debit Transaction!";
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.WaitForDebitSaving = false;
                $scope.ErrorMsg = "Unable to save Debit Transaction! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };
    $scope.getFastPaymentCollectionDataExtra = function () {

        $http.get($scope.getFastPaymentCollectionDataExtraUrl)
            .success(function (result) {
                if (!result.HasError) {
                    if (result.DataExtra.OfficialBankList != null) {
                        $scope.OfficialBankList = result.DataExtra.OfficialBankList;
                    }
                    if (result.DataExtra.TransactionTypeEnumList != null) {
                        $scope.TransactionTypeEnumList = result.DataExtra.TransactionTypeEnumList;
                    }
                    if (result.DataExtra.ParticularNameList != null) {
                        $scope.ParticularNameList = result.DataExtra.ParticularNameList;
                    }

                    $scope.PaymentStatusEnumList = result.DataExtra.PaymentStatusEnumList;

                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to get Data Extra! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            })
            .error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Data Extra! " + "Status: " + status.toString() + ". " + result.Message.toString();
                alertError($scope.ErrorMsg);
            });
    };
    $scope.getBalanceRecalculateByStudentId = function () {
        $http.get($scope.getBalanceRecalculateByStudentIdUrl, {
            params: {
                "studentId": $scope.StudentId
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.getTransactionListByStudentId();
                alertSuccess("Balance Recalculate Successfully!");

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to Recalculate Balance! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to Recalculate Balance! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };


    $scope.getRegenerateStudentBillByStdId = function () {
        $http.get($scope.getRegenerateStudentBillByStdIdUrl, {
            params: {
                "stdId": $scope.StudentId
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.getTransactionListByStudentId();
                alertSuccess("Regenerate Student Bill Successfully!");

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to Regenerate Student Bill! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to Regenerate Student Bill ! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };

    // Api Calling End

    $scope.validateStdTransaction = function () {
        return true;
    };

    // Local Function Start 
    $scope.newPayment = function () {
        if ($scope.CollectionTransaction != null && $scope.CollectionTransaction.StdTransactionDetailListJson != null) {
            $scope.clearPaymentCollectionList();
        }
        $scope.CanEditCollection = true;
        $scope.CollectionTransaction = $scope.getBlankCollectionTransaction();
    };

    $scope.addCollectionItemsModelOpen = function () {
        if ($scope.CanEditCollection === false && $scope.CollectionTransaction.Id !== 0) {
            alertWarning("To Add new Credit Item In this Collection, First Enable Edit Mode or Click 'Start New Payslip'!");
            //row.IsSelected = false;
            return;
        }
        if ($scope.CollectionTransaction == null) {
            $scope.CollectionTransaction = $scope.getBlankCollectionTransaction();
        }
        $scope.CollectionItem = $scope.getBlankCollectionTransactionDetail();
        $('#addCreditItemModal').modal('show');
    };
    $scope.addBlankSemesterPaymentCreditItem = function () {//addBlankSemesterPaymentCreditItem
        if ($scope.CanEditCollection === false && $scope.CollectionTransaction.Id !== 0) {
            alertWarning("To Add new Credit Item In this Collection, First Enable Edit Mode or Click 'Start New Payslip'!");
            //row.IsSelected = false;
            return;
        }
        if ($scope.CollectionTransaction == null) {
            $scope.CollectionTransaction = $scope.getBlankCollectionTransaction();
        }
        //$scope.CollectionTransaction.Name = "Semester Payment";
        $scope.CollectionTransaction.IsSemesterFee = true;
        //$scope.CollectionTransaction.ParticularId = 8;
        //$scope.CollectionTransaction.ParticularTypeEnumId = 6; //IsPackagePayment
        $scope.CollectionTransaction.ParticularTypeEnumId = 6;

        $scope.CollectionItem = $scope.getBlankCollectionTransactionDetail();



        $scope.CollectionItem.ParticularId = 8;
        $scope.CollectionItem.IsPackagePayment = true;
        //$scope.CollectionTransaction.ParticularTypeEnumId = 6;

        $scope.addCollectionItems();
    };

    $scope.addCollectionItems = function () {

        if ($scope.CollectionItem.ParticularId === null || $scope.CollectionItem.ParticularId === 0) {
            alertError("Please Select Particular");
            return;
        }

        if ($scope.CanEditCollection === false && $scope.CollectionTransaction.Id !== 0) {
            alertWarning("To Add new Credit Item In this Collection, First Enable Edit Mode or Click 'Start New Payslip'!");
            //row.IsSelected = false;
            return;
        }

        var findParticular =
            $filter('filter')($scope.ParticularNameList, { Id: $scope.CollectionItem.ParticularId }, true)[0];

        $scope.CollectionItem.Name = findParticular.Name;
        $scope.CollectionItem.ParticularTypeEnumId = findParticular.ParticularTypeEnumId;
        $scope.CollectionItem.SemesterId = $scope.SelectedSemesterId;
        $scope.CollectionItem.StudentId = $scope.StudentId;

        if ($scope.CollectionTransaction.Name === "" || $scope.CollectionTransaction.Name === null) {
            $scope.CollectionTransaction.Name = findParticular.Name;
        }
        $scope.CollectionTransaction.StdTransactionDetailListJson.push($scope.CollectionItem);
        $scope.CollectionItem = $scope.getBlankCollectionTransactionDetail();

    };
    $scope.onChangeSemester = function () {

        $scope.SelectedSemesterName =
            $filter('filter')($scope.SemesterList, { Id: $scope.SelectedSemesterId }, true)[0].Name;

        $scope.perSemesterBalanceCalculation();

        // Semester Exam Term Bill And Due
        $scope.semesterBillAndDue();


        // Clear Right Side Collection Panel
        // $scope.CollectionTransaction.StdTransactionDetailListJson = [];


        //$scope.alreadySelectedTransPushInPaymentCollectionList();
        //$scope.unselectedDebitTransaction();

    };

    $scope.semesterBillAndDue = function () {

        // Semester Exam Term Bill And Due
        var semesterId = Number($scope.SelectedSemesterId);
        $scope.SemesterTermBillAndDueObj = $filter('filter')($scope.SemesterDebitList.SemesterList, { SemesterId: semesterId }, true)[0];


        if ($scope.SemesterTermBillAndDueObj != null || $scope.SemesterTermBillAndDueObj !== undefined) {
            $scope.MidtermBill = $scope.SemesterTermBillAndDueObj.MidtermBill;
            $scope.MidtermDue = $scope.SemesterTermBillAndDueObj.MidtermDue;
            $scope.FinalTermBill = $scope.SemesterTermBillAndDueObj.FinalTermBill;
            $scope.FinalTermDue = $scope.SemesterTermBillAndDueObj.FinalTermDue;
        } else {
            $scope.RefreshUi();
        }
        
    };

    $scope.perSemesterBalanceCalculation = function () {
        // This Semester Calculation
        $scope.ThisSemesterDebitTransactionList = $filter('filter')($scope.StudentTransactionList,
            {
                SemesterId: $scope.SelectedSemesterId,
                IsDebited: true,
                IsDeleted: false
            }, true);

        $scope.ThisSemesterCreditTransactionList = $filter('filter')($scope.StudentTransactionList,
            {
                SemesterId: $scope.SelectedSemesterId,
                IsDebited: false,
                IsDeleted: false
            }, true);

        $scope.ThisSemesterTotalPaid = $scope.total($scope.ThisSemesterCreditTransactionList, "CreditAmount");

        $scope.ThisSemesterPayable = $scope.total($scope.ThisSemesterDebitTransactionList, "DebitAmount");
        //$scope.CurrentBalance = $scope.ThisSemesterPayable - $scope.ThisSemesterTotalPaid;

        $scope.ThisSemesterBalanceTransaction.DebitAmount = $scope.CurrentBalance;


        // Total Balance Calculation
        $scope.TotalDebitList = $filter('filter')($scope.StudentTransactionList,
            {
                IsDebited: true,
                IsDeleted: false
            }, true);
        $scope.TotalCreditList = $filter('filter')($scope.StudentTransactionList,
            {
                IsDebited: false,
                IsDeleted: false
            }, true);

        $scope.TotalDebitAmount = $scope.total($scope.TotalDebitList, "DebitAmount");
        $scope.TotalCreditAmount = $scope.total($scope.TotalCreditList, "CreditAmount");

        $scope.Balance = $scope.TotalDebitAmount - $scope.TotalCreditAmount;
        $scope.NetBalanceTransaction.DebitAmount = $scope.Balance;


        // Previous Semester Calculation
        $scope.getPreviousSemesterList();

        $scope.TotalPreviousSemesterDebitList = $filter('filter')($scope.PreviousSemesterList,
            {
                IsDebited: true,
                IsDeleted: false

            }, true);
        $scope.TotalPreviousSemesterCreditList = $filter('filter')($scope.PreviousSemesterList,
            {
                IsDebited: false,
                IsDeleted: false

            }, true);

        $scope.TotalPreviousSemesterDebitAmount = $scope.total($scope.TotalPreviousSemesterDebitList, "DebitAmount");
        $scope.TotalPreviousSemesterCreditAmount = $scope.total($scope.TotalPreviousSemesterCreditList, "CreditAmount");

        $scope.PreviousBalance = $scope.TotalPreviousSemesterDebitAmount - $scope.TotalPreviousSemesterCreditAmount;
        $scope.CurrentBalance = $scope.ThisSemesterPayable - $scope.ThisSemesterTotalPaid + $scope.PreviousBalance;

        $scope.PreviousBalanceTransaction.DebitAmount = $scope.PreviousBalance;
    };
    $scope.getPreviousSemesterList = function () {

        $scope.PreviousSemesterList = [];

        if ($scope.StudentTransactionList.length === 0 || $scope.StudentTransactionList === null) {
            return;
        }

        for (var i = 0; i < $scope.StudentTransactionList.length; i++) {

            if ($scope.StudentTransactionList[i].SemesterId < $scope.SelectedSemesterId) {
                $scope.PreviousSemesterList.push($scope.StudentTransactionList[i]);
            }
        }
    };
    $scope.total = function(calculatedList, propertyName) {
        var totalValue = 0;
        for (var i = 0; i < calculatedList.length; i++) {

            if (calculatedList[i][propertyName] == null) {
                continue;
            }
            totalValue += calculatedList[i][propertyName];
        }
        return totalValue;
    };
    $scope.addRemoveTransactionInPaymentCollectionList = function (row) {

        if (row.IsSelected) {

            if ($scope.CanEditCollection === false && $scope.CollectionTransaction.Id!==0) {
                alertWarning("To Add new Credit Item In this Collection, First enable New Payment or Edit Mode");
                row.IsSelected = false;
                return;
            }

            row.IsSelected = true;

            var transDtl = $scope.mapTransactionDetailFromTransaction(row);



            //Is selected Net Balance then clean PaymentCollectionList then push Net Balance
            if (transDtl.Id < -1) {
                $scope.clearPaymentCollectionListForNetBalance(row);
            }

            $scope.CollectionTransaction.StdTransactionDetailListJson.push(transDtl);
            

        } else {

            var obj = $filter('filter')($scope.CollectionTransaction.StdTransactionDetailListJson, { Id: row.Id, IsFromTransaction: true })[0];
            var index = $scope.CollectionTransaction.StdTransactionDetailListJson.indexOf(obj);
            row.IsSelected = false;
            $scope.CollectionTransaction.StdTransactionDetailListJson.splice(index, 1);
            
            if ($scope.CollectionTransaction.StdTransactionDetailListJson.length === 1) {

                $scope.CollectionTransaction.Name = $scope.CollectionTransaction.StdTransactionDetailListJson[0].Name;
            }

        }
    };

    $scope.addRemoveTransactionDetailInPaymentCollectionList = function (debitTransDetail) {

        if (debitTransDetail.IsSelected) {

            if ($scope.CanEditCollection === false && $scope.CollectionTransaction.Id !== 0) {
                alertWarning("To Add new Credit Item In this Collection, First enable New Payment or Edit Mode");
                debitTransDetail.IsSelected = false;
                return;
            }

            debitTransDetail.IsSelected = true;

            var transDtl = $scope.mapTransactionDetailFromTransactionDetail(debitTransDetail);



            //Is selected Net Balance then clean PaymentCollectionList then push Net Balance
            //if (transDtl.Id < -1) {
            //    $scope.clearPaymentCollectionListForNetBalance(row);
            //}

            $scope.CollectionTransaction.StdTransactionDetailListJson.push(transDtl);
            

        } else {

            var obj = $filter('filter')($scope.CollectionTransaction.StdTransactionDetailListJson, { Id: debitTransDetail.Id, IsFromTransaction: false })[0];
            var index = $scope.CollectionTransaction.StdTransactionDetailListJson.indexOf(obj);
            debitTransDetail.IsSelected = false;
            $scope.CollectionTransaction.StdTransactionDetailListJson.splice(index, 1);

            if ($scope.CollectionTransaction.StdTransactionDetailListJson.length === 1) {

                $scope.CollectionTransaction.Name = $scope.CollectionTransaction.StdTransactionDetailListJson[0].Name;
            }

        }
    };



    $scope.removeTransactionFromPaymentCollectionList = function (index) {

        var obj = $scope.CollectionTransaction.StdTransactionDetailListJson[index];
        // This Condition for $scope.StudentTransactionList
        if (obj.Id > 0) {

            var removeObj = $filter('filter')($scope.StudentTransactionList, { Id: obj.Id }, true)[0];
            removeObj.IsSelected = false;

        } else {
            if (obj.Id === -1) {
                $scope.PreviousBalanceTransaction.IsSelected = false;
            }
            if (obj.Id === -2) {
                $scope.ThisSemesterBalanceTransaction.IsSelected = false;
            }
            if (obj.Id === -3) {
                $scope.NetBalanceTransaction.IsSelected = false;
            }
        }

        $scope.CollectionTransaction.StdTransactionDetailListJson.splice(index, 1);

        if ($scope.CollectionTransaction.StdTransactionDetailListJson.length === 1) {

            $scope.CollectionTransaction.Name = $scope.CollectionTransaction.StdTransactionDetailListJson[0].Name;
        }
    };
    $scope.alreadySelectedTransPushInPaymentCollectionList = function () {

        for (var i = 0; i < $scope.ThisSemesterDebitTransactionList.length; i++) {

            if ($scope.ThisSemesterDebitTransactionList[i].IsSelected) {
                $scope.addRemoveTransactionInPaymentCollectionList($scope.ThisSemesterDebitTransactionList[i]);
            }
        }

    };
    $scope.clearPaymentCollectionListForNetBalance = function (row) {
        var previousBlncObj = null;
        for (var i = 0; i < $scope.CollectionTransaction.StdTransactionDetailListJson.length; i++) {
            $scope.unselecteObj = $filter('filter')($scope.StudentTransactionList,
                { Id: $scope.CollectionTransaction.StdTransactionDetailListJson[i].Id })[0];
            if ($scope.unselecteObj != null) {
                $scope.unselecteObj.IsSelected = false;
            }

            if (row.Id === -2 && $scope.CollectionTransaction.StdTransactionDetailListJson[i].Id === -1) {
                previousBlncObj = $scope.CollectionTransaction.StdTransactionDetailListJson[i];
            }
        }

        $scope.CollectionTransaction.StdTransactionDetailListJson = [];
        if (previousBlncObj != null) {
            $scope.CollectionTransaction.StdTransactionDetailListJson.push(previousBlncObj);
        }
    };

    $scope.mapTransactionDetailFromTransaction = function (trans) {
        var particularId;
        var transName;
        var particularTypeEnumId;
        $scope.CollectionTransaction.SemesterId = $scope.CurrentSemesterId; //$scope.SelectedSemesterId;
        $scope.CollectionTransaction.BankId = $scope.SelectedBankId;
        $scope.CollectionTransaction.BankName = $scope.SelectedBankName;
        // Check Semester Payment and set Particular Name and Id
        if (trans.IsSemesterFee === true) {
            transName = "Semester Payment";
            particularId = 8;
            particularTypeEnumId = 6;
            $scope.CollectionTransaction.Name = "Semester Payment";
        } else {
            transName = trans.Name;
            particularId = trans.StdTransactionDetailListJson[0].ParticularId;
            particularTypeEnumId = trans.StdTransactionDetailListJson[0].ParticularTypeEnumId;
            $scope.CollectionTransaction.Name = transName;
        }
        if ($scope.CollectionTransaction.StdTransactionDetailListJson.length > 0) {

            $scope.CollectionTransaction.Name = "Bill Collection";
        }

        var amount = 0;
        if (trans.DebitAmount - trans.CreditAmount > 0) {
            amount = trans.DebitAmount - trans.CreditAmount;
        }

        var collectionTransactionDetail = {
            "IsAlreadyAdded": false,
            "Id": trans.Id, //This Id Need For javascript validation
            "Name": transName,
            "ParticularId": particularId,
            "ParticularTypeEnumId": particularTypeEnumId,
            "TrasectionId": 0,
            "SemesterId": $scope.CurrentSemesterId, //$scope.SelectedSemesterId,
            "StudentId": trans.StudentId,
            "Amount": amount,
            "IsDebited": trans.IsDebited,
            "IsAutoEntry": false,
            "IsPackagePayment": trans.IsPackagePayment,
            "History": null,
            "OrderNo": 0,
            "IsDeleted": false,
            "IsSelected": false,
            "DebitId": trans.Id,
            "ActualAmount": trans.DebitAmount,
            "Remark": trans.Remark,
            "IsFromTransaction": true  // this transaction come from transaction, so it's true. This is use for unselect  and remove.

        };
        return collectionTransactionDetail;
    };

    $scope.mapTransactionDetailFromTransactionDetail = function (debitTransDetail) {
        

        $scope.CollectionTransaction.SemesterId = $scope.CurrentSemesterId; //$scope.SelectedSemesterId;
        $scope.CollectionTransaction.BankId = $scope.SelectedBankId;
        $scope.CollectionTransaction.BankName = $scope.SelectedBankName;
        // Check Semester Payment and set Particular Name and Id
        $scope.CollectionTransaction.Name = debitTransDetail.Name;

        if ($scope.CollectionTransaction.StdTransactionDetailListJson.length > 0) {
            $scope.CollectionTransaction.Name = "Bill Collection";
        }

        

        var collectionTransactionDetail = {
            "IsAlreadyAdded": false,
            "Id": debitTransDetail.Id, //This Id Need For javascript validation
            "Name": debitTransDetail.Name,
            "ParticularId": debitTransDetail.ParticularId,
            "ParticularTypeEnumId": debitTransDetail.ParticularTypeEnumId,
            "TrasectionId": 0,
            "SemesterId": $scope.CurrentSemesterId, //$scope.SelectedSemesterId,
            "StudentId": debitTransDetail.StudentId,
            "Amount": debitTransDetail.Amount,
            "IsDebited": debitTransDetail.IsDebited,
            "IsAutoEntry": false,
            "IsPackagePayment": debitTransDetail.IsPackagePayment,
            "History": null,
            "OrderNo": 0,
            "IsDeleted": false,
            "IsSelected": false,
            "DebitId": debitTransDetail.TrasectionId,
            "ActualAmount": debitTransDetail.Amount,
            "Remark": "",
            "IsFromTransaction": false  // this transaction come from transaction detail, so it's false. This is use for unselect  and remove.

        };
        return collectionTransactionDetail;
    };



    $scope.clearPaymentCollectionList = function () {
        for (var i = 0; i < $scope.CollectionTransaction.StdTransactionDetailListJson.length; i++) {

            $scope.unselecteObj = $filter('filter')($scope.StudentTransactionList,
                { Id: $scope.CollectionTransaction.StdTransactionDetailListJson[i].Id })[0];

            if ($scope.unselecteObj != null) {
                $scope.unselecteObj.IsSelected = false;
            }
        }
        //log("Call clearPaymentCollectionList");
        //$scope.CollectionTransaction.StdTransactionDetailListJson = [];
    };
    $scope.onChangeBank = function (id, receiveObj) {
        $scope.SelectedBankId = id;
        var officialBankObj = $filter('filter')($scope.OfficialBankList, { Id: id }, true)[0];
        receiveObj.BankName = officialBankObj.Name;
        $scope.SelectedBankName = officialBankObj.Name;
    };
    $scope.onChangeParticular = function (id) {
        var particularObj = $filter('filter')($scope.ParticularNameList, { Id: id }, true)[0];
        $scope.StudentTransaction.Name = particularObj.Name;
        $scope.StudentTransaction.StdTransactionDetailListJson[0].ParticularId = particularObj.Id;
        $scope.StudentTransaction.StdTransactionDetailListJson[0].Name = particularObj.Name;
        $scope.StudentTransaction.StdTransactionDetailListJson[0].ParticularTypeEnumId =
            particularObj.ParticularTypeEnumId;
    };
    $scope.onChangeTransDtlAmount = function (list) {
        if ($scope.StudentTransaction.IsDebited) {
            $scope.StudentTransaction.DebitAmount = $scope.total(list, "Amount");
        } else {
            $scope.StudentTransaction.CreditAmount = $scope.total(list, "Amount");
        }

    };
    $scope.editPaymentCollection = function ($index) {

        $scope.unselectedDebitTransaction();

        $scope.CanEditCollection = false;
        $scope.CollectionTransaction = angular.copy($scope.ThisSemesterCreditTransactionList[$index]);

        $scope.selectedCollectionTransactionIndex = $index;
    };
    $scope.getBlankCollectionTransaction = function () {
        var blankCollectionTransaction = {
            "SemesterName": null,
            "Amount": 0,
            "StdTransactionDetailListJson": [],
            "Id": 0,
            "Name": null,
            "StudentId": $scope.StudentId,
            "SemesterId": $scope.CurrentSemesterId,
            "BankId": $scope.SelectedBankId,
            "BankName": $scope.SelectedBankName,
            "PaySlipId": null,
            "ManualPaySlipNo": null,
            "VoucherId": null,
            "VoucherNo": null,
            "Date": $scope.DateTimeNow,
            "DebitAmount": 0,
            "CreditAmount": 0,
            "IsDebited": true,
            "Balance": 0,
            "Remark": null,
            "History": null,
            "PaymentStatusEnumId": 0,
            "PaymentStatus": null,
            "TransactionTypeEnumId": 0,
            "TransactionType": null,
            "IsAutoEntry": false,
            "IsSemesterFee": false,
            "IsPackagePayment": false,
            "OrderNo": 0,
            "IsDeleted": false,
            "CreateDate": $scope.DateTimeNow,
            "CreateById": null,
            "LastChanged": $scope.DateTimeNow,
            "LastChangedById": null,
            "IsSelected": false,
            "IsAlreadyAdded": false
        };
        return blankCollectionTransaction;
    };
    $scope.getBlankCollectionTransactionDetail = function () {
        var blankCollectionTransactionDetail = {
            "IsAlreadyAdded": false,
            "Id": 0,
            "Name": "",
            "ParticularId": 0,
            "ParticularTypeEnumId": 0,
            "TrasectionId": 0,
            "SemesterId": $scope.CurrentSemesterId,
            "StudentId": $scope.StudentId,
            "Amount": 0,
            "IsDebited": true,
            "IsAutoEntry": false,
            "IsPackagePayment": false,
            "History": null,
            "OrderNo": 0,
            "IsDeleted": false,
            "DebitId": null,
            "ParticularType": 0,
            "IsSelected": false
        };
        return blankCollectionTransactionDetail;
    };
    $scope.editDebitTransaction = function (row) {
        $scope.StudentTransaction = angular.copy(row);
        $('#editDebitTransactionModal').modal('show');
    };

    // This function remove after testing
    $scope.testEditCreditTransactionModalShow = function (row) {
        $scope.CollectionTransaction = angular.copy(row);
        $('#testEditCreditTransactionModal').modal('show');
    };

    $scope.editCreditTransactionModalShow = function (row) {
        $scope.CollectionTransaction = angular.copy(row);
        $('#editCreditTransactionModal').modal('show');
    };
    $scope.editStdCreditTransaction = function () {
        $scope.CanEditCollection = true;
        $scope.saveStdCreditTransaction();
    }
    $scope.addDebitTransactionModelShow = function () {
        if ($scope.SelectedSemesterId == null || $scope.SelectedSemesterId === "") {
            alertError("Select a Semester.");
            return;
        }

        $scope.StudentTransaction = [];
        $scope.StudentTransaction = $scope.getBlankCollectionTransaction();
        $scope.StudentTransaction.IsDebited = true;
        $scope.StudentTransaction.SemesterId = $scope.SelectedSemesterId;
        $scope.IsAutoEntry = false;
        var transDtel = $scope.getBlankCollectionTransactionDetail();

        transDtel.SemesterId = $scope.SelectedSemesterId;
        transDtel.IsDebited = true;
        $scope.StudentTransaction.StdTransactionDetailListJson.push(transDtel);
        $('#addDebitTransactionModal').modal('show');
        $('#addDebitTransactionModal').focus();
        $('#newDrEntryParticularName').focus();
    };
    $scope.collectionEditActive = function () {
        $scope.CanEditCollection = !$scope.CanEditCollection;
    }
    $scope.onChangeIsCheckedAllSemester = function () {
        if ($scope.isCheckedAllSemester === true) {

            if ($scope.AllSemesterList === null) {
                $scope.getAllSemesterList();
            } else {
                $scope.SemesterList = $scope.AllSemesterList;
            }

        } else {
            $scope.SemesterList = $scope.TransactionSemesterList;
        }
    };
    $scope.unselectedDebitTransaction = function () {
        for (var i = 0; i < $scope.ThisSemesterDebitTransactionList.length; i++) {
            if ($scope.ThisSemesterDebitTransactionList[i].IsSelected) {
                $scope.ThisSemesterDebitTransactionList[i].IsSelected = false;
            }
        }
    }
    // Local Function End 

    $scope.loadPaymentDetails = function (semesterId) {
        loadPaymentDetailsForAccount($scope.StudentId, semesterId);
    }

    var loadPaymentDetailsForAccount = function (studentId, semesterId) {
        $.get($scope.viewPaymentDetailsByStudentIdSemesterIdUrl + "/?studentId=" + studentId + "&semesterId=" + semesterId, function (data) {
            $(".payment-details").html(data);
            $(".assessment").addClass("table table-hover table-responsive");
            $('#showAssesmentModal').modal('show');
        }).done(function (result) {

        }).fail(function (result) {
            $(".payment-details").html("Data loading failed!");
        });
    }

    $scope.renderHtml = function (html_code) {
        return $sce.trustAsHtml(html_code);
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */

    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====
    $scope.Init = function (
        searchUserName
         , getStudentByUserNameUrl
        , getTransactionListByStudentIdUrl
        , viewPaymentDetailsByStudentIdSemesterIdUrl
        , saveStdCreditTransactionUrl
        , getNewTransactionByStudentIdUrl
        , getFastPaymentCollectionDataExtraUrl
        , saveStdDebitTransactionUrl
        , getAllSemesterListUrl
        , getDeleteUndeleteDebitTransactionByIdStdIdUrl
        , getDeleteUndeleteCreditTransactionByIdStdIdUrl
        , getBalanceRecalculateByStudentIdUrl
        , studentMoneyReceiptUrl
        , getRegenerateStudentBillByStdIdUrl
        ) {
        $scope.SearchUserName = searchUserName;
        $scope.getStudentByUserNameUrl = getStudentByUserNameUrl;
        $scope.getTransactionListByStudentIdUrl = getTransactionListByStudentIdUrl;
        $scope.viewPaymentDetailsByStudentIdSemesterIdUrl = viewPaymentDetailsByStudentIdSemesterIdUrl;
        $scope.saveStdCreditTransactionUrl = saveStdCreditTransactionUrl;
        $scope.getNewTransactionByStudentIdUrl = getNewTransactionByStudentIdUrl;
        $scope.getFastPaymentCollectionDataExtraUrl = getFastPaymentCollectionDataExtraUrl;
        $scope.saveStdDebitTransactionUrl = saveStdDebitTransactionUrl;
        $scope.getAllSemesterListUrl = getAllSemesterListUrl;
        $scope.getDeleteUndeleteDebitTransactionByIdStdIdUrl = getDeleteUndeleteDebitTransactionByIdStdIdUrl;
        $scope.getDeleteUndeleteCreditTransactionByIdStdIdUrl = getDeleteUndeleteCreditTransactionByIdStdIdUrl;
        $scope.getBalanceRecalculateByStudentIdUrl = getBalanceRecalculateByStudentIdUrl;
        $scope.studentMoneyReceiptUrl = studentMoneyReceiptUrl;
        $scope.getRegenerateStudentBillByStdIdUrl = getRegenerateStudentBillByStdIdUrl;
        $scope.loadPage();
    };

    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



