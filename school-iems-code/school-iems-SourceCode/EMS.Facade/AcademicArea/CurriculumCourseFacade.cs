using System;
using System.Collections.Generic;
using System.Linq;
using EMS.DataAccess.Data;
using EMS.Framework.Objects;
using EMS.Framework.Utils;

namespace EMS.Facade.AcademicArea
{
    public class CurriculumCourseFacade : BaseFacade
    {
        //TODO Delete. Not used.
        public CurriculumCourseFacade(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {
        }

        #region CurriculumCourse
        public List<Aca_CurriculumCourse> GetCurriculumCourseList(string name, long? curriculumId, int? pageSize, int? pageNo, ref int count)
        {
            IQueryable<Aca_CurriculumCourse> query = (from q in DbInstance.Aca_CurriculumCourse
                        .Include("Aca_StudyLevelTerm")
                    select q)
                .OrderBy(x => x.Aca_Curriculum.CurriculumNo)
                .ThenBy(x => x.Aca_Curriculum.Aca_Program.ProgramTypeEnumId)
                .ThenByDescending(x => x.Aca_Curriculum.Aca_Program.Id)
                .ThenByDescending(x => x.CurriculumId)
                .ThenBy(x => x.Code);
            if (curriculumId != null && curriculumId > 0)
            {
                query = from q in query
                        where q.CurriculumId == curriculumId
                        select q;
            }
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrWhiteSpace(name))
            {
                query = from q in query
                        where q.Name.Contains(name)
                        select q;
            }
            count = query.Count();
            if (pageNo.HasValue && pageSize.HasValue && pageNo.Value >= 0 && pageSize.Value >= 0)
            {
                if (pageNo <= 1)
                {
                    pageNo = 0;
                }
                else
                {
                    pageNo = pageNo - 1;
                }

                query = (from q in query
                         select q)
                        .Skip(pageNo.Value * pageSize.Value)
                    .Take(pageSize.Value);
            }
            return query.ToList();
        }
        public Aca_CurriculumCourse GetCurriculumCourseById(long id)
        {
            var result = DbInstance.Aca_CurriculumCourse.Find(id) ?? Aca_CurriculumCourse.GetNew();
            return result;
        }
        private bool ValidateCurriculumCourse(Aca_CurriculumCourse CurriculumCourse, out string error)
        {
            error = string.Empty;

            if (string.IsNullOrWhiteSpace(CurriculumCourse.Name))
            {
                error = "Enter CurriculumCourse Name !";
                return false;
            }
            return true;
        }
        public bool SaveCurriculumCourse(Aca_CurriculumCourse CurriculumCourse, out string error)
        {
            //check permission
            error = string.Empty;
            if (ValidateCurriculumCourse(CurriculumCourse, out error))
            {
                using (var scope = DbInstance.Database.BeginTransaction())
                {
                    try
                    {
                        bool isNewObject = true;
                        Aca_CurriculumCourse objToSave = null;
                        if (CurriculumCourse.Id != 0)
                        {
                            objToSave = DbInstance.Aca_CurriculumCourse.SingleOrDefault(x => x.Id == CurriculumCourse.Id);
                            isNewObject = false;
                        }
                        if (objToSave == null)
                        {
                            isNewObject = true;
                            objToSave = Aca_CurriculumCourse.GetNew(BigInt.NewBigInt());
                            DbInstance.Aca_CurriculumCourse.Add(objToSave);
                            objToSave.CreateDate = CurriculumCourse.CreateDate = DateTime.Now;
                            objToSave.CreateById = CurriculumCourse.CreateById = Profile.UserId;
                        }
                        CopyUtil.Copy(CurriculumCourse, objToSave);
                        objToSave.LastChanged = CurriculumCourse.LastChanged = DateTime.Now;
                        objToSave.LastChangedById = CurriculumCourse.LastChangedById = Profile.UserId;
                        DbInstance.SaveChanges();
                        scope.Commit();
                        if (isNewObject)
                        {
                            CurriculumCourse.Id = objToSave.Id;
                        }
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

            return false;
        }
        #endregion CurriculumCourse

    }
}
