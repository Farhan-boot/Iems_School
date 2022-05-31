
//File: Acnt_SemWiseScholarship Anjular  Controller
emsApp.controller('SemWiseScholarshipAddEditCtrl', function ($scope, $http, $filter, $document, cfpLoadingBar, $sce) {
    $scope.SemWiseScholarship = [];
    $scope.SemWiseScholarshipId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (SemWiseScholarshipId)
    {
       if(SemWiseScholarshipId!=null)
        $scope.SemWiseScholarshipId=SemWiseScholarshipId;
       
       $scope.loadSemWiseScholarshipDataExtra($scope.SemWiseScholarshipId);
       $scope.getSemWiseScholarshipById($scope.SemWiseScholarshipId);
    };
   $scope.getNewSemWiseScholarship= function()
    {
    	  $scope.getSemWiseScholarshipById(0);
    };
   $scope.getSemWiseScholarshipById= function(SemWiseScholarshipId)
    {
        if(SemWiseScholarshipId!=null && SemWiseScholarshipId!=='')
        $scope.SemWiseScholarshipId=SemWiseScholarshipId;
        else return;
        
        $http.get($scope.getSemWiseScholarshipByIdUrl, {
            params: { "id": $scope.SemWiseScholarshipId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.SemWiseScholarship = result.Data;
                updateUrlQuery('id' , $scope.SemWiseScholarship.Id);
                $scope.onAfterGetSemWiseScholarshipById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Sem Wise Scholarship! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Sem Wise Scholarship! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadSemWiseScholarshipDataExtra= function(SemWiseScholarshipId)
    {
        if(SemWiseScholarshipId!=null)
            $scope.SemWiseScholarshipId=SemWiseScholarshipId;
            
            $http.get($scope.getSemWiseScholarshipDataExtraUrl, {
                params: { "id": $scope.SemWiseScholarshipId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.DiscountCategoryEnumList!=null)
                          $scope.DiscountCategoryEnumList = result.DataExtra.DiscountCategoryEnumList;
                      if (result.DataExtra.ProgramList != null)
                          $scope.ProgramList = result.DataExtra.ProgramList;
                      if (result.DataExtra.SemesterList != null)
                          $scope.SemesterList = result.DataExtra.SemesterList;
                      if (result.DataExtra.ParticularNameList != null)
                          $scope.ParticularNameList = result.DataExtra.ParticularNameList;

                      if (result.DataExtra.ScholarshipParticularNameList != null)
                          $scope.ScholarshipParticularNameList = result.DataExtra.ScholarshipParticularNameList;


                        $scope.onAfterLoadSemWiseScholarshipDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Sem Wise Scholarship! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Sem Wise Scholarship! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveSemWiseScholarship= function(){
    	if(!$scope.validateSemWiseScholarship())
        {
          return;
        }
        $http.post($scope.saveSemWiseScholarshipUrl + "/", $scope.SemWiseScholarship).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.SemWiseScholarship = result.Data;
                    updateUrlQuery('id', $scope.SemWiseScholarship.Id);
                   }
                   alertSuccess("Successfully saved Sem Wise Scholarship data!");
                   $scope.onAfterSaveSemWiseScholarship(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Sem Wise Scholarship! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Sem Wise Scholarship! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteSemWiseScholarshipById= function(){
    	
    };
   $scope.validateSemWiseScholarship= function(){
        return true;
   };

   $scope.renderHtml = function (html_code) {
       return $sce.trustAsHtml(html_code);
   };
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (SemWiseScholarshipId,getSemWiseScholarshipByIdUrl,getSemWiseScholarshipDataExtraUrl, saveSemWiseScholarshipUrl,deleteSemWiseScholarshipByIdUrl) {
        $scope.SemWiseScholarshipId = SemWiseScholarshipId;
        $scope.getSemWiseScholarshipByIdUrl = getSemWiseScholarshipByIdUrl;
        $scope.saveSemWiseScholarshipUrl = saveSemWiseScholarshipUrl;
        $scope.getSemWiseScholarshipDataExtraUrl = getSemWiseScholarshipDataExtraUrl;
        $scope.deleteSemWiseScholarshipByIdUrl = deleteSemWiseScholarshipByIdUrl;

        $scope.loadPage(SemWiseScholarshipId);
    };
    
  $scope.onAfterSaveSemWiseScholarship= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetSemWiseScholarshipById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadSemWiseScholarshipDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
         

         if(result.DataExtra.StudentList!=null)
         $scope.StudentList =  result.DataExtra.StudentList; 
  /*
  //Child Table Bindings, need to fix     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

