
//File: User_Relationship Anjular  Controller
emsApp.controller('RelationshipAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.Relationship = [];
    $scope.RelationshipId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (RelationshipId)
    {
       if(RelationshipId!=null)
        $scope.RelationshipId=RelationshipId;
       
       $scope.loadRelationshipDataExtra($scope.RelationshipId);
       $scope.getRelationshipById($scope.RelationshipId);
    };
   $scope.getNewRelationship= function()
    {
    	  $scope.getRelationshipById(0);
    };
   $scope.getRelationshipById= function(RelationshipId)
    {
        if(RelationshipId!=null && RelationshipId!=='')
        $scope.RelationshipId=RelationshipId;
        else return;
        
        $http.get($scope.getRelationshipByIdUrl, {
            params: { "id": $scope.RelationshipId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.Relationship = result.Data;
                updateUrlQuery('id' , $scope.Relationship.Id);
                $scope.onAfterGetRelationshipById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Relationship! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Relationship! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadRelationshipDataExtra= function(RelationshipId)
    {
        if(RelationshipId!=null)
            $scope.RelationshipId=RelationshipId;
            
            $http.get($scope.getRelationshipDataExtraUrl, {
                params: { "id": $scope.RelationshipId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.GenderEnumList!=null)
                         $scope.GenderEnumList = result.DataExtra.GenderEnumList;
                        $scope.onAfterLoadRelationshipDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Relationship! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Relationship! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveRelationship= function(){
    	if(!$scope.validateRelationship())
        {
          return;
        }
        $http.post($scope.saveRelationshipUrl + "/", $scope.Relationship).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.Relationship = result.Data;
                    updateUrlQuery('id', $scope.Relationship.Id);
                   }
                   alertSuccess("Successfully saved Relationship data!");
                   $scope.onAfterSaveRelationship(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Relationship! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Relationship! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteRelationshipById= function(){
    	
    };
   $scope.validateRelationship= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (RelationshipId,getRelationshipByIdUrl,getRelationshipDataExtraUrl, saveRelationshipUrl,deleteRelationshipByIdUrl) {
        $scope.RelationshipId = RelationshipId;
        $scope.getRelationshipByIdUrl = getRelationshipByIdUrl;
        $scope.saveRelationshipUrl = saveRelationshipUrl;
        $scope.getRelationshipDataExtraUrl = getRelationshipDataExtraUrl;
        $scope.deleteRelationshipByIdUrl = deleteRelationshipByIdUrl;

        $scope.loadPage(RelationshipId);
    };
    
  $scope.onAfterSaveRelationship= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetRelationshipById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadRelationshipDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
  /*
  //Child Table Bindings, need to fix
             $scope.EmergencyContactRelationshipIdList =  result.DataExtra.EmergencyContactRelationshipIdList; 
     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

