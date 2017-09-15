using FreshMvvm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XrnCourse.BucketList.Domain.Services.Abstract;
using XrnCourse.BucketList.Domain.Services.Mock;
using XrnCourse.BucketList.ViewModels;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XrnCourse.BucketList
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            FreshIOC.Container.Register<IAppSettingsService, AppSettingsInMemoryService>();
            FreshIOC.Container.Register<IUsersService, UsersInMemoryService>();
            FreshIOC.Container.Register<IBucketsService, BucketsInMemoryService>();

            MainPage = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<MainViewModel>());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
