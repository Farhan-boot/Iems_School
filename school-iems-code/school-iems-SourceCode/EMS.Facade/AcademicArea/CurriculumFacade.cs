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
    public class CurriculumFacade : BaseFacade
    {
        public CurriculumFacade(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {
        }

        #region Curriculum
        public List<Aca_Curriculum> GetCurriculumList(string name, long? programId, int? pageSize, int? pageNo, ref int count)
        {
            IQueryable<Aca_Curriculum> query = (from q in DbInstance.Aca_Curriculum
                                            select q)
                                        //.Include("")
                                        .OrderBy(x=>x.CurriculumNo)
                                        .ThenBy(x=>x.Aca_Program.ProgramTypeEnumId)
                                        .ThenByDescending(x=>x.Aca_Program.Id)
                                        .ThenByDescending(x => x.Session);
            if (programId!=null && programId>0)
            {
                query = from q in query
                        where q.ProgramId==programId
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

        private bool ValidateCurriculum(Aca_Curriculum Curriculum, out string error)
        {
            error = string.Empty;

            if (string.IsNullOrWhiteSpace(Curriculum.Name))
            {
                error = "Enter Curriculum Name !";
                return false;
            }
            return true;
        }
        public Aca_Curriculum GetCurriculumById(long id)
        {
            var result = DbInstance.Aca_Curriculum.Find(id) ?? Aca_Curriculum.GetNew();
            return result;
        }
        public bool SaveCurriculum(Aca_Curriculum Curriculum, out string error)
        {
            //check permission
            error = string.Empty;
            if (ValidateCurriculum(Curriculum, out error))
            {
                using (var scope = DbInstance.Database.BeginTransaction())
                {
                    try
                    {
                        bool isNewObject = true;
                        Aca_Curriculum objToSave = null;
                        if (Curriculum.Id != 0)
                        {
                            objToSave = DbInstance.Aca_Curriculum.SingleOrDefault(x => x.Id == Curriculum.Id);
                            isNewObject = false;
                        }
                        if (objToSave == null)
                        {
                            isNewObject = true;
                            objToSave = Aca_Curriculum.GetNew();
                            DbInstance.Aca_Curriculum.Add(objToSave);
                            objToSave.CreateDate = Curriculum.CreateDate = DateTime.Now;
                            objToSave.CreateById = Curriculum.CreateById = Profile.UserId;
                        }
                        CopyUtil.Copy(Curriculum, objToSave);
                        objToSave.LastChanged = Curriculum.LastChanged = DateTime.Now;
                        objToSave.LastChangedById = Curriculum.LastChangedById = Profile.UserId;
                        DbInstance.SaveChanges();
                        scope.Commit();
                        if (isNewObject)
                        {
                            Curriculum.Id = objToSave.Id;
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
        #endregion Curriculum

    }
}
