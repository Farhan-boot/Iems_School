
//File: User_Rank Anjular  Controller
emsApp.controller('RankAddEditCtrl', function ($scope, $http, $filter) {
    $scope.Rank = [];
    $scope.RankId=0;
    
    $scope.ErrorMsg = "";
    $scope.HasError= false;

   $scope.loadPage= function (RankId)
    {
       if(RankId!=null)
        $scope.RankId=RankId;
       
       $scope.loadRankExtraData($scope.RankId);
       $scope.getRankById($scope.RankId);
    }
   $scope.getNewRank= function()
    {
    	  $scope.getRankById(0);
    }
   $scope.getRankById= function(RankId)
    {
        if(RankId!=null)
        $scope.RankId=RankId;
        
        
        $http.get($scope.getRankByIdUrl, {
            params: { "id": $scope.RankId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.Rank = result.Data;
                $scope.onAfterGetRankById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Rank! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
             
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Rank! "+"Status: " +status.toString()+". "+ result.Message.toString();
             alertError($scope.ErrorMsg);
             
        });
    }
   $scope.loadRankExtraData= function(RankId)
    {
        if(RankId!=null)
            $scope.RankId=RankId;
            
            
            $http.get($scope.getRankExtraDataUrl, {
                params: { "id": $scope.RankId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.CategoryEnumList!=null)
                         $scope.CategoryEnumList = result.DataExtra.CategoryEnumList;
                      if(result.DataExtra.JobClassEnumList!=null)
                         $scope.JobClassEnumList = result.DataExtra.JobClassEnumList;
                      if(result.DataExtra.AcademicLevelEnumList!=null)
                         $scope.AcademicLevelEnumList = result.DataExtra.AcademicLevelEnumList;
                        $scope.onAfterLoadRankExtraData(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Rank! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
                 
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Rank! "+"Status: " +status.toString()+". "+ result.Message.toString();
                 alertError($scope.ErrorMsg);
                 
            });
    }
   $scope.saveRank= function(){
    	if(!$scope.validateRank())
        {
          return;
        }
        
        $http.post($scope.saveRankUrl + "/", $scope.Rank).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.Rank = result.Data;}
                   alertSuccess("Successfully saved Rank data!");
                   $scope.onAfterSaveRank(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Rank! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
                
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Rank! "+"Status: " +status.toString()+". "+ result.Message.toString();
                alertError($scope.ErrorMsg);
                
            });
    }

   $scope.deleteRankById= function(){
    	
    }
   $scope.validateRank= function(){
        return true;
    } 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (RankId,getRankByIdUrl,getRankExtraDataUrl, saveRankUrl,deleteRankByIdUrl) {
        $scope.RankId = RankId;
        $scope.getRankByIdUrl = getRankByIdUrl;
        $scope.saveRankUrl = saveRankUrl;
        $scope.getRankExtraDataUrl = getRankExtraDataUrl;
        $scope.deleteRankByIdUrl = deleteRankByIdUrl;

        $scope.loadPage(RankId);
    };

    $scope.onAfterSaveRank = function(result) {
        //var data=result.Data;
    };
    $scope.onAfterGetRankById = function(result) {
        //var data=result.Data;
        /*//Child Table Bindings
             $scope.EmployementHistory_RankIdList =  result.DataExtra.EmployementHistory_RankIdList; 

             $scope.Account_RankIdList =  result.DataExtra.Account_RankIdList; 

             $scope.RankHistory_RankIdList =  result.DataExtra.RankHistory_RankIdList; 
     */
    };
    $scope.onAfterLoadRankExtraData = function(result) {
        //var DataExtra=result.DataExtra;
        //DropDown Option Tables    
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});