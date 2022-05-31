
//File: Invt_Category Anjular  Controller
emsApp.controller('CategoryAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.Category = [];
    $scope.CategoryId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (CategoryId)
    {
       if(CategoryId!=null)
        $scope.CategoryId=CategoryId;
       
       $scope.loadCategoryDataExtra($scope.CategoryId);
       $scope.getCategoryById($scope.CategoryId);
    };
   $scope.getNewCategory= function()
    {
    	  $scope.getCategoryById(0);
    };
   $scope.getCategoryById= function(CategoryId)
    {
        if(CategoryId!=null && CategoryId!=='')
        $scope.CategoryId=CategoryId;
        else return;
        
        $http.get($scope.getCategoryByIdUrl, {
            params: { "id": $scope.CategoryId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.Category = result.Data;
                updateUrlQuery('id' , $scope.Category.Id);
                $scope.onAfterGetCategoryById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Category! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Category! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadCategoryDataExtra= function(CategoryId)
    {
        if(CategoryId!=null)
            $scope.CategoryId=CategoryId;
            
            $http.get($scope.getCategoryDataExtraUrl, {
                params: { "id": $scope.CategoryId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                        $scope.onAfterLoadCategoryDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Category! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Category! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveCategory= function(){
    	if(!$scope.validateCategory())
        {
          return;
        }
        $http.post($scope.saveCategoryUrl + "/", $scope.Category).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.Category = result.Data;
                    updateUrlQuery('id', $scope.Category.Id);
                   }
                   alertSuccess("Successfully saved Category data!");
                   $scope.onAfterSaveCategory(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Category! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Category! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteCategoryById= function(){
    	
    };
   $scope.validateCategory= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (CategoryId,getCategoryByIdUrl,getCategoryDataExtraUrl, saveCategoryUrl,deleteCategoryByIdUrl) {
        $scope.CategoryId = CategoryId;
        $scope.getCategoryByIdUrl = getCategoryByIdUrl;
        $scope.saveCategoryUrl = saveCategoryUrl;
        $scope.getCategoryDataExtraUrl = getCategoryDataExtraUrl;
        $scope.deleteCategoryByIdUrl = deleteCategoryByIdUrl;

        $scope.loadPage(CategoryId);
    };
    
  $scope.onAfterSaveCategory= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetCategoryById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadCategoryDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
  /*
  //Child Table Bindings, need to fix
             $scope.CategoryIdList =  result.DataExtra.CategoryIdList; 
     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

