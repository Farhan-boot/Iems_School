
//File: Acnt_ScholarshipType Anjular  Controller
emsApp.controller('ScholarshipTypeAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.ScholarshipType = [];
    $scope.ScholarshipTypeId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;

   $scope.loadPage= function (ScholarshipTypeId)
    {
       if(ScholarshipTypeId!=null)
        $scope.ScholarshipTypeId=ScholarshipTypeId;
       
       $scope.loadScholarshipTypeExtraData($scope.ScholarshipTypeId);
       $scope.getScholarshipTypeById($scope.ScholarshipTypeId);
    };
   $scope.getNewScholarshipType= function()
    {
    	  $scope.getScholarshipTypeById(0);
    };
   $scope.getScholarshipTypeById= function(ScholarshipTypeId)
    {
        if(ScholarshipTypeId!=null && ScholarshipTypeId!=='')
        $scope.ScholarshipTypeId=ScholarshipTypeId;
        else return;
        
        $http.get($scope.getScholarshipTypeByIdUrl, {
            params: { "id": $scope.ScholarshipTypeId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.ScholarshipType = result.Data;
                updateUrlQuery('id' , $scope.ScholarshipType.Id);
                $scope.onAfterGetScholarshipTypeById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Scholarship Type! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Scholarship Type! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadScholarshipTypeExtraData= function(ScholarshipTypeId)
    {
        if(ScholarshipTypeId!=null)
            $scope.ScholarshipTypeId=ScholarshipTypeId;
            
            $http.get($scope.getScholarshipTypeExtraDataUrl, {
                params: { "id": $scope.ScholarshipTypeId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.StatusEnumList!=null)
                         $scope.StatusEnumList = result.DataExtra.StatusEnumList;
                        $scope.onAfterLoadScholarshipTypeExtraData(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Scholarship Type! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Scholarship Type! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveScholarshipType= function(){
    	if(!$scope.validateScholarshipType())
        {
          return;
        }
        $http.post($scope.saveScholarshipTypeUrl + "/", $scope.ScholarshipType).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.ScholarshipType = result.Data;
                    updateUrlQuery('id', $scope.ScholarshipType.Id);
                   }
                   alertSuccess("Successfully saved Scholarship Type data!");
                   $scope.onAfterSaveScholarshipType(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Scholarship Type! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Scholarship Type! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteScholarshipTypeById= function(){
    	
    };
   $scope.validateScholarshipType= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (ScholarshipTypeId,getScholarshipTypeByIdUrl,getScholarshipTypeExtraDataUrl, saveScholarshipTypeUrl,deleteScholarshipTypeByIdUrl) {
        $scope.ScholarshipTypeId = ScholarshipTypeId;
        $scope.getScholarshipTypeByIdUrl = getScholarshipTypeByIdUrl;
        $scope.saveScholarshipTypeUrl = saveScholarshipTypeUrl;
        $scope.getScholarshipTypeExtraDataUrl = getScholarshipTypeExtraDataUrl;
        $scope.deleteScholarshipTypeByIdUrl = deleteScholarshipTypeByIdUrl;

        $scope.loadPage(ScholarshipTypeId);
    };
    
  $scope.onAfterSaveScholarshipType= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetScholarshipTypeById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadScholarshipTypeExtraData= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
  /*
  //Child Table Bindings, need to fix
             $scope.ScholarshipTypeIdList =  result.DataExtra.ScholarshipTypeIdList; 
     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

