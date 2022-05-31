using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.DataAccess.Data;
using EMS.Facade.Library;
using EMS.Framework.Objects;
using EMS.Framework.Utils;

namespace EMS.Facade.Academic
{
    public class OfferedCourseFacade : BaseFacade
    {
        public OfferedCourseFacade(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {
        }

        #region OfferedCourse
        //public List<Aca_OfferedCourse> GetOfferedCourseList(string name, string code, int? pageSize, int? pageNo, ref int count)
        //{
        //    IQueryable<Aca_OfferedCourse> query = (from q in DbInstance.Aca_OfferedCourse
        //                                    select q)
        //                                //.Include("")
        //                                .OrderBy(x => x.CurriculumCourseId);
        //    if (!string.IsNullOrEmpty(name) && !string.IsNullOrWhiteSpace(name))
        //    {
        //        query = from q in query
        //                //where q.Name.Contains(name)
        //                select q;
        //    }
        //    if (!string.IsNullOrEmpty(code) && !string.IsNullOrWhiteSpace(code))
        //    {
        //        query = from q in query
        //                //where q.Code.Contains(code)
        //                select q;
        //    }
        //    count = query.Count();
        //    if (pageNo.HasValue && pageSize.HasValue && pageNo.Value >= 0 && pageSize.Value >= 0)
        //    {
        //        if (pageNo <= 1)
        //        {
        //            pageNo = 0;
        //        }
        //        else
        //        {
        //            pageNo = pageNo - 1;
        //        }

        //        query = (from q in query
        //                 select q)
        //                .Skip(pageNo.Value * pageSize.Value)
        //            .Take(pageSize.Value);
        //    }
        //    return query.ToList();
        //}
        //private bool ValidateOfferedCourse(Aca_OfferedCourse OfferedCourse, out string error)
        //{
        //    error = string.Empty;

        //    //if (string.IsNullOrWhiteSpace(OfferedCourse.Name))
        //    //{
        //    //    error = "Enter OfferedCourse Name !";
        //    //    return false;
        //    //}
        //    //if (string.IsNullOrWhiteSpace(OfferedCourse.Code))
        //    //{
        //    //    error = "Enter OfferedCourse Code !";
        //    //    return false;
        //    //}
        //    return true;
        //}
        //public bool SaveOfferedCourse(Aca_OfferedCourse OfferedCourse, out string error)
        //{
        //    //check permission
        //    error = string.Empty;
        //    if (ValidateOfferedCourse(OfferedCourse, out error))
        //    {
        //        using (var scope = DbInstance.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                bool isNewObject = true;
        //                Aca_OfferedCourse objToSave = null;
        //                if (OfferedCourse.Id != 0)
        //                {
        //                    objToSave = DbInstance.Aca_OfferedCourse.SingleOrDefault(x => x.Id == OfferedCourse.Id);
        //                    isNewObject = false;
        //                }
        //                if (objToSave == null)
        //                {
        //                    isNewObject = true;
        //                    objToSave = Aca_OfferedCourse.GetNew();
        //                    DbInstance.Aca_OfferedCourse.Add(objToSave);
        //                    objToSave.CreateDate = OfferedCourse.CreateDate = DateTime.Now;
        //                    objToSave.CreateById = OfferedCourse.CreateById = Profile.UserId;
        //                }
        //                CopyUtil.Copy(OfferedCourse, objToSave);
        //                objToSave.LastChanged = OfferedCourse.LastChanged = DateTime.Now;
        //                objToSave.LastChangedById = OfferedCourse.LastChangedById = Profile.UserId;
        //                DbInstance.SaveChanges();
        //                scope.Commit();
        //                if (isNewObject)
        //                {
        //                    OfferedCourse.Id = objToSave.Id;
        //                }
        //                return true;
        //            }
        //            catch (Exception ex)
        //            {
        //                error = ex.Message;
        //                scope.Rollback();
        //                return false;
        //            }
        //        }

        //    }

        //    return false;
        //}
        #endregion OfferedCourse

    }
}
