
//File: Adm_StudentQuotaName Anjular  Controller
emsApp.controller('StudentQuotaNameAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.StudentQuotaName = [];
    $scope.StudentQuotaNameId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (StudentQuotaNameId)
    {
       if(StudentQuotaNameId!=null)
        $scope.StudentQuotaNameId=StudentQuotaNameId;
       
       $scope.loadStudentQuotaNameDataExtra($scope.StudentQuotaNameId);
       $scope.getStudentQuotaNameById($scope.StudentQuotaNameId);
    };
   $scope.getNewStudentQuotaName= function()
    {
    	  $scope.getStudentQuotaNameById(0);
    };
   $scope.getStudentQuotaNameById= function(StudentQuotaNameId)
    {
        if(StudentQuotaNameId!=null && StudentQuotaNameId!=='')
        $scope.StudentQuotaNameId=StudentQuotaNameId;
        else return;
        
        $http.get($scope.getStudentQuotaNameByIdUrl, {
            params: { "id": $scope.StudentQuotaNameId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.StudentQuotaName = result.Data;
                updateUrlQuery('id' , $scope.StudentQuotaName.Id);
                $scope.onAfterGetStudentQuotaNameById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Student Quota Name! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Student Quota Name! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadStudentQuotaNameDataExtra= function(StudentQuotaNameId)
    {
        if(StudentQuotaNameId!=null)
            $scope.StudentQuotaNameId=StudentQuotaNameId;
            
            $http.get($scope.getStudentQuotaNameDataExtraUrl, {
                params: { "id": $scope.StudentQuotaNameId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                        $scope.onAfterLoadStudentQuotaNameDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Student Quota Name! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Student Quota Name! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveStudentQuotaName= function(){
    	if(!$scope.validateStudentQuotaName())
        {
          return;
        }
        $http.post($scope.saveStudentQuotaNameUrl + "/", $scope.StudentQuotaName).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.StudentQuotaName = result.Data;
                    updateUrlQuery('id', $scope.StudentQuotaName.Id);
                   }
                   alertSuccess("Successfully saved Student Quota Name data!");
                   $scope.onAfterSaveStudentQuotaName(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Student Quota Name! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Student Quota Name! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteStudentQuotaNameById= function(){
    	
    };
   $scope.validateStudentQuotaName= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (StudentQuotaNameId,getStudentQuotaNameByIdUrl,getStudentQuotaNameDataExtraUrl, saveStudentQuotaNameUrl,deleteStudentQuotaNameByIdUrl) {
        $scope.StudentQuotaNameId = StudentQuotaNameId;
        $scope.getStudentQuotaNameByIdUrl = getStudentQuotaNameByIdUrl;
        $scope.saveStudentQuotaNameUrl = saveStudentQuotaNameUrl;
        $scope.getStudentQuotaNameDataExtraUrl = getStudentQuotaNameDataExtraUrl;
        $scope.deleteStudentQuotaNameByIdUrl = deleteStudentQuotaNameByIdUrl;

        $scope.loadPage(StudentQuotaNameId);
    };
    
  $scope.onAfterSaveStudentQuotaName= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetStudentQuotaNameById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadStudentQuotaNameDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
  /*
  //Child Table Bindings, need to fix
             $scope.StudentQuotaNameIdList =  result.DataExtra.StudentQuotaNameIdList; 
     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

