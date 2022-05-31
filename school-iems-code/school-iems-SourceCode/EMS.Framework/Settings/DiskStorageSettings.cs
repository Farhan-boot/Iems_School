using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EMS.Framework.Settings
{
    public class DiskStorageSettings
    {
        //<add key = "StudentPhotoPath" value="E:\\StudentPhoto\\"/>
        //<!--<add key = "TeacherPhotoPath" value="E:\\TeacherPhoto\\"/>-->
        //<add key = "TeacherPhotoPath" value="C:\DMS\"/>
        //<add key = "OtherUsersPhotoPath" value="E:\\OtherUserPhoto\\"/>
        //<add key = "AdmissionResultUploadPath" value="E:\AdmissionResult\"/>
        //<add key = "UserScanDocumentPath" value="C:\DMS\"/>
        //<add key = "WorkProcessDocumentsPath" value="E:\WorkProcessDocuments\"/>
        //<!--<add key = "WorkProcessDocumentsPath" value="C:\DMS\"/>-->
        //<!--<add key = "VehiclePhotoPath" value="C:\VehiclePhotos\"/>-->
        //<add key = "VehicleScanDocumentPath" value="C:\VehicleScanDocuments\"/>
        //<add key = "BookImagePath" value="C:\BookImages\"/>
        //<add key = "OcrDataPath" value="E:\OcrProcessor\tessdata"/> 

        public static string RootStoragePath = ConfigurationManager.AppSettings["RootStoragePath"];
        public static string RootStoragePathForProfilePictures = ConfigurationManager.AppSettings["RootStoragePathForProfilePictures"];

        public static string TmpFilePath = RootStoragePath+ ConfigurationManager.AppSettings["TmpFilePath"];

        //public static string AdmissionOtherFilePath = RootStoragePath + ConfigurationManager.AppSettings["AdmissionOtherFilePath"];
        //public static string AdmissionBscPhotoPath = RootStoragePath + ConfigurationManager.AppSettings["AdmissionBscPhotoPath"];
        //public static string AdmissionBscOtherFilePath = RootStoragePath + ConfigurationManager.AppSettings["AdmissionBscOtherFilePath"];
        //public static string AdmissionMsPhotoPath = RootStoragePath + ConfigurationManager.AppSettings["AdmissionMsPhotoPath"];
        //public static string AdmissionMsOtherFilePath = RootStoragePath + ConfigurationManager.AppSettings["AdmissionMsOtherFilePath"];
        //public static string AdmissionPhdPhotoPath = RootStoragePath + ConfigurationManager.AppSettings["AdmissionPhdPhotoPath"];
        //public static string AdmissionPhdOtherFilePath = RootStoragePath + ConfigurationManager.AppSettings["AdmissionPhdOtherFilePath"];

        //public static string EmsUserPhotoPath = RootStoragePath + ConfigurationManager.AppSettings["EmsUserPhotoPath"];
        //public static string EmsUserUploadPath = RootStoragePath + ConfigurationManager.AppSettings["EmsUserUploadPath"];
        //public static string EmsScanFilePath = RootStoragePath + ConfigurationManager.AppSettings["EmsScanFilePath"];

        //public static string EmsClassMaterialPath = RootStoragePath + "ClassMaterials\\";//ConfigurationManager.AppSettings["EmsClassNotePath"];
        //public static string EmsAssignmentUploadPath = RootStoragePath + ConfigurationManager.AppSettings["EmsAssignmentUploadPath"];

        //public static string LibraryDigitalBookPath = RootStoragePath + ConfigurationManager.AppSettings["LibraryDigitalBookPath"];
        //public static string LibraryBookImagePath = RootStoragePath + ConfigurationManager.AppSettings["LibraryBookImagePath"];
        //public static string LibraryOtherFilePath = RootStoragePath + ConfigurationManager.AppSettings["LibraryOtherFilePath"];

        public static class ItemFolderNames
        {
            public const string ClassMaterialFolderName = "ClassMaterials"; 
            public const string BankOffLinePaymentCollection = "BankOffLinePaymentCollection";
            public const string ClassMarkExcelUpload = "ClassMarkExcelUpload";
            public const string StudentProfilePictureFolderName = "Student/ProfilePictures";//ProgramId(databasePrimaryKey)/AdmissionSemesterId/id.jpg
            public const string EmployeeProfilePictureFolderName = "Employee/ProfilePictures";//DepatId/id.jp
            //ConfigurationManager.AppSettings["EmsClassNotePath"];
            //public static string AssignmentUploadFolderName = RootStoragePath + ConfigurationManager.AppSettings["EmsAssignmentUploadPath"];
        }

    }
}
