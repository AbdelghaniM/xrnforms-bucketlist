using System;
using System.Threading.Tasks;
using XrnCourse.BucketList.Domain.Models;

namespace XrnCourse.BucketList.Domain.Services.Mock
{
    public class AppSettingsInMemoryService
    {
        private static AppSettings currentSettings = new AppSettings
        {
            CurrentUserId = Guid.Empty, //refers to (first) local user
            EnableListSharing = true,
            EnableNotifications = false
        };

        public async Task<AppSettings> GetSettings()
        {
            await Task.Delay(Constants.Mocking.FakeDelay);
            return currentSettings;
        }

        public async Task SaveSettings(AppSettings settings)
        {
            await Task.Delay(Constants.Mocking.FakeDelay);
            currentSettings = settings;
        }
    }
}
