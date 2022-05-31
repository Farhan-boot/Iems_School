using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.Communication.SMSProxy;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.CustomEntity;
using EMS.DataAccess.Data;
using EMS.Framework.Objects;
using EMS.Framework.Utils;

namespace EMS.Facade.CommunicationLog

{
    public class SmsFacade : BaseFacade
    {


        public SmsFacade(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {

        }
        public bool SendSmsAndSaveLogForAbsentClassAttendance(List<Aca_ClassAttendanceSmsLog> smsLogsToSent, bool send1SmsForEachClassAbsent, out string error)
        {
            error = null;
            //need to check is current semester
            //need to send  sms once for each class
            //need to get mobile number
            if (smsLogsToSent == null || smsLogsToSent.Count <= 0)
            {
                return false;
            }
            try
            {

                // using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
                {

                    foreach (var smsLog in smsLogsToSent)
                    {
                        //if not current semester
                        var alreadyLogged =
                            DbInstance.Aca_ClassAttendanceSmsLog.FirstOrDefault(
                                x => x.AttendanceSettingId == smsLog.AttendanceSettingId
                                && x.StudentId == smsLog.StudentId
                                );

                        //todo why
                        if (alreadyLogged != null
                               && alreadyLogged.DeliveryStatusEnumId == (byte)DeliveryStatusEnum.SentSuccessfully
                              )
                        {
                            continue;
                        }

                        var student = DbInstance.User_Student.SingleOrDefault(x => x.Id == smsLog.StudentId);
                        if (student == null)
                        {
                            continue;
                        }
                        var mobile = student.FatherMobile.IsValid() ? student.FatherMobile : student.MotherMobile;

                        //if no mobile
                        if (!mobile.IsValid())
                        {
                            //if(alreadyLogged == null)
                            {   //insert
                                smsLog.DeliveryStatus = DeliveryStatusEnum.NotSent;
                                smsLog.MobileNumber = "[Not Available]";
                                smsLog.DeliveryErrorText = "SMS not sent, Parents Mobile Number Not Available!";
                                smsLog.SmsText = "SMS not sent, Parents Mobile Number Not Available! Text:(" + smsLog.SmsText + ")";
                                //if no mobile number and previously loged 
                                DbInstance.Aca_ClassAttendanceSmsLog.Add(smsLog);
                                DbInstance.SaveChanges(); //
                                                          //scope.Commit();
                            }
                            //else
                            //{   //update
                            //    //alreadyLogged.CreateDate = DateTime.Now;
                            //    //alreadyLogged.CreateById = HttpUtil.Profile.UserId;
                            //    alreadyLogged.DeliveryStatus = Aca_ClassAttendanceSmsLog.DeliveryStatusEnum.NotSent;
                            //    alreadyLogged.DeliveryErrorText = "SMS not sent, Parents Mobile Number Not Available!";
                            //    alreadyLogged.MobileNumber = mobile;
                            //    //alreadyLogged.SmsText = string.Format(AbsentAttenceSmsTemplate, classObj.Code);
                            //    DbInstance.SaveChanges(); //
                            //                              // scope.Commit();
                            //}

                        }
                        else
                        {    //sent sms
                            // SiteSettings.Instance.SentSmsToParentOnAbsentAttendance
                            var smsSender = new SmsSender();
                            var result = smsSender.SendSmsByBanglaPhoneApi(mobile + ",01769023800", smsLog.SmsText, out error);//added comandent and salahuddin sir
                            //if previously logged and sms not sent, update existing
                            //if (alreadyLogged != null)
                            //{
                            //    //update
                            //    //alreadyLogged.CreateDate = DateTime.Now;
                            //    //alreadyLogged.CreateById = HttpUtil.Profile.UserId;
                            //    alreadyLogged.DeliveryStatusEnumId = (byte)result.Status;
                            //    alreadyLogged.DeliveryErrorText = result.ErrorText;
                            //    alreadyLogged.MobileNumber = mobile;
                            //    //alreadyLogged.SmsText = string.Format(AbsentAttenceSmsTemplate, classObj.Code);
                            //    DbInstance.SaveChanges(); //
                            //                              //scope.Commit();
                            //}
                            //else
                            {
                                //insert
                                smsLog.DeliveryStatusEnumId = (byte)(result.IsSentSuccess ? 1 : 0);

                                smsLog.DeliveryErrorText = result.IsSentSuccess ? "" : result.ErrorText;
                                smsLog.MobileNumber = mobile;
                                //smsLog.SmsText = string.Format(AbsentAttenceSmsTemplate, classObj.Code);
                                //todo why
                                if (alreadyLogged == null)
                                {
                                    DbInstance.Aca_ClassAttendanceSmsLog.Add(smsLog);
                                }

                                DbInstance.SaveChanges(); //
                                //scope.Commit();

                            }

                            error.AddString(result.ErrorText);

                        }
                    }
                }
            }
            catch (Exception ex)
            {

                return false;
            }

            return true;
        }
    }
}
