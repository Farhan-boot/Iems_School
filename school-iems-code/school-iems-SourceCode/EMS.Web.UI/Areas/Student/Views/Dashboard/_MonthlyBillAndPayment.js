
//File:BillAndPaymen Anjular  Controller
emsApp.controller('MonthlyBillAndPaymentCtrl', function ($scope, $http, $filter) {
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.IsShowPaymentMethodPage = false;
    $scope.IsPayAbleSelected = false;
    $scope.HasViewPermission = false;
    $scope.OnlinePaymentJson = [];
    $scope.CurrentStepNo = 0;
    $scope.FeeDetails = [];
    $scope.IsPrevoiusDueAvailable = false;

    $scope.loadPage = function () {
        $scope.getOnlinePaymentDataExtra();
    }

    $scope.getOnlinePaymentDataExtra = function () {
        $http.get($scope.getOnlinePaymentDataExtraUrl, {
            params: {}
        }).success(function (result) {
            if (!result.HasError) {

                $scope.HasViewPermission = result.HasViewPermission;

                $scope.OnlinePaymentJson = result.DataExtra.OnlinePaymentJson;

                if (result.DataExtra.TransactionSemesterList != null) {
                    $scope.TransactionSemesterList = result.DataExtra.TransactionSemesterList;
                }

                if (result.DataExtra.SelectedSemesterId != null) {
                    $scope.SelectedSemesterId = result.DataExtra.SelectedSemesterId;
                }

                if (result.DataExtra.SelectedSemesterName != null) {
                    $scope.SelectedSemesterName = result.DataExtra.SelectedSemesterName;
                }

                if (result.DataExtra.OnlinePaymentSemesterName != null) {
                    $scope.OnlinePaymentSemesterName = result.DataExtra.OnlinePaymentSemesterName;
                }

                if (result.DataExtra.PaymentGatewayList != null) {
                    $scope.PaymentGatewayList = result.DataExtra.PaymentGatewayList;
                }

                
                $scope.getMonthlyBill();

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Online Payment Data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }

        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Online Payment Data! " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);

        });
    };

    $scope.getMonthlyBill = function () {
        $http.get($scope.getMonthlyBillUrl, {
            params: {
                "semesterId": $scope.SelectedSemesterId
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.MonthlyBill = result.Data;
                log($scope.MonthlyBill);
                if (result.DataExtra.IsPreviousSemesterDueAvailable != null) {
                    $scope.IsPreviousSemesterDueAvailable = result.DataExtra.IsPreviousSemesterDueAvailable;

                    log(result.DataExtra.IsPreviousSemesterDueAvailable);
                    log($scope.IsPreviousSemesterDueAvailable);
                }
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Semester Bill! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }

        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Semester Bill! " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);

        });
    };

    $scope.saveOnlinePayment = function () {

        if ($scope.IsBusySaveResultComponent) {
            alertWarning("Please Wait! Another process running.");
            return;
        }
        $scope.IsBusySaveResultComponent = true;
        $http.post($scope.saveAndLockSingleScreenMarkEntryResultComponentListUrl + "/", $scope.SingleScreenMarkEntryJson).
            success(function (result) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    $scope.getSingleScreenMarkEntryResultByClassId();
                    alertSuccess("Data saved successfully!");

                } else {
                    $scope.HasError = true;
                    $scope.SingleScreenMarkEntryJson.LockRequestComponentSettingId = "";
                    $scope.ErrorMsg = "" + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
                $scope.IsBusySaveResultComponent = false;
            }).error(function (result, status) {
                $scope.HasError = true;
                $scope.SingleScreenMarkEntryJson.LockRequestComponentSettingId = "";
                $scope.ErrorMsg = "Unable to save Data! " + "Status: " + status.toString() + ". " + result.toString();
                alertError($scope.ErrorMsg);
                $scope.IsBusySaveResultComponent = false;
            });
    };


    $scope.onChangeSemester = function () {

        $scope.getMonthlyBill();
    }


    $("input:checkbox").on('click', function () {
        // in the handler, 'this' refers to the box clicked on
        var $box = $(this);
        if ($box.is(":checked")) {
            // the name of the box is retrieved using the .attr() method
            // as it is assumed and expected to be immutable
            var group = "input:checkbox[name='" + $box.attr("name") + "']";
            // the checked state of the group/box on the other hand will change
            // and the current value is retrieved using .prop() method
            $(group).prop("checked", false);
            $box.prop("checked", true);
        } else {
            $box.prop("checked", false);
        }

        if ($box.is(":checked")) {
            $scope.IsPayAbleSelected = true;
        } else {
            $scope.IsPayAbleSelected = false;
        }
       
    });

    $scope.nextStep=function() {
        $scope.CurrentStepNo = $scope.CurrentStepNo+1;
    }

    $scope.previousStep = function () {
        $scope.CurrentStepNo = $scope.CurrentStepNo - 1;
        $scope.FeeDetails = [];
    }

    $scope.getPaymentInfo = function (payment) {
        $scope.OnlinePaymentJson.OnlinePaymentDetailListJson = [];
        $scope.SelectedPaymentList = [];
        $scope.totalPay = 0;
        log($scope.MonthlyBill);
        var index = $scope.MonthlyBill.indexOf(payment);
        //console.log(payment);
        for (i = 0; i <= index; i++) {
            if ($scope.MonthlyBill[i].Due != 0) {
                if (i != 0) {
                    $scope.OnlinePaymentJson.OnlinePaymentDetailListJson.push({
                        DebitTransactionId : $scope.MonthlyBill[i].Id,
                        ParticularName: $scope.MonthlyBill[i].DebitName,
                        Amount: $scope.MonthlyBill[i].DuesUpto - $scope.MonthlyBill[i - 1].DuesUpto
                    });
                } else {
                    $scope.OnlinePaymentJson.OnlinePaymentDetailListJson.push({
                        DebitTransactionId: $scope.MonthlyBill[i].Id,
                        ParticularName: $scope.MonthlyBill[i].DebitName,
                        Amount: $scope.MonthlyBill[i].DuesUpto
                    });
                }
            }
        }
        angular.forEach($scope.OnlinePaymentJson.OnlinePaymentDetailListJson, function (value, key) {
            $scope.totalPay += Number(value.Amount || 0);
            $scope.OnlinePaymentJson.TotalAmount += Number(value.Amount || 0);
        });
        $scope.shouldPay = $scope.totalPay;

        $scope.SelectedPaymentList = $scope.OnlinePaymentJson.OnlinePaymentDetailListJson;
        $scope.CurrentStepNo = 1;
    }
    //======Custom property and Functions Start=======================================================  

    $scope.addNewVoucherDtl = function() {
        alert("hi");
    }

    $scope.IsOnlinePaymentShowAble = function () {
        $scope.paying = "";
        $scope.transactionDetails = [];

        var temp = $scope.totalPay;

        if (temp == undefined || temp == 0) {
            alertError("Please Select a Valid Amount");
            return false;
        } else if (temp > $scope.shouldPay) {
            alertError("You Can't Pay more than your Debit");
            return false;
        } else {
            angular.forEach($scope.OnlinePaymentJson.OnlinePaymentDetailListJson,
                function (value, index) {
                    if (value.Amount <= temp) {
                        $scope.paying += value.ParticularName + ":" + value.Amount + "<br/>";
                        temp -= value.Amount;
                        /*console.log(value);*/
                        $scope.transactionDetails.push({
                            DebitTransactionId: value.DebitTransactionId,
                            ParticularName: value.ParticularName,
                            Amount: value.Amount
                        });
                    } else if (value.Amount > temp) {
                        if (temp != 0) {
                            $scope.paying += value.ParticularName + ":" + temp + "<br/>";
                            $scope.transactionDetails.push({
                                DebitTransactionId: value.DebitTransactionId,
                                ParticularName: value.ParticularName,
                                Amount: temp
                            });
                            temp = 0;
                        }
                    }
                });
            //console.log($scope.totalPay + " " + $scope.shouldPay);
        }

        if ($scope.totalPay < $scope.shouldPay) {
            //$scope.IsShowPaymentMethodPage = true;
            //$scope.OnlinePaymentJson.TotalAmount = $scope.totalPay;
            var msg = "With This Partial Payment <br/>" +
                $scope.paying +
                "Will be Cleared<br/>Are you sure you want to pay?";
            bootbox.confirm(msg,
                function (yes) {
                    if (yes) {
                        $scope.OnlinePaymentJson.TotalAmount = $scope.totalPay;
                        if ($scope.OnlinePaymentJson.TotalAmount < 10) {
                            alertError("You cannot Pay less than " + 10 + " Taka.");
                            return;
                        } else {
                            $scope.$apply(function () {
                                $scope.OnlinePaymentJson.OnlinePaymentDetailListJson = $scope.transactionDetails;
                                $scope.IsShowPaymentMethodPage = true;
                            });
                        }
                    }
                });
        } else {
            $scope.OnlinePaymentJson.TotalAmount = $scope.totalPay;
            if ($scope.OnlinePaymentJson.TotalAmount < 10) {
                alertError("You cannot Pay less than " + 10 + " Taka.");
                return;
            } else {
                $scope.IsShowPaymentMethodPage = true;
                return;
            }
        }
    }

    $scope.calculateTotalPayable = function () {
        $scope.OnlinePaymentJson.TotalAmount = 0;
        angular.forEach($scope.OnlinePaymentJson.OnlinePaymentDetailListJson, function (value, key) {
            $scope.OnlinePaymentJson.TotalAmount +=  Number(value.Amount || 0);
        });
        $scope.totalPay = $scope.OnlinePaymentJson.TotalAmount;
    }

    $scope.selectPayable = function (amount) {
        //$scope.IsPayAbleSelected = true;
        if ($scope.IsPayAbleSelected) {
            $scope.CurrentStepNo = 1;
        } else {
            $scope.CurrentStepNo = 0;
        }

        $scope.OnlinePaymentJson.OnlinePaymentDetailListJson[0].Amount = amount;
        $scope.OnlinePaymentJson.TotalAmount = amount;

    }

    $scope.Amount = 0;
    $scope.bKashPaymentModalShow = function (amount) {
        
        $scope.Amount = amount;
        $('#bKashPaymentModal').modal('show');
    };

    $scope.verificationCodeModalShow = function () {
        $('#bKashPaymentModal').modal('hide');
        $('#verificationCodeModal').modal('show');
    };

    $scope.pinModalShow = function () {
        $('#verificationCodeModal').modal('hide');
        $('#pinModal').modal('show');
    };

    $scope.paymentSuccess = function () {
        $('#pinModal').modal('hide');
        alertSuccess("Payment Successful.");

    };

    //==============Start Bkash Code==================

    $scope.paySlip = {
        "ApplicantId": 30,
        "UnitId": 0,
        "FeeAmount": 2.0,
        "TransactionFee": 0.0,
        "OrderId": 0,
        "IsPaid": false,
        "GatewayId": 1,
        "InitiateTime": null,
        "ResponseTime": "0001-01-01T00:00:00",
        "TransStatusEnumId": 0,
        "ResponseMessage": null,
        "ResponseCode": null,
        "Currency": null,
        "Intent": null,
        "Adm_Applicant": null,
        "Adm_AppliedUnit": [],
        "Adm_PaymentGateway": null,
        "Adm_Unit": null,
        "TransStatus": 0,
        "IsSelected": false,
        "PaymentId": 0,
        "PaymentTypeEnumId": 1,
        "QuesPaperLangEnumId": 1
    }

    $scope.isBusy = false;

    $scope.payViaBkash = function () {
        if (!$scope.isBusy) {
            $scope.OnlinePaymentJson.PaymentMethodEnumId = 1;
            $scope.calculateTotalPayable();
            if ($scope.OnlinePaymentJson.TotalAmount < 10) {
                alertError("You cannot Pay less than " + 10 + " Taka.");
                return;
            }
            $scope.bKashInit();
            $("#bKash_button").trigger("click");
        }
    }

    $scope.payViaNagad = function () {
        $('#gatewayModal').modal('hide');
        $scope.OnlinePaymentJson.TotalAmount = 0;
        angular.forEach($scope.OnlinePaymentJson.OnlinePaymentDetailListJson, function (value, key) {
            $scope.OnlinePaymentJson.TotalAmount += Number(value.Amount || 0);
        });
        $scope.OnlinePaymentJson.PaymentMethodEnumId = 2;
        $http.post($scope.savePaymentUrl + "/", $scope.OnlinePaymentJson)
            .success(function (result, status) {
                console.log(result);
                if (result.HasError) {
                    alertError(result.Errors);
                    return;
                } else {
                    log(result.Data);
                    window.location = result.Data.callBackUrl;
                }
            }).error(function (result, status) {
                alert("Error Occured See Console");
                console.log(result);
            });
    }

    $scope.payOnline = function (fees, unit) {
        $scope.fees = fees;
        $scope.unit = unit;
        $('#gatewayModal').modal('show');
    }

    $scope.bKashInit = function () {
        log("Call bKashInit");
        bKash.init({
            paymentMode: 'checkout',
            paymentRequest: { amount: $scope.OnlinePaymentJson.TotalAmount, invoice: '' },
            createRequest: function (request) {
                if (!$scope.isBusy) {
                    $scope.isBusy = true;
                    $http.post($scope.savePaymentUrl + "/", $scope.OnlinePaymentJson)
                        .success(function (result, status) {
                            //console.log(result);
                            if (result.HasError) {
                                $scope.HasError = true;
                                $scope.ErrorMsg = result.ErrorHtml;
                                alertError(result.Errors);
                                bKash.create().onError();
                                $scope.isBusy = false;
                                return;
                            } else {
                                $scope.OnlinePaymentJson.UniqueOrderId = result.Data.merchantInvoiceNumber;
                                bKash.create().onSuccess(result.Data);
                                $scope.isBusy = false;
                                console.log(result.Data);
                            }
                        }).error(function (result, status) {
                            alert("Error Occured See Console");
                            bKash.create().onError();
                            $scope.isBusy = false;
                            console.log(result);
                        });
                }
            },
            executeRequestOnAuthorization: function () {
                $http.post($scope.confirmPaymentUrl + "/", $scope.OnlinePaymentJson)
                    .success(function (result, status) {
                        if (result.HasError) {
                        $scope.HasError = true;
                        $scope.ErrorMsg = result.ErrorHtml;
                        alertError(result.ErrorHtml);
                        console.log(result.Errors);
                        bKash.execute().onError();
                        $scope.isBusy = false;
                        return;
                    }
                    //console.log("Success Log:\n " + JSON.stringify(result));
                    //alertSuccess("Payment of tk." + $scope.OnlinePaymentJson.TotalAmount + "Was successful");
                    $scope.isBusy = false;
                    setTimeout(function () {
                        window.location = $scope.confirmationUrl+"/?id=" + result.Data;
                        },
                        1000);
                    $scope.isBusy = false;
                }).error(function (result, status) {
                    alertError("Something went wrong. See console.");
                    console.log(result);
                    $scope.isBusy = false;
                    bKash.execute().onError();
                });
            },
            onClose: function () {
                $scope.isBusy = false;
                $http.post($scope.cancelPaymentUrl + "/", $scope.OnlinePaymentJson)
                    .success(function (result, status) {
                        alertError("You have Canceled the Payment!!!");
                        $scope.isBusy = false;
                    }).error(function (result, status) {
                        alertError("You have Canceled the Payment!!!");
                        $scope.isBusy = false;
                    });
            }
        });
    }

    //==============End Bkash Code==================

    $scope.cancel3rdStep = function() {
        $scope.IsShowPaymentMethodPage = false;
        $scope.OnlinePaymentJson.OnlinePaymentDetailListJson = $scope.SelectedPaymentList;
    }

    //======Custom Variables goes hare=====
    $scope.Init = function (
        getOnlinePaymentDataExtraUrl
        , getMonthlyBillUrl
        , savePaymentUrl
        , confirmPaymentUrl
        , minimumOnlinePayableAmount
        , confirmationUrl
        , cancelPaymentUrl
    ) {
        $scope.getOnlinePaymentDataExtraUrl = getOnlinePaymentDataExtraUrl;
        $scope.getMonthlyBillUrl = getMonthlyBillUrl;
        $scope.savePaymentUrl = savePaymentUrl;
        $scope.confirmPaymentUrl = confirmPaymentUrl;
        $scope.minimumOnlinePayableAmount = minimumOnlinePayableAmount;
        $scope.confirmationUrl = confirmationUrl;
        $scope.cancelPaymentUrl = cancelPaymentUrl;
        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 



});



