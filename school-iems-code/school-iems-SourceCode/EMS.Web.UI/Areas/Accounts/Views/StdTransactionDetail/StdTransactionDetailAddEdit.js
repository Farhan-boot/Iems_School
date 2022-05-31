
//File: Acnt_StdTransactionDetail Anjular  Controller
emsApp.controller('StdTransactionDetailAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.StdTransactionDetail = [];
    $scope.StdTransactionDetailId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (StdTransactionDetailId)
    {
       if(StdTransactionDetailId!=null)
        $scope.StdTransactionDetailId=StdTransactionDetailId;
       
       $scope.loadStdTransactionDetailDataExtra($scope.StdTransactionDetailId);
       $scope.getStdTransactionDetailById($scope.StdTransactionDetailId);
    };
   $scope.getNewStdTransactionDetail= function()
    {
    	  $scope.getStdTransactionDetailById(0);
    };
   $scope.getStdTransactionDetailById= function(StdTransactionDetailId)
    {
        if(StdTransactionDetailId!=null && StdTransactionDetailId!=='')
        $scope.StdTransactionDetailId=StdTransactionDetailId;
        else return;
        
        $http.get($scope.getStdTransactionDetailByIdUrl, {
            params: { "id": $scope.StdTransactionDetailId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.StdTransactionDetail = result.Data;
                updateUrlQuery('id' , $scope.StdTransactionDetail.Id);
                $scope.onAfterGetStdTransactionDetailById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Std Transaction Detail! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Std Transaction Detail! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadStdTransactionDetailDataExtra= function(StdTransactionDetailId)
    {
        if(StdTransactionDetailId!=null)
            $scope.StdTransactionDetailId=StdTransactionDetailId;
            
            $http.get($scope.getStdTransactionDetailDataExtraUrl, {
                params: { "id": $scope.StdTransactionDetailId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.ParticularTypeEnumList!=null)
                         $scope.ParticularTypeEnumList = result.DataExtra.ParticularTypeEnumList;
                        $scope.onAfterLoadStdTransactionDetailDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Std Transaction Detail! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Std Transaction Detail! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveStdTransactionDetail= function(){
    	if(!$scope.validateStdTransactionDetail())
        {
          return;
        }
        $http.post($scope.saveStdTransactionDetailUrl + "/", $scope.StdTransactionDetail).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.StdTransactionDetail = result.Data;
                    updateUrlQuery('id', $scope.StdTransactionDetail.Id);
                   }
                   alertSuccess("Successfully saved Std Transaction Detail data!");
                   $scope.onAfterSaveStdTransactionDetail(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Std Transaction Detail! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Std Transaction Detail! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteStdTransactionDetailById= function(){
    	
    };
   $scope.validateStdTransactionDetail= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (StdTransactionDetailId,getStdTransactionDetailByIdUrl,getStdTransactionDetailDataExtraUrl, saveStdTransactionDetailUrl,deleteStdTransactionDetailByIdUrl) {
        $scope.StdTransactionDetailId = StdTransactionDetailId;
        $scope.getStdTransactionDetailByIdUrl = getStdTransactionDetailByIdUrl;
        $scope.saveStdTransactionDetailUrl = saveStdTransactionDetailUrl;
        $scope.getStdTransactionDetailDataExtraUrl = getStdTransactionDetailDataExtraUrl;
        $scope.deleteStdTransactionDetailByIdUrl = deleteStdTransactionDetailByIdUrl;

        $scope.loadPage(StdTransactionDetailId);
    };
    
  $scope.onAfterSaveStdTransactionDetail= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetStdTransactionDetailById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadStdTransactionDetailDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
         if(result.DataExtra.SemesterList!=null)
         $scope.SemesterList =  result.DataExtra.SemesterList; 
         if(result.DataExtra.StdTransactionList!=null)
         $scope.StdTransactionList =  result.DataExtra.StdTransactionList; 
         if(result.DataExtra.StudentList!=null)
         $scope.StudentList =  result.DataExtra.StudentList; 
  /*
  //Child Table Bindings, need to fix     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

