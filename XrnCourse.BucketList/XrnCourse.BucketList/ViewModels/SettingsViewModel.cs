using FreshMvvm;
using System.Windows.Input;
using Xamarin.Forms;
using XrnCourse.BucketList.Domain.Services;

namespace XrnCourse.BucketList.ViewModels
{
    public class SettingsViewModel : FreshBasePageModel
    {
        INavigation navigation;
        AppSettingsInMemoryService settingsService;
        UsersInMemoryService usersService;

        public SettingsViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            settingsService = new AppSettingsInMemoryService();
            usersService = new UsersInMemoryService();
        }

        #region Properties

        private string username;
        public string UserName
        {
            get { return username; }
            set { username = value;
                RaisePropertyChanged(nameof(UserName));
            }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                RaisePropertyChanged(nameof(Email));
            }
        }

        private bool enableListSharing;
        public bool EnableListSharing
        {
            get { return enableListSharing; }
            set
            {
                enableListSharing = value;
                RaisePropertyChanged(nameof(EnableListSharing));
            }
        }

        private bool enableNotifications;
        public bool EnableNotifications
        {
            get { return enableNotifications; }
            set
            {
                enableNotifications = value;
                RaisePropertyChanged(nameof(EnableNotifications));
            }
        }

        #endregion

        /// <summary>
        /// Callled whenever the page is navigated to.
        /// </summary>
        /// <param name="initData"></param>
        public async override void Init(object initData)
        {
            //get settings and intialize controls
            var settings = await settingsService.GetSettings();
            EnableListSharing = settings.EnableListSharing;
            EnableNotifications = settings.EnableNotifications;

            //get current User and intialize controls
            var currentUser = await usersService.GetUserById(settings.CurrentUserId);
            UserName = currentUser.UserName;
            Email = currentUser.Email;

            base.Init(initData);
        }

        public ICommand SaveSettingsCommand => new Command(
            async () => {
                //save app settings
                var currentSettings = await settingsService.GetSettings();
                currentSettings.EnableListSharing = EnableListSharing;
                currentSettings.EnableNotifications = EnableNotifications;
                await settingsService.SaveSettings(currentSettings);

                //save user info settings
                var user = await usersService.GetUserById(currentSettings.CurrentUserId);
                user.UserName = UserName?.Trim();
                user.Email = Email?.Trim();
                await usersService.SaveUser(user);

                //close settings page
                await this.navigation.PopAsync();
            }
        );
    }
}
