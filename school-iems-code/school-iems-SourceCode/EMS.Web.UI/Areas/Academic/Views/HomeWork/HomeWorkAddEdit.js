
//File: Aca_HomeWork Anjular  Controller
emsApp.controller('HomeWorkAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.HomeWork = [];
    $scope.HomeWorkId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (HomeWorkId)
    {
       if(HomeWorkId!=null)
        $scope.HomeWorkId=HomeWorkId;
       
       $scope.loadHomeWorkDataExtra($scope.HomeWorkId);
       $scope.getHomeWorkById($scope.HomeWorkId);
    };
   $scope.getNewHomeWork= function()
    {
    	  $scope.getHomeWorkById(0);
    };
   $scope.getHomeWorkById= function(HomeWorkId)
    {
        if(HomeWorkId!=null && HomeWorkId!=='')
        $scope.HomeWorkId=HomeWorkId;
        else return;
        
        $http.get($scope.getHomeWorkByIdUrl, {
            params: { "id": $scope.HomeWorkId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.HomeWork = result.Data;
                updateUrlQuery('id' , $scope.HomeWork.Id);
                $scope.onAfterGetHomeWorkById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Home Work! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Home Work! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadHomeWorkDataExtra= function(HomeWorkId)
    {
        if(HomeWorkId!=null)
            $scope.HomeWorkId=HomeWorkId;
            
            $http.get($scope.getHomeWorkDataExtraUrl, {
                params: { "id": $scope.HomeWorkId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.GroupEnumList!=null)
                         $scope.GroupEnumList = result.DataExtra.GroupEnumList;
                      if(result.DataExtra.HomeworkTypeEnumList!=null)
                         $scope.HomeworkTypeEnumList = result.DataExtra.HomeworkTypeEnumList;
                        $scope.onAfterLoadHomeWorkDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Home Work! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Home Work! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveHomeWork= function(){
    	if(!$scope.validateHomeWork())
        {
          return;
        }
        $http.post($scope.saveHomeWorkUrl + "/", $scope.HomeWork).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.HomeWork = result.Data;
                    updateUrlQuery('id', $scope.HomeWork.Id);
                   }
                   alertSuccess("Successfully saved Home Work data!");
                   $scope.onAfterSaveHomeWork(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Home Work! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Home Work! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteHomeWorkById= function(){
    	
    };
   $scope.validateHomeWork= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (HomeWorkId,getHomeWorkByIdUrl,getHomeWorkDataExtraUrl, saveHomeWorkUrl,deleteHomeWorkByIdUrl) {
        $scope.HomeWorkId = HomeWorkId;
        $scope.getHomeWorkByIdUrl = getHomeWorkByIdUrl;
        $scope.saveHomeWorkUrl = saveHomeWorkUrl;
        $scope.getHomeWorkDataExtraUrl = getHomeWorkDataExtraUrl;
        $scope.deleteHomeWorkByIdUrl = deleteHomeWorkByIdUrl;

        $scope.loadPage(HomeWorkId);
    };
    
  $scope.onAfterSaveHomeWork= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetHomeWorkById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadHomeWorkDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
         if(result.DataExtra.ClassShiftSectionMapList!=null)
         $scope.ClassShiftSectionMapList =  result.DataExtra.ClassShiftSectionMapList; 
         if(result.DataExtra.SubjectList!=null)
         $scope.SubjectList =  result.DataExtra.SubjectList; 
  /*
  //Child Table Bindings, need to fix
             $scope.HomeworkIdList =  result.DataExtra.HomeworkIdList; 
     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

