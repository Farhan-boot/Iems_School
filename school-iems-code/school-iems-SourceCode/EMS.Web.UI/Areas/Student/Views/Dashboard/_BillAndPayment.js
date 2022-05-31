
//File:BillAndPaymen Anjular  Controller
emsApp.controller('BillAndPaymentCtrl', function ($scope, $http, $filter) {
    $scope.Math = window.Math;
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.IsShowPaymentMethodPage = false;
    $scope.IsPayAbleSelected = false;
    $scope.HasViewPermission = false;
    $scope.OnlinePaymentJson = [];
    $scope.CurrentStepNo = 0;
    $scope.IsOtherPaymentSelectAble = true;

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

                
                $scope.getSemesterBill();

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

    $scope.getSemesterBill = function () {
        //log("Call Semester Bill");
        $http.get($scope.getSemesterBillUrl, {
            params: {
                "semesterId": $scope.SelectedSemesterId
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.SemesterBill = result.Data;
                //log($scope.SemesterBill);

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

        $scope.getSemesterBill();
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
        $scope.CurrentStepNo = $scope.CurrentStepNo + 1;
        $scope.IsOtherPaymentSelectAble = false;
        $scope.OnlinePaymentJson.TotalAmount = Math.ceil($scope.OnlinePaymentJson.TotalAmount);
    }

    $scope.previousStep = function () {
        $scope.CurrentStepNo = $scope.CurrentStepNo - 1;
        $scope.IsOtherPaymentSelectAble = true;
    }
    //======Custom property and Functions Start=======================================================  

    $scope.addNewVoucherDtl = function() {
        alert("hi");
    }

    $scope.IsOnlinePaymentShowAble = function () {

        if (isNaN($scope.OnlinePaymentJson.TotalAmount)) {
            alertError("Invalid Payable Amount!");
            return;
        }
        if ($scope.OnlinePaymentJson.TotalAmount < 10) {
            alertError("You cannot Pay less than " + 10 + " Taka.");
            return;
        } else {
            $scope.IsShowPaymentMethodPage = true;
        }
    }
    $scope.calculateTotalPayable = function () {
        angular.forEach($scope.OnlinePaymentJson.OnlinePaymentDetailListJson, function (value, key) {
            //value.Amount = value.Amount.toString().replaceAll(".-", "");;
            $scope.OnlinePaymentJson.TotalAmount = Number($scope.feeAmount || 0) + Number(value.Amount || 0);
        });
        $scope.OnlinePaymentJson.TotalAmount = Math.ceil($scope.OnlinePaymentJson.TotalAmount);
    }

    $scope.payingOnlyOtherPayment = function() {
        $scope.IsOtherPaymentSelectAble = true;
        $scope.IsPayAbleSelected = true;
        //$scope.CurrentStepNo = 1;
    }

    $scope.selectPayable = function (amount) {
        //$scope.IsPayAbleSelected = true;
        if ($scope.IsPayAbleSelected && amount>9) {
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

        if ($scope.IsScriptLoaded) {

        }

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
        angular.forEach($scope.OnlinePaymentJson.OnlinePaymentDetailListJson, function (value, key) {
            $scope.OnlinePaymentJson.TotalAmount = Number($scope.feeAmount || 0) + Number(value.Amount || 0);
        });
        $scope.OnlinePaymentJson.PaymentMethodEnumId = 2;
        $http.post($scope.savePaymentUrl + "/", $scope.OnlinePaymentJson)
            .success(function (result, status) {
                //console.log(result);
                if (result.HasError) {
                    alertError(result.Errors);
                    return;
                } else {
                    //log(result.Data);
                    window.location = result.Data.callBackUrl;
                }
            }).error(function (result, status) {
                alert("Error Occured See Console");
                //console.log(result);
            });
    }

    $scope.payOnline = function (fees, unit) {
        $scope.fees = fees;
        $scope.unit = unit;
        $('#gatewayModal').modal('show');
    }

    $scope.bKashInit = function () {
        //log("Call bKashInit");
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
                                //console.log(result.Data);
                            }
                        }).error(function (result, status) {
                            alert("Error Occured See Console");
                            bKash.create().onError();
                            $scope.isBusy = false;
                            //console.log(result);
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
                        //console.log(result.Errors);
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
                    //console.log(result);
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

    //======Custom Variables goes hare=====
    $scope.Init = function (
        getOnlinePaymentDataExtraUrl
        , getSemesterBillUrl
        , savePaymentUrl
        , confirmPaymentUrl
        , minimumOnlinePayableAmount
        , confirmationUrl
        , cancelPaymentUrl
    ) {
        $scope.getOnlinePaymentDataExtraUrl = getOnlinePaymentDataExtraUrl;
        $scope.getSemesterBillUrl = getSemesterBillUrl;
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



