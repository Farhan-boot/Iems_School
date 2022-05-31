
//File: General_Notice Anjular  Controller
emsApp.controller('NoticeAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.Notice = [];
    $scope.NoticeId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (NoticeId)
    {
       if(NoticeId!=null)
        $scope.NoticeId=NoticeId;
       
       $scope.loadNoticeDataExtra($scope.NoticeId);
       $scope.getNoticeById($scope.NoticeId);
    };
   $scope.getNewNotice= function()
    {
    	  $scope.getNoticeById(0);
    };
   $scope.getNoticeById= function(NoticeId)
    {
        if(NoticeId!=null && NoticeId!=='')
        $scope.NoticeId=NoticeId;
        else return;
        
        $http.get($scope.getNoticeByIdUrl, {
            params: { "id": $scope.NoticeId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.Notice = result.Data;
                updateUrlQuery('id' , $scope.Notice.Id);
                $scope.onAfterGetNoticeById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Notice! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Notice! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadNoticeDataExtra= function(NoticeId)
    {
        if(NoticeId!=null)
            $scope.NoticeId=NoticeId;
            
            $http.get($scope.getNoticeDataExtraUrl, {
                params: { "id": $scope.NoticeId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.VisibilityTypeEnumList!=null)
                         $scope.VisibilityTypeEnumList = result.DataExtra.VisibilityTypeEnumList;
                        $scope.onAfterLoadNoticeDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Notice! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Notice! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveNotice= function(){
    	if(!$scope.validateNotice())
        {
          return;
        }
        $http.post($scope.saveNoticeUrl + "/", $scope.Notice).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.Notice = result.Data;
                    updateUrlQuery('id', $scope.Notice.Id);
                   }
                   alertSuccess("Successfully saved Notice data!");
                   $scope.onAfterSaveNotice(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Notice! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Notice! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteNoticeById= function(){
    	
    };
   $scope.validateNotice= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (NoticeId,getNoticeByIdUrl,getNoticeDataExtraUrl, saveNoticeUrl,deleteNoticeByIdUrl) {
        $scope.NoticeId = NoticeId;
        $scope.getNoticeByIdUrl = getNoticeByIdUrl;
        $scope.saveNoticeUrl = saveNoticeUrl;
        $scope.getNoticeDataExtraUrl = getNoticeDataExtraUrl;
        $scope.deleteNoticeByIdUrl = deleteNoticeByIdUrl;

        $scope.loadPage(NoticeId);
    };
    
  $scope.onAfterSaveNotice= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetNoticeById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadNoticeDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
  /*
  //Child Table Bindings, need to fix     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

