using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.DataAccess.Data;
using EMS.DataMigration.Win.Data;
using EMS.Facade;
using EMS.Framework.Objects;
using EMS.Framework.Utils;

namespace EMS.DataMigration.Win.Utils
{
    public abstract class BaseMigrator:IDisposable
    {
        //int taskDone, int totalTask,
        public TheFacade Facade { get { return new TheFacade(HttpUtil.DbContext, HttpUtil.GetSuperUserProfile()); } }
        public string MigratorName { get; set; }
        public  delegate void LogReport(int pecentageDone, string message, bool isError=false);
        public static event LogReport OnLogReport;
        public delegate void TaskCompleted(string message, bool isError = false);
        public static event TaskCompleted OnTaskCompleted;
       
        public EmsDbContext DbInstance { get; set; }
        public UserProfile Profile { get; }

        public readonly BackgroundWorker bgWorker;
        public ConcurrentStack<string> _errors { get; set; }
        public BaseMigrator(string migratorName)
        {
            MigratorName = migratorName;
            _errors = new ConcurrentStack<string>();
            this.bgWorker = new BackgroundWorker();
            InitBgWorker();


            DbInstance = new EmsDbContext();
            DbInstance.Configuration.LazyLoadingEnabled = false;
            DbInstance.Configuration.ProxyCreationEnabled = false;
            //DbInstance.Configuration.AutoDetectChangesEnabled = false;
            //DbInstance.Configuration.ValidateOnSaveEnabled = false;
            Profile = HttpUtil.GetSuperUserProfile();


            //MainForm.OnCancelTask += CancelWork;
        }


        void InitBgWorker()
        {

            this.bgWorker.WorkerReportsProgress = true;
            this.bgWorker.WorkerSupportsCancellation = true;
            this.bgWorker.DoWork += BgWorker_DoWork; ;
            this.bgWorker.ProgressChanged += WorkProgressChanged; ;
            this.bgWorker.RunWorkerCompleted += WorkCompleted; ;
        }


        protected abstract void BgWorker_DoWork(object sender, DoWorkEventArgs e);

        protected virtual void WorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_errors.Any())
            {
                Log(100, $"================={MigratorName} Error Log=======================");
                foreach (var err in _errors)
                {

                    Log(100, err);
                }
                _errors.Clear();
            }
            
           
            if (OnTaskCompleted != null)
            {
                if (e.Cancelled)
                {
                    OnTaskCompleted($"{MigratorName} Migration Canceled.");
                    return;
                    
                }
                OnTaskCompleted($"{MigratorName} Migration Compleate.");
            }
        }

        protected virtual void WorkProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
        }

        public virtual void DoWork(int taskId=1)
        {
            bgWorker.RunWorkerAsync(taskId);
        }
        public void CancelWork()
        {
            //if (!bgWorker.IsBusy)
                bgWorker.CancelAsync();
        }


        protected void Log(int pecentage, string msg)
        {
            //bgWorker.ReportProgress(pecentage, msg);
            if (OnLogReport != null)
            {
                OnLogReport(pecentage, msg);
            }
            Debug.WriteLine(msg);
        }
        protected void LogError(int pecentage, string msg)
        {
            //bgWorker.ReportProgress(pecentage, msg);
            _errors.Push(msg);
            if (OnLogReport != null)
            {
                OnLogReport(pecentage, msg,true);
            }
            Debug.WriteLine(msg);
        }


        public void Dispose()
        {
            bgWorker.CancelAsync();
            DbInstance.Dispose();
           
            bgWorker.Dispose();
            _errors.Clear();
        }
    }


}
