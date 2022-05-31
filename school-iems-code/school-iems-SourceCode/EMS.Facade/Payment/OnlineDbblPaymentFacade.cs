using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using EMS.CoreLibrary;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.Framework.Objects;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;
using EMS.OnlinePaymentProxy.DBBL;

namespace EMS.Facade.Payment
{
    public class OnlineDbblPaymentFacade : BaseFacade
    {
        public OnlineDbblPaymentFacade(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {
        }

        public double GetAdmissionFee(EnumCollection.ParentsPrimaryJobStatusEnum parentsPrimaryJob)
        {
            var tuitionFee = 0.00;
            double transprotationDevFee = 1500.00;
            if (parentsPrimaryJob > EnumCollection.ParentsPrimaryJobStatusEnum.NonGovernmentServiceRetired)
            {

                tuitionFee = 79500.00;
            }
            else
            {
                tuitionFee = 94500.00;
            }
            //if (applicant.AdmissionTestRollNumber == 2571
            //    || applicant.AdmissionTestRollNumber == 4781
            //    || applicant.AdmissionTestRollNumber == 2210
            //    || applicant.AdmissionTestRollNumber == 0417
            //    || applicant.AdmissionTestRollNumber == 5009)
            //{
            //    wavierAmount = 50000.00;
            //}
            var totalAmount = tuitionFee + transprotationDevFee;
            return totalAmount;
        }

        private bool IsvalidToPayOnlineForUgAdmission(AdmUG_Applicant applicant, out string error)
        {     error = null;
            //Admitted already
            if (applicant.AdmissionDate != null)
            {
                error = "Sorry! you are already admitted.";
                return false;
            }
            //not selected for admission
            if (applicant.UnitA_SelectionStatus !=
                EnumCollection.AdmissionUG.ApplicantSelectionStatusEnum.SelectedForAdmission &&
                applicant.UnitB_SelectionStatus !=
                EnumCollection.AdmissionUG.ApplicantSelectionStatusEnum.SelectedForAdmission)
            {
                error = "Sorry! you are not selected for admission.";
                return false;
            }
            //pre registration not confirmed
            if (applicant.PreRegistrationStatus != EnumCollection.AdmissionUG.PreRegistrationStatusEnum.Confirmed)
            {
                error = "Sorry! you are not completed your pre-registration.";
                return false;
            }

            return true;
        }
        public bool InitiateUgAdmissionPayment(long id, DbblConstant.CardTypeEnum cardType, out string msg)
        {
            msg = null;
            var applicant = DbInstance.AdmUG_Applicant.SingleOrDefault(x => x.Id == id);
            if (applicant != null && IsvalidToPayOnlineForUgAdmission(applicant, out msg))
            {
                var mainPayment = GetAdmissionFee(applicant.ParentsPrimaryJobStatus);

               return InitiatePaymentRequest(id, mainPayment, cardType, out msg);
            }


            return false;
        }
        private bool IsPermitedToPay(long id, out string error)
        {
            error = null;
            if (id != HttpUtil.Profile.UserId)
            {

                error += "Sorry! You Are Not Permitted To Do This Task";
                return false;
            }
            return true;

        }
        private bool ValidateTransectionRequest()
        {
            string error = "";
            //1. check session for any remain transection
            DbblPaymentProcessor testDbblGateway = HttpUtil.OnlinePaymentProfile;
            if (testDbblGateway != null)
            {
                testDbblGateway.OnlinePaymentStatus = EnumCollection.Accounting.OnlinePaymentStatusEnum.Failed;
                if (!SaveTransection(testDbblGateway, out error))
                {
                    error = "Transection saving Failed!";
                }
                HttpUtil.OnlinePaymentProfile = null;
                return false;
            }
            return true;
        }
        private bool IsValidSuccessRequest()
        {
            string error = "";
            PaymentProfile testDbblGateway = HttpUtil.OnlinePaymentProfile;
            if (testDbblGateway != null)
            {
                var session = HttpContext.Current.Session.SessionID;
                var ip = HttpUtil.GetUserIp();
                if (testDbblGateway.TransactionId.IsStringValid() && testDbblGateway.UserId == HttpUtil.Profile.UserId && testDbblGateway.SessionId.Equals(session) && testDbblGateway.ClientIP.Equals(ip))
                {
                    return true;
                }
            }
            else
            {
                //testDbblGateway.OnlinePaymentStatus = EnumCollection.Accounting.OnlinePaymentStatusEnum.InvalidTry;
                //if (!SaveTransection(testDbblGateway, out error))
                //{
                //    error = "Transection saving Failed!";
                //}
                HttpUtil.OnlinePaymentProfile = null;
                return false;
            }
            return true;
        }
        public bool InitiatePaymentRequest(long id, double mainAmount, DbblConstant.CardTypeEnum cardType, out string msg)
        {
            //check permission
            if (!IsPermitedToPay(id, out msg))
            {
                return false;
            }
            //1. remove session for any remain transection
            ValidateTransectionRequest();

            PaymentProfile profile = new PaymentProfile();
            profile.UserId = HttpUtil.Profile.UserId;
            profile.CardType = cardType;
            profile.ClientIP = HttpUtil.GetUserIp();
            profile.SessionId = HttpContext.Current.Session.SessionID;

            profile.CertificateDiskPath = DbblConstant.TestCertificateDiskPath;
            profile.RedirectUrlPrefix = DbblConstant.TestRedirectUrlPrefix;

            profile.MainAmount = mainAmount;
            profile.OnlinePaymentFee = DbblConstant.GetFeeByCardType(profile.MainAmount, profile.CardType);
            profile.Description = profile.Description;
            profile.OnlinePaymentFee = profile.OnlinePaymentFee;

            //string jarPath = DbblConstant.LiveCertificateDiskPath2;
            //string redirectUrlPrefix = DbblConstant.LiveRedirectUrlPrefix2;
            DbblPaymentProcessor dbblGateway = new DbblPaymentProcessor(profile);
            if (dbblGateway.InitiateTransaction())
            {
                if (SaveTransection(dbblGateway, out msg))
                {
                    HttpUtil.OnlinePaymentProfile = dbblGateway;
                    //Response.Redirect(testDbblGateway.GetRedirectUrl());
                    msg = dbblGateway.RedirectUrl;
                    return true;
                }
            }
            //remove session on fail
            HttpUtil.OnlinePaymentProfile = null;
            msg = dbblGateway.ExecutionResult + ". " + dbblGateway.Error;
            return false;
        }

        public bool ProcessSuccessRequest(long id, out string msg)
        {
            msg = string.Empty;

            if (!IsPermitedToPay(id, out msg))
            {
                return false;
            }
            IsValidSuccessRequest();

            if (IsValidSuccessRequest())
            {
                PaymentProfile profile = HttpUtil.OnlinePaymentProfile;
                DbblPaymentProcessor dbblGateway = new DbblPaymentProcessor(profile);
                dbblGateway.ValidateSuccessRequest();

                if (SaveTransection(dbblGateway, out msg))
                {
                    // clear the transection
                }
                msg = msg + " " + dbblGateway.ExecutionResult + ". " + dbblGateway.Error;
                if (dbblGateway.IsSuccess)
                {
                    HttpUtil.OnlinePaymentProfile = null;
                }

                return dbblGateway.IsSuccess;
            }
            return false;
        }

        public void DbblPaymentTestTransection()
        {

        }

        public void GetDbblPaymentSettingsByCardType()
        {

        }
        public void GetDbblGatewaySettings()
        {

        }

        private bool SaveTransection(DbblPaymentProcessor paymentProfile, out string error)
        {

            error = string.Empty;
            //if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionModule.ExamCenterBuilding.CanUpdate, out error))
            //{
            //    return false;
            //}
            bool success = true;
            using (var scope = DbInstance.Database.BeginTransaction())
            {
                try
                {

                    var objToSave = DbInstance.OnlinePaymentAudits.SingleOrDefault(x => x.TransectionId.Equals(paymentProfile.TransactionId) && x.UserId == paymentProfile.UserId && x.OnlinePaymentStatus == EnumCollection.Accounting.OnlinePaymentStatusEnum.Initiated);
                    if (objToSave == null)
                    {
                        objToSave = new OnlinePaymentAudit();
                        objToSave.Id = BigInt.NewBigInt();
                        objToSave.UserId = paymentProfile.UserId;

                        objToSave.CardTypeId = (int)paymentProfile.CardType;
                        objToSave.MainAmount = paymentProfile.MainAmount;
                        objToSave.OnlinePaymentFee = paymentProfile.OnlinePaymentFee;
                        objToSave.TotalAmount = paymentProfile.TotalAmount;
                        objToSave.TransectionId = paymentProfile.TransactionId;
                        objToSave.ClientIP = paymentProfile.ClientIP;
                        objToSave.SessionID = paymentProfile.SessionId;
                        objToSave.GatewayId = 1;//DBBL
                        objToSave.InvoiceId = 1;
                        objToSave.CreateDate = DateTime.Now;
                    }
                    else
                    {
                        objToSave.CompletedTime = DateTime.Now;
                    }
                    objToSave.OnlinePaymentStatus = paymentProfile.OnlinePaymentStatus;

                    if (objToSave.OnlinePaymentStatus == EnumCollection.Accounting.OnlinePaymentStatusEnum.Success)
                    {
                        objToSave.SucessResult = paymentProfile.ExecutionResult;

                    }
                    else
                    {
                        objToSave.ErrorResult = paymentProfile.ExecutionResult + ". " + paymentProfile.Error;

                    }

                    DbInstance.OnlinePaymentAudits.Add(objToSave);

                    objToSave.LastChanged = DateTime.Now;
                    objToSave.LastChangedBy = HttpUtil.Profile.UserId;

                    DbInstance.SaveChanges();

                    scope.Commit();
                    return true;

                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    scope.Rollback();
                    return false;
                }

            }
        }

    }
}
