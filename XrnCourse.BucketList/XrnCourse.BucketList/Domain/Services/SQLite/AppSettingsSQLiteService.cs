using System;
using System.Diagnostics;
using System.Threading.Tasks;
using XrnCourse.BucketList.Domain.Models;
using XrnCourse.BucketList.Domain.Services.Abstract;

namespace XrnCourse.BucketList.Domain.Services.SQLite
{
    public class AppSettingsSQLiteService : SQLiteServiceBase, IAppSettingsService
    {
        public async Task<AppSettings> GetSettings()
        {
            return await Task.Run<AppSettings>(async () =>
            {
                try
                {
                    int numSettings = connection.Table<AppSettings>().Count();
                    if (numSettings == 0)
                    {
                        await SaveSettings(new AppSettings
                        {
                            CurrentUserId = Guid.Empty,
                            EnableListSharing = true,
                            EnableNotifications = false
                        });
                    }
                    return connection.Table<AppSettings>().FirstOrDefault();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });

        }

        public async Task SaveSettings(AppSettings settings)
        {
            await Task.Run(() =>
            {
                try
                {
                    connection.InsertOrReplace(settings);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }
    }
}
