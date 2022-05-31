
//File: Aca_HomeWorkSubmission Anjular  Controller
emsApp.controller('HomeWorkSubmissionAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.HomeWorkSubmission = [];
    $scope.HomeWorkSubmissionId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (HomeWorkSubmissionId)
    {
       if(HomeWorkSubmissionId!=null)
        $scope.HomeWorkSubmissionId=HomeWorkSubmissionId;
       
       $scope.loadHomeWorkSubmissionDataExtra($scope.HomeWorkSubmissionId);
       $scope.getHomeWorkSubmissionById($scope.HomeWorkSubmissionId);
    };
   $scope.getNewHomeWorkSubmission= function()
    {
    	  $scope.getHomeWorkSubmissionById(0);
    };
   $scope.getHomeWorkSubmissionById= function(HomeWorkSubmissionId)
    {
        if(HomeWorkSubmissionId!=null && HomeWorkSubmissionId!=='')
        $scope.HomeWorkSubmissionId=HomeWorkSubmissionId;
        else return;
        
        $http.get($scope.getHomeWorkSubmissionByIdUrl, {
            params: { "id": $scope.HomeWorkSubmissionId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.HomeWorkSubmission = result.Data;
                updateUrlQuery('id' , $scope.HomeWorkSubmission.Id);
                $scope.onAfterGetHomeWorkSubmissionById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Home Work Submission! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Home Work Submission! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadHomeWorkSubmissionDataExtra= function(HomeWorkSubmissionId)
    {
        if(HomeWorkSubmissionId!=null)
            $scope.HomeWorkSubmissionId=HomeWorkSubmissionId;
            
            $http.get($scope.getHomeWorkSubmissionDataExtraUrl, {
                params: { "id": $scope.HomeWorkSubmissionId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.SubmissionStatusEnumList!=null)
                         $scope.SubmissionStatusEnumList = result.DataExtra.SubmissionStatusEnumList;
                        $scope.onAfterLoadHomeWorkSubmissionDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Home Work Submission! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Home Work Submission! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveHomeWorkSubmission= function(){
    	if(!$scope.validateHomeWorkSubmission())
        {
          return;
        }
        $http.post($scope.saveHomeWorkSubmissionUrl + "/", $scope.HomeWorkSubmission).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.HomeWorkSubmission = result.Data;
                    updateUrlQuery('id', $scope.HomeWorkSubmission.Id);
                   }
                   alertSuccess("Successfully saved Home Work Submission data!");
                   $scope.onAfterSaveHomeWorkSubmission(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Home Work Submission! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Home Work Submission! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteHomeWorkSubmissionById= function(){
    	
    };
   $scope.validateHomeWorkSubmission= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (HomeWorkSubmissionId,getHomeWorkSubmissionByIdUrl,getHomeWorkSubmissionDataExtraUrl, saveHomeWorkSubmissionUrl,deleteHomeWorkSubmissionByIdUrl) {
        $scope.HomeWorkSubmissionId = HomeWorkSubmissionId;
        $scope.getHomeWorkSubmissionByIdUrl = getHomeWorkSubmissionByIdUrl;
        $scope.saveHomeWorkSubmissionUrl = saveHomeWorkSubmissionUrl;
        $scope.getHomeWorkSubmissionDataExtraUrl = getHomeWorkSubmissionDataExtraUrl;
        $scope.deleteHomeWorkSubmissionByIdUrl = deleteHomeWorkSubmissionByIdUrl;

        $scope.loadPage(HomeWorkSubmissionId);
    };
    
  $scope.onAfterSaveHomeWorkSubmission= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetHomeWorkSubmissionById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadHomeWorkSubmissionDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
         if(result.DataExtra.HomeWorkList!=null)
         $scope.HomeWorkList =  result.DataExtra.HomeWorkList; 
         if(result.DataExtra.StudentList!=null)
         $scope.StudentList =  result.DataExtra.StudentList; 
  /*
  //Child Table Bindings, need to fix     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

