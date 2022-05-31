
//File: Adm_EducationBoard Anjular  Controller
emsApp.controller('EducationBoardAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.EducationBoard = [];
    $scope.EducationBoardId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;

   $scope.loadPage= function (EducationBoardId)
    {
       if(EducationBoardId!=null)
        $scope.EducationBoardId=EducationBoardId;
       
       $scope.loadEducationBoardExtraData($scope.EducationBoardId);
       $scope.getEducationBoardById($scope.EducationBoardId);
    };
   $scope.getNewEducationBoard= function()
    {
    	  $scope.getEducationBoardById(0);
    };
   $scope.getEducationBoardById= function(EducationBoardId)
    {
        if(EducationBoardId!=null && EducationBoardId!=='')
        $scope.EducationBoardId=EducationBoardId;
        else return;
        
        $http.get($scope.getEducationBoardByIdUrl, {
            params: { "id": $scope.EducationBoardId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.EducationBoard = result.Data;
                updateUrlQuery('id' , $scope.EducationBoard.Id);
                $scope.onAfterGetEducationBoardById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Education Board! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Education Board! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadEducationBoardExtraData= function(EducationBoardId)
    {
        if(EducationBoardId!=null)
            $scope.EducationBoardId=EducationBoardId;
            
            $http.get($scope.getEducationBoardExtraDataUrl, {
                params: { "id": $scope.EducationBoardId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.BoardTypeEnumList!=null)
                         $scope.BoardTypeEnumList = result.DataExtra.BoardTypeEnumList;
                        $scope.onAfterLoadEducationBoardExtraData(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Education Board! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Education Board! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveEducationBoard= function(){
    	if(!$scope.validateEducationBoard())
        {
          return;
        }
        $http.post($scope.saveEducationBoardUrl + "/", $scope.EducationBoard).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.EducationBoard = result.Data;
                    updateUrlQuery('id', $scope.EducationBoard.Id);
                   }
                   alertSuccess("Successfully saved Education Board data!");
                   $scope.onAfterSaveEducationBoard(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Education Board! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Education Board! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteEducationBoardById= function(){
    	
    };
   $scope.validateEducationBoard= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (EducationBoardId,getEducationBoardByIdUrl,getEducationBoardExtraDataUrl, saveEducationBoardUrl,deleteEducationBoardByIdUrl) {
        $scope.EducationBoardId = EducationBoardId;
        $scope.getEducationBoardByIdUrl = getEducationBoardByIdUrl;
        $scope.saveEducationBoardUrl = saveEducationBoardUrl;
        $scope.getEducationBoardExtraDataUrl = getEducationBoardExtraDataUrl;
        $scope.deleteEducationBoardByIdUrl = deleteEducationBoardByIdUrl;

        $scope.loadPage(EducationBoardId);
    };
    
  $scope.onAfterSaveEducationBoard= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetEducationBoardById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadEducationBoardExtraData= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
  /*
  //Child Table Bindings, need to fix
             $scope.BoardIdList =  result.DataExtra.BoardIdList; 
     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

