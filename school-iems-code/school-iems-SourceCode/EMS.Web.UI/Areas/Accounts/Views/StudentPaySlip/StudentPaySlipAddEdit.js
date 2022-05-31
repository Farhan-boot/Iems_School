
//File: Acnt_StudentPaySlip Anjular  Controller
emsApp.controller('StudentPaySlipAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.StudentPaySlip = [];
    $scope.StudentPaySlipId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (StudentPaySlipId)
    {
       if(StudentPaySlipId!=null)
        $scope.StudentPaySlipId=StudentPaySlipId;
       
       $scope.loadStudentPaySlipDataExtra($scope.StudentPaySlipId);
       $scope.getStudentPaySlipById($scope.StudentPaySlipId);
    };
   $scope.getNewStudentPaySlip= function()
    {
    	  $scope.getStudentPaySlipById(0);
    };
   $scope.getStudentPaySlipById= function(StudentPaySlipId)
    {
        if(StudentPaySlipId!=null && StudentPaySlipId!=='')
        $scope.StudentPaySlipId=StudentPaySlipId;
        else return;
        
        $http.get($scope.getStudentPaySlipByIdUrl, {
            params: { "id": $scope.StudentPaySlipId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.StudentPaySlip = result.Data;
                updateUrlQuery('id' , $scope.StudentPaySlip.Id);
                $scope.onAfterGetStudentPaySlipById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Student Pay Slip! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Student Pay Slip! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadStudentPaySlipDataExtra= function(StudentPaySlipId)
    {
        if(StudentPaySlipId!=null)
            $scope.StudentPaySlipId=StudentPaySlipId;
            
            $http.get($scope.getStudentPaySlipDataExtraUrl, {
                params: { "id": $scope.StudentPaySlipId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.PostStatusEnumList!=null)
                         $scope.PostStatusEnumList = result.DataExtra.PostStatusEnumList;
                        $scope.onAfterLoadStudentPaySlipDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Student Pay Slip! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Student Pay Slip! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveStudentPaySlip= function(){
    	if(!$scope.validateStudentPaySlip())
        {
          return;
        }
        $http.post($scope.saveStudentPaySlipUrl + "/", $scope.StudentPaySlip).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.StudentPaySlip = result.Data;
                    updateUrlQuery('id', $scope.StudentPaySlip.Id);
                   }
                   alertSuccess("Successfully saved Student Pay Slip data!");
                   $scope.onAfterSaveStudentPaySlip(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Student Pay Slip! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Student Pay Slip! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteStudentPaySlipById= function(){
    	
    };
   $scope.validateStudentPaySlip= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (StudentPaySlipId,getStudentPaySlipByIdUrl,getStudentPaySlipDataExtraUrl, saveStudentPaySlipUrl,deleteStudentPaySlipByIdUrl) {
        $scope.StudentPaySlipId = StudentPaySlipId;
        $scope.getStudentPaySlipByIdUrl = getStudentPaySlipByIdUrl;
        $scope.saveStudentPaySlipUrl = saveStudentPaySlipUrl;
        $scope.getStudentPaySlipDataExtraUrl = getStudentPaySlipDataExtraUrl;
        $scope.deleteStudentPaySlipByIdUrl = deleteStudentPaySlipByIdUrl;

        $scope.loadPage(StudentPaySlipId);
    };
    
  $scope.onAfterSaveStudentPaySlip= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetStudentPaySlipById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadStudentPaySlipDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
  /*
  //Child Table Bindings, need to fix     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

