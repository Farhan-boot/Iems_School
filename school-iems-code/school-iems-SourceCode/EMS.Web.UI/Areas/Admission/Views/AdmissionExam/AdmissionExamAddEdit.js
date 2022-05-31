
//File: Adm_AdmissionExam Anjular  Controller
emsApp.controller('AdmissionExamAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.AdmissionExam = [];
    $scope.AdmissionExamId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;

   $scope.loadPage= function (AdmissionExamId)
    {
       if(AdmissionExamId!=null)
        $scope.AdmissionExamId=AdmissionExamId;
       
       $scope.loadAdmissionExamExtraData($scope.AdmissionExamId);
       $scope.getAdmissionExamById($scope.AdmissionExamId);
    };
   $scope.getNewAdmissionExam= function()
    {
    	  $scope.getAdmissionExamById(0);
    };
   $scope.getAdmissionExamById= function(AdmissionExamId)
    {
        if(AdmissionExamId!=null && AdmissionExamId!=='')
        $scope.AdmissionExamId=AdmissionExamId;
        else return;
        
        $http.get($scope.getAdmissionExamByIdUrl, {
            params: { "id": $scope.AdmissionExamId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.AdmissionExam = result.Data;
                updateUrlQuery('id' , $scope.AdmissionExam.Id);
                $scope.onAfterGetAdmissionExamById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Admission Exam! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Admission Exam! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadAdmissionExamExtraData= function(AdmissionExamId)
    {
        if(AdmissionExamId!=null)
            $scope.AdmissionExamId=AdmissionExamId;
            
            $http.get($scope.getAdmissionExamExtraDataUrl, {
                params: { "id": $scope.AdmissionExamId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.ProgramTypeEnumList!=null)
                         $scope.ProgramTypeEnumList = result.DataExtra.ProgramTypeEnumList;
                      if(result.DataExtra.CircularStatusEnumList!=null)
                          $scope.CircularStatusEnumList = result.DataExtra.CircularStatusEnumList;

                      if (result.DataExtra.ProgramList != null)
                          $scope.ProgramList = result.DataExtra.ProgramList;

                      if (result.DataExtra.BatchList != null)
                          $scope.BatchList = result.DataExtra.BatchList;

                        $scope.onAfterLoadAdmissionExamExtraData(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Admission Exam! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Admission Exam! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveAdmissionExam= function(){
    	if(!$scope.validateAdmissionExam())
        {
          return;
        }
        $http.post($scope.saveAdmissionExamUrl + "/", $scope.AdmissionExam).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.AdmissionExam = result.Data;
                    updateUrlQuery('id', $scope.AdmissionExam.Id);
                   }
                   alertSuccess("Successfully saved Admission Exam data!");
                   $scope.onAfterSaveAdmissionExam(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Admission Exam! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Admission Exam! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteAdmissionExamById= function(){
    	
    };
   $scope.validateAdmissionExam= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (AdmissionExamId,getAdmissionExamByIdUrl,getAdmissionExamExtraDataUrl, saveAdmissionExamUrl,deleteAdmissionExamByIdUrl) {
        $scope.AdmissionExamId = AdmissionExamId;
        $scope.getAdmissionExamByIdUrl = getAdmissionExamByIdUrl;
        $scope.saveAdmissionExamUrl = saveAdmissionExamUrl;
        $scope.getAdmissionExamExtraDataUrl = getAdmissionExamExtraDataUrl;
        $scope.deleteAdmissionExamByIdUrl = deleteAdmissionExamByIdUrl;

        $scope.loadPage(AdmissionExamId);
    };
    
  $scope.onAfterSaveAdmissionExam= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetAdmissionExamById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadAdmissionExamExtraData= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
         if(result.DataExtra.SemesterList!=null)
         $scope.SemesterList =  result.DataExtra.SemesterList; 
  /*
  //Child Table Bindings, need to fix     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

