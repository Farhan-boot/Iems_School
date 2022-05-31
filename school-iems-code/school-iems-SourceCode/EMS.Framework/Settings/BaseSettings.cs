using System;
using System.Collections.Generic;
using System.Linq;

using EMS.Framework.Utils;

namespace EMS.Framework.Settings
{
    public abstract class BaseSettings
    {
        Dictionary<int, string> _settings;

        //protected BaseSettings()
        //{
        //    var context = HttpUtil.DbContext;
        //    if (!context.Database.Exists())
        //    {
        //        return;
        //    }

        //    var db = HttpUtil.DbContext;
        //    _settings = new Dictionary<int, string>();
        //    var settings = db.Settings.ToList();
        //    foreach (var s in settings)
        //        _settings.Add(s.SettingNo, s.Value);
        //}

        //protected void SetSetting(int settingNo, object value)
        //{
        //    var db = HttpUtil.DbContext;
        //    if (!db.Database.Exists())
        //        return;

        //    var setting = db.Settings.SingleOrDefault(bs => bs.SettingNo == settingNo);

        //    if (setting == null)
        //    {
        //        setting = new Setting()
        //                  {
        //                      LastChanged = DateTime.Now,
        //                      LastChangedById = HttpUtil.Profile == null ? 1 : HttpUtil.Profile.UserID
        //                  };

        //        db.Settings.Add(setting);
        //    }

        //    setting.SettingNo = settingNo;
        //    setting.Value = Convert.ToString(value);

        //    if (_settings.ContainsKey(settingNo))
        //        _settings[settingNo] = Convert.ToString(value);
        //    else
        //        _settings.Add(settingNo, Convert.ToString(value));

        //    db.SaveChanges();
        //}

        //protected object GetSetting(int settingNo)
        //{
        //    var db = HttpUtil.DbContext;
        //    if (!db.Database.Exists())
        //        return 0;

        //    var settingDict = _settings.ContainsKey(settingNo) ? _settings[settingNo] : null;
        //    if (settingDict != null) 
        //        return settingDict;

        //    var settingDb = db.Settings.SingleOrDefault(bs => bs.SettingNo == settingNo);
        //    if (settingDb == null) 
        //        return null;

        //    _settings.Add(settingNo, settingDb.Value); 
        //    return settingDb.Value;
        //}
    }
}
