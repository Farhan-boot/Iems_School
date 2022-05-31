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
    public class ProgramFacade : BaseFacade
    {
        public ProgramFacade(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {
        }

        #region Program

        public Aca_Program GetProgramById(long id)
        {
            var result = DbInstance.Aca_Program.Find(id) ?? Aca_Program.GetNew();
            return result;
        }
        public List<Aca_Program> GetProgramList(string name, int? pageSize, int? pageNo, ref int count)
        {
            IQueryable<Aca_Program> query = (from q in DbInstance.Aca_Program
                                            select q)
                                        //.Include("")
                                        .OrderBy(x => x.ProgramTypeEnumId);

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

        private bool ValidateProgram(Aca_Program program, out string error)
        {
            error = string.Empty;

            if (string.IsNullOrWhiteSpace(program.Name))
            {
                error = "Enter Program Name !";
                return false;
            }

            return true;
        }
        public bool SaveProgram(Aca_Program Program, out string error)
        {
            //check permission
            error = string.Empty;
            if (ValidateProgram(Program, out error))
            {
                using (var scope = DbInstance.Database.BeginTransaction())
                {
                    try
                    {
                        bool isNewObject = true;
                        Aca_Program objToSave = null;
                        if (Program.Id != 0)
                        {
                            objToSave = DbInstance.Aca_Program.SingleOrDefault(x => x.Id == Program.Id);
                            isNewObject = false;
                        }
                        if (objToSave == null)
                        {
                            isNewObject = true;
                            objToSave = Aca_Program.GetNew();
                            DbInstance.Aca_Program.Add(objToSave);
                            objToSave.CreateDate = Program.CreateDate = DateTime.Now;
                            objToSave.CreateById = Program.CreateById = Profile.UserId;
                        }
                        CopyUtil.Copy(Program, objToSave);
                        objToSave.LastChanged = Program.LastChanged = DateTime.Now;
                        objToSave.LastChangedById = Program.LastChangedById = Profile.UserId;
                        DbInstance.SaveChanges();
                        scope.Commit();
                        if (isNewObject)
                        {
                            Program.Id = objToSave.Id;
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
        #endregion Program

    }
}
