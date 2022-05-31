
//File:Accounts_StdOnlinePaySlip List Anjular  Controller
emsApp.controller('StdOnlinePaySlipListCtrl', function ($scope, $http, $filter, cfpLoadingBar, $sce) {
    $scope.StdOnlinePaySlipList = [];
    $scope.SelectedStdOnlinePaySlip = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = true;
     $scope.SelectedSemesterId=0;      
     $scope.SelectedBankId=0;      
     $scope.SelectedStdTransectionId=0;      
     $scope.StudentId = "";
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 20;
    $scope.PageNo = 1;
    $scope.toggleAdvSearch = false;

   
    $scope.loadPage = function () {
        $scope.getPagedStdOnlinePaySlipList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedStdOnlinePaySlipList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedStdOnlinePaySlipList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedStdOnlinePaySlipList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedStdOnlinePaySlipList();
    };
    $scope.searchStdOnlinePaySlipList = function () {
        $scope.getPagedStdOnlinePaySlipList();
    };
    $scope.getPagedStdOnlinePaySlipList = function () {
        $scope.getStdOnlinePaySlipList();
    };
    /*For Search on data end*/
    $scope.getStdOnlinePaySlipList = function () {
        $http.get($scope.getPagedStdOnlinePaySlipUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
           ,"semesterId": $scope.SelectedSemesterId      
           ,"bankId": $scope.SelectedBankId      
           ,"stdTransectionId": $scope.SelectedStdTransectionId      
           , "studentId": $scope.StudentId
            ,"gatewayId": $scope.SelectedGatewayId
            , "isPaid": $scope.IsPaid      
            , "gatewayTransId": $scope.GatewayTransId
            , "transStatusEnumId": $scope.TransStatusEnumId
            ,"payslipGenerateDateFrom": $scope.PayslipGenerateDateFrom      
            ,"payslipGenerateDateTo": $scope.PayslipGenerateDateTo      
            , "payslipNo": $scope.PayslipNo
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.StdOnlinePaySlipList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Std Online Pay Slip list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Std Online Pay Slip list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getStdOnlinePaySlipListExtraData= function()
    {
            $http.get($scope.getStdOnlinePaySlipListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.TransStatusEnumList!=null)
                         $scope.TransStatusEnumList = result.DataExtra.TransStatusEnumList;
                  //DropDown Option Tables
                     if(result.DataExtra.SemesterList!=null)
                       $scope.SemesterList =  result.DataExtra.SemesterList; 

                     if(result.DataExtra.OfficialBankList!=null)
                       $scope.OfficialBankList =  result.DataExtra.OfficialBankList; 

                     if (result.DataExtra.PaymentGatewayList != null)
                         $scope.PaymentGatewayList = result.DataExtra.PaymentGatewayList;

                     if(result.DataExtra.StdTransactionList!=null)
                       $scope.StdTransactionList =  result.DataExtra.StdTransactionList; 

                     if(result.DataExtra.StudentList!=null)
                       $scope.StudentList =  result.DataExtra.StudentList; 

                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Std Online Pay Slip! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Std Online Pay Slip! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllStdOnlinePaySlipList = function () {
        $scope.IsLoading++;
        $http.get($scope.getStdOnlinePaySlipListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.StdOnlinePaySlipList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Std Online Pay Slip list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Std Online Pay Slip list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteStdOnlinePaySlipById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteStdOnlinePaySlipByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.StdOnlinePaySlipList.indexOf(obj);
                        $scope.StdOnlinePaySlipList.splice(i, 1);
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
    /*$scope.deleteStdOnlinePaySlipByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteStdOnlinePaySlipByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.StdOnlinePaySlipList.indexOf(obj);
                        $scope.StdOnlinePaySlipList.splice(i, 1);
                       alertSuccess("Data successfully deleted!");
                    } else {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Sorry unable to delete! " + result.Errors.toString();
                        alertError($scope.ErrorMsg);
                    }
                }).error(function (result, status) {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Sorry unable to delete! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                    alertError($scope.ErrorMsg);
                });
            }
        });
    };*/

    $scope.selectAll = function ($event) {
    var checkbox = $event.target;
    for (var i = 0; i < $scope.StdOnlinePaySlipList.length; i++) {
        var entity = $scope.StdOnlinePaySlipList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    $scope.toggleAdvancedSearch = function () {
        $scope.toggleAdvSearch = !$scope.toggleAdvSearch;
    }
    $scope.recheckPaymentStatus = function (payslip) {
        $scope.selectedPayslipId = payslip.Id;
        $http.get($scope.getRecheckPaymentStatusByIdUrl, {
            params: {
                "payslipId": $scope.selectedPayslipId,
                "gateway": null
            }
        }).success(function (result, status) {
            $scope.trnxId = "";
            //$("#PaymentRecheckModal").modal('hide');
            if (!result.HasError) {
                $scope.GatewayResponse = result.Data;
                alertSuccess("Payment Status Updated Successfully.");
                //$("#PaymentConfirmationModal").modal('show');
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
            if (result.DataExtra.Payslip != null) {
                var payment = $scope.StdOnlinePaySlipList.filter(x => x.Id == result.DataExtra.Payslip.Id);
                if (payment != null) {
                    payment[0].TransStatus = result.DataExtra.Payslip.TransStatus;
                    payment[0].TransStatusEnumId = result.DataExtra.Payslip.TransStatusEnumId;
                    payment[0].IsPaid = result.DataExtra.Payslip.IsPaid;
                    payment[0].GatewayResponseTime = result.DataExtra.Payslip.GatewayResponseTime;
                }
            }
        }).error(function (result, status) {
            $("#PaymentRecheckModal").modal('hide');
            $scope.trnxId = "";
            $scope.HasError = true;
            $scope.ErrorMsg = JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    }

    //$scope.confirmPayment = function () {
    //    $http.post($scope.postConfirmPaymentByIdUrl + "/", $scope.GatewayResponse).
    //        success(function (result, status) {
    //            $("#PaymentConfirmationModal").modal('hide');
    //            if (!result.HasError) {
    //                $scope.HasError = false;
    //                if (result.Data != null) {
    //                    //$scope.Applicant = result.Data;
    //                }
    //                alertSuccess("Successfully Payment Status Updated!");
    //            } else {
    //                $scope.HasError = true;
    //                $scope.ErrorMsg = "Unable to confirm Payment! " + result.Errors.toString();
    //                alertError($scope.ErrorMsg);
    //            }
               
    //            //$scope.loadPage();
    //        }).
    //        error(function (result, status) {
    //            $("#PaymentConfirmationModal").modal('hide');
    //            $scope.HasError = true;
    //            $scope.ErrorMsg = "Unable to save Applicant! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
    //            alertError($scope.ErrorMsg);
    //        });
    //}
    $scope.renderHtml = function (html_code) {
        return $sce.trustAsHtml(html_code);
    };
    $scope.updateBulkPayment = function () {
        $http.get($scope.getUpdateBulkPaymentUrl, {
        }).success(function (result, status) {
            $scope.isShowBulkMessage = true;
            $scope.BulkResultMessage = result;
            $scope.getStdOnlinePaySlipListExtraData();
            $scope.getPagedStdOnlinePaySlipList();
        }).error(function (result, status) {
            $("#PaymentRecheckModal").modal('hide');
            $scope.trnxId = "";
            $scope.HasError = true;
            $scope.ErrorMsg = JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    }


    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedStdOnlinePaySlipUrl
        ,deleteStdOnlinePaySlipByIdUrl
        ,getStdOnlinePaySlipListDataExtraUrl
        ,saveStdOnlinePaySlipListUrl
        ,getStdOnlinePaySlipByIdUrl
        ,getStdOnlinePaySlipDataExtraUrl
        , saveStdOnlinePaySlipUrl
         , getRecheckPaymentStatusByIdUrl
         , getUpdateBulkPaymentUrl
        ) {
        $scope.getPagedStdOnlinePaySlipUrl = getPagedStdOnlinePaySlipUrl;
        $scope.deleteStdOnlinePaySlipByIdUrl = deleteStdOnlinePaySlipByIdUrl;
        /*bind extra url if need*/
        $scope.getStdOnlinePaySlipListDataExtraUrl = getStdOnlinePaySlipListDataExtraUrl;
        $scope.saveStdOnlinePaySlipListUrl = saveStdOnlinePaySlipListUrl;
        $scope.getStdOnlinePaySlipByIdUrl = getStdOnlinePaySlipByIdUrl;
        $scope.getStdOnlinePaySlipDataExtraUrl = getStdOnlinePaySlipDataExtraUrl;
        $scope.saveStdOnlinePaySlipUrl = saveStdOnlinePaySlipUrl;
        $scope.getRecheckPaymentStatusByIdUrl = getRecheckPaymentStatusByIdUrl;
        $scope.getUpdateBulkPaymentUrl = getUpdateBulkPaymentUrl;
        $scope.getStdOnlinePaySlipListExtraData();
        //$scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



