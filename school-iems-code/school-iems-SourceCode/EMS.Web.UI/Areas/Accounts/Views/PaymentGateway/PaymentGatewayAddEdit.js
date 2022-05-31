
//File: Acnt_PaymentGateway Anjular  Controller
emsApp.controller('PaymentGatewayAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.PaymentGateway = [];
    $scope.PaymentGatewayId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;
   $scope.loadPage= function (PaymentGatewayId)
    {
       if(PaymentGatewayId!=null)
        $scope.PaymentGatewayId=PaymentGatewayId;
       
       $scope.loadPaymentGatewayDataExtra($scope.PaymentGatewayId);
       $scope.getPaymentGatewayById($scope.PaymentGatewayId);

       if ($scope.PaymentGatewayId != 0) {
           $scope.getPaymentGatewayProgramMapListExtraData();
           $scope.getPagedPaymentGatewayProgramMapList();
       }
    };
   $scope.getNewPaymentGateway= function()
    {
    	  $scope.getPaymentGatewayById(0);
    };
   $scope.getPaymentGatewayById= function(PaymentGatewayId)
    {
        if(PaymentGatewayId!=null && PaymentGatewayId!=='')
        $scope.PaymentGatewayId=PaymentGatewayId;
        else return;
        
        $http.get($scope.getPaymentGatewayByIdUrl, {
            params: { "id": $scope.PaymentGatewayId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.PaymentGateway = result.Data;
                log($scope.PaymentGateway);
                updateUrlQuery('id' , $scope.PaymentGateway.Id);
                $scope.onAfterGetPaymentGatewayById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Payment Gateway! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Payment Gateway! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadPaymentGatewayDataExtra= function(PaymentGatewayId)
    {
        if(PaymentGatewayId!=null)
            $scope.PaymentGatewayId=PaymentGatewayId;
            
            $http.get($scope.getPaymentGatewayDataExtraUrl, {
                params: { "id": $scope.PaymentGatewayId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.ActiveStatusEnumList!=null)
                         $scope.ActiveStatusEnumList = result.DataExtra.ActiveStatusEnumList;
                      if(result.DataExtra.TypeEnumList!=null)
                         $scope.TypeEnumList = result.DataExtra.TypeEnumList;
                        $scope.onAfterLoadPaymentGatewayDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Payment Gateway! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Payment Gateway! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.savePaymentGateway= function(){
    	if(!$scope.validatePaymentGateway())
        {
          return;
        }
        $http.post($scope.savePaymentGatewayUrl + "/", $scope.PaymentGateway).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.PaymentGateway = result.Data;
                    updateUrlQuery('id', $scope.PaymentGateway.Id);
                   }
                   alertSuccess("Successfully saved Payment Gateway data!");
                   $scope.onAfterSavePaymentGateway(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Payment Gateway! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Payment Gateway! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deletePaymentGatewayById= function(){
    	
    };
   $scope.validatePaymentGateway= function(){
        return true;
    }; 
    //======Custom property and Functions Start=======================================================  
   //$scope.getPaymentGatewaySettings = function () {
   //    if ($scope.PaymentGateway.TypeEnumId == undefined || $scope.PaymentGateway.TypeEnumId == null) {
   //        return;
   //    }

   //    $http.get($scope.getPaymentGatewaySettingsUrl, {
   //         params: { "gatewayTypeEnumId": $scope.PaymentGateway.TypeEnumId }
   //     }).success(function (result, status) {
   //         if (!result.HasError) {
   //             $scope.PaymentGateway.SettingJson = result.Data;
   //         } else {
   //             $scope.HasError = true;
   //             $scope.ErrorMsg = "Unable to get Gateway Settings! " + result.Errors.toString();
   //             alertError($scope.ErrorMsg);
   //         }
   //     }).error(function (result, status) {
   //         $scope.HasError = true;
   //         $scope.ErrorMsg = "Unable to get Gateway Settings! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
   //         alertError($scope.ErrorMsg);
   //     });
   // };
   $scope.getPagedPaymentGatewayProgramMapList = function () {
       $scope.getPaymentGatewayProgramMapList();
   };
    //List 
   $scope.getPaymentGatewayProgramMapList = function () {
       $http.get($scope.getPagedPaymentGatewayProgramMapUrl, {
           params: {
               "textkey": $scope.SearchText
           , "pageSize": $scope.PageSize
           , "pageNo": $scope.PageNo
          , "gatewayId": $scope.PaymentGatewayId
           }
       }).success(function (result) {
           if (!result.HasError) {
               $scope.HasError = false;
               $scope.HasViewPermission = result.HasViewPermission;
               $scope.PaymentGatewayProgramMapList = result.Data;
               $scope.totalItems = result.Count;
               $scope.totalServerItems = result.Data.length;
           } else {
               $scope.HasError = true;
               $scope.ErrorMsg = "Unable to get Payment Gateway Program Map list data! " + result.Errors.toString();
               alertError($scope.ErrorMsg);
           }
       }).error(function (result, status) {
           $scope.HasError = true;
           $scope.ErrorMsg = "Unable to get Payment Gateway Program Map list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
           alertError($scope.ErrorMsg);
       });
   };
   $scope.getPaymentGatewayProgramMapListExtraData = function () {
       $http.get($scope.getPaymentGatewayProgramMapListDataExtraUrl, {
           params: {}
       }).success(function (result, status) {
           if (!result.HasError) {
               $scope.HasError = false;
               //DropDown Option Tables
               if (result.DataExtra.PaymentGatewayList != null)
                   $scope.PaymentGatewayList = result.DataExtra.PaymentGatewayList;
               if (result.DataExtra.ProgramList != null)
                   $scope.ProgramList = result.DataExtra.ProgramList;
               if (result.DataExtra.OfficialBankList != null)
                   $scope.OfficialBankList = result.DataExtra.OfficialBankList;

           } else {
               $scope.HasError = true;
               $scope.ErrorMsg = "Unable to load option data for Payment Gateway Program Map! " + result.Errors.toString();
               alertError($scope.ErrorMsg);
           }
       }).error(function (result, status) {
           $scope.HasError = true;
           $scope.ErrorMsg = "Unable to load option data for Payment Gateway Program Map! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
           alertError($scope.ErrorMsg);
       });
   };

    $scope.deletePaymentGatewayProgramMapById = function (obj) {
       if (obj == null) {
           alertError("Please select a row to delete!");
       }
       bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
           if (yes) {
               $http.get($scope.deletePaymentGatewayProgramMapByIdUrl, {
                   params: { "id": obj.Id }
               }).success(function (result, status) {
                   if (!result.HasError) {
                       $scope.HasError = false;
                       var i = $scope.PaymentGatewayProgramMapList.indexOf(obj);
                       $scope.PaymentGatewayProgramMapList.splice(i, 1);
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

    //Single 
    $scope.getNewPaymentGatewayProgramMap = function () {
        $scope.getPaymentGatewayProgramMapById(0);
    };
    $scope.getPaymentGatewayProgramMapById = function (PaymentGatewayProgramMapId) {
        if (PaymentGatewayProgramMapId != null && PaymentGatewayProgramMapId !== '')
            $scope.PaymentGatewayProgramMapId = PaymentGatewayProgramMapId;
        else return;

        $http.get($scope.getPaymentGatewayProgramMapByIdUrl, {
            params: { "id": $scope.PaymentGatewayProgramMapId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.PaymentGatewayProgramMap = result.Data;
                $scope.PaymentGatewayProgramMap.GatewayId = Number($scope.PaymentGatewayId || 0);
                $('#ProgramMappingModal').modal('show');
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Payment Gateway Program Map! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Payment Gateway Program Map! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.savePaymentGatewayProgramMap = function () {
        
        $http.post($scope.savePaymentGatewayProgramMapUrl + "/", $scope.PaymentGatewayProgramMap).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.Data != null) {
                        $scope.PaymentGatewayProgramMap = result.Data;
                        var obj = $scope.PaymentGatewayProgramMapList.filter(
                            (detail) => detail.Id === $scope.PaymentGatewayProgramMapId)[0];
                        log(obj);
                        if (obj == null || obj == undefined) {
                            $scope.PaymentGatewayProgramMapList.push(result.Data);
                        } else {
                            var i = $scope.PaymentGatewayProgramMapList.indexOf(obj);
                            $scope.PaymentGatewayProgramMapList[i] = result.Data;
                        }
                    }
                    $('#ProgramMappingModal').modal('hide');
                    alertSuccess("Successfully saved Payment Gateway Program Map data!");
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to save Payment Gateway Program Map! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to save Payment Gateway Program Map! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };
//======Custom Variabales goes hare=====

    $scope.Init = function (
        PaymentGatewayId,
        getPaymentGatewayByIdUrl,
        getPaymentGatewayDataExtraUrl,
        savePaymentGatewayUrl,
        deletePaymentGatewayByIdUrl,
        getPaymentGatewaySettingsUrl,
        getPagedPaymentGatewayProgramMapUrl,
        getPaymentGatewayProgramMapListDataExtraUrl,
        deletePaymentGatewayProgramMapByIdUrl,
        getPaymentGatewayProgramMapByIdUrl,
        savePaymentGatewayProgramMapUrl
    ) {
        $scope.PaymentGatewayId = PaymentGatewayId;
        $scope.getPaymentGatewayByIdUrl = getPaymentGatewayByIdUrl;
        $scope.savePaymentGatewayUrl = savePaymentGatewayUrl;
        $scope.getPaymentGatewayDataExtraUrl = getPaymentGatewayDataExtraUrl;
        $scope.deletePaymentGatewayByIdUrl = deletePaymentGatewayByIdUrl;
        $scope.getPaymentGatewaySettingsUrl = getPaymentGatewaySettingsUrl;
        $scope.deletePaymentGatewayProgramMapByIdUrl = deletePaymentGatewayProgramMapByIdUrl;
        $scope.getPaymentGatewayProgramMapListDataExtraUrl = getPaymentGatewayProgramMapListDataExtraUrl;
        $scope.getPagedPaymentGatewayProgramMapUrl = getPagedPaymentGatewayProgramMapUrl;
        $scope.getPaymentGatewayProgramMapByIdUrl = getPaymentGatewayProgramMapByIdUrl;
        $scope.savePaymentGatewayProgramMapUrl = savePaymentGatewayProgramMapUrl;

        $scope.loadPage(PaymentGatewayId);
    };
    
  $scope.onAfterSavePaymentGateway= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetPaymentGatewayById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadPaymentGatewayDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
         if(result.DataExtra.OfficialBankList!=null)
         $scope.OfficialBankList =  result.DataExtra.OfficialBankList; 
  /*
  //Child Table Bindings, need to fix
             $scope.GatewayIdList =  result.DataExtra.GatewayIdList; 
     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

