
//File: Aca_StudyTerm Anjular  Controller
emsApp.controller('StudyTermAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.StudyTerm = [];
    $scope.StudyTermId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (StudyTermId)
    {
       if(StudyTermId!=null)
        $scope.StudyTermId=StudyTermId;
       
       $scope.loadStudyTermDataExtra($scope.StudyTermId);
       $scope.getStudyTermById($scope.StudyTermId);
    };
   $scope.getNewStudyTerm= function()
    {
    	  $scope.getStudyTermById(0);
    };
   $scope.getStudyTermById= function(StudyTermId)
    {
        if(StudyTermId!=null && StudyTermId!=='')
        $scope.StudyTermId=StudyTermId;
        else return;
        
        $http.get($scope.getStudyTermByIdUrl, {
            params: { "id": $scope.StudyTermId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.StudyTerm = result.Data;
                updateUrlQuery('id' , $scope.StudyTerm.Id);
                $scope.onAfterGetStudyTermById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Study Term! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Study Term! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadStudyTermDataExtra= function(StudyTermId)
    {
        if(StudyTermId!=null)
            $scope.StudyTermId=StudyTermId;
            
            $http.get($scope.getStudyTermDataExtraUrl, {
                params: { "id": $scope.StudyTermId }
            }).success(function (result, status) {
                if (!result.HasError) {
                    if (result.DataExtra.SemesterDurationList != null) {
                        $scope.SemesterDurationList = result.DataExtra.SemesterDurationList;
                    }
                   $scope.HasError= false;
                        $scope.onAfterLoadStudyTermDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Study Term! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Study Term! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveStudyTerm= function(){
    	if(!$scope.validateStudyTerm())
        {
          return;
        }
        $http.post($scope.saveStudyTermUrl + "/", $scope.StudyTerm).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.StudyTerm = result.Data;
                    updateUrlQuery('id', $scope.StudyTerm.Id);
                   }
                   alertSuccess("Successfully saved Study Term data!");
                   $scope.onAfterSaveStudyTerm(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Study Term! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Study Term! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteStudyTermById= function(){
    	
    };
   $scope.validateStudyTerm= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (StudyTermId,getStudyTermByIdUrl,getStudyTermDataExtraUrl, saveStudyTermUrl,deleteStudyTermByIdUrl) {
        $scope.StudyTermId = StudyTermId;
        $scope.getStudyTermByIdUrl = getStudyTermByIdUrl;
        $scope.saveStudyTermUrl = saveStudyTermUrl;
        $scope.getStudyTermDataExtraUrl = getStudyTermDataExtraUrl;
        $scope.deleteStudyTermByIdUrl = deleteStudyTermByIdUrl;

        $scope.loadPage(StudyTermId);
    };
    
  $scope.onAfterSaveStudyTerm= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetStudyTermById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadStudyTermDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
  /*
  //Child Table Bindings, need to fix
             $scope.TermIdList =  result.DataExtra.TermIdList; 

             $scope.TermIdList =  result.DataExtra.TermIdList; 

             $scope.TermIdList =  result.DataExtra.TermIdList; 

             $scope.TermIdList =  result.DataExtra.TermIdList; 
     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

