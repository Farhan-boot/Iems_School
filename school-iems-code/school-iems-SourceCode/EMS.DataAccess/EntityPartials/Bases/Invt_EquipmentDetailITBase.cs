//File: Entity Partial Base
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;

namespace EMS.DataAccess.Data
{

    /// <summary>
    /// Do not modify this class, edit partial instead.
    /// </summary>   
    public partial class Invt_EquipmentDetailIT
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_Model = "Model";		            
		public const string Property_Processor = "Processor";		            
		public const string Property_HDD = "HDD";		            
		public const string Property_Ram = "Ram";		            
		public const string Property_Motherboard = "Motherboard";		            
		public const string Property_SoundCard = "SoundCard";		            
		public const string Property_GraphicsCard = "GraphicsCard";		            
		public const string Property_LANCard = "LANCard";		            
		public const string Property_Casing = "Casing";		            
		public const string Property_PowerSupply = "PowerSupply";		            
		public const string Property_FloppyDrive = "FloppyDrive";		            
		public const string Property_OpticalDrive = "OpticalDrive";		            
		public const string Property_Other1Title = "Other1Title";		            
		public const string Property_Other1Desc = "Other1Desc";		            
		public const string Property_Other2Title = "Other2Title";		            
		public const string Property_Other2Desc = "Other2Desc";		            
		public const string Property_CreateDate = "CreateDate";		            
		public const string Property_CreateById = "CreateById";		            
		public const string Property_LastChanged = "LastChanged";		            
		public const string Property_LastChangedById = "LastChangedById";		            
		public const string Property_IsDeleted = "IsDeleted";		            
              
        /// <summary>
        /// This is for detect selected columns in List.
        /// This is not database property.
        /// </summary>
        public const string Property_IsSelected = "IsSelected";
        /// <summary>
        /// This is detect already added item in desired table
        /// This is not database property.
        /// </summary>
        public const string Property_IsAlreadyAdded = "IsAlreadyAdded";
	  #endregion
      
        /// <summary>
        /// This is for detect selected columns in List.
        /// This is not database property.
        /// </summary>
        public bool IsSelected { get; set; }
        /// <summary>
        /// This is detect already added item in desired table
        /// This is not database property.
        /// </summary>
        public bool IsAlreadyAdded = true;
      
     
     
      
        public static Invt_EquipmentDetailIT GetNew(Int32 id=0)
        {
           Invt_EquipmentDetailIT obj = new Invt_EquipmentDetailIT();
               obj.Id = id;
                obj.Model = string.Empty ; 
                obj.Processor = null ; 
                obj.HDD = null ; 
                obj.Ram = null ; 
                obj.Motherboard = null ; 
                obj.SoundCard = null ; 
                obj.GraphicsCard = null ; 
                obj.LANCard = null ; 
                obj.Casing = null ; 
                obj.PowerSupply = null ; 
                obj.FloppyDrive = null ; 
                obj.OpticalDrive = null ; 
                obj.Other1Title = null ; 
                obj.Other1Desc = null ; 
                obj.Other2Title = null ; 
                obj.Other2Desc = null ; 
                obj.CreateDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.CreateById = 1 ;
                obj.LastChanged = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.LastChangedById = 1 ;
                obj.IsDeleted = false ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}
