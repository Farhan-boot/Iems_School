
//File: Inv_Supplier Anjular  Controller
emsApp.controller('SupplierAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.Supplier = [];
    $scope.SupplierId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (SupplierId)
    {
       if(SupplierId!=null)
        $scope.SupplierId=SupplierId;
       
       $scope.loadSupplierDataExtra($scope.SupplierId);
       $scope.getSupplierById($scope.SupplierId);
    };
   $scope.getNewSupplier= function()
    {
    	  $scope.getSupplierById(0);
    };
   $scope.getSupplierById= function(SupplierId)
    {
        if(SupplierId!=null && SupplierId!=='')
        $scope.SupplierId=SupplierId;
        else return;
        
        $http.get($scope.getSupplierByIdUrl, {
            params: { "id": $scope.SupplierId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.Supplier = result.Data;
                updateUrlQuery('id' , $scope.Supplier.Id);
                $scope.onAfterGetSupplierById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Supplier! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Supplier! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadSupplierDataExtra= function(SupplierId)
    {
        if(SupplierId!=null)
            $scope.SupplierId=SupplierId;
            
            $http.get($scope.getSupplierDataExtraUrl, {
                params: { "id": $scope.SupplierId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                        $scope.onAfterLoadSupplierDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Supplier! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Supplier! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveSupplier= function(){
    	if(!$scope.validateSupplier())
        {
          return;
        }
        $http.post($scope.saveSupplierUrl + "/", $scope.Supplier).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.Supplier = result.Data;
                    updateUrlQuery('id', $scope.Supplier.Id);
                   }
                   alertSuccess("Successfully saved Supplier data!");
                   $scope.onAfterSaveSupplier(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Supplier! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Supplier! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteSupplierById= function(){
    	
    };
   $scope.validateSupplier= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (SupplierId,getSupplierByIdUrl,getSupplierDataExtraUrl, saveSupplierUrl,deleteSupplierByIdUrl) {
        $scope.SupplierId = SupplierId;
        $scope.getSupplierByIdUrl = getSupplierByIdUrl;
        $scope.saveSupplierUrl = saveSupplierUrl;
        $scope.getSupplierDataExtraUrl = getSupplierDataExtraUrl;
        $scope.deleteSupplierByIdUrl = deleteSupplierByIdUrl;

        $scope.loadPage(SupplierId);
    };
    
  $scope.onAfterSaveSupplier= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetSupplierById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadSupplierDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
  /*
  //Child Table Bindings, need to fix
             $scope.SupplierIdList =  result.DataExtra.SupplierIdList; 
     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

