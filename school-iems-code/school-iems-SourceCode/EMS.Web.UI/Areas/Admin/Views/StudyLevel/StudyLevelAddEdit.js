
//File: Aca_StudyLevel Anjular  Controller
emsApp.controller('StudyLevelAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.StudyLevel = [];
    $scope.StudyLevelId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (StudyLevelId)
    {
       if(StudyLevelId!=null)
        $scope.StudyLevelId=StudyLevelId;
       
       $scope.loadStudyLevelDataExtra($scope.StudyLevelId);
       $scope.getStudyLevelById($scope.StudyLevelId);
    };
   $scope.getNewStudyLevel= function()
    {
    	  $scope.getStudyLevelById(0);
    };
   $scope.getStudyLevelById= function(StudyLevelId)
    {
        if(StudyLevelId!=null && StudyLevelId!=='')
        $scope.StudyLevelId=StudyLevelId;
        else return;
        
        $http.get($scope.getStudyLevelByIdUrl, {
            params: { "id": $scope.StudyLevelId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.StudyLevel = result.Data;
                updateUrlQuery('id' , $scope.StudyLevel.Id);
                $scope.onAfterGetStudyLevelById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Study Level! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Study Level! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadStudyLevelDataExtra= function(StudyLevelId)
    {
        if(StudyLevelId!=null)
            $scope.StudyLevelId=StudyLevelId;
            
            $http.get($scope.getStudyLevelDataExtraUrl, {
                params: { "id": $scope.StudyLevelId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                        $scope.onAfterLoadStudyLevelDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Study Level! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Study Level! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveStudyLevel= function(){
    	if(!$scope.validateStudyLevel())
        {
          return;
        }
        $http.post($scope.saveStudyLevelUrl + "/", $scope.StudyLevel).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.StudyLevel = result.Data;
                    updateUrlQuery('id', $scope.StudyLevel.Id);
                   }
                   alertSuccess("Successfully saved Study Level data!");
                   $scope.onAfterSaveStudyLevel(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Study Level! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Study Level! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteStudyLevelById= function(){
    	
    };
   $scope.validateStudyLevel= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (StudyLevelId,getStudyLevelByIdUrl,getStudyLevelDataExtraUrl, saveStudyLevelUrl,deleteStudyLevelByIdUrl) {
        $scope.StudyLevelId = StudyLevelId;
        $scope.getStudyLevelByIdUrl = getStudyLevelByIdUrl;
        $scope.saveStudyLevelUrl = saveStudyLevelUrl;
        $scope.getStudyLevelDataExtraUrl = getStudyLevelDataExtraUrl;
        $scope.deleteStudyLevelByIdUrl = deleteStudyLevelByIdUrl;

        $scope.loadPage(StudyLevelId);
    };
    
  $scope.onAfterSaveStudyLevel= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetStudyLevelById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadStudyLevelDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
  /*
  //Child Table Bindings, need to fix
             $scope.LevelIdList =  result.DataExtra.LevelIdList; 

             $scope.LevelIdList =  result.DataExtra.LevelIdList; 

             $scope.LevelIdList =  result.DataExtra.LevelIdList; 
     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

