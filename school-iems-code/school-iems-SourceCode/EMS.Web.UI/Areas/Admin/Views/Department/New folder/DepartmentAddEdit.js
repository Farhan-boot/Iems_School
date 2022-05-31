
//File: HR_Department Anjular  Controller
emsApp.controller('DepartmentAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.Department = [];
    $scope.DepartmentId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;

   $scope.loadPage= function (DepartmentId)
    {
       if(DepartmentId!=null)
        $scope.DepartmentId=DepartmentId;
       
       $scope.loadDepartmentExtraData($scope.DepartmentId);
       $scope.getDepartmentById($scope.DepartmentId);
    };
   $scope.getNewDepartment= function()
    {
    	  $scope.getDepartmentById(0);
    };
   $scope.getDepartmentById= function(DepartmentId)
    {
        if(DepartmentId!=null && DepartmentId!=='')
        $scope.DepartmentId=DepartmentId;
        else return;
        
        $http.get($scope.getDepartmentByIdUrl, {
            params: { "id": $scope.DepartmentId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.Department = result.Data;
                updateUrlQuery('id' , $scope.Department.Id);
                $scope.onAfterGetDepartmentById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Department! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Department! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadDepartmentExtraData= function(DepartmentId)
    {
        if(DepartmentId!=null)
            $scope.DepartmentId=DepartmentId;
            
            $http.get($scope.getDepartmentExtraDataUrl, {
                params: { "id": $scope.DepartmentId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.TypeEnumList!=null)
                         $scope.TypeEnumList = result.DataExtra.TypeEnumList;
                      if(result.DataExtra.StatusEnumList!=null)
                         $scope.StatusEnumList = result.DataExtra.StatusEnumList;
                        $scope.onAfterLoadDepartmentExtraData(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Department! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Department! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveDepartment= function(){
    	if(!$scope.validateDepartment())
        {
          return;
        }
        $http.post($scope.saveDepartmentUrl + "/", $scope.Department).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.Department = result.Data;
                    updateUrlQuery('id', $scope.Department.Id);
                   }
                   alertSuccess("Successfully saved Department data!");
                   $scope.onAfterSaveDepartment(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Department! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Department! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteDepartmentById= function(){
    	
    };
   $scope.validateDepartment= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (DepartmentId,getDepartmentByIdUrl,getDepartmentExtraDataUrl, saveDepartmentUrl,deleteDepartmentByIdUrl) {
        $scope.DepartmentId = DepartmentId;
        $scope.getDepartmentByIdUrl = getDepartmentByIdUrl;
        $scope.saveDepartmentUrl = saveDepartmentUrl;
        $scope.getDepartmentExtraDataUrl = getDepartmentExtraDataUrl;
        $scope.deleteDepartmentByIdUrl = deleteDepartmentByIdUrl;

        $scope.loadPage(DepartmentId);
    };
    
  $scope.onAfterSaveDepartment= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetDepartmentById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadDepartmentExtraData= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
         if(result.DataExtra.OrganizationList!=null)
         $scope.OrganizationList =  result.DataExtra.OrganizationList; 
  /*
  //Child Table Bindings, need to fix
             $scope.DepartmentIdList =  result.DataExtra.DepartmentIdList; 

             $scope.OfferedByDepartmentIdList =  result.DataExtra.OfferedByDepartmentIdList; 

             $scope.DepartmentIdList =  result.DataExtra.DepartmentIdList; 

             $scope.DepartmentIdList =  result.DataExtra.DepartmentIdList; 

             $scope.DepartmentIdList =  result.DataExtra.DepartmentIdList; 

             $scope.DepartmentIdList =  result.DataExtra.DepartmentIdList; 

             $scope.DepartmentIdList =  result.DataExtra.DepartmentIdList; 

             $scope.DepartmentIdList =  result.DataExtra.DepartmentIdList; 

             $scope.JoiningDepartmentIdList =  result.DataExtra.JoiningDepartmentIdList; 

             $scope.DepartmentIdList =  result.DataExtra.DepartmentIdList; 
     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

