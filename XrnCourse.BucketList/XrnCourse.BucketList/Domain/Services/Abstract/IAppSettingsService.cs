using System.Threading.Tasks;
using XrnCourse.BucketList.Domain.Models;

namespace XrnCourse.BucketList.Domain.Services.Abstract
{
    public interface IAppSettingsService
    {
        Task<AppSettings> GetSettings();
        Task SaveSettings(AppSettings settings);
    }
}