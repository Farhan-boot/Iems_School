
//File: General_OnlineClass Anjular  Controller
emsApp.controller('OnlineClassAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.OnlineClass = [];
    $scope.OnlineClassId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (OnlineClassId)
    {
       if(OnlineClassId!=null)
        $scope.OnlineClassId=OnlineClassId;
       
       $scope.loadOnlineClassDataExtra($scope.OnlineClassId);
       $scope.getOnlineClassById($scope.OnlineClassId);
    };
   $scope.getNewOnlineClass= function()
    {
    	  $scope.getOnlineClassById(0);
    };
   $scope.getOnlineClassById= function(OnlineClassId)
    {
        if(OnlineClassId!=null && OnlineClassId!=='')
        $scope.OnlineClassId=OnlineClassId;
        else return;
        
        $http.get($scope.getOnlineClassByIdUrl, {
            params: { "id": $scope.OnlineClassId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.OnlineClass = result.Data;
                updateUrlQuery('id' , $scope.OnlineClass.Id);
                $scope.onAfterGetOnlineClassById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Online Class! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Online Class! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadOnlineClassDataExtra= function(OnlineClassId)
    {
        if(OnlineClassId!=null)
            $scope.OnlineClassId=OnlineClassId;
            
            $http.get($scope.getOnlineClassDataExtraUrl, {
                params: { "id": $scope.OnlineClassId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.LiveClassMediumEnumList!=null)
                         $scope.LiveClassMediumEnumList = result.DataExtra.LiveClassMediumEnumList;
                        $scope.onAfterLoadOnlineClassDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Online Class! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Online Class! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveOnlineClass= function(){
    	if(!$scope.validateOnlineClass())
        {
          return;
        }
        $http.post($scope.saveOnlineClassUrl + "/", $scope.OnlineClass).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.OnlineClass = result.Data;
                    updateUrlQuery('id', $scope.OnlineClass.Id);
                   }
                   alertSuccess("Successfully saved Online Class data!");
                   $scope.onAfterSaveOnlineClass(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Online Class! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Online Class! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteOnlineClassById= function(){
    	
    };
   $scope.validateOnlineClass= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (OnlineClassId,getOnlineClassByIdUrl,getOnlineClassDataExtraUrl, saveOnlineClassUrl,deleteOnlineClassByIdUrl) {
        $scope.OnlineClassId = OnlineClassId;
        $scope.getOnlineClassByIdUrl = getOnlineClassByIdUrl;
        $scope.saveOnlineClassUrl = saveOnlineClassUrl;
        $scope.getOnlineClassDataExtraUrl = getOnlineClassDataExtraUrl;
        $scope.deleteOnlineClassByIdUrl = deleteOnlineClassByIdUrl;

        $scope.loadPage(OnlineClassId);
    };
    
  $scope.onAfterSaveOnlineClass= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetOnlineClassById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadOnlineClassDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
         if(result.DataExtra.ClassShiftSectionMapList!=null)
         $scope.ClassShiftSectionMapList =  result.DataExtra.ClassShiftSectionMapList; 
  /*
  //Child Table Bindings, need to fix     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

