
//File: Aca_Exam Anjular  Controller
emsApp.controller('ExamAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar, $sce) {
    $scope.Exam = [];
    $scope.ExamId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (ExamId)
    {
       if(ExamId!=null)
        $scope.ExamId=ExamId;
       
       $scope.loadExamDataExtra($scope.ExamId);
       $scope.getExamById($scope.ExamId);
    };
   $scope.getNewExam= function()
    {
    	  $scope.getExamById(0);
    };
   $scope.getExamById= function(ExamId)
    {
        if(ExamId!=null && ExamId!=='')
        $scope.ExamId=ExamId;
        else return;
        
        $http.get($scope.getExamByIdUrl, {
            params: { "id": $scope.ExamId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.Exam = result.Data;
                log($scope.Exam);
                updateUrlQuery('id' , $scope.Exam.Id);
                $scope.onAfterGetExamById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Exam! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Exam! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadExamDataExtra= function(ExamId)
    {
        if(ExamId!=null)
            $scope.ExamId=ExamId;
            
            $http.get($scope.getExamDataExtraUrl, {
                params: { "id": $scope.ExamId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.ProgramTypeEnumList!=null)
                         $scope.ProgramTypeEnumList = result.DataExtra.ProgramTypeEnumList;
                      if(result.DataExtra.ExamTypeEnumList!=null)
                         $scope.ExamTypeEnumList = result.DataExtra.ExamTypeEnumList;
                        $scope.onAfterLoadExamDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Exam! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Exam! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveExam= function(){
    	if(!$scope.validateExam())
        {
          return;
        }
        $http.post($scope.saveExamUrl + "/", $scope.Exam).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.Exam = result.Data;
                    updateUrlQuery('id', $scope.Exam.Id);
                   }
                   alertSuccess("Successfully saved Exam data!");
                   $scope.onAfterSaveExam(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Exam! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Exam! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteExamById= function(){
    	
    };
   $scope.validateExam= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (ExamId,getExamByIdUrl,getExamDataExtraUrl, saveExamUrl,deleteExamByIdUrl) {
        $scope.ExamId = ExamId;
        $scope.getExamByIdUrl = getExamByIdUrl;
        $scope.saveExamUrl = saveExamUrl;
        $scope.getExamDataExtraUrl = getExamDataExtraUrl;
        $scope.deleteExamByIdUrl = deleteExamByIdUrl;

        $scope.loadPage(ExamId);
    };
    
  $scope.onAfterSaveExam= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetExamById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadExamDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
         if(result.DataExtra.GradingPolicyOptionList!=null)
         $scope.GradingPolicyOptionList =  result.DataExtra.GradingPolicyOptionList; 
         if(result.DataExtra.SemesterList!=null)
         $scope.SemesterList =  result.DataExtra.SemesterList; 
  /*
  //Child Table Bindings, need to fix
             $scope.ExamIdList =  result.DataExtra.ExamIdList; 

             $scope.ExamIdList =  result.DataExtra.ExamIdList; 

             $scope.ExamIdList =  result.DataExtra.ExamIdList; 

             $scope.ExamIdList =  result.DataExtra.ExamIdList; 

             $scope.ExamIdList =  result.DataExtra.ExamIdList; 

             $scope.ExamIdList =  result.DataExtra.ExamIdList; 

             $scope.ExamIdList =  result.DataExtra.ExamIdList; 

             $scope.ExamIdList =  result.DataExtra.ExamIdList; 

             $scope.ExamIdList =  result.DataExtra.ExamIdList; 
     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 
    $scope.renderHtml = function (html_code) {
        return $sce.trustAsHtml(html_code);
    };
//======Custom property and Functions End========================================================== 
});

